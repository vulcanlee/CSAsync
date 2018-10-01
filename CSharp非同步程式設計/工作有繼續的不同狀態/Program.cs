using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 工作有繼續的不同狀態
{
    public enum 工作的接續動作Enum
    {
        完成,
        取消,
        異常失敗
    }

    /// <summary>
    /// 當工作完成之後，我們想要接續工作內容，繼續來處理，我們可以指定要接續工作的類型，例如：
    /// 正常完成的時候，要執行那些工作、若有發出取消指令的時候，要執行那些工作、當工作有異常發生的時候，該做哪些事情
    /// 這些接續工作的類型，可以在 ContinueWith 使用 TaskContinuationOptions 參數來指定
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 建立 CancellationTokenSource
            var cts = new CancellationTokenSource();
            // 取得 取消Token
            var token = cts.Token;

            // 嘗試取消下列的不同接續工作類型變數註解，查看不同的 Task 執行結果

            // 若指定工作完成接續工作類型是底下的類型，則，最後會輸出 工作正常無誤執行完畢
            工作的接續動作Enum fooTaskAction = 工作的接續動作Enum.完成;

            // 若指定工作完成接續工作類型是底下的類型，則，最後會輸出 這個工作已經被使用者取消了
            //工作的接續動作Enum fooTaskAction = 工作的接續動作Enum.取消;

            // 若指定工作完成接續工作類型是底下的類型，則，最後會輸出 這個工作有例外異常發生了
            //工作的接續動作Enum fooTaskAction = 工作的接續動作Enum.異常失敗;

            #region 定義要執行的工作
            Task<int> 新的工作 = Task.Run(async () =>
            {
                // 這裡表示這個非同步工作需要執行約1秒的時間
                await Task.Delay(1000);
                if (fooTaskAction == 工作的接續動作Enum.異常失敗)
                {
                    throw new Exception("唉，真糟糕呀，真糟糕");
                }
                else if (fooTaskAction == 工作的接續動作Enum.取消)
                {
                    token.ThrowIfCancellationRequested();
                }
                return 42;
            }, token); // 這個工作會判斷是否有取消需求發出

            #region 設定當工作完成之後，接續要怎麼處理；
            新的工作.ContinueWith((接續的工作) =>
            {
                Console.WriteLine("偵測到 這個工作已經被使用者取消了");
            }, TaskContinuationOptions.OnlyOnCanceled); // 這裡指定了這個接續工作，只有是在 [TaskContinuationOptions.OnlyOnCanceled] 也就是取消請求發生的時候，才會執行

            新的工作.ContinueWith((接續的工作) =>
            {
                Console.WriteLine("偵測到 這個工作有例外異常發生了:{0}", 接續的工作.Exception.InnerExceptions[0].Message);
            }, TaskContinuationOptions.OnlyOnFaulted);  // 這裡指定了這個接續工作，只有是在 [TaskContinuationOptions.OnlyOnFaulted] 也就工作內有例外異常發生的時候，才會執行

            var completedTask = 新的工作.ContinueWith((接續的工作) =>
            {
                Console.WriteLine("工作正常無誤執行完畢");  // 這裡指定了這個接續工作，只有是在 [TaskContinuationOptions.OnlyOnRanToCompletion] 也就工作正常結束的時候，才會執行
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            #endregion

            #endregion

            // 這裡將會設定該工作的處理過程中，究竟是會正常完成、取消、還是會有異常失敗產生
            switch (fooTaskAction)
            {
                case 工作的接續動作Enum.完成:
                    // 因為工作正常完成，所以，使用同步方式等候工作與接續工作完成
                    completedTask.Wait();
                    Console.WriteLine($"Task 回傳結果為 {新的工作.Result}");
                    break;
                case 工作的接續動作Enum.取消:
                    // 對這個工作發出取消請求
                    cts.Cancel();
                    break;
                case 工作的接續動作Enum.異常失敗:
                    // 讓工作在執行過程中，自動發出例外異常事件，接著，接續工作便會處理相關工作
                    break;
            }

            Console.WriteLine("按下任一按鍵，結束處理程序");
            Console.ReadKey();
        }
    }
}
