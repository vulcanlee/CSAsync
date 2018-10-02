# 摘要說明

在這個範例中，因為使用 WPF 製作範例程式，屬於  STA單執行緒套間  [Single Threaded Apartment](https://msdn.microsoft.com/en-us/library/system.threading.apartmentstate(v=vs.110).aspx)，其中， UI 執行緒內有定義 執行緒的同步處理內容(SynchronizationContext) 

我們可以在呼叫非同步方法後，也就是在 await 之後，嘗試將接續封送處理回原始擷取的內容

若執行 .ConfigureAwait(true) 表示【要】接續封送處理回原始擷取的內容，在此，SynchronizationContext 指的就是 UI 執行緒

若執行 .ConfigureAwait(false) 表示【不要】接續封送處理回原始擷取的內容
    
詳情，請參考: https://msdn.microsoft.com/zh-tw/library/hh873173(v=vs.110).aspx

## 練習說明

* 請分別針對這七個按鈕的執行結果，了解與分析，執行緒在前後的執行變化、輸入Log的輸出順序、與應用程式的執行結果
* [1] 有await 非同步工作且會回到UI執行緒
* [2] 有await 非同步工作且不會回到UI執行緒
* [3] 沒有await 非同步工作且會回到UI執行緒
* [4] 沒有await 非同步工作且不會回到UI執行緒
* [5] 把非同步方法，當作同步方式呼叫(程式會凍結)
* [6] 把非同步方法，當作同步方式呼叫(不會凍結)
* [7] 開始(UI執行緒忙碌中)


* 執行該範例專案
