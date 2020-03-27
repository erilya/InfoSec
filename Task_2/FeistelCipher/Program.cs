using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeistelCipher
{
    internal static class Program
    {
        private static void Main()
        {
            const byte rounds = 10;

            var key64 = RandomKey();
            Console.WriteLine($"Ключ: {key64:X}");

            var iv = RandomKey();
            Console.WriteLine($"IV: {iv:X}");

            Console.Write("Введите сообщение: ");
            var message = Padding(Console.ReadLine());
            var mesgCB = ToBlocks(message);
         
            Console.Write("Режим CBC. ");
            for (var i = 0; i < mesgCB.Length; i++)
            {
                mesgCB[i] = Encrypt(i == 0 ? mesgCB[i] ^ iv : mesgCB[i] ^ mesgCB[i - 1], key64, rounds);
            }
            message = MessageToHex(mesgCB);
            Console.WriteLine($"Зашифрованное сообщение: {message}");

            Console.Write("Режим CBC. ");
            var mesgP = new ulong[mesgCB.Length];
            mesgCB.CopyTo(mesgP, 0);
            for (var i = 0; i < mesgP.Length; i++)
            {
                mesgP[i] = i == 0 ? iv ^ Decrypt(mesgP[i], key64, rounds) : mesgCB[i - 1] ^ Decrypt(mesgP[i], key64, rounds);
            }
            message = MessageToString(mesgP);
            Console.WriteLine($"Расшифрованное сообщение: {message}");

          
            Console.ReadKey();
        }

        private static string Padding(string input)
        {
            var n = input.Length * 16 % 64;
            if (n == 0) return input;
            var sb = new StringBuilder(input);
            var k = (64 - n) / 16;
            sb.Append(new char(), k);
            return sb.ToString();
        }

        private static ulong[] ToBlocks(string input)
        {
            var result = new ulong[input.Length / 4];
            var temp = new uint[2];
            for (int i = 0, j = 0; i < input.Length; i += 4, j++)
            {
                temp[0] = (uint)input[i] << 16 | input[i + 1];
                temp[1] = (uint)input[i + 2] << 16 | input[i + 3];

                result[j] = (ulong)temp[0] << 2 * 16 | temp[1];
            }
            return result;
        }

        private static ulong RandomKey()
        {
            var rand = new Random((int)(DateTime.Now.Ticks & 0xFFFFFFFF));
            var buffer = new byte[sizeof(ulong)];
            rand.NextBytes(buffer);
            ulong res = 0;
            for (var i = 0; i < sizeof(ulong); i++)
            {
                ulong temp = buffer[i];
                temp = temp << 8 * i;
                res = res | temp;
            }
            return res;
        }

        private static ulong Encrypt(ulong msg, ulong key64, uint rounds)
        {
            var right = (uint)(msg << 2 * 16 >> 2 * 16);
            var left = (uint)(msg >> 2 * 16);
            for (var i = 0; i < rounds; i++)
            {
                var key32I = KeyGenerator(i, key64);
                var function = F(right, key32I);
                var tmp = right;
                right = left ^ function;
                left = tmp;
            }
            var tmp1 = (ulong)left << 2 * 16;
            var tmp2 = (ulong)right;
            return tmp1 | tmp2;
        }

        private static ulong Decrypt(ulong msg, ulong key64, int iteration)
        {
            var right = (uint)(msg << 2 * 16 >> 2 * 16);
            var left = (uint)(msg >> 2 * 16);
            for (var i = iteration - 1; i >= 0; i--)
            {
                var key32I = KeyGenerator(i, key64);
                var function = F(left, key32I);
                var tmp = left;
                left = right ^ function;
                right = tmp;
            }
            var tmp1 = (ulong)left << 2 * 16;
            var tmp2 = (ulong)right;
            return tmp1 | tmp2;
        }

        private static string MessageToHex(IEnumerable<ulong> msg)
        {
            var result = string.Empty;
            var tmp = new ushort[4];
            foreach (var item in msg)
            {
                tmp[0] = (ushort)(item >> 6 * 8); 
                tmp[1] = (ushort)(item >> 4 * 8 << 6 * 8 >> 6 * 8); 
                tmp[2] = (ushort)(item << 4 * 8 >> 6 * 8); 
                tmp[3] = (ushort)(item << 6 * 8 >> 6 * 8); 
                result = tmp.Aggregate(result, (current, t) => current + t.ToString("X"));
            }
            byte[] res = Encoding.Default.GetBytes(result);
            var hex = BitConverter.ToString(res);
            hex = hex.Replace("-", "");
            return result;
        }

        private static string MessageToString(IEnumerable<ulong> msg)
        {
            var result = string.Empty;
            var tmp = new ushort[4];
            foreach (var item in msg)
            {
                tmp[0] = (ushort)(item >> 3 * 16);
                tmp[1] = (ushort)(item >> 2 * 16 << 3 * 16 >> 3 * 16);
                tmp[2] = (ushort)(item << 2 * 16 >> 3 * 16);
                tmp[3] = (ushort)(item << 3 * 16 >> 3 * 16);
                result = tmp.Aggregate(result, (current, t) => current + Convert.ToChar(t));
            }
            return result;
        }

        #region cycleMove
        private static ulong CycleMoveRight(uint number, byte offset) 
            => number >> offset | number << 32 - offset;

        private static uint CycleMoveLeft(uint number, byte offset) 
            => number << offset | number >> 32 - offset;
        #endregion

        private static uint F(uint left, uint key) 
            => (uint)(CycleMoveLeft(left, 9) ^ (CycleMoveRight(key, 11) + left));

        private static uint KeyGenerator(int round, ulong key64) 
            => (uint)(CycleMoveLeft((uint)key64, (byte)(round * 8)) << 0 >> 32);
    }
}
