using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 用Mutex限制只有一個處理程序可以執行
{
    class Program
    {
        private static string MutexName = "com.miniasp.process.Mutex";
        private static Mutex mutex;
        public static void Main()
        {
            if (!Mutex.TryOpenExisting(MutexName, out mutex))
            {
                mutex = new Mutex(false, MutexName);
                Console.WriteLine("該程式將會於5秒鐘後自動結束");
                Thread.Sleep(5000);
            }
            else
            {
                Console.WriteLine($"已經有相同的程序啟動了，無法再度執行");
                return;
            }
        }

    }
}
