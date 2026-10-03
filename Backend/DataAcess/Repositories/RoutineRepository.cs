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
    public class RoutineRepository : IRoutineRepository
    {
        private readonly string _connectionString;

        public RoutineRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<Routine>> CreateAsync(Routine routine)
            => await ExecuteSingle("USP_CreateRoutine", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", routine.StudentId);
                cmd.Parameters.AddWithValue("@Name", (object?)routine.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)routine.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StartDate", (object?)routine.StartDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EndDate", (object?)routine.EndDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object?)routine.Status ?? "ACTIVE");
                cmd.Parameters.AddWithValue("@CreatedByUserId", (object?)routine.CreatedByUserId ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<List<Routine>>> GetByStudentAsync(int studentId, string? status)
        {
            var items = new List<Routine>();
            var response = new RepositoryResponse<List<Routine>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRoutinesByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapRoutine(reader));
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
                return new RepositoryResponse<List<Routine>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Routine>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetRoutineById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<Routine>> UpdateAsync(int id, Routine routine)
            => await ExecuteSingle("USP_UpdateRoutine", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", (object?)routine.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)routine.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StartDate", (object?)routine.StartDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EndDate", (object?)routine.EndDate ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<Routine>> SetStatusAsync(int id, string status)
            => await ExecuteSingle("USP_SetRoutineStatus", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", status);
            });

        private async Task<RepositoryResponse<Routine>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new Routine();
            var response = new RepositoryResponse<Routine>();

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
                            itemReturned = MapRoutine(reader);
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
                return new RepositoryResponse<Routine> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static Routine MapRoutine(SqlDataReader reader) => new()
        {
            Id = (int)reader["Id"],
            StudentId = (int)reader["StudentId"],
            Name = reader["Name"] == DBNull.Value ? null : reader["Name"].ToString(),
            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
            StartDate = reader["StartDate"] == DBNull.Value ? null : (DateTime?)reader["StartDate"],
            EndDate = reader["EndDate"] == DBNull.Value ? null : (DateTime?)reader["EndDate"],
            Status = reader["Status"] == DBNull.Value ? null : reader["Status"].ToString(),
            CreatedByUserId = reader["CreatedByUserId"] == DBNull.Value ? null : (int?)reader["CreatedByUserId"],
            CreatedAt = (DateTime)reader["CreatedAt"]
        };
    }
}
