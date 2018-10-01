# 摘要說明

在這個範例，我們需要針對 計算繫結 CPU Bound 需求，我們使用 TaskCompletionSource 直接定義出非同步工作方法。 

在這個範例中，我們建立一個IO繫結非同步的方法，但是，不需要依賴 Thread Pool ，也就是不需要一直持續耗用一個以上 Thread 來執行這個非同步方法

## 練習說明

* 在這個範例中，將會透過 TaskCompletionSource 建立一個非同步工作
* 使用 WebClient 進行非同步工作，並且根據當時情況，回報是否有例外異常、取消情況
* 請實際執行這個範例程式
  