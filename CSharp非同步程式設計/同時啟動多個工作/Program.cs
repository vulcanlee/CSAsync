using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 同時啟動多個工作
{
    /// <summary>
    /// 在這個範例中，我們使用了 Task.Factory.StartNew 同時啟動了 8 個非同步工作，用來計算總合
    /// </summary>
    class Program
    {
        public static void Main()
        {
            // 建立八個非同步工作，且這些非同步工作在建立後，就開始執行了
            Task<Double>[] taskArray = {
                Task<Double>.Factory.StartNew(() => DoComputation(1, 99999)),
                Task<Double>.Factory.StartNew(() => DoComputation(100000, 199999)),
                Task<Double>.Factory.StartNew(() => DoComputation(200000, 299999)),
                Task<Double>.Factory.StartNew(() => DoComputation(300000, 399999)),
                Task<Double>.Factory.StartNew(() => DoComputation(400000, 499999)),
                Task<Double>.Factory.StartNew(() => DoComputation(500000, 599999)),
                Task<Double>.Factory.StartNew(() => DoComputation(600000, 699999)),
                Task<Double>.Factory.StartNew(() => DoComputation(700000, 799999)),
            };

            var results = new Double[taskArray.Length];
            // 統計所有工作的計算總合
            Double sum = 0;

            for (int i = 0; i < taskArray.Length; i++)
            {
                // 取得第 i 個非同步的執行結果
                results[i] = taskArray[i].Result;
                Console.Write("{0:N1} {1}", results[i], i == taskArray.Length - 1 ? "= " : "+ ");
                sum += results[i];
            }
            Console.WriteLine("{0:N1}", sum);
            Console.ReadKey();
        }

        private static Double DoComputation(Double start, Double end)
        {
            // 這裡可以設定休息任意時間
            Thread.Sleep(1500);
            Double sum = 0;
            for (var value = start; value <= end; value += 1.0)
                sum += value;

            return sum;
        }
    }
}
