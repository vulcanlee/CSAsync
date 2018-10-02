# 摘要說明

在這個範例中，因為使用 Console 範本製作範例程式，不屬於 STA（單執行緒套間 Single Threaded Apartment） 

在主執行緒內沒有定義 執行緒的同步處理內容(SynchronizationContext) ，
我們可以在呼叫非同步方法後，也就是在 await 之後，嘗試將接續封送處理回原始擷取的內容

若執行 .ConfigureAwait(true) 表示【要】接續封送處理回原始擷取的內容，在此，是使用 ThreadPool 作為SynchronizationContext

若執行 .ConfigureAwait(false) 表示【不要】接續封送處理回原始擷取的內容

詳情，請參考: https://msdn.microsoft.com/zh-tw/library/hh873173(v=vs.110).aspx

## 練習說明

* 在這裡，我們呼叫 HttpClient 非同步讀取網頁資料資料，分別使用 ConfigureAwait(true) 與 ConfigureAwait(false)，觀察執行前後的執行緒變化
* 執行該範例專案
