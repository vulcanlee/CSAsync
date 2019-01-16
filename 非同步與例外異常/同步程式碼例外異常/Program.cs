using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 同步程式碼例外異常
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("程式開始執行");
            // 當在執行緒內發生了例外異常，應用程式將會結束執行
            throw new Exception("發生了例外異常");

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
