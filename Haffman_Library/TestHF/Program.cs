using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haffman_Library;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Haffman hf = new Haffman(Console.ReadLine());
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

