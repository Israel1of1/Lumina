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
    public class KeywordRepository : IKeywordRepository
    {
        private readonly string _connectionString;

        public KeywordRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<(List<Keyword> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            var items = new List<Keyword>();
            var response = new RepositoryResponse<(List<Keyword>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllKeywords", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@Search", (object?)search ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapKeyword(reader));

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
                return new RepositoryResponse<(List<Keyword>, int)> { Data = (items, 0), OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Keyword>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetKeywordById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<Keyword>> CreateAsync(string name)
            => await ExecuteSingle("USP_CreateKeyword", cmd => cmd.Parameters.AddWithValue("@Name", name));

        public async Task<RepositoryResponse<Keyword>> UpdateAsync(int id, string name)
            => await ExecuteSingle("USP_UpdateKeyword", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteKeyword", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    await cmd.ExecuteNonQueryAsync();

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = returnedValue == 0;
                    response.OperationStatusCode = returnedValue;
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.Data = false;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<bool> { Data = false, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private async Task<RepositoryResponse<Keyword>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var keywordReturned = new Keyword();
            var response = new RepositoryResponse<Keyword>();

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
                            keywordReturned = MapKeyword(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = keywordReturned;
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
                return new RepositoryResponse<Keyword> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static Keyword MapKeyword(SqlDataReader reader)
        {
            return new Keyword
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString()!,
                CreatedAt = (DateTime)reader["CreatedAt"],
                UsageCount = (int)reader["UsageCount"]
            };
        }
    }
}
