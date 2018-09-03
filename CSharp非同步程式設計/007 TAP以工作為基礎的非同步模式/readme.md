# 摘要說明

這個專案說明了如何使用 TAP Task-based Asynchronous Pattern 來呼叫非同步的工作。在這個設計模式下，我們不需要呼叫 BeginXXX/EndXXX，也不再需要透過事件 Call Back來得知是否工作完成，取而代之的是，我們不再需要使用任何複雜的非同步程式設計方式來寫程式碼，而是使用同步的方式來寫程式，但寫出來的程式是具有非同步效果，並且不會造成 Thread Block。

注意下列並沒有依照底下說明進行設定，將無法 在 Main 方法內使用 await 關鍵字，這是因為在App的 Entry Point內，無法使用 Async關鍵字。

請滑鼠雙擊專案的 [Property] 節點，切換到標籤頁次 [建置] > [進階]，在 [進階建置設定]對話窗內，選擇 [一般] > [語言版本]，選擇 C# 7.1 以上版本

## 練習說明

* DownloadHtmlAsyncTask 將會透 HttpClient 非同步的方式取得網頁內容，因此，需要設定函式簽章為 async Task<string>
* 當要讀取網頁內容時候，我們使用了 await httpClient.GetStringAsync(url) 表示式搭配 C# 的 await 關鍵字，形成一個非同步的方法呼叫
* 請實際執行這個範例程式
  * 觀察 呼叫 Await 之前與之後，當時的執行緒會有所不同，您可以解釋為什麼嗎？
  