using async_web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace async_web.Data
{
    public class BaseComputerAdoRepository
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ComputerDb"].ConnectionString);
        }

        protected SqlCommand CreateComputersCommand(SqlConnection connection, int start, int take)
        {
            var command = connection.CreateCommand();
            command.CommandText =
            @"with comps as 
            (select *, row_number() over (order by id) as rownum from Computers c)
            select id,name,statusmessage,updated from comps
            where rownum between @start and @end";
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@start", start));
            command.Parameters.Add(new SqlParameter("@end", start + take));
            return command;
        }

        protected SqlCommand CreateGetByIdCommand(SqlConnection connection, int id)
        {
            var command = connection.CreateCommand();
            command.CommandText =
            @"select * from Computers c where id = @id";
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@id", id));
            return command;
        }

        protected SqlCommand CreateSaveCommand(SqlConnection connection, Computer computer)
        {
            var command = connection.CreateCommand();
            command.CommandText =
            @"update Computers 
                set StatusMessage = @msg,
                Updated = @upd
                where Id = @id";
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@id", computer.Id));
            command.Parameters.Add(new SqlParameter("@msg", computer.StatusMessage));
            command.Parameters.Add(new SqlParameter("@upd", computer.Updated));
            return command;
        }

        protected static Computer ReadComputer(SqlDataReader reader)
        {
            Computer comp = new Computer
            {
                Id = (int)reader["id"],
                Name = (string)reader["name"],
                StatusMessage = (string)reader["StatusMessage"],
                Updated = (DateTime)reader["Updated"]
            };

            return comp;
        }

    }
}