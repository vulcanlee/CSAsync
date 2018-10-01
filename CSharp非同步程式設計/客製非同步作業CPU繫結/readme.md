# 摘要說明

在這個範例，我們使用 Task.Run 直接定義出非同步工作方法，並且使用額外執行緒來處理。 

在這個範例中，我們建立一個CPU繫結非同步的方法，但是，需要依賴 Thread Pool與資料平行化的方式來進行處理，執行這個非同步方法

## 練習說明

* 使用 Thread 類別，產生一個新的執行緒，其為一個前景執行緒，可以檢查  Thread.IsBackground 屬性值，並且可以取得一個 Thread 物件
* 使用 ThreadPool.QueueUserWorkItem，產生一個新的執行緒，其為一個背景執行緒並沒有任何 Thread 物件可以取得
* 對於 Thread 物件變數，可以使用 Join() 方法來等候該執行緒執行完畢 
* 對於沒有 Thread 物件變數的背景執行緒，可以使用 AutoResetEvent.WaitOne() 方法來等候該背景執行緒執行完畢
* 請實際執行這個範例程式
  