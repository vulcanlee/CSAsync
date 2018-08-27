# 摘要說明

在這個範例內，我們將會體驗要停止一個執行緒的執行，可以使用Thread.Abort方法；然而，因為 Abort 方法是在另外一個執行緒上執行(我們是從主執行緒呼叫該方法)，並於所在執行緒中引發 ThreadAbortException，開始處理執行緒的結束作業。 

從 .NET Framework version 2.0 開始，CLR 通用語言執行平台允許執行緒中大多數未處理的例外狀況自然地繼續，不過，如果在 CLR 通用語言執行平台所建立的執行緒中有遇到因為呼叫了 Abort，而導致執行緒中擲回 ThreadAbortException 狀況，則此例外狀況會終止執行緒，但通用語言執行平台不允許例外狀況進一步繼續作業。

## 練習說明

* 請在此設定中斷點 : Console.WriteLine("異常訊息 : " + ex.Message);
* 若執行緒中引發 ThreadAbortException，開始處理執行緒的結束作業
* 我們可以使用 AppDomain.CurrentDomain.UnhandledException 事件來捕捉到任何 .NET 環境中沒有被應用程式捕捉到的例外異常事件
* 執行緒委派方法內沒有進行例外異常捕捉，會如何呢？
  * 發生了例外異常 -> 觸發UnhandledException，處理程序結束執行
  * 觸發了 Thread.Abort() -> 該執行緒結束執行 
* 執行緒委派方法內有進行例外異常捕捉，會如何呢？
  * 發生了例外異常 -> 
    * 若 try catch 有捕捉該異常，該執行緒結束執行，其他不受到影響
    * 否則，觸發UnhandledException，處理程序結束執行
  * 觸發了 Thread.Abort() -> 該執行緒結束執行，其他不受影響
* 若在執行緒內發生了例外異常(請根據上述情境來測試)，我們可以透過 UnhandledException 捕捉到嗎
* 請實際執行這個範例程式

  