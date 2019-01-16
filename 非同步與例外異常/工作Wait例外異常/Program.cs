using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工作Wait例外異常
{
    class Program
    {
        static void Main(string[] args)
        {
            var fooTask = Task.Run(async () =>
            {
                await Task.Delay(500);
                throw new Exception("發生了例外異常");
            });

            fooTask.Wait();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
