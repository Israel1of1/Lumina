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
    public class RoutineDetailRepository : IRoutineDetailRepository
    {
        private readonly string _connectionString;

        public RoutineDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<RoutineDetail>> CreateAsync(RoutineDetail detail)
            => await ExecuteSingle("USP_CreateRoutineDetail", cmd =>
            {
                cmd.Parameters.AddWithValue("@RoutineId", detail.RoutineId);
                cmd.Parameters.AddWithValue("@TimeOfDay", (object?)detail.TimeOfDay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Activity", (object?)detail.Activity ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)detail.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DurationMinutes", (object?)detail.DurationMinutes ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<List<RoutineDetail>>> GetByRoutineAsync(int routineId)
        {
            var items = new List<RoutineDetail>();
            var response = new RepositoryResponse<List<RoutineDetail>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRoutineDetailsByRoutine", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoutineId", routineId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapDetail(reader));
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
                return new RepositoryResponse<List<RoutineDetail>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<RoutineDetail>> UpdateAsync(int id, RoutineDetail detail)
            => await ExecuteSingle("USP_UpdateRoutineDetail", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@TimeOfDay", (object?)detail.TimeOfDay ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Activity", (object?)detail.Activity ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)detail.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DurationMinutes", (object?)detail.DurationMinutes ?? DBNull.Value);
            });
        public async Task<RepositoryResponse<RoutineDetailWithStudent>> GetByIdWithStudentAsync(int id)
        {
            var itemReturned = new RoutineDetailWithStudent();
            var response = new RepositoryResponse<RoutineDetailWithStudent>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRoutineDetailById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            itemReturned = new RoutineDetailWithStudent
                            {
                                Id = (int)reader["Id"],
                                RoutineId = (int)reader["RoutineId"],
                                TimeOfDay = reader["TimeOfDay"] == DBNull.Value ? null : (TimeSpan?)reader["TimeOfDay"],
                                Activity = reader["Activity"] == DBNull.Value ? null : reader["Activity"].ToString(),
                                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                                DurationMinutes = reader["DurationMinutes"] == DBNull.Value ? null : (int?)reader["DurationMinutes"],
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
                return new RepositoryResponse<RoutineDetailWithStudent> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteRoutineDetail", connection);
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

        private async Task<RepositoryResponse<RoutineDetail>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new RoutineDetail();
            var response = new RepositoryResponse<RoutineDetail>();

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
                            itemReturned = MapDetail(reader);
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
                return new RepositoryResponse<RoutineDetail> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static RoutineDetail MapDetail(SqlDataReader reader) => new()
        {
            Id = (int)reader["Id"],
            RoutineId = (int)reader["RoutineId"],
            TimeOfDay = reader["TimeOfDay"] == DBNull.Value ? null : (TimeSpan?)reader["TimeOfDay"],
            Activity = reader["Activity"] == DBNull.Value ? null : reader["Activity"].ToString(),
            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
            DurationMinutes = reader["DurationMinutes"] == DBNull.Value ? null : (int?)reader["DurationMinutes"]
        };
    }
}
