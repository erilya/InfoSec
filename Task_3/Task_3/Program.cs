using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
        
            Console.WriteLine();
            Tester.Test_seq();
            Console.WriteLine();
            Console.ReadKey();
        }

        static void WriteSeq()
        {
            Console.WriteLine();
            for (int i = 0; i < RandDat.Count; i++)
            {
                Console.WriteLine(" " + RandDat.Nums[i]);
            }
            Console.Write(" " + "...");
            Console.WriteLine();
        }

        static void WriteResult()
        {

        }

    }
}
