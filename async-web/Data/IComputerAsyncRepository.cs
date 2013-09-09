using async_web.Models;
using System;
using System.Threading.Tasks;

namespace async_web.Data
{
    public interface IComputerAsyncRepository
    {
        Task<Computer> GetByIdAsync(int id);
        Task<Computer[]> GetComputersAsync(int start, int take);
        Task<Computer> SaveAsync(Computer computer);
    }
}
