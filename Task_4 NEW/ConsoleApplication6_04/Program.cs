using System;
using System.Text;
using System.Numerics;

namespace ConsoleApplication6_04
{
    class Program
    {

        static String codes2string(String str)
        {
            StringBuilder text = new StringBuilder();
            Byte[] bytes = new Byte[str.Length / 2];
            for (int i = 0; i < str.Length / 2; i++)
            {
                String code = str.Substring(2 * i, 2);
                bytes[i] = Byte.Parse(code);
            }
            return ASCIIEncoding.ASCII.GetString(bytes);
        }


        private static long calculateD(long e, long phi)
        {
            long tmp = 1;
            while (true)
            {
                tmp = tmp + phi;
                if (tmp % e == 0)
                    return tmp / e;
            }

        }


        static void Main(string[] args)
        {

            long e = 23311;
            long n = 274611845366113;
            long p = 2432321;
            long q = 112901153;
            long phi = (p - 1) * (q - 1);
            Console.WriteLine("phi = {0}", phi);
            long d = calculateD(e, phi);
            Console.WriteLine("{0}", (d * e) % phi);
            String[] c_text = "108230462382949 240744446393133 139920760825242 128635453394626 156290136879344".Split(' ');
            StringBuilder text = new StringBuilder();
            StringBuilder c_text_ = new StringBuilder();
            foreach (String c_str in c_text)
            {
                BigInteger c_msg = ulong.Parse(c_str);
                BigInteger msg = BigInteger.ModPow(c_msg, d, n);
                text.Append(msg.ToString());

                BigInteger c_msg_ = BigInteger.ModPow(msg, e, n);
                c_text_.Append(c_msg_.ToString());
            }
            Console.WriteLine(c_text_);

            Console.WriteLine(codes2string(text.ToString()));

            Console.ReadKey();
        }
    }
}



