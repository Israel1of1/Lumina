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
    public class RoutineLogRepository : IRoutineLogRepository
    {
        private readonly string _connectionString;

        public RoutineLogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<RoutineLog>> CreateAsync(RoutineLog log)
            => await ExecuteSingle("USP_CreateRoutineLog", cmd =>
            {
                cmd.Parameters.AddWithValue("@RoutineDetailId", log.RoutineDetailId);
                cmd.Parameters.AddWithValue("@StudentId", log.StudentId);
                cmd.Parameters.AddWithValue("@Status", (object?)log.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observation", (object?)log.Observation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LogDate", (object?)log.LogDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RegisteredById", (object?)log.RegisteredById ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<List<RoutineLog>>> GetByStudentAsync(int studentId, DateTime? fromDate, DateTime? toDate)
            => await ExecuteList("USP_GetRoutineLogsByStudent", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@FromDate", (object?)fromDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", (object?)toDate ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<List<RoutineLog>>> GetByDetailAsync(int routineDetailId)
            => await ExecuteList("USP_GetRoutineLogsByDetail", cmd => cmd.Parameters.AddWithValue("@RoutineDetailId", routineDetailId));

        public async Task<RepositoryResponse<RoutineLog>> UpdateAsync(int id, RoutineLog log)
            => await ExecuteSingle("USP_UpdateRoutineLog", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", (object?)log.Status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observation", (object?)log.Observation ?? DBNull.Value);
            });
        public async Task<RepositoryResponse<RoutineLog>> GetByIdAsync(int id)
             => await ExecuteSingle("USP_GetRoutineLogById", cmd => cmd.Parameters.AddWithValue("@Id", id));


        private async Task<RepositoryResponse<List<RoutineLog>>> ExecuteList(string spName, Action<SqlCommand> bindParams)
        {
            var items = new List<RoutineLog>();
            var response = new RepositoryResponse<List<RoutineLog>>();

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
                        while (await reader.ReadAsync())
                            items.Add(MapLog(reader));
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
                return new RepositoryResponse<List<RoutineLog>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private async Task<RepositoryResponse<RoutineLog>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new RoutineLog();
            var response = new RepositoryResponse<RoutineLog>();

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
                            itemReturned = MapLog(reader);
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
                return new RepositoryResponse<RoutineLog> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static RoutineLog MapLog(SqlDataReader reader) => new()
        {
            Id = (int)reader["Id"],
            RoutineDetailId = (int)reader["RoutineDetailId"],
            StudentId = (int)reader["StudentId"],
            Status = reader["Status"] == DBNull.Value ? null : reader["Status"].ToString(),
            Observation = reader["Observation"] == DBNull.Value ? null : reader["Observation"].ToString(),
            LogDate = reader["LogDate"] == DBNull.Value ? null : (DateTime?)reader["LogDate"],
            RegisteredById = reader["RegisteredById"] == DBNull.Value ? null : (int?)reader["RegisteredById"]
        };
    }
}
