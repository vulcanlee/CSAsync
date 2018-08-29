# 摘要說明

在這個範例示範如何使用資料位置 (data slots)來儲存執行緒特定資訊，也就是要建立一個 LocalDataStoreSlot 類別物件

在這裡，我們會在每個執行緒中使用 Random 這個類別產生的物件，根據 [The System.Random class and thread safety](https://docs.microsoft.com/zh-tw/dotnet/api/system.random?view=netframework-4.7.2#the-systemrandom-class-and-thread-safety) 描述：此類型的任何 public static 成員皆為安全執行緒。不保證任何執行個體成員為安全執行緒。

## 練習說明

* 在所有的執行緒上配置未命名的資料插槽 LocalDataStoreSlot : Thread.AllocateDataSlot();
* 在每個執行緒的資料槽中設置不同的資料，允許其他執行緒執行資料插槽操作時候，是唯一的
* 使用 Thread.SetData 和 Thread.GetData 方法來設定和擷取插槽中的資訊
* 注意，這個存取資料插槽不是強型別，是針對 Object 來操作
* 請實際執行這個範例程式
  

  