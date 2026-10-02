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
    public class StudentHabitRepository : IStudentHabitRepository
    {
        private readonly string _connectionString;

        public StudentHabitRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<StudentHabit>>> GetByStudentAsync(int studentId)
        {
            var items = new List<StudentHabit>();
            var response = new RepositoryResponse<List<StudentHabit>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetStudentHabitsByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapStudentHabit(reader));
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
                return new RepositoryResponse<List<StudentHabit>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<StudentHabit>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetStudentHabitById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<StudentHabit>> CreateAsync(StudentHabit habit)
            => await ExecuteSingle("USP_CreateStudentHabit", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", (object?)habit.StudentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectId", (object?)habit.SubjectId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", (object?)habit.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Frequency", (object?)habit.Frequency ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observations", (object?)habit.Observations ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<StudentHabit>> UpdateAsync(int id, StudentHabit habit)
            => await ExecuteSingle("USP_UpdateStudentHabit", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@StudentId", (object?)habit.StudentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectId", (object?)habit.SubjectId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", (object?)habit.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Frequency", (object?)habit.Frequency ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observations", (object?)habit.Observations ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteStudentHabit", connection);
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

        private async Task<RepositoryResponse<StudentHabit>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var habitReturned = new StudentHabit();
            var response = new RepositoryResponse<StudentHabit>();

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
                            habitReturned = MapStudentHabit(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = habitReturned;
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
                return new RepositoryResponse<StudentHabit> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static StudentHabit MapStudentHabit(SqlDataReader reader)
        {
            return new StudentHabit
            {
                Id = (int)reader["id"],
                StudentId = reader["studentId"] == DBNull.Value ? null : (int?)reader["studentId"],
                SubjectId = reader["subjectId"] == DBNull.Value ? null : (int?)reader["subjectId"],
                Name = reader["name"] == DBNull.Value ? null : reader["name"].ToString(),
                Frequency = reader["frequency"] == DBNull.Value ? null : reader["frequency"].ToString(),
                Observations = reader["observations"] == DBNull.Value ? null : reader["observations"].ToString(),
                CreatedAt = reader["createdAt"] == DBNull.Value ? null : (DateTime?)reader["createdAt"]
            };
        }
    }
}