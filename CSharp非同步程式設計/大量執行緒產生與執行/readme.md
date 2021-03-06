# 摘要說明

在這個範例中，我們將會產生2000個執行緒，接著我們會觀察整體系統的 CPU使用率、Process數量、執行緒數量、可用記憶體數量、環境交換次數，嘗試了解，當執行緒過多的時候，對於系統有何影響?

## 練習說明

* 為了要說明處理程序建立了大量的執行緒對系統有何影響，請先開啟效能監視器
  * 選擇物件為 [System] > 計數器為 [Context Switch/sec] 
  
    是在電腦上所有處理器從一個執行緒切換到另一個執行緒的合併速率
  * 物件為 [System] > 計數器為 [Processes]
  
    是在取樣時間中電腦上的處理程序數目
  * 物件為 [System] > 計數器為 [Threads]
  
    是在取樣時間中電腦上的執行緒數目
  * 物件為 [Memory] > 計數器為 [Available MBtyes]
  
    是指電腦上的可用實體記憶體的數量 (以 MB 計算)，可立即配置給處理程序或供系統使用
    
* 原則上，當有大量執行緒產生並且開始執行的時候，這四個計數器會呈現
  * 物件為 [System] > 計數器為 [Context Switch/sec]
  
    數值越來越大，也就是處理器必須花費更多時間在內容交換上，也間接造成每個執行緒可以使用的處理器次數大幅減少
  * 物件為 [System] > 計數器為 [Processes]  
  
    原則上，這個數值是沒有變化的
  * 物件為 [System] > 計數器為 [Threads]
  
    這個數值會越來越大，表示系統中有更多的執行緒存在
  * 物件為 [Memory] > 計數器為 [Available MBtyes]
  
    這個數值會逐漸減少，因為，每產生一個執行緒，系統至少減少 1MB 記憶體(因為執行緒需要使用到)
* 請實際執行這個範例程式
  * 看看會發生甚麼樣的結果，整個處理程序可以正常執行完成嗎？
  

  