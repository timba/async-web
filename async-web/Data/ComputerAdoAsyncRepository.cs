using async_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace async_web.Data
{
    public class ComputerAdoAsyncRepository : BaseComputerAdoRepository, IComputerAsyncRepository
    {
        public async Task<Computer[]> GetComputersAsync(int start, int take)
        {
            using (var connection = GetConnection())
            {
                var command = base.CreateComputersCommand(connection,start,take);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                List<Computer> computers = new List<Computer>();
                while (await reader.ReadAsync())
                {
                    Computer comp = ReadComputer(reader);
                    computers.Add(comp);
                }

                return computers.ToArray();
            }
        }

        public async Task<Computer> GetByIdAsync(int id)
        {
            using (var connection = GetConnection())
            {
                var command = CreateGetByIdCommand(connection, id);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                List<Computer> computers = new List<Computer>();
                if (await reader.ReadAsync())
                {
                    Computer comp = ReadComputer(reader);
                    return comp;
                }

                return null;
            }
        }

        public async Task<Computer> SaveAsync(Computer computer)
        {
            using (var con = GetConnection())
            {
                var com = CreateSaveCommand(con, computer);
                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
                return computer;
            }
        }
    }
}