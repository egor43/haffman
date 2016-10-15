using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haffman_Not_Tree
{
    /// <summary>
    /// Представляет метод Хаффмана (без построения дерева)
    /// </summary>
    public class HaffmanNotTreeLbr
    {
        #region Polya

        private string message; // Хранит в себе введенную строку
        private List<Element> list = new List<Element>(); //Переменная для хранения списка эдементов типа Element

        #endregion

        #region Svoistva

        /// <summary>
        /// Позволяет получить закодированное сообщение
        /// </summary>
        public string MessageCode { get; private set; }

        /// <summary>
        /// Позволяет получить словарь со связкой "символ - код символа"
        /// </summary>
        public Dictionary<char, string> DictionaryCodes { get; private set; }

        #endregion

        #region Vlozhennye Classy
        /// <summary>
        /// Представляет описание элемента (символа)
        /// </summary>
        private class Element
        {
            public char Symbol { get; set; } //Пересенная для хранения символа
            public double Probability { get; set; } //Пересенная для хранения вероятности появления символа
            public string Code { get; set; } ////Пересенная для хранения кода символа
        }

        #endregion

        #region Private Metody

        /// <summary>
        /// Вычисляет частоту появления в данной строке каждого уникального символа
        /// </summary>
        /// <param name="message">строка для кодирования</param>
        /// <returns>отсортированный по убыванию список, содержащий элементы типа "Element"</returns>
        private List<Element> ProbabilityList(string message)
        {
            Dictionary<char, double> dict = new Dictionary<char, double>();
            char ch = '\0';
            int count;
            for (int i = 0; i < message.Length; i++) //В цикле: вычисляем частоту вхождения каждого уникального символа в сообщении
            {
                ch = message[i];
                count = 0; //Счетчик количества вхождений символа "ch" в строку.
                if (!dict.ContainsKey(ch))
                {
                    for (int j = i; j < message.Length; j++)
                    {
                        if (ch == message[j]) count++;
                    }
                    dict.Add(ch, (count / Convert.ToDouble(message.Length)));
                }
            }
            var x = from v in dict               //
                    orderby v.Value descending   // LINQ выражение для сортировки значения по убыванию и выборки из словаря элементов.
                    select v;                    //
            List<Element> result = new List<Element>();
            foreach (var c in x) //Заполнение списка элемнтами из словаря, полученного в результате выполнения LINQ выражения.
            {
                result.Add(new Element() { Symbol = c.Key, Probability = c.Value }); 
            }
            return result;
        }

        /// <summary>
        /// Заполняет код символа для последних двух элементов списка 
        /// </summary>
        /// <param name="list">список для обработки</param>
        /// <returns>обработанный список</returns>
        private List<Element> AddCodeListElements(List<Element> list)
        {
            if (list.Count > 1)
            {
                list[list.Count - 1].Code = "1";
                list[list.Count - 2].Code = "0";
            }
            return list;
        }

        /// <summary>
        /// Вычисляет код для каждого символа.
        /// </summary>
        /// <param name="alist"></param>
        /// <returns></returns>
        private List<Element> AddArrList(List<Element> alist)
        {
            List<Element> list = new List<Element>(); //Переменная для хранения переданного в метод списка
            foreach(var v in alist) //В цикле: выполняется копирование входящего списка
            {                                  
                Element el = new Element();
                el.Code = v.Code;
                el.Probability = v.Probability;
                list.Add(el);
            }
            if (list.Count > 2) 
            {
                Element element = new Element(); 
                element.Probability = list[list.Count - 1].Probability+ list[list.Count - 2].Probability;  //Записываем в новый элемент сумму вероятностей двух последних элементов списка list
                for (int i = 0; i < 2; i++) //Удаление последних элементов списка list
                {
                    list.RemoveAt(list.Count - 1);
                }
                list.Add(element);
                list = AddCodeListElements(SortList(list)); //Сортируем список list --> Присваиваем двум последним элементам коды символов --> записываем измененный список в list
                List<Element> newlist = AddArrList(list); //Рекурсия для обработки списка до последних двух элементов
                for(int i=0; i<alist.Count-2;i++) //В цикле: исключаем, из полученного в результате рекурсии списка, элементы, которые не являются суммой вероятностей двух последних элементов входящего списка alist  
                {
                    int count = 0;
                    for (int j=1; j<newlist.Count;)
                    {
                        if(alist[i].Probability==newlist[count].Probability)
                        {
                            alist[i].Code += newlist[count].Code;
                            newlist.RemoveAt(count);
                            break;
                        }
                        count++;
                    }
                } //В результате работы цикла всегда должен оставаться один элемент
                string str = newlist[0].Code+ alist[alist.Count - 2].Code; //
                alist[alist.Count - 2].Code = str;                         // Заполняем коды символов для последних двух элементов входящего списка alist. (Код был записан, в процессе рекурсии, в элемент представляющий их сумму)
                str = newlist[0].Code + alist[alist.Count - 1].Code;       //
                alist[alist.Count - 1].Code = str;                         //
                return alist;
            }
            else return AddCodeListElements(list); //Записываем коды для двух последних элементов списка
        }

        /// <summary>
        /// Сортировка списка по убыванию (пузырек)
        /// </summary>
        /// <param name="list">список для сортировки</param>
        /// <returns>отсортированный список</returns>
        private List<Element> SortList(List<Element> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (list[j].Probability <= list[j + 1].Probability)
                    {
                        list.Reverse(j, 2);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Формирует закодированное сообщение
        /// </summary>
        /// <param name="dict"></param>
        private void SetCodeMessage(Dictionary<char, string> dict)
        {
            for (int i = 0; i < message.Length; i++)
            {
                foreach (var v in dict)
                {
                    if (v.Key == message[i]) MessageCode += v.Value;
                }
            }
        }

        #endregion

        #region Constryctory

        /// <summary>
        /// Выполняет инициализацию полей и вызов внутренних методов
        /// </summary>
        /// <param name="message">сообщение для кодирования</param>
        public HaffmanNotTreeLbr(string message)
        {
            this.message = message;
            list = ProbabilityList(message); //Получаем отсортированный по убыванию вероятностей вхождения символов список.
            DictionaryCodes = new Dictionary<char, string>();
            list = AddCodeListElements(list); //Получаем список с начальными кодами символов. В дальнейшем этот список обрабатывается рекурсионно в методе AddArrList
            list = AddArrList(list); //Получаем список с заполненными кодами сиволов
            foreach (var v in list) //В цикле: заполняем словарь связками "символ - код символа" 
            {
                DictionaryCodes.Add(v.Symbol, v.Code);
            }
            SetCodeMessage(DictionaryCodes); //Формируем закодированную строку
        }

        #endregion
    }
}
