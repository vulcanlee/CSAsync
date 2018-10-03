using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace 當任一工作完成執行
{
    /// <summary>
    /// 在這個範例中，我們要抓取網頁的內容與知道全部字串的長度，不過，我們需要抓取六個網頁
    /// 只要其中一個網頁內容有成功抓取到，我們就將該網頁的文字長度列印出來
    /// 
    /// 這裡，我們使用了 Task.WhenAny ： 建立當任何一個提供的工作完成時才會完成的工作
    /// 這個靜態方法可以不用封鎖 (Block) 現在執行緒
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            int fooResult = await 讀取任一網頁內容();

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }

        static async Task<int> 讀取任一網頁內容()
        {
            // 定義需要抓取的網頁網址有哪些
            string[] foo所有的網址 =  {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/en-us/library/hh156528(VS.110).aspx",
                "http://msdn.microsoft.com/en-us/library/67w7t67f.aspx",
                "http://www.microsoft.com/surface/en-us?ocid=OCTEVENT_MSCOM",
                "http://www.microsoftstore.com/store/msusa/en_US/pdp/productID.324438600",
                "http://www.xbox.com/zh-TW/",
            };

            // 產生同時抓取上述定義網址的非同步工作
            //IEnumerable<Task<string>> foo多網頁讀取 = from url in foo所有的網址 select Get網頁內容(url);
            Task<string>[] foo多網頁讀取 = (from url in foo所有的網址 select Get網頁內容(url)).ToArray();

            // 取得任何一個已經完成的工作(也就是，該網頁內容已經成功讀取了)
            Task<string> foo已經完成的其中一個工作 = await Task.WhenAny(foo多網頁讀取);

            // 取得該網頁的所有文字內容
            string foo網頁內容 = await foo已經完成的其中一個工作;
            Console.WriteLine("成功讀取網頁長度為:{0}", foo網頁內容.Length);

            return foo網頁內容.Length;
        }

        static async Task<string> Get網頁內容(string url)
        {
            HttpClient client = new HttpClient();

            string fooResult = await client.GetStringAsync(url);
            return fooResult;
        }
    }
}
