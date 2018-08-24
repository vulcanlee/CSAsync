# 摘要說明

這個範例展示了 BackgroundWork 這個類別，讓您可以在不同執行緒上執行作業，也就是一個背景工作

這個類別簡化了要進行多工非同步的程式設計複雜度，並且提供了多樣性的事件，可讓我們方便處理各項事務，這樣，讓我們可以不用再面對 Thread，也可以做到多工處理。

這個 BackgroundWork 已經過時，不建議使用，您可以考慮使用 Task 來替代

## 練習說明

* 當想要透過 BackgroundWorker 類別建立一個非同步執行緒的處理工作，您可以訂閱相關事件，關注背景執行緒執行情況。
* RunWorkerAsync 方法 ： 開始執行背景作業
* DoWork 事件 ： 發生於當 RunWorkerAsync 呼叫 
* ProgressChanged 事件 ： 發生於當 ReportProgress(System.Int32) 呼叫 
* RunWorkerCompleted 事件 ： 發生於背景作業已完成、 已取消，或引發例外狀況 
* 請實際執行這個範例程式
  * 在這個範例中，主執行緒與各處理事件所用到的執行緒ID，為什麼會有這樣的情況發生呢?
* 更多資訊，請參考 [BackgroundWorker 元件概觀](https://docs.microsoft.com/zh-tw/dotnet/framework/winforms/controls/backgroundworker-component-overview)
  