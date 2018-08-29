using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒匿名資料插槽
{
    /// <summary>
    /// 在這個範例示範如何使用資料匿名位置來儲存執行緒特定資訊
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 產生四個執行緒
            Thread[] newThreads = new Thread[4];
            for (int i = 0; i < newThreads.Length; i++)
            {
                newThreads[i] = new Thread(new ThreadStart(Slot.執行緒委派方法));
                newThreads[i].Start();
            }
            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }

    class Slot
    {
        /// <summary>
        /// 隨機數
        /// </summary>
        static Random randomGenerator;
        /// <summary>
        /// 封裝記憶體位置以儲存區域資料
        /// </summary>
        static LocalDataStoreSlot localSlot;

        static Slot()
        {
            randomGenerator = new Random();
            // 在所有的執行緒上配置未命名的資料插槽。為獲得較佳的效能，請改用以 System.ThreadStaticAttribute 屬性標示的欄位。
            localSlot = Thread.AllocateDataSlot();
        }

        public static void 執行緒委派方法()
        {
            // 在每個執行緒的資料槽中設置不同的資料
            Thread.SetData(localSlot, randomGenerator.Next(1, 200));

            // 從每個執行緒的資料槽中寫入資料
            Console.WriteLine("資料在執行緒{0}的資料槽中: {1,3}", Thread.CurrentThread.ManagedThreadId, Thread.GetData(localSlot).ToString());

            // 允許其他執行緒執行 SetData 執行緒資料槽是唯一的執行緒時間
            Thread.Sleep(randomGenerator.Next(500, 1500));

            Console.WriteLine("資料在執行緒{0}資料槽中: {1,3}",
                Thread.CurrentThread.ManagedThreadId,
                Thread.GetData(localSlot).ToString());
        }
    }
}
