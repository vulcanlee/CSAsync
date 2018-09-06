# 摘要說明

以結束非同步作業的方式封鎖應用程式執行，無法在等候非同步作業的結果時繼續執行其他工作的應用程式必須封鎖，直到作業完成為止。 

請使用下列其中一個選項，在等候非同步作業完成時，封鎖應用程式的主執行緒
* 呼叫非同步作業的 EndOperationName方法。 
* 請使用非同步作業的BeginOperationName 方法所傳回之 IAsyncResult 的 AsyncWaitHandle 屬性。 

https://msdn.microsoft.com/zh-tw/library/ms228967(v=vs.110).aspx

下列程式碼範例會示範使用 Dns 類別中的非同步方法，以擷取使用者指定之電腦的網域名稱系統資訊。 

請注意，null  會傳遞給 BeginGetHostByNamerequestCallback 和 stateObject 參數，因為在使用這個方法時，並不需要這些引數。

## 練習說明

* 使用 APM 呼叫 BeginXXX 啟動非同步工作，但是，無須傳送 callback 委派方法
* 此時可以立即得到 IAsyncResult 型別物件，因為執行緒沒有被封鎖住，您可以在這裡處理其他工作
* 直接呼叫 EndXXX 方法，直接等候非同步的處理最後結果(也許是成功、也許是失敗)
  
  不過此時的 Thread 是被鎖定的，也就是無法繼續執行其他工作
* 請實際執行這個範例程式
  * 請比較與 APM + Callback 的用法差異與優缺點
  