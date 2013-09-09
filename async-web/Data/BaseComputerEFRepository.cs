using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class BaseComputerEFRepository
    {
        protected ComputerDb CreateContext()
        {
            return new ComputerDb();
        }
    }
}