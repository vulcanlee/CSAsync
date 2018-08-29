using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒具名資料插槽
{
    /// <summary>
    /// 在這個範例示範如何使用具名 資料位置/資料插槽 來儲存執行緒特定資訊
    /// 如果具名的位置不存在，則會配置新的位置。 具名的資料位置是公用的任何人都可以管理。
    /// </summary>
    class Program
    {
        // 設定具名名稱的字串
        public static readonly string slotName = "MySlot";
        static void Main(string[] args)
        {
            #region 資料位置/資料插槽 data slots
            Console.WriteLine("資料位置/資料插槽 data slots");
            // 尋找具名的資料位置。
            LocalDataStoreSlot locSlot = Thread.GetNamedDataSlot(slotName);
            new Thread(() =>
            {
                // 進行 資料位置/資料插槽 的資料儲存
                Thread.SetData(locSlot, 0);
                for (int x = 0; x < 100; x++)
                {
                    // 進行 資料位置/資料插槽 的資料取得
                    int fooInt = (int)Thread.GetData(locSlot);
                    fooInt++;
                    Thread.SetData(locSlot, fooInt);
                    Console.WriteLine("執行緒 A: {0}", fooInt);
                }
            }).Start();
            new Thread(() =>
            {
                // 進行 資料位置/資料插槽 的資料儲存
                Thread.SetData(locSlot, 0);
                for (int x = 0; x < 100; x++)
                {
                    // 進行 資料位置/資料插槽 的資料取得
                    int fooInt = (int)Thread.GetData(locSlot);
                    fooInt++;
                    Thread.SetData(locSlot, fooInt);
                    Console.WriteLine("執行緒 B: {0}", fooInt);
                }
            }).Start();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion
        }
    }
}
