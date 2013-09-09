using async_web.Models;
using System;

namespace async_web.Data
{
    public interface IComputerSyncRepository
    {
        Computer GetById(int id);
        Computer[] GetComputers(int start, int take);
        Computer Save(Computer computer);
    }
}
