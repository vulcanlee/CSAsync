# 摘要說明

在這個範例內，這個專案針對單一非同步工作，若非同步工作內有異常發生的時候，使用 TPL方式與 await 關鍵字，其針對例外異常處理的方式

使用 Task.Wait() 方式來 Block 等候非同步工作完成，此時，若非同步工作內有異常發生，則會丟出 AggregateException

使用 await Task 方式來非同步等候工作完成，若此時非同步 工作內有異常發生，則會丟出當時非同步工作內的異常

## 練習說明

* 請實際執行這個範例程式

  