# 摘要說明

這裡的範例程式碼，將會體驗如何使用在主執行緒使用 WaitHandle 類別的靜態 WaitAny 和 WaitAll 方法等候工作完成時，兩個執行緒要執行背景工作的方式。

這個類別一般做為同步物件 (Synchronization Object) 的基底類別 (Base Class) 使用。 衍生自 WaitHandle 的類別會定義一項信號機制，表示取得或者釋出共用資源的存取權，但在等候存取共用資源的期間，則會使用繼承的 WaitHandle 方法來加以封鎖。

## 練習說明

* 不同執行緒間取得或者釋出共用資源的存取權，做到執行緒同步需求
* WaitHandle : 將等候共用資源獨佔存取權限的特定作業系統物件封裝起來
* AutoResetEvent : 通知等待中的執行緒事件已發生
* AutoResetEvent.Set() 將事件的狀態設定為收到信號，允許一個或多個等待中的執行緒繼續執行
* AutoResetEvent.Reset() 將事件的狀態設定為未收到信號，因而造成執行緒封鎖
* 我們需要做到 ： 使用 ThreadPool.QueueUserWorkItem 產生兩個執行緒，並且`等候所有的執行緒都結束` 與 `等候任意一個執行緒結束`
* 這裡需要使用 WaitHandle.WaitAll 和 WaitHandle.WaitAny
* 請實際執行這個範例程式
  