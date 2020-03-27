using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_3
{
     
    
    public static class Tester
    {

        public static List<int> Ls;
        public static List<int> U;
        public static List<int> p;
        public static List<double> U0;
        public static List<int> M;
        public static List<double> Dis;
        public static List<int> pp;
        public static List<double> mm;

        public static void Test_seq()
        {
            Ls = new List<int>();
            U = new List<int>();
            p = new List<int>();
            U0 = new List<double>();
            M = new List<int>();
            Dis = new List<double>();
            mm = new List<double>();
            pp=new List<int>();
            //Тестирование для следующих больших простых чисел p
            pp.AddRange(new int[] { 5087,5387, 5987, 8293,8933,9221,9521,9767,9829,9973 });

             int L = 0;
             double m ;
             double d ;
             int MM; 
            foreach (int x in pp)
            {
                 L = 0;
                 m = 0;
                 d = 0;
                // по условию М=p- ближайшая степень 3
                 MM = x - 6561 < 0 ? x - 2187 : x - 6561;
                RandDat.SetSeq(MM, x, MM);
            
                L = RandDat.Count - 1;
                m = RandDat.M;
                d = RandDat.Dis;
                M.Add(MM);
                p.Add(x);
                Ls.Add(L);
                Dis.Add(d);
                mm.Add(m);
              // вывод : L-апериод, M- математическое ожидание, D- дисперсия 
                Console.WriteLine("L = {0,6}      M = {1,-6}       D = {2,-6}",
                                   L, Math.Round(10000 * m) * .0001, Math.Round(10000 * d) * .0001);
               
                }
            RetRes();
        }
// выбор наибольшего значения апериода
        private static void RetRes()
        {
            int j = -1;
            int k = 0;
            for (int i = 0; i < Ls.Count; i++)
            {
                if (Ls[i] > k)
                {
                    j = i;
                    k = Ls[i];
                }
            }

            Console.WriteLine();
            Console.WriteLine("Наибольшая длина при: p = " + p[j] + ", M = " + M[j] + "  Наибольшая длина - L = " + Ls[j]);
            Console.WriteLine("MО = " + Math.Round(10000 * mm[j]) * .0001 + " и D = " + Math.Round(10000 * Dis[j]) * .0001);
            Console.WriteLine();

            Console.Write("Среднее мат.ожидание = ");
           
            Console.Write(Math.Round(10000 * mm.Sum() / mm.Count) * .0001);
            
            Console.Write(" и дисперсия = ");
           
            Console.Write(Math.Round(10000 * Dis.Sum() / Dis.Count) * .0001);
            
            Console.WriteLine();

        }
    }
}

