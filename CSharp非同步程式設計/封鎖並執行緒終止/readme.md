# 摘要說明

這裡的範例程式碼，將會體驗如何使用 Thread(前景) & ThreadPool(背景)兩個類別所產生的執行緒已經正常結束了，請使用這個方法來確保執行緒已經結束。 如果執行緒不結束，呼叫端會無限期封鎖。 如果呼叫 Join 時執行緒已經終止，此方法就會立即傳回。

## 練習說明

* 使用 Thread 類別，產生一個新的執行緒，其為一個前景執行緒，可以檢查  Thread.IsBackground 屬性值，並且可以取得一個 Thread 物件
* 使用 ThreadPool.QueueUserWorkItem，產生一個新的執行緒，其為一個背景執行緒並沒有任何 Thread 物件可以取得
* 對於 Thread 物件變數，可以使用 Join() 方法來等候該執行緒執行完畢 
* 對於沒有 Thread 物件變數的背景執行緒，可以使用 AutoResetEvent.WaitOne() 方法來等候該背景執行緒執行完畢
* 請實際執行這個範例程式
  