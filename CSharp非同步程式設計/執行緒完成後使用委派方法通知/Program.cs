using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒完成後使用委派方法通知
{
    /// <summary>
    /// 在這個範例中，我們展示了當執行緒執行完成之後，會呼叫一個我們指定的委派方法，通知這個執行緒已經完成了
    /// </summary>
    class Program
    {
        static int _count;
        static void Main(string[] args)
        {
            ThreadWorker worker = new ThreadWorker(執行緒執行完成之委派方法);

            // 產生一個新的執行緒，並且開始執行該執行緒
            Console.WriteLine("產生一個新的執行緒，並且開始執行該執行緒");
            Thread thread1 = new Thread(worker.Run);
            thread1.Start();

            _count = 1;

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static void 執行緒執行完成之委派方法()
        {
            if (_count == 1)
            {
                Console.WriteLine("執行緒1已經執行完成");
                ThreadWorker worker = new ThreadWorker(執行緒執行完成之委派方法);

                Console.WriteLine("產生第二個新的執行緒，並且開始執行該執行緒");
                // 產生第二個新的執行緒，並且開始執行該執行緒
                Thread thread2 = new Thread(worker.Run);
                thread2.Start();

                _count++;
            }
            else
            {
                Console.WriteLine("執行緒2已經執行完成");
            }
        }

    }

    class ThreadWorker
    {
        Action 完成後的委派方法;
        public ThreadWorker(Action action)
        {
            完成後的委派方法 = action;
        }

        public void Run(object state)
        {
            // 在這裡進行相關執行緒處理工作
            Thread.Sleep(2000);

            完成後的委派方法?.Invoke();
        }
    }
}
