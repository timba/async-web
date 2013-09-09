using async_web.Data;
using async_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace async_web.Controllers
{
    public class ComputersAsyncController : AsyncController
    {
        public static int CallsCount = 0;

        public async Task<ActionResult> IndexAsync()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int start = r.Next(14980);
            var repo = ComputerRepositoryFactory.GetAsyncRepository();
            var computers = await repo.GetComputersAsync(start, 20);
            return View(new ComputersViewModel { Computers = computers, IsAsync = true });
        }

        public async Task<ActionResult> Ping(int id)
        {
            var repo = ComputerRepositoryFactory.GetAsyncRepository();

            var comp = await repo.GetByIdAsync(id);
            if (comp == null)
                return View((IView)null);
            comp.StatusMessage = "Alive, " + Guid.NewGuid();
            comp.Updated = DateTime.Now;
            comp = await repo.SaveAsync(comp);
            Interlocked.Increment(ref CallsCount);
            return View(comp);

        }
    }
}
