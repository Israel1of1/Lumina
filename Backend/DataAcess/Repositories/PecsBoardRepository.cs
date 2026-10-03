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
    public class PecsBoardRepository : IPecsBoardRepository
    {

        private readonly string _connectionString;

        public PecsBoardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<PecsBoard>> CreateAsync(PecsBoard board)
            => await ExecuteSingle("USP_CreatePecsBoard", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", board.StudentId);
                cmd.Parameters.AddWithValue("@Name", (object?)board.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)board.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<List<PecsBoard>>> GetByStudentAsync(int studentId)
        {
            var items = new List<PecsBoard>();
            var response = new RepositoryResponse<List<PecsBoard>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetPecsBoardsByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapBoard(reader));
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = items;
                    response.OperationStatusCode = returnedValue;
                    return response;
                }
            }
            catch (SqlException ex)
            {
                response.Data = items;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
                return response;
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<List<PecsBoard>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<PecsBoard>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetPecsBoardById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<PecsBoard>> UpdateAsync(int id, PecsBoard board)
            => await ExecuteSingle("USP_UpdatePecsBoard", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", (object?)board.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)board.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeletePecsBoard", connection);
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

        private async Task<RepositoryResponse<PecsBoard>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new PecsBoard();
            var response = new RepositoryResponse<PecsBoard>();

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
                            itemReturned = MapBoard(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = itemReturned;
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
                return new RepositoryResponse<PecsBoard> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static PecsBoard MapBoard(SqlDataReader reader) => new()
        {
            Id = (int)reader["Id"],
            StudentId = (int)reader["StudentId"],
            Name = reader["Name"] == DBNull.Value ? null : reader["Name"].ToString(),
            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
            CreatedAt = (DateTime)reader["CreatedAt"]
        };
    }
}

