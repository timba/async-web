using async_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;


namespace async_web.Data
{
    public class ComputerEFAsyncRepository : BaseComputerEFRepository, IComputerAsyncRepository
    {
        public async Task<Computer> GetByIdAsync(int id)
        {
            using (var db = CreateContext())
            {
                return await db.Computers.FirstOrDefaultAsync(c => c.Id == id);
            }
        }

        public async Task<Computer[]> GetComputersAsync(int start, int take)
        {
            using (var db = CreateContext())
            {
                return await db.Computers.OrderBy(c => c.Id).Skip(start).Take(take).ToArrayAsync();
            }
        }

        public async Task<Computer> SaveAsync(Computer computer)
        {
            using (var db = CreateContext())
            {
                db.Computers.Attach(computer);
                var entry = db.Entry(computer);
                entry.Property(e => e.StatusMessage).IsModified = true;
                entry.Property(e => e.Updated).IsModified = true;
                db.Entry(computer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return computer;
            }
        }
    }
}