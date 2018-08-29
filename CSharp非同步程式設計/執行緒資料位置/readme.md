# 摘要說明

在這個範例中，您可使用 Managed 執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。

.NET Framework 提供兩種方法來使用 Managed TLS：執行緒相關的靜態欄位 (thread-relative static fields) 和資料位置 (data slots)。執行緒相關的靜態欄位提供更佳的效能比資料位置

在 .NET Framework 4 中，您可以使用 System.Threading.ThreadLocal<T> 類別來建立第一次取用物件時進行延遲初始化的執行緒區域物件。

## 練習說明

  使用 Thread.SetData 和 Thread.GetData 方法來設定和擷取插槽中的資訊
* 請實際執行這個範例程式
  

  