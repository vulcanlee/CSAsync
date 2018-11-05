using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 非獨佔鎖定Interlocked
{
    class Program
    {
        static long TotalInt = 0;
        static double TotalDouble = 0.0;
        static void Main(string[] args)
        {
            //Thread t1 = new Thread(SumInt);
            //Thread t2 = new Thread(SumInt);
            //t1.Start(); t2.Start();

            //t1.Join();
            //t2.Join();

            //Console.WriteLine($"整數計算結果 {TotalInt}");

            Thread t3 = new Thread(SumDouble);
            Thread t4 = new Thread(SumDouble);
            t3.Start(); t4.Start();

            t3.Join();
            t4.Join();

            Console.WriteLine($"倍精度計算結果 {TotalDouble}");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void SumDouble()
        {
            double initialValue, computedValue;
            for (int i = 0; i < 10000000; i++)
            {
                //TotalDouble += 1.0;
                do
                {
                    initialValue = TotalDouble;

                    computedValue = initialValue + 1.0;
                }
                while (initialValue != Interlocked.CompareExchange(ref TotalDouble, computedValue, initialValue));
            }
        }

        private static void SumInt(object state)
        {
            for (int i = 0; i < 10000000; i++)
            {
                //TotalInt += 1;
                Interlocked.Add(ref TotalInt, 1);
            }
        }
    }
}
