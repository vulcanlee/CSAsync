using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF當任一工作完成執行
{
    /// <summary>
    /// 這個範例將會使用查詢命令，產生許多Task工作，這些工作將會讀取各個網頁的非同步工作
    /// 之後，我們將會等候任何一個工作完成即可
    /// </summary>
    public partial class MainWindow : Window
    {
        // 宣告 CancellationTokenSource ，可以用來發出取消的請求
        CancellationTokenSource cts;

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            // 初始化 CancellationTokenSource.
            cts = new CancellationTokenSource();

            resultsTextBox.Clear();
            var fooColor = resultsTextBox.Background;
            resultsTextBox.Background = Brushes.LightBlue;

            try
            {
                await AccessTheWebAsync(cts.Token);
                resultsTextBox.Text += "\r\n下載完成";
            }
            catch (OperationCanceledException)
            {
                resultsTextBox.Text += "\r\n下載取消";
            }
            catch (Exception)
            {
                resultsTextBox.Text += "\r\n下載失敗";
            }
            resultsTextBox.Background = fooColor;

            // 當下載完成之後，設定 CancellationTokenSource 為 null
            cts = null;
        }


        // 發出取消請求
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }


        // 這裡提供了取消 Token
        async Task AccessTheWebAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            List<string> urlList = SetUpURLList();  // 建立要讀取所有網頁的網址清單

            // 建立一個查詢，產生讀取各個網頁的非同步工作
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURLAsync(url, client, ct);

            // 將產生的所有非同步工作清單，轉換成為陣列
            Task<int>[] downloadTasks = downloadTasksQuery.ToArray();

            // 等候任何一個已經完成的工作產生出來
            Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

            // 發出取消請求，要求取消其他尚未完成的工作
            cts.Cancel();

            // 等候第一個工作完成，以便取得工作完成的結果
            var length = await firstFinishedTask;
            resultsTextBox.Text += String.Format("\r\n已經下載網頁內容的長度為:  {0}\r\n", length);
        }


        // 綁訂一個方法，使用非同步的方式來讀取網頁內容
        async Task<int> ProcessURLAsync(string url, HttpClient client, CancellationToken ct)
        {
            // GetAsync 將會回傳一個 Task<HttpResponseMessage> 物件，
            // 因為我們要傳入 CancellationToken 物件，所以，當發出取消請求的時候，該網頁讀取工作就會取消了
            HttpResponseMessage response = await client.GetAsync(url, ct);

            // 從 HttpResponseMessage 物件中，取得該網頁的內容
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

            return urlContents.Length;
        }


        // 建立要讀取所有網頁的網址清單.
        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>
            {
                "http://msdn.microsoft.com",
                "http://msdn.microsoft.com/en-us/library/hh290138.aspx",
                "http://msdn.microsoft.com/en-us/library/hh290140.aspx",
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx"
            };
            return urls;
        }
    }
}
