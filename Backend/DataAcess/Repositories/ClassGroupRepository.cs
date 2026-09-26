using Core.Common;
using Core.Entities;
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
    public class ClassGroupRepository : IClassGroupRepository
    {
        private readonly string _connectionString;

        public ClassGroupRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<(List<ClassGroup> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, bool? isActive)
        {
            var items = new List<ClassGroup>();
            var response = new RepositoryResponse<(List<ClassGroup>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllClassGroups", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@IsActive", (object?)isActive ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapClassGroup(reader));

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                            totalRecords = (int)reader["TotalRecords"];
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = (items, totalRecords);
                    response.OperationStatusCode = returnedValue;
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.Data = (items, 0);
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<(List<ClassGroup>, int)> { Data = (items, 0), OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<ClassGroup>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetClassGroupById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<ClassGroup>> CreateAsync(ClassGroup group)
            => await ExecuteSingle("USP_CreateClassGroup", cmd =>
            {
                cmd.Parameters.AddWithValue("@Name", group.Name);
                cmd.Parameters.AddWithValue("@GradeLevel", (object?)group.GradeLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)group.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<ClassGroup>> UpdateAsync(int id, ClassGroup group)
            => await ExecuteSingle("USP_UpdateClassGroup", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", group.Name);
                cmd.Parameters.AddWithValue("@GradeLevel", (object?)group.GradeLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)group.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<ClassGroup>> SetActiveAsync(int id, bool isActive)
            => await ExecuteSingle("USP_SetClassGroupActive", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
            });

        private async Task<RepositoryResponse<ClassGroup>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var groupReturned = new ClassGroup();
            var response = new RepositoryResponse<ClassGroup>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand(spName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    bindParams(cmd);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            groupReturned = MapClassGroup(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = groupReturned;
                    response.OperationStatusCode = returnedValue;
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<ClassGroup> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static ClassGroup MapClassGroup(SqlDataReader reader)
        {
            return new ClassGroup
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString()!,
                GradeLevel = reader["GradeLevel"] == DBNull.Value ? null : reader["GradeLevel"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                IsActive = (bool)reader["IsActive"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                StudentCount = (int)reader["StudentCount"]
            };
        }
    }
}
