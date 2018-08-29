# 摘要說明

在這個範例中，您可使用 Managed 執行緒區域儲存區 (Thread Local Storage，TLS) 來儲存對執行緒及應用程式定義域來說是唯一的資料。

.NET Framework 提供兩種方法來使用 Managed TLS：執行緒相關的靜態欄位 (thread-relative static fields) 和資料位置 (data slots)。執行緒相關的靜態欄位提供更佳的效能比資料位置

* 如果您可以預期編譯時期的確切需求，請使用執行緒相關的靜態欄位 (thread-relative static fields) (在 Visual Basic 中為執行緒相關 Shared 欄位)。
  
  執行緒相關靜態欄位提供最佳效能。 它們也提供編譯時期類型檢查的優點。

* 若您的實際需求只能在執行階段進行探索，請使用資料插槽 (data slots)。

  比起執行緒相關靜態欄位，資料插槽的速度更慢且更難使用，而且會將資料儲存為 Object 型別，因此您必須將它轉換成正確的型別才能使用它。

無論您使用的是執行緒相關靜態欄位或資料插槽，受控 TLS 中的資料對執行緒和應用程式定義域的組合都是唯一的。

* 在應用程式定義域內，一個執行緒無法修改另一個執行緒的資料，即使這兩個執行緒使用同一個欄位或插槽也一樣。

* 當執行緒從多個應用程式定義域存取相同的欄位或插槽時，會在每個應用程式定義域中維護個別的值。

關於更多關於執行緒區域儲存區方面的資訊，可以參考 [執行緒相關的靜態欄位和資料位置](https://docs.microsoft.com/zh-tw/dotnet/standard/threading/thread-local-storage-thread-relative-static-fields-and-data-slots)

## 練習說明

* 將靜態欄位使用 [ThreadStatic] 屬性標示，使得每個執行緒都有一份儲存空間
* 建立兩個執行緒，每個執行緒都存取自己的靜態欄位
* 請實際執行這個範例程式
  * 確認彼靜態欄位在不同執行緒間，不會受到影響
  

  