# 摘要說明

SynchronizationContext 類別.aspx)可以提供在各種同步處理模式中傳播同步處理內容的基本功能。這通常會在我們進行 GUI 類型應用程式的時候會用到，例如： Win Forms / WPF / Xamarin.Forms 這些類型的專案，會有這樣的需求那是因為，在這裡 GUI 類型的專案中，若想要設定 UI 控制項相關的屬性或者要變更其設定值，此時，您僅能夠在 UI Thread (UI 執行緒，或稱為 Main Thread 主執行緒) 內來執行這些程式碼。不過，有些時候，我們會需要透過多執行緒的程式設計方式，讓許多需要花費比較多時間的方法，在其他執行緒中來執行，不過，當在其他執行緒執行的方法內，有需要設定 UI 相關的屬性，我們就需要讓這些程式碼能夠在 UI 執行緒內來執行，這個時候，我們就可以透過 SynchronizationContext 類別 來滿足這樣需求。

由於 Win Forms / WPF / Xamarin.Forms 這些類型的專案內，已經內建這樣的機制，提供您在不同執行緒內，可以指定某些程式碼可以在 UI 執行緒內來執行；而在這篇文章中，我們將會建立一個 Console 類型的專案，並且自訂一個 SynchronizationContext 類別，提供相同的功能。
## 練習說明

* MySynchronizationContext 繼承了 SynchronizationContext 類別，將會是我們自己設計的同步內容類別
* 我們需要複寫 Send / Post 這兩個方法，以提供同步與非同的委派方法執行
* RunMessagePump() 方法則是我們建立的 MySynchronizationContext 類別內，用來檢查是否別的執行緒提出請求，要在主執行緒下來執行指定的委派方法
* 在主執行緒下，我們使用 SynchronizationContext.SetSynchronizationContext(new MySynchronizationContext()) 方法，設定我們正在使用的同步內容物件
* 在主執行緒下，我們建立兩個執行緒，一個是會在該執行緒下，透過同步內容來執行一個非同步的委派方法，另外一個是監聽來自於使用者的鍵盤輸入指令
  