using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<IEnumerable<Role>>> GetAllAsync()
        {
            var roles = new List<Role>();
            var response = new RepositoryResponse<IEnumerable<Role>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllRole", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new Role
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()!,
                                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                                IsActive = (bool)reader["IsActive"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)reader["UpdatedAt"] : null
                            });
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = roles;
                    response.OperationStatusCode = returnedValue;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<RepositoryResponse<Role>> GetByIdAsync(int id)
        {
            var roleReturned = new Role();
            var response = new RepositoryResponse<Role>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRoleById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            roleReturned.Id = (int)reader["Id"];
                            roleReturned.Name = reader["Name"].ToString()!;
                            roleReturned.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null;
                            roleReturned.IsActive = (bool)reader["IsActive"];
                            roleReturned.CreatedAt = (DateTime)reader["CreatedAt"];
                            roleReturned.UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)reader["UpdatedAt"] : null;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = roleReturned;
                    response.OperationStatusCode = returnedValue;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<RepositoryResponse<Role>> AddAsync(Role role)
        {
            var roleReturned = new Role();
            var response = new RepositoryResponse<Role>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_InsertRole", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", role.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)role.Description ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            roleReturned.Id = (int)reader["Id"];
                            roleReturned.Name = reader["Name"].ToString()!;
                            roleReturned.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null;
                            roleReturned.IsActive = (bool)reader["IsActive"];
                            roleReturned.CreatedAt = (DateTime)reader["CreatedAt"];
                            roleReturned.UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)reader["UpdatedAt"] : null;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = roleReturned;
                    response.OperationStatusCode = returnedValue;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<RepositoryResponse<Role>> UpdateAsync(int id, Role role)
        {
            var roleUpdated = new Role();
            var response = new RepositoryResponse<Role>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_UpdateRole", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", role.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)role.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", role.IsActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            roleUpdated.Id = (int)reader["Id"];
                            roleUpdated.Name = reader["Name"].ToString()!;
                            roleUpdated.Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null;
                            roleUpdated.IsActive = (bool)reader["IsActive"];
                            roleUpdated.CreatedAt = (DateTime)reader["CreatedAt"];
                            roleUpdated.UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? (DateTime?)reader["UpdatedAt"] : null;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = roleUpdated;
                    response.OperationStatusCode = returnedValue;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}