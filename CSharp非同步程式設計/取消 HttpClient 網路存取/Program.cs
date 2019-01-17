using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 取消_HttpClient_網路存取
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                cts.CancelAfter(2500);

                await AccessTheWebAsync(cts.Token);
                Console.WriteLine($"{Environment.NewLine}全部下載完成");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"{Environment.NewLine}下載已經取消");
            }
            catch (Exception)
            {
                Console.WriteLine($"{Environment.NewLine}下載發現例外異常，已經中斷");
            }

            cts = null;

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        static async Task AccessTheWebAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            List<string> urlList = SetUpURLList();

            foreach (var url in urlList)
            {
                HttpResponseMessage response = await client.GetAsync(url, ct);

                byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

                Console.WriteLine($"{Environment.NewLine}下載字串長度 {urlContents.Length}");
            }
        }
        static List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "https://msdn.microsoft.com",
                "https://msdn.microsoft.com/library/windows/apps/br211380.aspx",
                "https://msdn.microsoft.com/library/hh290136.aspx",
                "https://msdn.microsoft.com/library/ee256749.aspx",
                "https://msdn.microsoft.com/library/ms404677.aspx",
                "https://msdn.microsoft.com/library/ff730837.aspx"
            };
            return urls;
        }
    }
}
