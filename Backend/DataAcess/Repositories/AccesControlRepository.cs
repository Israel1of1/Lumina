using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AccessControlRepository : IAccessControlRepository
    {
        private readonly string _connectionString;

        public AccessControlRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<bool> HasStudentAccessAsync(int userId, int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand cmd = new SqlCommand("USP_CheckStudentAccess", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                var result = await cmd.ExecuteScalarAsync();
                return result != null && (bool)result;
            }
        }
    }
}
