using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 用Mutex進行跨處理程序的執行緒同步
{
    class Program
    {
        private static string MutexName = "com.miniasp.process.Mutex";
        private static Mutex mutex;
        private static string ProcessName;
        private static int Sleep;

        public static void Main()
        {
            if (!Mutex.TryOpenExisting(MutexName, out mutex))
            {
                Console.WriteLine("得到 Mutex");
                mutex = new Mutex(false, MutexName);
                ProcessName = "Master 執行程序";
                Sleep = 3000;
            }
            else
            {
                ProcessName = "Slave 執行程序";
                Sleep = 1000;
            }

            for (int i = 0; i < 10; i++)
            {
                mutex.WaitOne();
                Console.WriteLine($"{ProcessName} 執行時間需要 {Sleep} ms");
                Thread.Sleep(Sleep);
                mutex.ReleaseMutex();
            }
        }

    }
}
