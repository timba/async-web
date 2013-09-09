using async_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace async_web.Data
{
    public class ComputerAdoSyncRepository : BaseComputerAdoRepository, IComputerSyncRepository
    {
        public Computer[] GetComputers(int start, int take)
        {
            using (var con = GetConnection())
            {
                var command = CreateComputersCommand(con, start, take);
                con.Open();
                var reader = command.ExecuteReader();
                List<Computer> computers = new List<Computer>();
                while (reader.Read())
                {
                    Computer comp = ReadComputer(reader);
                    computers.Add(comp);
                }

                return computers.ToArray();
            }
        }

        public Computer GetById(int id)
        {
            using (var connection = GetConnection())
            {
                var command = base.CreateGetByIdCommand(connection, id);
                connection.Open();
                var reader = command.ExecuteReader();
                List<Computer> computers = new List<Computer>();
                if (reader.Read())
                {
                    Computer computer = ReadComputer(reader);
                    return computer;
                }

                return null;
            }
        }

        public Computer Save(Computer computer)
        {
            using (var connection = GetConnection())
            {
                var com = CreateSaveCommand(connection, computer);
                connection.Open();
                com.ExecuteNonQuery();
                return computer;
            }
        }
    }
}