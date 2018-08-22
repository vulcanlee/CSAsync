using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace 執行緒的同步處理內容ASPNET.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // 是否要回到原先執行緒的同步內文中
            // https://msdn.microsoft.com/zh-tw/library/system.threading.tasks.task.configureawait(v=vs.110).aspx
            bool continueOnCapturedContext = true;
            Response.Write($"{Log("進行 System.Web.HttpContext.Current 多執行緒的同步內文測試")}<br/><hr/><br/>");
            try
            {
                Response.Write($"{Log("等候 非同步工作 完成")}<br/><hr/><br/>");

                // 使用我們指定的條件，進行呼叫非同步方法
                await Task.Run(async () =>
                {
                    Response.Write($"{Log($"正在非同步工作中 / Sleep 方法將會被呼叫")}<br/>");
                    // 要休息 1000 毫秒數
                    System.Threading.Thread.Sleep(1000);
                    Response.Write($"{Log($"準備結束非同步工作 / 是否要回到原先執行緒的同步內文中 <strong>{continueOnCapturedContext.ToString()}</strong>")}<br/>");
                }).ConfigureAwait(continueOnCapturedContext);

                Response.Write($"{Log("非同步工作 已經完成")}<br/><hr/>");
            }
            catch (Exception e)
            {
                Response.Write($"{Log($"Error {e.Message}")}<br/>");
            }

            return new HttpStatusCodeResult(200);
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