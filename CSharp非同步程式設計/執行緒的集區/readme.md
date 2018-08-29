# 摘要說明

這個範例展示了如何透過 ThreadPool 來做到多執行緒的多工處理

## 練習說明

* 透過 ThreadPool 建立兩個背景執行緒
* 使用 ThreadPool.QueueUserWorkItem 並傳入一個委派方法，從 執行緒的集區 取得一個 執行緒來執行委派工作
* 也可以接受一個 物件型別 object 傳遞參數到委派方法內
* 請實際執行這個範例程式
  