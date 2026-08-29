using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class LessonStepRepository : ILessonStepRepository
    {
        private readonly string _connectionString;

        public LessonStepRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<PagedResponse<IEnumerable<LessonStep>>>> GetAllAsync(PaginationParams pagination)
        {
            var lessonSteps = new List<LessonStep>();
            var response = new RepositoryResponse<PagedResponse<IEnumerable<LessonStep>>>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetAllLessonStep", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pagination.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pagination.PageSize);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lessonSteps.Add(new LessonStep
                            {
                                Id = (int)reader["id"],
                                LessonId = (int)reader["lessonId"],
                                StepNumber = reader["stepNumber"] != DBNull.Value ? (int?)reader["stepNumber"] : null,
                                Title = reader["title"] != DBNull.Value ? reader["title"].ToString() : null,
                                Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                                ContentType = reader["contentType"] != DBNull.Value ? reader["contentType"].ToString() : null,
                                ContentUrl = reader["contentUrl"] != DBNull.Value ? reader["contentUrl"].ToString() : null,
                                IsActive = (bool)reader["isActive"],
                                CreatedAt = (DateTime)reader["createdAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                            totalRecords = reader.GetInt32(0);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = new PagedResponse<IEnumerable<LessonStep>>
                    {
                        Data = lessonSteps,
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

        public async Task<RepositoryResponse<LessonStep>> GetByIdAsync(int id)
        {
            var lessonStepReturned = new LessonStep();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetLessonStepById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonStepReturned.Id = (int)reader["id"];
                            lessonStepReturned.LessonId = (int)reader["lessonId"];
                            lessonStepReturned.StepNumber = reader["stepNumber"] != DBNull.Value ? (int?)reader["stepNumber"] : null;
                            lessonStepReturned.Title = reader["title"] != DBNull.Value ? reader["title"].ToString() : null;
                            lessonStepReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonStepReturned.ContentType = reader["contentType"] != DBNull.Value ? reader["contentType"].ToString() : null;
                            lessonStepReturned.ContentUrl = reader["contentUrl"] != DBNull.Value ? reader["contentUrl"].ToString() : null;
                            lessonStepReturned.IsActive = (bool)reader["isActive"];
                            lessonStepReturned.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<LessonStep>
                    {
                        Data = lessonStepReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<LessonStep>> AddAsync(LessonStep lessonStep)
        {
            var lessonStepReturned = new LessonStep();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewLessonStep", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LessonId", lessonStep.LessonId);
                    cmd.Parameters.AddWithValue("@StepNumber", (object?)lessonStep.StepNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Title", (object?)lessonStep.Title ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object?)lessonStep.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContentType", (object?)lessonStep.ContentType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContentUrl", (object?)lessonStep.ContentUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", lessonStep.IsActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonStepReturned.Id = (int)reader["id"];
                            lessonStepReturned.LessonId = (int)reader["lessonId"];
                            lessonStepReturned.StepNumber = reader["stepNumber"] != DBNull.Value ? (int?)reader["stepNumber"] : null;
                            lessonStepReturned.Title = reader["title"] != DBNull.Value ? reader["title"].ToString() : null;
                            lessonStepReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonStepReturned.ContentType = reader["contentType"] != DBNull.Value ? reader["contentType"].ToString() : null;
                            lessonStepReturned.ContentUrl = reader["contentUrl"] != DBNull.Value ? reader["contentUrl"].ToString() : null;
                            lessonStepReturned.IsActive = (bool)reader["isActive"];
                            lessonStepReturned.CreatedAt = reader["createdAt"] != DBNull.Value ? (DateTime)reader["createdAt"] : DateTime.Now;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<LessonStep>
                    {
                        Data = lessonStepReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<LessonStep>> UpdateAsync(int id, LessonStep lessonStep)
        {
            var lessonStepUpdated = new LessonStep();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_UpdateLessonStep", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@LessonId", lessonStep.LessonId);
                    cmd.Parameters.AddWithValue("@StepNumber", (object?)lessonStep.StepNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Title", (object?)lessonStep.Title ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object?)lessonStep.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContentType", (object?)lessonStep.ContentType ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ContentUrl", (object?)lessonStep.ContentUrl ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", lessonStep.IsActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            lessonStepUpdated.Id = (int)reader["id"];
                            lessonStepUpdated.LessonId = (int)reader["lessonId"];
                            lessonStepUpdated.StepNumber = reader["stepNumber"] != DBNull.Value ? (int?)reader["stepNumber"] : null;
                            lessonStepUpdated.Title = reader["title"] != DBNull.Value ? reader["title"].ToString() : null;
                            lessonStepUpdated.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            lessonStepUpdated.ContentType = reader["contentType"] != DBNull.Value ? reader["contentType"].ToString() : null;
                            lessonStepUpdated.ContentUrl = reader["contentUrl"] != DBNull.Value ? reader["contentUrl"].ToString() : null;
                            lessonStepUpdated.IsActive = (bool)reader["isActive"];
                            lessonStepUpdated.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<LessonStep>
                    {
                        Data = lessonStepUpdated,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<LessonStep> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
    }
}