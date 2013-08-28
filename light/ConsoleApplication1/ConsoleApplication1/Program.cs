using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static int[] table = null;
        static int count = 0;
        static List<string> steps = new List<string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("input the width of matrix");
                count = Convert.ToInt32(Console.ReadLine());
                table = new int[count * count];
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        table[i * count + j] = 0;
                    }
                }
                BeginLight();
            }
        }
        static void BeginLight()
        {
            int count2 = Convert.ToInt32(Math.Pow(2, count));
            int n = 0;
            for (int i = 0; i < count2; i++)
            {
                string countstr = Convert.ToString(i, 2);
                while (countstr.Length < count)
                {
                    countstr = "0" + countstr;
                }
                string countstrReverse = new string(countstr.Reverse().ToArray());
                countstr = countstrReverse;
                int index = 0;
                while (countstr.IndexOf('1', index) >= 0)
                {
                    index = countstr.IndexOf('1', index);
                    LightRound(index);
                    index++;
                }
                if (LightRow(1))
                {
                    string s = countstr.Replace('0', '□').Replace('1', '●');
                    Console.WriteLine(s);
                    foreach (string item in steps)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine();
                    steps.Clear();
                    continue;
                }
                else
                {
                    //Console.WriteLine(countstr + " can't pass");
                    n++;
                    Clear();
                    continue;
                }
            }
        }
        static bool LightRow(int i)
        {
            string stepi = "";
            for (int m = 0; m < count; m++)
            {
                if (table[(i - 1) * count + m] % 2 == 0)
                {
                    LightRound(i * count + m);
                    stepi += "●";
                }
                else
                {
                    stepi += "□";
                }
            }
            steps.Add(stepi);
            if (i == count - 1)
            {
                for (int n = 0; n < count; n++)
                {
                    if (table[i * count + n] % 2 == 0)
                        return false;
                }
            }
            else
            {
                return LightRow(i + 1);
            }
            return true;
        }
        static void LightRound(int b)
        {
            table[b]++;
            if (b > count)
                table[b - count]++;
            if (b < count * (count - 1))
                table[b + count]++;
            if (b % count != 0)
            {
                table[b - 1]++;
            }
            if ((b + 1) % count != 0)
            {
                table[b + 1]++;
            }
        }
        static void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = 0;
            }
            steps.Clear();
        }
    }
}
