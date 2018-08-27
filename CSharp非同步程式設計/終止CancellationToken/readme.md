# 摘要說明

在這個範例內，我們將會體驗要停止一個執行緒的執行，可以使用 取消許可證來源 CancellationTokenSource 物件，向 CancellationToken 發出訊號，表示應該將它取消。

其中，我們將會用到這兩個類別的物件，可以從底下文件查看到更多資訊

* CancellationTokenSource
  
  https://docs.microsoft.com/zh-tw/dotnet/api/system.threading.cancellationtokensource?view=netframework-4.7.2
* CancellationToken 
  
  https://docs.microsoft.com/zh-tw/dotnet/api/system.threading.cancellationtoken?view=netframework-4.7.2。  

## 練習說明

* 想要使用 CancellationTokenSource 類別的取消需求功能，可以遵循底下的步驟
    * 建立 CancellationTokenSource 類別的物件
    * 將 CancellationTokenSource.Token 傳入到執行緒委派方法內
    * 若在其他執行緒，想要取消該執行緒的執行，可以呼叫 CancellationTokenSource.Cancel() 方法
    * 在要關注的執行緒內
      * 採用輪詢的方式來檢查 CancellationToken.IsCancellationRequested 
      * 採用輪詢的方式來呼叫 CancellationToken.ThrowIfCancellationRequested() 方法，不過，採用此方法，該執行緒的委派方法內要在 try...catch 內
* 請實際執行這個範例程式

  