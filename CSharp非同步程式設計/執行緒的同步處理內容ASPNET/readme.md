# 摘要說明

在這篇文章中，我們將要來說明在 ASP.NET 使用 執行緒的同步內容 SynchronizationContext 進行非同步工作處理問題，也就是我們在 ASP.NET 專案程式碼開發時候，當使用者發出一個 Http Request 請求的時候， IIS 將會從執行緒集區取出一個執行緒，用來處理這個使用者請求；此時，在這個執行緒中，當時的同步內容 SynchronizationContext 將會是一個 System.Web.AspNetSynchronizationContext 類別物件，這個類別物件將會用於處理 ASP.NET 的執行緒同步內容的處理工作。

因此，若我們在這個 Http Request 請求執行緒中，另外進行一個非同步的工作，而該非同步的工作將會在另外一個新的執行緒中來執行，此時，在這個新的執行緒中，將無法取得 `System.Web.HttpContext.Current` 屬性值，此時，該屬性值將會是 null。

## 練習說明

* 請實際執行這個範例程式
* 請設定continueOnCapturedContext = true; 或者 continueOnCapturedContext = false;
  > 觀看執行結果有何不同
  >
  > 為什麼會有這樣的現象產生
  