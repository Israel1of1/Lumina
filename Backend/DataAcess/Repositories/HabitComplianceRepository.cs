using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class HabitComplianceRepository : IHabitComplianceRepository
    {
        private readonly string _connectionString;

        public HabitComplianceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<HabitCompliance>>> GetByHabitAsync(int habitId)
        {
            var items = new List<HabitCompliance>();
            var response = new RepositoryResponse<List<HabitCompliance>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetHabitCompliancesByHabit", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HabitId", habitId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapHabitCompliance(reader));
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
                return new RepositoryResponse<List<HabitCompliance>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<HabitCompliance>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetHabitComplianceById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<HabitCompliance>> CreateAsync(HabitCompliance compliance)
            => await ExecuteSingle("USP_CreateHabitCompliance", cmd =>
            {
                cmd.Parameters.AddWithValue("@HabitId", compliance.HabitId);
                cmd.Parameters.AddWithValue("@ComplianceDate", compliance.ComplianceDate);
                cmd.Parameters.AddWithValue("@IsFulfilled", compliance.IsFulfilled);
                cmd.Parameters.AddWithValue("@Observation", (object?)compliance.Observation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RegisteredById", (object?)compliance.RegisteredById ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<HabitCompliance>> UpdateAsync(int id, HabitCompliance compliance)
            => await ExecuteSingle("USP_UpdateHabitCompliance", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@HabitId", compliance.HabitId);
                cmd.Parameters.AddWithValue("@ComplianceDate", compliance.ComplianceDate);
                cmd.Parameters.AddWithValue("@IsFulfilled", compliance.IsFulfilled);
                cmd.Parameters.AddWithValue("@Observation", (object?)compliance.Observation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RegisteredById", (object?)compliance.RegisteredById ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteHabitCompliance", connection);
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

        private async Task<RepositoryResponse<HabitCompliance>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var complianceReturned = new HabitCompliance();
            var response = new RepositoryResponse<HabitCompliance>();

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
                            complianceReturned = MapHabitCompliance(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = complianceReturned;
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
                return new RepositoryResponse<HabitCompliance> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static HabitCompliance MapHabitCompliance(SqlDataReader reader)
        {
            return new HabitCompliance
            {
                Id = (int)reader["id"],
                HabitId = (int)reader["habitId"],
                ComplianceDate = (DateTime)reader["complianceDate"],
                IsFulfilled = (bool)reader["isFulfilled"],
                Observation = reader["observation"] == DBNull.Value ? null : reader["observation"].ToString(),
                RegisteredById = reader["registeredById"] == DBNull.Value ? null : (int?)reader["registeredById"],
                CreatedAt = (DateTime)reader["createdAt"]
            };
        }
    }
}