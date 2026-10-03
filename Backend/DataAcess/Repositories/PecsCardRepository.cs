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
        public class PecsCardRepository : IPecsCardRepository
        {
            private readonly string _connectionString;

            public PecsCardRepository(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection")!;
            }

            public async Task<RepositoryResponse<PecsCard>> CreateAsync(PecsCard card)
                => await ExecuteSingle("USP_CreatePecsCard", cmd =>
                {
                    cmd.Parameters.AddWithValue("@BoardId", card.BoardId);
                    cmd.Parameters.AddWithValue("@Title", (object?)card.Title ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImageUrl", (object?)card.ImageUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AudioUrl", (object?)card.AudioUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", (object?)card.Category ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrderNumber", (object?)card.OrderNumber ?? DBNull.Value);
                });

            public async Task<RepositoryResponse<List<PecsCard>>> GetByBoardAsync(int boardId)
            {
                var items = new List<PecsCard>();
                var response = new RepositoryResponse<List<PecsCard>>();

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();

                        SqlCommand cmd = new SqlCommand("USP_GetPecsCardsByBoard", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BoardId", boardId);
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                                items.Add(MapCard(reader));
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
                    return new RepositoryResponse<List<PecsCard>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
                }
            }

            public async Task<RepositoryResponse<PecsCardWithStudent>> GetByIdWithStudentAsync(int id)
            {
                var itemReturned = new PecsCardWithStudent();
                var response = new RepositoryResponse<PecsCardWithStudent>();

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();

                        SqlCommand cmd = new SqlCommand("USP_GetPecsCardByIdWithStudent", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                itemReturned = new PecsCardWithStudent
                                {
                                    Id = (int)reader["Id"],
                                    BoardId = (int)reader["BoardId"],
                                    Title = reader["Title"] == DBNull.Value ? null : reader["Title"].ToString(),
                                    ImageUrl = reader["ImageUrl"] == DBNull.Value ? null : reader["ImageUrl"].ToString(),
                                    AudioUrl = reader["AudioUrl"] == DBNull.Value ? null : reader["AudioUrl"].ToString(),
                                    Category = reader["Category"] == DBNull.Value ? null : reader["Category"].ToString(),
                                    OrderNumber = reader["OrderNumber"] == DBNull.Value ? null : (int?)reader["OrderNumber"],
                                    CreatedAt = (DateTime)reader["CreatedAt"],
                                    StudentId = (int)reader["StudentId"]
                                };
                            }
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
                    return new RepositoryResponse<PecsCardWithStudent> { Data = null, OperationStatusCode = -1, Message = ex.Message };
                }
            }

            public async Task<RepositoryResponse<PecsCard>> UpdateAsync(int id, PecsCard card)
                => await ExecuteSingle("USP_UpdatePecsCard", cmd =>
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", (object?)card.Title ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ImageUrl", (object?)card.ImageUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AudioUrl", (object?)card.AudioUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", (object?)card.Category ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OrderNumber", (object?)card.OrderNumber ?? DBNull.Value);
                });

            public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
            {
                var response = new RepositoryResponse<bool>();

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();

                        SqlCommand cmd = new SqlCommand("USP_DeletePecsCard", connection);
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

            private async Task<RepositoryResponse<PecsCard>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
            {
                var itemReturned = new PecsCard();
                var response = new RepositoryResponse<PecsCard>();

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
                                itemReturned = MapCard(reader);
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
                    return new RepositoryResponse<PecsCard> { Data = null, OperationStatusCode = -1, Message = ex.Message };
                }
            }

            private static PecsCard MapCard(SqlDataReader reader) => new()
            {
                Id = (int)reader["Id"],
                BoardId = (int)reader["BoardId"],
                Title = reader["Title"] == DBNull.Value ? null : reader["Title"].ToString(),
                ImageUrl = reader["ImageUrl"] == DBNull.Value ? null : reader["ImageUrl"].ToString(),
                AudioUrl = reader["AudioUrl"] == DBNull.Value ? null : reader["AudioUrl"].ToString(),
                Category = reader["Category"] == DBNull.Value ? null : reader["Category"].ToString(),
                OrderNumber = reader["OrderNumber"] == DBNull.Value ? null : (int?)reader["OrderNumber"],
                CreatedAt = (DateTime)reader["CreatedAt"]
            };
        }
    
}
