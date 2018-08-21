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

namespace 執行緒的同步處理內容WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnRunAsyncWithoutSynchronizationContext_Click(object sender, RoutedEventArgs e)
        {
            SynchronizationContext BeforeSynchronizationContext = SynchronizationContext.Current;
            int BeforeThreadID = Thread.CurrentThread.ManagedThreadId;
            var foo = await new System.Net.Http.HttpClient().GetStringAsync("http://www.google.com").ConfigureAwait(false);
            SynchronizationContext AfterSynchronizationContext = SynchronizationContext.Current;
            int AfterThreadID = Thread.CurrentThread.ManagedThreadId;
            tbkResult.Text = foo;
        }

        private async void btnRunAsyncWithSynchronizationContext_Click(object sender, RoutedEventArgs e)
        {
            SynchronizationContext BeforeSynchronizationContext = SynchronizationContext.Current;
            int BeforeThreadID = Thread.CurrentThread.ManagedThreadId;
            var foo = await new System.Net.Http.HttpClient().GetStringAsync("http://www.google.com");
            SynchronizationContext AfterSynchronizationContext = SynchronizationContext.Current;
            int AfterThreadID = Thread.CurrentThread.ManagedThreadId;
            tbkResult.Text = foo;
        }
    }
}
