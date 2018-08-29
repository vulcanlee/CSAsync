using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 執行緒區域儲存區
{
    // 您可使用 Managed 執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。
    // .NET Framework 提供兩種方法來使用 Managed TLS：執行緒相關的靜態欄位和資料位置。
    public static class Program
    {
        // 嘗試移除這個 ThreadStatic Attribute 看看結果
        // 指示每個執行緒的靜態欄位值是唯一的。
        // https://msdn.microsoft.com/zh-tw/library/system.threadstaticattribute(v=vs.110).aspx

        /// <summary>
        /// 執行緒相關的靜態欄位可提供最佳效能。 這也提供您在編譯時期檢查型別的好處。
        /// </summary>
        [ThreadStatic]
        public static int _field;
        public static void Main()
        {
            #region 靜態欄位 thread-relative static fields
            Console.WriteLine("靜態欄位 thread-relative static fields");
            new Thread(() =>
            {
                for (int x = 0; x < 100; x++)
                {
                    _field++;
                    Console.WriteLine("執行緒 A: {0}", _field);
                }
            }).Start();
            new Thread(() =>
            {
                for (int x = 0; x < 100; x++)
                {
                    _field++;
                    Console.WriteLine("執行緒 B: {0}", _field);
                }
            }).Start();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
            #endregion

        }
    }
}
