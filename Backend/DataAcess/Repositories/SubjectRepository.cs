
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly string _connectionString;

        public SubjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<PagedResponse<IEnumerable<Subject>>>> GetAllAsync(PaginationParams pagination)
        {
            var subjects = new List<Subject>();
            var response = new RepositoryResponse<PagedResponse<IEnumerable<Subject>>>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetAllSubject", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pagination.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pagination.PageSize);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subjects.Add(new Subject
                            {
                                Id = (int)reader["id"],
                                Name = reader["name"].ToString()!,
                                Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                                Color = reader["color"] != DBNull.Value ? reader["color"].ToString() : null,
                                Icon = reader["icon"] != DBNull.Value ? reader["icon"].ToString() : null,
                                CreatedAt = (DateTime)reader["createdAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                            totalRecords = reader.GetInt32(0);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = new PagedResponse<IEnumerable<Subject>>
                    {
                        Data = subjects,
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

        public async Task<RepositoryResponse<Subject>> GetByIdAsync(int id)
        {
            var subjectReturned = new Subject();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetSubjectById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            subjectReturned.Id = (int)reader["id"];
                            subjectReturned.Name = reader["name"].ToString()!;
                            subjectReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            subjectReturned.Color = reader["color"] != DBNull.Value ? reader["color"].ToString() : null;
                            subjectReturned.Icon = reader["icon"] != DBNull.Value ? reader["icon"].ToString() : null;
                            subjectReturned.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Subject>
                    {
                        Data = subjectReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Subject>> GetByNameAsync(string name)
        {
            var subjectReturned = new Subject();
            var response = new RepositoryResponse<Subject>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetSubjectByName", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            subjectReturned.Id = (int)reader["id"];
                            subjectReturned.Name = reader["name"].ToString()!;
                            subjectReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            subjectReturned.Color = reader["color"] != DBNull.Value ? reader["color"].ToString() : null;
                            subjectReturned.Icon = reader["icon"] != DBNull.Value ? reader["icon"].ToString() : null;
                            subjectReturned.CreatedAt = (DateTime)reader["createdAt"];
                        }
                        else
                        {
                            subjectReturned = new Subject();
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = subjectReturned;
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
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Subject>> AddAsync(Subject subject)
        {
            var subjectReturned = new Subject();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewSubject", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", subject.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)subject.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Color", (object?)subject.Color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Icon", (object?)subject.Icon ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            subjectReturned.Id = (int)reader["id"];
                            subjectReturned.Name = reader["name"].ToString()!;
                            subjectReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            subjectReturned.Color = reader["color"] != DBNull.Value ? reader["color"].ToString() : null;
                            subjectReturned.Icon = reader["icon"] != DBNull.Value ? reader["icon"].ToString() : null;
                            subjectReturned.CreatedAt = reader["createdAt"] != DBNull.Value ? (DateTime)reader["createdAt"] : DateTime.Now;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Subject>
                    {
                        Data = subjectReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Subject>> UpdateAsync(int id, Subject subject)
        {
            var subjectUpdated = new Subject();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_UpdateSubject", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Name", subject.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)subject.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Color", (object?)subject.Color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Icon", (object?)subject.Icon ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            subjectUpdated.Id = (int)reader["id"];
                            subjectUpdated.Name = reader["name"].ToString()!;
                            subjectUpdated.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            subjectUpdated.Color = reader["color"] != DBNull.Value ? reader["color"].ToString() : null;
                            subjectUpdated.Icon = reader["icon"] != DBNull.Value ? reader["icon"].ToString() : null;
                            subjectUpdated.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Subject>
                    {
                        Data = subjectUpdated,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Subject> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
    }
}