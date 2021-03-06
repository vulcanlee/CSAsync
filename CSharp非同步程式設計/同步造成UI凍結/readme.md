# 摘要說明

這裡的範例程式碼，將會體驗如何在這個WPF範例，說明了，當有大量同步程式在 UI Thread 上執行的時候，會造成 WPF 的整個視窗被凍結了，而無法操作；不過，若採用了非同步的方式來開發，因為大量運算是在其他的執行緒上執行，不會造成 UI 執行緒被封鎖住，所以，WPF App是可以與使用者互動的。

## 練習說明

* 請實際執行這個範例程式
* 當 btn同步會凍結_Click 按鈕事件被觸發之後
 
  此時將會計算從 0 到 99999999 的加總總合為多少的計算，不過，這樣的計算程式碼，將會在主執行緒 Main Thread / UI 執行緒 UI Thread 上來執行；但是，這樣的設計方式，將會造成整個應用程式凍結，進而在計算過程中，無法更新任何螢幕 UI 資訊。
* 當 btn非同步不會凍結_Click 按鈕事件被觸發之後

  此時將會計算從 0 到 99999999 的加總總合為多少的計算，不過，這樣的計算程式碼，將會在另外一個新建立的執行緒中來執行，並且當要進行 UI 控制項屬性更新的時候，我們會透過 Dispatcher.Invoke 傳入一個委派方法，透過同步內容機制，讓該委派方法在主執行緒中來執行

  