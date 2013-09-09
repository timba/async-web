using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class ComputerDbInitializer : CreateDatabaseIfNotExists<ComputerDb>
    {
        protected override void Seed(ComputerDb context)
        {
            for (int i = 1; i <= 15000; i++)
            {
                context.Computers.Add(new Models.Computer
                {
                    Id = i,
                    Name = "Computer " + i,
                    StatusMessage = "Created",
                    Updated = DateTime.Now
                });
            }
        }
    }
}