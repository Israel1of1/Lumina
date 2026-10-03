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
    public class LearningContentRepository : ILearningContentRepository
    {
        private readonly string _connectionString;

        public LearningContentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<(List<LearningContent> Items, int TotalRecords)>> GetAllAsync(LearningContentFilter filter)
        {
            var items = new List<LearningContent>();
            var response = new RepositoryResponse<(List<LearningContent>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllLearningContents", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", filter.PageSize);
                    cmd.Parameters.AddWithValue("@LessonId", (object?)filter.LessonId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SubjectId", (object?)filter.SubjectId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", (object?)filter.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Level", (object?)filter.Level ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Search", (object?)filter.Search ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", (object?)filter.IsActive ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapLearningContent(reader));

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
                return new RepositoryResponse<(List<LearningContent>, int)> { Data = (items, 0), OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<LearningContent>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetLearningContentById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<LearningContent>> CreateAsync(LearningContent content)
            => await ExecuteSingle("USP_CreateLearningContent", cmd =>
            {
                cmd.Parameters.AddWithValue("@LessonId", content.LessonId);
                cmd.Parameters.AddWithValue("@Title", (object?)content.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)content.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Type", (object?)content.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectId", (object?)content.SubjectId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Level", (object?)content.Level ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<LearningContent>> UpdateAsync(int id, LearningContent content)
            => await ExecuteSingle("USP_UpdateLearningContent", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@LessonId", content.LessonId);
                cmd.Parameters.AddWithValue("@Title", (object?)content.Title ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", (object?)content.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Type", (object?)content.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectId", (object?)content.SubjectId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Level", (object?)content.Level ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<LearningContent>> SetActiveAsync(int id, bool isActive)
            => await ExecuteSingle("USP_SetLearningContentActive", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
            });

        private async Task<RepositoryResponse<LearningContent>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var contentReturned = new LearningContent();
            var response = new RepositoryResponse<LearningContent>();

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
                            contentReturned = MapLearningContent(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = contentReturned;
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
                return new RepositoryResponse<LearningContent> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static LearningContent MapLearningContent(SqlDataReader reader)
        {
            return new LearningContent
            {
                Id = (int)reader["Id"],
                LessonId = (int)reader["LessonId"],
                LessonTitle = reader["LessonTitle"] == DBNull.Value ? null : reader["LessonTitle"].ToString(),
                Title = reader["Title"] == DBNull.Value ? null : reader["Title"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                Type = reader["Type"] == DBNull.Value ? null : reader["Type"].ToString(),
                IsDictionary = (bool)reader["IsDictionary"],
                IsRoutine = (bool)reader["IsRoutine"],
                IsException = (bool)reader["IsException"],
                SubjectId = reader["SubjectId"] == DBNull.Value ? null : (int?)reader["SubjectId"],
                SubjectName = reader["SubjectName"] == DBNull.Value ? null : reader["SubjectName"].ToString(),
                Level = reader["Level"] == DBNull.Value ? null : reader["Level"].ToString(),
                IsActive = (bool)reader["IsActive"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                Keywords = ParseCsv(reader["Keywords"])
            };
        }

        // El SP devuelve las palabras clave como texto separado por comas (STRING_AGG)
        private static List<string> ParseCsv(object value)
        {
            if (value == DBNull.Value || value is null)
                return new List<string>();

            var text = value.ToString();
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            return text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }
    }
}
