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
            public string SummSymbol { get; set; }
            public double Probability { get; set; }
            public string Code { get; set; }
            public Element UpElement { get; set; }
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

        #endregion

        #region Constryctory

        public Haffman (string message)
        {
            this.message = message;
            ProbabilityList(message);
        }

        #endregion
    }
}
