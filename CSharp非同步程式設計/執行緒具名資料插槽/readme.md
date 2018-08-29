# 摘要說明

在這個範例示範如何使用具名 資料位置/資料插槽 來儲存執行緒特定資訊

我們將會使用 Thread.GetNamedDataSlot 來取得要使用的 LocalDataStoreSlot 資料位置/資料插槽，如果具名的位置不存在，則會配置新的位置。 

具名的資料位置是公用的這個處理程序中的執行緒都可以管理。

## 練習說明

* 使用 Thread.GetNamedDataSlot 來取得要使用的 LocalDataStoreSlot 資料位置/資料插槽
* 使用 Thread.SetData 和 Thread.GetData 方法來設定和擷取插槽中的資訊
* 確認每個執行緒內都會使用自己那份的 資料位置/資料插槽 資料 
* 注意，這個存取資料插槽不是強型別，是針對 Object 來操作
* 請實際執行這個範例程式
  

  