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
    public class StudentProgressRepository : IStudentProgressRepository
    {
        private readonly string _connectionString;

        public StudentProgressRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<StudentProgress>>> GetByStudentAsync(int studentId)
        {
            var items = new List<StudentProgress>();
            var response = new RepositoryResponse<List<StudentProgress>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetStudentProgressesByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapStudentProgress(reader));
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
                return new RepositoryResponse<List<StudentProgress>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<StudentProgress>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetStudentProgressById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<StudentProgress>> CreateAsync(StudentProgress progress)
            => await ExecuteSingle("USP_CreateStudentProgress", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", progress.StudentId);
                cmd.Parameters.AddWithValue("@CompletionPercentage", (object?)progress.CompletionPercentage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CurrentLevel", (object?)progress.CurrentLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Strengths", (object?)progress.Strengths ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Weaknesses", (object?)progress.Weaknesses ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Recommendation", (object?)progress.Recommendation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalStudyTime", (object?)progress.TotalStudyTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LastSessionAt", (object?)progress.LastSessionAt ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<StudentProgress>> UpdateAsync(int id, StudentProgress progress)
            => await ExecuteSingle("USP_UpdateStudentProgress", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@CompletionPercentage", (object?)progress.CompletionPercentage ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CurrentLevel", (object?)progress.CurrentLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Strengths", (object?)progress.Strengths ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Weaknesses", (object?)progress.Weaknesses ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Recommendation", (object?)progress.Recommendation ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalStudyTime", (object?)progress.TotalStudyTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LastSessionAt", (object?)progress.LastSessionAt ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteStudentProgress", connection);
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

        private async Task<RepositoryResponse<StudentProgress>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var progressReturned = new StudentProgress();
            var response = new RepositoryResponse<StudentProgress>();

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
                            progressReturned = MapStudentProgress(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = progressReturned;
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
                return new RepositoryResponse<StudentProgress> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static StudentProgress MapStudentProgress(SqlDataReader reader)
        {
            return new StudentProgress
            {
                Id = (int)reader["id"],
                StudentId = (int)reader["studentId"],
                CompletionPercentage = reader["completionPercentage"] == DBNull.Value ? null : (decimal?)reader["completionPercentage"],
                CurrentLevel = reader["currentLevel"] == DBNull.Value ? null : reader["currentLevel"].ToString(),
                Strengths = reader["strengths"] == DBNull.Value ? null : reader["strengths"].ToString(),
                Weaknesses = reader["weaknesses"] == DBNull.Value ? null : reader["weaknesses"].ToString(),
                Recommendation = reader["recommendation"] == DBNull.Value ? null : reader["recommendation"].ToString(),
                TotalStudyTime = reader["totalStudyTime"] == DBNull.Value ? null : (int?)reader["totalStudyTime"],
                LastSessionAt = reader["lastSessionAt"] == DBNull.Value ? null : (DateTime?)reader["lastSessionAt"],
                CreatedAt = (DateTime)reader["createdAt"],
                UpdatedAt = reader["updatedAt"] == DBNull.Value ? null : (DateTime?)reader["updatedAt"]
            };
        }
    }
}