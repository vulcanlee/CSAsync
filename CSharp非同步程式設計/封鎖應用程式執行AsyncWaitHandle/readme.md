# 摘要說明

使用 AsyncWaitHandle 封鎖應用程式執行，https://msdn.microsoft.com/zh-tw/library/ms228962(v=vs.110).aspx 。下列程式碼範例會示範在 DNS 類別中使用非同步方法，以擷取使用者所指定之電腦的網域名稱系統資訊。 且會示範使用與非同步作業關聯的 WaitHandle 來進行封鎖。 請注意，由於在使用此處理方法時不需要 BeginGetHostByNamerequestCallback 和 stateObject 參數，對於這兩個參數都會傳遞 null。

## 練習說明

* 使用 APM 呼叫 BeginXXX 啟動非同步工作，但是，無須傳送 callback 委派方法
* 使用 AsyncWaitHandle.WaitOne 等候，直到整個處理程序完成(注意，此時，不是透過 callback 方法來處理後續工作)
* 由於非同步工作已經完成了，所以，我們在此呼叫了 EndXXX 方法，取得非同步工作的處理結果
* 請實際執行這個範例程式
  * 請比較與 APM + Callback 的用法差異與優缺點
  