using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 只有一個主要執行緒
{
    // 這個範例展示了，這個專案將會使用一個執行緒來執行所設計的 C# 程式碼
    // 也就是，依照 C# 程式碼設計邏輯，依序來執行，因此
    // 主執行緒 執行的時候，會輸出 M 字元
    class Program
    {
        public static void Main()
        {
            for (int i = 0; i < 90000; i++)
            {
                if (i % 50 == 0)
                {
                    Console.Write("M");
                }
            }
            Console.WriteLine($"{Environment.NewLine}主要執行緒 已經執行完畢");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
