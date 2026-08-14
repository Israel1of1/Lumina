using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly string _connectionString;

        public UserRoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<IEnumerable<Role>>> GetRolesByUserIdAsync(int userId)
        {
            var roles = new List<Role>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRolesByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new Role
                            {
                                Id = (int)reader["id"],
                                Name = reader["name"].ToString()!,
                                Description = reader["description"] as string,
                                IsActive = (bool)reader["isActive"]
                            });
                        }
                    }

                    return new RepositoryResponse<IEnumerable<Role>>
                    {
                        Data = roles,
                        OperationStatusCode = 0
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<IEnumerable<Role>>
                {
                    Data = null,
                    OperationStatusCode = ex.Number,
                    Message = ex.Message
                };
            }
        }
    }
}