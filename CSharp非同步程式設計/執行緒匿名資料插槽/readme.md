# 摘要說明

在這個範例示範如何使用匿名 資料位置/資料插槽 來儲存執行緒特定資訊

我們將會使用 Thread.AllocateDataSlot() 來取得要使用的 LocalDataStoreSlot 資料位置/資料插槽


在這裡，我們會在每個執行緒中使用 Random 這個類別產生的物件，根據 [The System.Random class and thread safety](https://docs.microsoft.com/zh-tw/dotnet/api/system.random?view=netframework-4.7.2#the-systemrandom-class-and-thread-safety) 描述：此類型的任何 public static 成員皆為安全執行緒。不保證任何執行個體成員為安全執行緒。

## 練習說明

* 使用 Thread.AllocateDataSlot() 來取得要使用的 LocalDataStoreSlot 資料位置/資料插槽
* 使用 Thread.SetData 和 Thread.GetData 方法來設定和擷取插槽中的資訊
* 確認每個執行緒內都會使用自己那份的 資料位置/資料插槽 資料 
* 注意，這個存取資料插槽不是強型別，是針對 Object 來操作
* 請實際執行這個範例程式
  

  