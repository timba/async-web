using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace async_web.Models
{
    public class ComputersViewModel
    {
        public IEnumerable<Computer> Computers { get; set; }
        public bool IsAsync { get; set; }
    }
}