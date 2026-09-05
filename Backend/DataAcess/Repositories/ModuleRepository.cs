using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly string _connectionString;

        public ModuleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<PagedResponse<IEnumerable<Module>>>> GetAllAsync(PaginationParams pagination)
        {
            var modules = new List<Module>();
            var response = new RepositoryResponse<PagedResponse<IEnumerable<Module>>>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetAllModule", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pagination.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pagination.PageSize);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            modules.Add(new Module
                            {
                                Id = (int)reader["id"],
                                SubjectId = (int)reader["subjectId"],
                                Name = reader["name"].ToString()!,
                                Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null,
                                IconUrl = reader["iconUrl"] != DBNull.Value ? reader["iconUrl"].ToString() : null,
                                CreatedAt = (DateTime)reader["createdAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                            totalRecords = reader.GetInt32(0);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = new PagedResponse<IEnumerable<Module>>
                    {
                        Data = modules,
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

        public async Task<RepositoryResponse<Module>> GetByIdAsync(int id)
        {
            var moduleReturned = new Module();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetModuleById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            moduleReturned.Id = (int)reader["id"];
                            moduleReturned.SubjectId = (int)reader["subjectId"];
                            moduleReturned.Name = reader["name"].ToString()!;
                            moduleReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            moduleReturned.IconUrl = reader["iconUrl"] != DBNull.Value ? reader["iconUrl"].ToString() : null;
                            moduleReturned.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Module>
                    {
                        Data = moduleReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Module>> AddAsync(Module module)
        {
            var moduleReturned = new Module();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewModule", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubjectId", module.SubjectId);
                    cmd.Parameters.AddWithValue("@Name", module.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)module.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IconUrl", (object?)module.IconUrl ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            moduleReturned.Id = (int)reader["id"];
                            moduleReturned.SubjectId = (int)reader["subjectId"];
                            moduleReturned.Name = reader["name"].ToString()!;
                            moduleReturned.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            moduleReturned.IconUrl = reader["iconUrl"] != DBNull.Value ? reader["iconUrl"].ToString() : null;
                            moduleReturned.CreatedAt = reader["createdAt"] != DBNull.Value ? (DateTime)reader["createdAt"] : DateTime.Now;
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Module>
                    {
                        Data = moduleReturned,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Module>> UpdateAsync(int id, Module module)
        {
            var moduleUpdated = new Module();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_UpdateModule", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@SubjectId", module.SubjectId);
                    cmd.Parameters.AddWithValue("@Name", module.Name);
                    cmd.Parameters.AddWithValue("@Description", (object?)module.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IconUrl", (object?)module.IconUrl ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            moduleUpdated.Id = (int)reader["id"];
                            moduleUpdated.SubjectId = (int)reader["subjectId"];
                            moduleUpdated.Name = reader["name"].ToString()!;
                            moduleUpdated.Description = reader["description"] != DBNull.Value ? reader["description"].ToString() : null;
                            moduleUpdated.IconUrl = reader["iconUrl"] != DBNull.Value ? reader["iconUrl"].ToString() : null;
                            moduleUpdated.CreatedAt = (DateTime)reader["createdAt"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    return new RepositoryResponse<Module>
                    {
                        Data = moduleUpdated,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = ex.Number, Message = ex.Message };
            }
            catch (Exception ex)
            {
                return new RepositoryResponse<Module> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
    }
}