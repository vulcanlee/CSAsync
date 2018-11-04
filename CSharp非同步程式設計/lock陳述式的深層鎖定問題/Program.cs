using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lock陳述式的深層鎖定問題
{
    /// <summary>
    /// 這個範例將會展示對於同一個鎖定物件，進行深層多此鎖定的練習，
    /// lock 陳述式知道哪個執行緒正在進行鎖定狀態，若同一個執行緒又執行一次 lock 陳述式，
    /// 這只是增加鎖定物件的鎖定計數器。
    /// 在這個範例中，背景工作執行緒需要等到主執行緒最外層 lock 陳述式執行完畢後，才會接續執行
    /// </summary>
    class Program
    {
        private static int counter = 0;
        private static object locker = new object();
        public static void Main()
        {
            new Thread(DoWork1).Start();
            Console.WriteLine($"主執行緒 準備進入到獨佔鎖定狀態 lock (locker)");
            lock (locker)
            {
                Console.WriteLine($"主執行緒 模擬要執行1秒鐘的時間");
                Thread.Sleep(1000);
                Console.WriteLine($"主執行緒 第二次準備進入到獨佔鎖定狀態 lock (locker)");
                lock (locker)
                {
                    Console.WriteLine($"主執行緒 模擬要執行1秒鐘的時間");
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"主執行緒 第一次鎖定準備要結束，模擬要執行5秒鐘的時間");
                Thread.Sleep(5000);
                Console.WriteLine($"主執行緒 第一次鎖定正要結束");
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        private static void DoWork1()
        {
            Console.WriteLine($"@ 工作執行緒 準備進入到獨佔鎖定狀態 lock (locker)");
            lock (locker)
            {
                Console.WriteLine($"@ 工作執行緒 模擬要執行3秒鐘的時間");
                Thread.Sleep(3000);
            }
            Console.WriteLine($"@ 工作執行緒 鎖定正要結束");
        }
    }
}
