# 摘要說明

當工作完成之後，我們想要接續工作內容，繼續來處理，我們可以指定要接續工作的類型，例如：

正常完成的時候，要執行那些工作、若有發出取消指令的時候，要執行那些工作、當工作有異常發生的時候，該做哪些事情

這些接續工作的類型，可以在 ContinueWith 使用 TaskContinuationOptions 參數來指定

## 練習說明

* 使用 工作的接續動作Enum 列舉來模擬 正常執行完成工作、取消工作、工作異常的狀態
* 當模擬異常發生的時候，使用 throw new Exception
* 當模擬取消工作的時候，建立 CancellationTokenSource 物件，使用 CancellationTokenSource.Cancel() 方法發出取消工作需求
  * 在工作內，使用 TokenThrowIfCancellationRequested() 判斷是否要發出工作取消例外異常
* 正常執行完畢後，要取得工作的回傳內容
* 請實際執行這個範例程式

  