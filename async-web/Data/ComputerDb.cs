using async_web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class ComputerDb : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
    }
}