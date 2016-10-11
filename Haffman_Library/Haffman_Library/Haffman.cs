using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haffman_Library
{
    public class Haffman
    {
        #region Polya

        private string message;
        private List<Element> list = new List<Element>();
        private Dictionary<char, string> Dictionary = new Dictionary<char, string>();

        #endregion

        #region Svoistva

        public string MessageCode { get; private set; }
        public Dictionary<char, string> DictionaryCodes { get; private set; }

        #endregion

        #region Vlozhennye Classy

        private class Element
        {
            public char Symbol { get; set; }
            public double Probability { get; set; }
            public string Code { get; set; }
            public Element ElementLeft { get; set; }
            public Element ElementRight { get; set; }
        }

        #endregion

        #region Private Metody

        private List<Element> ProbabilityList(string message)
        {
            Dictionary<char, double> dict = new Dictionary<char, double>();
            char ch = '\0';
            int count;
            for (int i = 0; i < message.Length; i++)
            {
                ch = message[i];
                count = 0;
                if (!dict.ContainsKey(ch))
                {
                    for (int j = i; j < message.Length; j++)
                    {
                        if (ch == message[j]) count++;
                    }
                    dict.Add(ch, (count / Convert.ToDouble(message.Length)));
                }
            }
            var x = from v in dict
                    orderby v.Value descending
                    select v;
            List<Element> result = new List<Element>();
            foreach (var c in x)
            {
                result.Add(new Element() { Symbol = c.Key, Probability = c.Value });
            }
            return result;
        }

        private void AddListElement(List<Element> list)
        {
            if (list.Count > 1)
            {
                Element element = new Element();
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        list[list.Count - 1 - i].Code += "1";
                        element.ElementRight = list[list.Count - 1 - i];
                    }
                    else
                    {
                        list[list.Count - 1 - i].Code += "0";
                        element.ElementLeft = list[list.Count - 1 - i];
                    }
                    element.Probability += list[list.Count - 1 - i].Probability;
                }
                for (int i = 0; i < 2; i++)
                {
                    list.RemoveAt(list.Count - 1);
                }
                list.Add(element);
                this.list = SortList(list);
            }
        }

        private List<Element> SortList(List<Element> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for(int j=0;j<list.Count-1-i;j++)
                {
                    if (list[j].Probability <= list[j + 1].Probability)
                    {
                        list.Reverse(j, 2);
                    }
                }
            }
            return list;
        }

        private void SetListElement(List<Element> list)
        {
            for (;;)
                if (list.Count > 1)
                {
                    AddListElement(list);
                }
                else break;
        }

        private void SetCode(Element element, string str)
        {
            string code=str.ToString();
            if ((element.ElementLeft == null) && (element.ElementRight == null))
            {
                element.Code = code;
                DictionaryCodes.Add(element.Symbol, element.Code);
            }
            else
            {
                if (element.ElementLeft != null)
                {
                    code = str.ToString();
                    code += element.ElementLeft.Code;
                    SetCode(element.ElementLeft, code);
                }
                if(element.ElementRight != null)
                {
                    code = str.ToString();
                    code += element.ElementRight.Code;
                    SetCode(element.ElementRight, code);
                }
            }
        }

        private void SetCodeMessage(Dictionary<char,string> dict)
        {
            for(int i=0;i<message.Length;i++)
            {
               foreach (var v in dict)
                {
                    if (v.Key == message[i]) MessageCode += v.Value;
                } 
            }
        }

        #endregion

        #region Constryctory

        public Haffman(string message)
        {
            this.message = message;
            SetListElement(ProbabilityList(message));
            DictionaryCodes = new Dictionary<char, string>();
            SetCode(list[0], "");
            SetCodeMessage(DictionaryCodes);
        }

        #endregion
    }
}
