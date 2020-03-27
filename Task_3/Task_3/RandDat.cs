using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Task_3
{
    

    static public class RandDat
    {
        static private List<double> nums;
        // математическое ожидание
        static public double M { get { return nums.Sum() / Count; } }
        // вычисление дисперсии
        static public double Dis
        {
            get
            {
                double m = M;
                double res = 0;
                foreach (double d in nums)
                    res += Math.Pow(d - m, 2);
                return res / nums.Count;
            }
        }
        static public int Count { get { return nums.Count; } }
        static public List<double> Nums { get { return nums; } }
        // вычисление апериода
        static public void SetSeq(int M, int p, int U0)
        {
            nums = new List<double>();
         
            int U = (U0*M)%p;
            double R=(double)U/p; 
           while (!nums.Contains(R)) 
            { 
                nums.Add(R);
                U = Next_Num(p, M, U, out R );  
            }
          
            nums.Add(R);
        }
        // вычисление следующего случайного числа из предыдущего
        static private int Next_Num(int p, int M, int U, out double r)
        {
            int u = (U * M) % p ;
            r=(double)u/p;
            return u;
        }
    }

}
