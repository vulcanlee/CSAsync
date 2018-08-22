using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HttpContext的各種非同步工作使用情境.Controllers
{
    public class HomeController : Controller
    {
        // 要休息時間毫秒數
        int SLEEPINTERVALMS = 1000;
        public async Task<ActionResult> Index()
        {
            // 是否需要休息一段時間
            bool needSleep = false;
            // 用來等候這個 Task 的 awaiter
            // https://msdn.microsoft.com/zh-tw/library/system.threading.tasks.task.configureawait(v=vs.110).aspx
            bool continueOnCapturedContext = true;
            // 接續工作的排程時間及其行為方式的適用選項
            // https://msdn.microsoft.com/zh-tw/library/system.threading.tasks.taskcontinuationoptions(v=vs.110).aspx
            bool taskContinuationOptions = true;
            // 要與接續工作產生關聯且於執行時使用的 TaskScheduler
            // https://msdn.microsoft.com/zh-tw/library/system.threading.tasks.taskscheduler(v=vs.110).aspx
            bool taskScheduler = false;
            Response.Write($"{Log("測試動作")}<br/><hr/><br/>");
            var currentContext = System.Threading.SynchronizationContext.Current;
            try
            {
                // 使用我們指定的條件，進行呼叫非同步方法
                await AsyncTaskMethod(needSleep, continueOnCapturedContext)
                    .ConfigureAwait(continueOnCapturedContext);

                Response.Write($"{Log("等候 非同步方法 AsyncTaskMethod 完成")}<br/><hr/><br/>");

                // 使用我們指定的條件，進行呼叫新的非同步方法(原有的非同步方法，將會在此新非同步方法內呼叫)
                //await Task.Factory.StartNew(
                //  async () => await NewTask(needSleep, continueOnCapturedContext)
                //      .ConfigureAwait(continueOnCapturedContext),
                //  System.Threading.CancellationToken.None,
                //  taskContinuationOptions ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None,
                //  taskScheduler ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current)
                //  .Unwrap().ConfigureAwait(continueOnCapturedContext);
                await Task.Factory.StartNew(
                    async () => await AsyncTaskMethod(needSleep, continueOnCapturedContext)
                        .ConfigureAwait(continueOnCapturedContext),
                    System.Threading.CancellationToken.None,
                    taskContinuationOptions ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None,
                    taskScheduler ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current)
                    .Unwrap().ConfigureAwait(continueOnCapturedContext);

                Response.Write($"{Log("等候 新的非同步工作 已經完成")}<br/><hr/>");
            }
            catch (Exception e)
            {
                Response.Write($"{Log($"Error {e.Message}")}<br/>");
            }

            return new HttpStatusCodeResult(200);
        }

        Task NewTask(bool needSleep, bool continueOnCapturedContext)
        {
            Response.Write($"{Log("執行 新的非同步工作 前執行緒的狀態")}<br/><hr/>");
            // 使用我們指定的條件，進行呼叫新的非同步方法(原有的非同步方法，將會在此新非同步方法內呼叫)
            return AsyncTaskMethod(needSleep, continueOnCapturedContext);
        }
        string Log(string msg)
        {
            var synchronizationContext = System.Threading.SynchronizationContext.Current;
            var taskScheduler = TaskScheduler.Current;
            var httpContextCurrent = System.Web.HttpContext.Current;
            return $"{msg} <ul><li>" +
                $"執行緒 Thread → {System.Threading.Thread.CurrentThread.ManagedThreadId} </li><li>" +
                $"工作排程 Task scheduler → {taskScheduler} </li><li>" +
                $"同步內文 SynchronizationContext → {synchronizationContext} </li><li>" +
                $"Http內文 Http Context → {httpContextCurrent} </li></ul>";
        }

        async Task AsyncTaskMethod(bool needSleep = true, bool continueOnCapturedContext = true)
        {
            if (needSleep)
            {
                Response.Write($"{Log($"非同步方法 AsyncTaskMethod 正在執行 / 用來等候這個 Task 的 awaiter 是 <strong>{continueOnCapturedContext.ToString()}</strong>. Sleep 方法將會被呼叫")}<br/>");
                await Task.Run(async () => System.Threading.Thread.Sleep(SLEEPINTERVALMS)).ConfigureAwait(continueOnCapturedContext);
            }
            else
            {
                Response.Write($"{Log($"非同步方法 AsyncTaskMethod 正在執行 / 用來等候這個 Task 的 awaiter 是  <strong>{continueOnCapturedContext.ToString()}</strong>. GetAsync 方法將會被呼叫")}<br/>");
                await new System.Net.Http.HttpClient().GetAsync("http://www.google.com").ConfigureAwait(continueOnCapturedContext);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}