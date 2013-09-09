using async_web.Data;
using async_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace async_web.Controllers
{
    public class ComputersSyncController : Controller
    {
        public ActionResult Index()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int start = r.Next(14980);
            var repo = ComputerRepositoryFactory.GetSyncRepository();
            var computers = repo.GetComputers(start, 20);
            return View(new ComputersViewModel { Computers = computers, IsAsync = false });
        }

        public ActionResult Ping(int id)
        {
            var repo = ComputerRepositoryFactory.GetSyncRepository();
            var comp = repo.GetById(id);
            if (comp == null)
                return View((IView)null);
            comp.StatusMessage = "Alive, " + Guid.NewGuid();
            comp.Updated = DateTime.Now;
            return View(repo.Save(comp));
        }
    }
}