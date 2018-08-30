# 摘要說明

這個範例將會產生體驗關於 [APM非同步程式設計模型] ，在這裡使用了 HttpRequest 來進行讀取遠端網頁(www.microsoft.com)上的內容，APM 的特色就是 使用 BeginXXX 方法啟動非同步的呼叫，完成後，使用 EndXXX結束非同步呼叫並且取得結果內容

使用 IAsyncResult 設計模式的非同步作業會被實作成兩個方法，名稱為 BeginOperationName 及 EndOperationName ，分別負責開始和結束非同步作業 OperationName。 

## 練習說明

* 使用 WebRequest.Create 建立 HttpWebRequest 物件
* 呼叫 HttpWebRequest.BeginGetResponse 非同步方法呼叫
* 在 回呼 callback 事件內，將參數轉型為 HttpWebRequest
* 呼叫 HttpWebRequest.EndGetResponse 方法
* 請實際執行這個範例程式
  * 請試著比較執行前後的執行緒有所不同
  