using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sleep與Delay差異
{
    /// <summary>
    /// 這個範例程式展示了 Thread.Sleep & Task.Delay 的不同
    /// Thread.Sleep 會造成當時執行緒被封鎖(例如，UI執行緒無法執行任何指令)
    /// Task.Delay 因為使用非同步等候方式，所以，當時的執行緒是不會被封鎖的
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn使用執行緒Sleep_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("開始時間:{0}", DateTime.UtcNow);
            Console.WriteLine("請注意，此時，App的UI會凍結3秒鐘，也就是 UI執行緒會有三秒鐘不會執行任何指令");
            var fooColor = btn使用執行緒Sleep.Background;
            btn使用執行緒Sleep.Background = Brushes.Red;
            Thread.Sleep(3000);
            btn使用執行緒Sleep.Background = fooColor;
            Console.WriteLine("結束時間:{0}", DateTime.UtcNow);
        }

        private async void btn使用TaskDelay_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("開始時間:{0}", DateTime.UtcNow);
            Console.WriteLine("請注意，此時，App的UI不會凍結，您可以試著拖拉App視窗");
            var fooColor = btn使用執行緒Sleep.Background;
            btn使用TaskDelay.Background = Brushes.Red;
            await Task.Delay(3000);
            btn使用TaskDelay.Background = fooColor;
            Console.WriteLine("結束時間:{0}", DateTime.UtcNow);
        }
    }
}
