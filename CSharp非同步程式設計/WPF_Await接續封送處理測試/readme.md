# 摘要說明

在這個範例中，因為使用 WPF 製作範例程式，屬於  STA單執行緒套間  [Single Threaded Apartment](https://msdn.microsoft.com/en-us/library/system.threading.apartmentstate(v=vs.110).aspx)，其中， UI 執行緒內有定義 執行緒的同步處理內容(SynchronizationContext) 

我們可以在呼叫非同步方法後，也就是在 await 之後，嘗試將接續封送處理回原始擷取的內容

若執行 .ConfigureAwait(true) 表示【要】接續封送處理回原始擷取的內容，在此，SynchronizationContext 指的就是 UI 執行緒

若執行 .ConfigureAwait(false) 表示【不要】接續封送處理回原始擷取的內容
    
詳情，請參考: https://msdn.microsoft.com/zh-tw/library/hh873173(v=vs.110).aspx

## 練習說明

* 執行該範例專案
