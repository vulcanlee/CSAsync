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

namespace 同步造成UI凍結
{
    /// <summary>
    /// 這個WPF範例，說明了，當有大量同步程式在 UI Thread 上執行的時候，會造成 WPF 的整個視窗被凍結了，而無法操作
    /// 不過，若採用了非同步的方式來開發，因為大量運算是在其他的執行緒上執行，不會造成 UI 執行緒被封鎖住，所以，WPF App是可以與使用者互動的
    /// </summary>
    public partial class MainWindow : Window
    {
        long 合計次數 = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 當按下了 [同步會凍結] (左下方按鈕)，會採用同步的方式來執行大量運算，
        /// 在所呼叫的方法尚未執行完成之前，這個 WPF程式是被凍結住的，而無法操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn同步會凍結_Click(object sender, RoutedEventArgs e)
        {
            tbk狀態.Text = "開始執行...";
            大量迴圈計算次數();
            tbk狀態.Text = "";
        }

        /// <summary>
        /// 當按下了 [非同步不會凍結] (右下方按鈕)，會採用非同步的方式來執行大量運算，
        /// 因為，大量運算是在另外一個執行緒上執行，所以，WPF App不會被凍結
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn非同步不會凍結_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                合計次數 = 0;
                Dispatcher.Invoke(() =>
                {
                    tbk狀態.Text = "開始執行...";
                });
                Thread.Sleep(1000);
                for (long i = 0; i < 99999999; i++)
                {
                    if (i % 500 == 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            tbk狀態.Text = 合計次數.ToString();
                        });
                    }
                    合計次數++;
                }
                Dispatcher.Invoke(() =>
                {
                    tbk狀態.Text = "";
                });
            }).Start();
        }

        void 大量迴圈計算次數()
        {
            合計次數 = 0;
            Thread.Sleep(1000);
            for (long i = 0; i < 99999999; i++)
            {
                if (i % 500 == 0)
                {
                    tbk狀態.Text = 合計次數.ToString();
                }
                合計次數++;
            }
        }
    }
}
