using async_web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class ComputerEFSyncRepository : BaseComputerEFRepository, IComputerSyncRepository
    {
        public Computer GetById(int id)
        {
            using (var db = CreateContext())
            {
                return db.Computers.FirstOrDefault(c => c.Id == id);
            }
        }

        public Computer[] GetComputers(int start, int take)
        {
            using (var db = CreateContext())
            {
                return db.Computers.OrderBy(c => c.Id).Skip(start).Take(take).ToArray();
            }
        }

        public Computer Save(Computer computer)
        {
            using (var db = CreateContext())
            {
                db.Computers.Attach(computer);
                var entry = db.Entry(computer);
                entry.Property(e => e.StatusMessage).IsModified = true;
                entry.Property(e => e.Updated).IsModified = true;
                db.Entry(computer).State = EntityState.Modified;
                db.SaveChanges();
                return computer;
            }
        }
    }
}