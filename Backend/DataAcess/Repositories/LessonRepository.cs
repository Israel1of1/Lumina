using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly string _connectionString;

        public LessonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<PagedResponse<IEnumerable<Lesson>>>> GetAllAsync(PaginationParams pagination)
        {
            var lessons = new List<Lesson>();
            var response = new RepositoryResponse<PagedResponse<IEnumerable<Lesson>>>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetAllLesson", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pagination.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pagination.PageSize);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lessons.Add(new Lesson
                            {
                                Id = (int)reader["id"],
                                ModuleId = (int)reader["moduleId"],
                                Title = reader["title"].ToString()!,
                                Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                                Type = reader["type"] != DBNull.Value ? reader["type"].ToString() : null,
                                DurationMinutes = reader["durationMinutes"] != DBNull.Value ? (int?)reader["durationMinutes"] : null,
                                CreatedAt = (DateTime)reader["createdAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                            totalRecords = reader.GetInt32(0);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = new PagedResponse<IEnumerable<Lesson>>
                    {
                        Data = lessons,
                        PageNumber = pagination.PageNumber,
                        PageSize = pagination.PageSize,
                        TotalRecords = totalRecords
                    };
                    response.OperationStatusCode = returnedValue;
                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<RepositoryResponse<Lesson>> GetByIdAsync(int id)
        {
            var lessonReturned = new Lesson();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetLessonById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonReturned.Id = (int)reader["id"];
                            lessonReturned.ModuleId = (int)reader["moduleId"];
                            lessonReturned.Title = reader["title"].ToString()!;
                            lessonReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonReturned.Type = reader["type"] != DBNull.Value ? reader["type"].ToString() : null;
                            lessonReturned.DurationMinutes = reader["durationMinutes"] != DBNull.Value ? (int?)reader["durationMinutes"] : null;
                            lessonReturned.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Lesson>
                    {
                        Data = lessonReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Lesson>> AddAsync(Lesson lesson)
        {
            var lessonReturned = new Lesson();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewLesson", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ModuleId", lesson.ModuleId);
                    cmd.Parameters.AddWithValue("@Title", lesson.Title);
                    cmd.Parameters.AddWithValue("@Description", (object?)lesson.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", (object?)lesson.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DurationMinutes", (object?)lesson.DurationMinutes ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonReturned.Id = (int)reader["id"];
                            lessonReturned.ModuleId = (int)reader["moduleId"];
                            lessonReturned.Title = reader["title"].ToString()!;
                            lessonReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonReturned.Type = reader["type"] != DBNull.Value ? reader["type"].ToString() : null;
                            lessonReturned.DurationMinutes = reader["durationMinutes"] != DBNull.Value ? (int?)reader["durationMinutes"] : null;
                            lessonReturned.CreatedAt = reader["createdAt"] != DBNull.Value ? (DateTime)reader["createdAt"] : DateTime.Now;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Lesson>
                    {
                        Data = lessonReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Lesson>> UpdateAsync(int id, Lesson lesson)
        {
            var lessonUpdated = new Lesson();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_UpdateLesson", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@ModuleId", lesson.ModuleId);
                    cmd.Parameters.AddWithValue("@Title", lesson.Title);
                    cmd.Parameters.AddWithValue("@Description", (object?)lesson.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Type", (object?)lesson.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DurationMinutes", (object?)lesson.DurationMinutes ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonUpdated.Id = (int)reader["id"];
                            lessonUpdated.ModuleId = (int)reader["moduleId"];
                            lessonUpdated.Title = reader["title"].ToString()!;
                            lessonUpdated.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonUpdated.Type = reader["type"] != DBNull.Value ? reader["type"].ToString() : null;
                            lessonUpdated.DurationMinutes = reader["durationMinutes"] != DBNull.Value ? (int?)reader["durationMinutes"] : null;
                            lessonUpdated.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Lesson>
                    {
                        Data = lessonUpdated,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Lesson> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
    }
}