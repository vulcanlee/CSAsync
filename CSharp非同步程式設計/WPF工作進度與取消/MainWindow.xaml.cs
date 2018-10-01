using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace WPF工作進度與取消
{
    /// <summary>
    /// 在這個範例中，將會從網路下載一個大檔案 master.zip，
    /// 當我們按下開始按鈕後，便會開始下載，下載的時候，進度狀態棒會根據下載數量更新完成百分比，當然，您也可以按下取消按鈕，已取消這個非同步工作
    /// </summary>
    public partial class MainWindow : Window
    {
        // 宣告 CancellationTokenSource ，可以用來發出取消的請求
        CancellationTokenSource cts;
        public MainWindow()
        {
            InitializeComponent();
            控制項初始化();
        }

        public void 控制項初始化()
        {
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            probar進度棒.Value = 0;
        }

        public void 開始下載()
        {
            btn開始.IsEnabled = false;
            btn取消.IsEnabled = true;
            probar進度棒.Value = 0;
        }

        public void 取消下載()
        {
            btn開始.IsEnabled = true;
            btn取消.IsEnabled = false;
            probar進度棒.Value = 0;
        }

        private async void btn開始_Click(object sender, RoutedEventArgs e)
        {
            string page = "https://download.microsoft.com/download/2/2/A/22AA9422-C45D-46FA-808F-179A1BEBB2A7/office2007sp3-kb2526086-fullfile-en-us.exe";
            //string page = "https://github.com/mono/mono/archive/master.zip";
            cts = new CancellationTokenSource();
            var progressIndicator = new Progress<int>((s) =>
            {
                probar進度棒.Value = s;
            });

            開始下載();
            try
            {
                await 下載檔案內容(page, cts.Token, progressIndicator);
            }
            catch { }
        }

        private async void btn取消_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
            await Task.Delay(500);
            取消下載();
        }

        public Task 下載檔案內容(string page, CancellationToken token, IProgress<int> progress)
        {
            var tcs = new TaskCompletionSource<string>();

            if(toggleButton.IsChecked.Value == false)
            {
                #region 使用 WebClient 與 EAP 模式來設計
                // 建立與指名工作已經被開始與準備好了
                var wc = new WebClient();
                wc.DownloadStringCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        tcs.TrySetException(e.Error); // 發生錯誤的時候，需要把異常設定到工作的 Exception 內
                    }
                    else if (e.Cancelled)
                    {
                        tcs.TrySetCanceled(); // 若收到取消請情，要設定該工作已經被取消了
                    }
                    else
                    {
                        tcs.TrySetResult(e.Result); // 正常完成工作之後，將結果設定到工作內
                    }
                };

                wc.DownloadProgressChanged += (s, e) =>
                {
                    if (progress != null)
                    {
                        progress.Report((int)(e.BytesReceived * 100 / e.TotalBytesToReceive));
                    }
                };

                token.Register(() => wc.CancelAsync()); // 若收到取消請求，要對 WebClient 發出取消請求

                // 開始啟動非同步的抓取網頁
                wc.DownloadStringAsync(new Uri(page));
                // 將這個非同步工作傳回去
                return tcs.Task;
                #endregion
            }
            else
            {
                #region 使用 HttpClient 與 TAP 模式來設計
                return Task.Run(async () =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync(page, cts.Token))
                        {
                            using (HttpContent content = response.Content)
                            {
                                using (var httpStream = await content.ReadAsStreamAsync())
                                {
                                    long 串流總共長度 = httpStream.Length;
                                    long 串流已讀取長度 = 0;
                                    byte[] buffer = new byte[1024];
                                    while (true)
                                    {
                                        int read = await httpStream.ReadAsync(buffer, 0, buffer.Length, token);
                                        if (read <= 0)
                                            break;
                                        串流已讀取長度 += read;
                                        if (progress != null)
                                        {
                                            progress.Report((int)(串流已讀取長度 * 100 / 串流總共長度));
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
            }

        }
    }
}
