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
    public class StudyHistoryRepository : IStudyHistoryRepository
    {
        private readonly string _connectionString;

        public StudyHistoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<StudyHistory>>> GetByStudentAsync(int studentId)
        {
            var items = new List<StudyHistory>();
            var response = new RepositoryResponse<List<StudyHistory>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetStudyHistoryByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapStudyHistory(reader));
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
                return new RepositoryResponse<List<StudyHistory>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<StudyHistory>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetStudyHistoryById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<StudyHistory>> CreateAsync(StudyHistory history)
            => await ExecuteSingle("USP_CreateStudyHistory", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", history.StudentId);
                cmd.Parameters.AddWithValue("@SubjectId", history.SubjectId);
                cmd.Parameters.AddWithValue("@LessonId", history.LessonId);
                cmd.Parameters.AddWithValue("@Score", (object?)history.Score ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StudyTime", (object?)history.StudyTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StudyDate", (object?)history.StudyDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Result", (object?)history.Result ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Difficulty", (object?)history.Difficulty ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<StudyHistory>> UpdateAsync(int id, StudyHistory history)
            => await ExecuteSingle("USP_UpdateStudyHistory", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@SubjectId", history.SubjectId);
                cmd.Parameters.AddWithValue("@LessonId", history.LessonId);
                cmd.Parameters.AddWithValue("@Score", (object?)history.Score ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StudyTime", (object?)history.StudyTime ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StudyDate", (object?)history.StudyDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Result", (object?)history.Result ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Difficulty", (object?)history.Difficulty ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteStudyHistory", connection);
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

        private async Task<RepositoryResponse<StudyHistory>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var historyReturned = new StudyHistory();
            var response = new RepositoryResponse<StudyHistory>();

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
                            historyReturned = MapStudyHistory(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = historyReturned;
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
                return new RepositoryResponse<StudyHistory> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static StudyHistory MapStudyHistory(SqlDataReader reader)
        {
            return new StudyHistory
            {
                Id = (int)reader["id"],
                StudentId = (int)reader["studentId"],
                SubjectId = (int)reader["subjectId"],
                LessonId = (int)reader["lessonId"],
                Score = reader["score"] == DBNull.Value ? null : (decimal?)reader["score"],
                StudyTime = reader["studyTime"] == DBNull.Value ? null : (int?)reader["studyTime"],
                StudyDate = reader["studyDate"] == DBNull.Value ? null : (DateTime?)reader["studyDate"],
                Result = reader["result"] == DBNull.Value ? null : reader["result"].ToString(),
                Difficulty = reader["difficulty"] == DBNull.Value ? null : reader["difficulty"].ToString()
            };
        }
    }
}