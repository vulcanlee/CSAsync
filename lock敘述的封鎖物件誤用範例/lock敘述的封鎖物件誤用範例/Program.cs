using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lock敘述的封鎖物件誤用範例
{
    /// <summary>
    /// 在這個範例中，MyClass 的 AddNumber 方法，當要進行多執行緒下的數值相加作業時候，需要使用 lock 來做到執行緒同步處理
    /// 由於採用 lock(this) 敘述，將會造成程式除錯上的盲點，因為，只要有參考到這個執行個體的變數，
    /// 都可以拿來做同步封鎖物件，進而影響到該類別內的原有封鎖作業，間接會有死結的問題
    /// </summary>
    public class MyClass
    {
        int counter = 0;
        private object locker = new object();

        public void AddNumber(int cc)
        {
            Console.WriteLine("執行 lock(this) 敘述之前 " + Thread.CurrentThread.ManagedThreadId);
            // 不建議使用 lock(this) 這個方法，而要採用私有 object 物件
            lock (locker)
            {
                Console.WriteLine("正在 lock(this) 敘述之內 " + +Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("在 lock(this) 敘述內需要執行5秒的工作 " + +Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                counter += cc;
                Console.WriteLine(counter);
            }
            Console.WriteLine("執行 lock(this) 敘述完成 " + +Thread.CurrentThread.ManagedThreadId);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass fooMyClass = new MyClass();

            // 產生兩個背景執行緒，進行數值相加作業
            ThreadPool.QueueUserWorkItem((x) =>
            {
                fooMyClass.AddNumber(10);
            });
            ThreadPool.QueueUserWorkItem((x) =>
            {
                fooMyClass.AddNumber(20);
            });

            // 使用 fooMyClass 這個變數作業同步封鎖的物件來源，只要這裡一封鎖，背景執行緒就無法進入到要封鎖的程式碼區段內
            Console.WriteLine("執行 lock(fooMyClass) 敘述之前 " + Thread.CurrentThread.ManagedThreadId);
            lock (fooMyClass)
            {
                Console.WriteLine("正在 lock(fooMyClass) 敘述之內 " + +Thread.CurrentThread.ManagedThreadId);
                fooMyClass.AddNumber(33);
                Console.WriteLine("在 lock(fooMyClass) 敘述內需要執行3秒的工作 " + +Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(3000);
            }
            Console.WriteLine("執行 lock(fooMyClass) 敘述完成 " + +Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
