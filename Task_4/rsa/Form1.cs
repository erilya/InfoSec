using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace rsa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

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

        private static long calculateP(long n)
        {
            long p = 0;

            for (long i = 3; i <= Math.Sqrt(Convert.ToDouble(n)); i = i + 2)
            {
                while (n % i == 0)
                {
                    p = i;
                    n = n / i;
                }
            }

            return p;
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

        private void button1_Click(object sender, EventArgs e)
        {
            long E = 12977;
            long n = 491384118735611;

            long p = calculateP(n);
            long q = n / p;
            //BigInteger p = 2432237;
            //BigInteger q = 202029703;

            long phi = (p - 1) * (q - 1);
            long d = calculateD(E, phi);
            //BigInteger d = 491383914273672;

            textBox2.Text = phi.ToString();
            String[] c_text = "251795486131 905115130709 179039466550 284270258229 698732628520 578347430910 621273659090 95368".Split(' ');
            
            StringBuilder text = new StringBuilder();
            StringBuilder c_text_ = new StringBuilder();

            foreach (String c_str in c_text)
            {
                BigInteger c_msg = UInt64.Parse(c_str);
                BigInteger msg = BigInteger.ModPow(c_msg, d, n);
                text.Append(msg.ToString());
            }
            textBox1.Text += codes2string(text.ToString());

        }
    }
}






//BigInteger c_msg_ = BigInteger.ModPow(msg, E, n);
//c_text_.Append(c_msg.ToString());