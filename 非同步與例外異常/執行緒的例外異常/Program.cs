using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒的例外異常
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                // 當在執行緒內發生了例外異常，應用程式將會結束執行
                throw new Exception("發生了例外異常");
            });


            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
