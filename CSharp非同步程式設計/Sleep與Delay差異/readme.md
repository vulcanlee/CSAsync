# 摘要說明

這個範例程式展示了 Thread.Sleep & Task.Delay 的不同

Thread.Sleep 會造成當時執行緒被封鎖(例如，UI執行緒無法執行任何指令)

Task.Delay 因為使用非同步等候方式，所以，當時的執行緒是不會被封鎖的

## 練習說明

* 透過 GUI 應用程式，如 WPF，實際體驗 Sleep 與 Delay 的差異
* 請實際執行這個範例程式
  