using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haffman_Not_Tree;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HaffmanNotTreeLbr hf = new HaffmanNotTreeLbr(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine(hf.MessageCode);
            Console.WriteLine();
            foreach (var v in hf.DictionaryCodes)
            {
                Console.WriteLine("Symbol: {0}   Code: {1}", v.Key, v.Value);
            }
            Console.ReadLine();
        }
    }
}

