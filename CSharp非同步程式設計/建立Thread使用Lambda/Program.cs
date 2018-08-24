using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 建立Thread使用Lambda
{
    // 這個範例說明了，如何使用 Lambda 表示式 來指定一個委派的方法，
    // 並且啟動一個執行緒，非同步的來執行這個委派方法
    // 這是一個非常典型的使用 Thread 來做到非同步作業的範例
    class Program
    {
        public static void Main()
        {
            // 產生一個新的 Thread並且立即啟動 (ThreadStart)
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("執行緒方法1: {0}", i);
                    // 告知作業系統，這個 Thread 可用片段時間已經處理完成所有工作，可以提早切換到其他Thread了
                    // 執行緒會將其剩餘的時間配量讓與準備好要執行的任何同等優先權執行緒
                    // https://msdn.microsoft.com/zh-tw/library/d00bd51t(v=vs.110).aspx
                    Thread.Sleep(0);
                }
            }).Start();

            // 產生一個新的 Thread，並且立即啟動，且傳遞一個參數到執行緒委派方法內 (ParameterizedThreadStart)
            new Thread((para) =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("執行緒方法2({1}): {0}", i, para.ToString());
                    Thread.Sleep(0);
                }
            }).Start("啟動執行緒的參數");

            // 當 Thread 正在執行的同時，主要 Thread 也正在執行，也就是說，兩個 Thread同時在執行中，
            // 每個 Thread 在執行過程中，只能夠使用到片段的CPU時間，使用完後，就需要切換到別的Thread上，讓其他Thread可以來執行
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("主執行緒: 執行某些工作.");
                // 告知作業系統，這個 Thread 可用片段時間已經處理完成所有工作，可以提早切換到其他Thread了
                // 執行緒會將其剩餘的時間配量讓與準備好要執行的任何同等優先權執行緒
                Thread.Sleep(0);
            }
            // 等候該Thread執行完畢，此時，主要執行緒將無法處理任何事情(因為被 阻擋 Block了)，只有等候
            // https://msdn.microsoft.com/zh-tw/library/95hbf2ta(v=vs.110).aspx
            Console.ReadKey();
        }
    }
}
