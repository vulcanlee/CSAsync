# 摘要說明

這是使用 EAP Event-based Asynchronous Pattern 範例，這個樣式特色為啟動非同步呼叫之後，就會再另外一個 Thread 繼續來執行這些非同步的需求，不會封鎖呼叫執行緒。當完成之後，會透過 Call Back來繼續處理相關工作，而是否完成與完成的結果內容，可以透過這個委派事件參數取得

將非同步功能公開到用戶端程式碼的方式有許多種； 事件架構非同步模式會針對要呈現非同步行為之類別指示一個方法，您可以參考這份 [文件](https://msdn.microsoft.com/zh-tw/library/ms228969(v=vs.110).aspx)

這裡使用 WebClient 類別來做為 EAP 設計方法的練習說明

## 練習說明

* 建立 WebClient 物件
* 訂閱 WebClient.DownloadStringCompleted (可以使用具名或者匿名 delegate / Lambda 委派方法來設計)
* 完成非同步呼叫之後，可以透過委派事件內的參數，取得此次非同步工作的失敗/成功狀態，完成結果內容
* 使用 WebClient.DownloadStringAsync 進行非同步方法呼叫
* 請實際執行這個範例程式
  * 請試著比較執行前後的執行緒有所不同
  