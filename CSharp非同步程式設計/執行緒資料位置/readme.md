# 摘要說明

在這個範例中，您可使用 ThreadLocal 來建立執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。

在 .NET Framework 4 中，您可以使用 System.Threading.ThreadLocal<T> 類別來建立第一次取用物件時進行延遲初始化的執行緒區域物件

更多關於 延遲初始設定 的資訊，可以參考 [這裡](https://docs.microsoft.com/zh-tw/dotnet/framework/performance/lazy-initialization)

## 練習說明

* 使用 ThreadLocal<T> 來宣告靜態欄位或者執行個體欄位，使得每個執行緒都有一份儲存空間
* 建立兩個執行緒，每個執行緒都存取自己的靜態欄位
* 請實際執行這個範例程式
  * 確認彼靜態欄位在不同執行緒間，不會受到影響
  

  