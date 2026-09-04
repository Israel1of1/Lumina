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
    public class GuardianRepository : IGuardianRepository
    {

        private readonly string _connectionString;

        public GuardianRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<Guardian>> GetByUserIdAsync(int userId)
        {
            var profileReturned = new Guardian();
            var response = new RepositoryResponse<Guardian>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetGuardianByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            profileReturned = MapGuardianProfile(reader);
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = profileReturned;
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
                return new RepositoryResponse<Guardian>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        public async Task<RepositoryResponse<Guardian>> UpdateProfileAsync(int userId, Guardian profile)
        {
            var profileReturned = new Guardian();
            var response = new RepositoryResponse<Guardian>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_UpdateGuardianProfile", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FirstName", (object?)profile.FirstName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastName", (object?)profile.LastName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NationalId", (object?)profile.NationalId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PersonalEmail", (object?)profile.PersonalEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object?)profile.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object?)profile.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object?)profile.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Photo", (object?)profile.Photo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Relationship", (object?)profile.relationship ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            profileReturned = MapGuardianProfile(reader);
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = profileReturned;
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
                return new RepositoryResponse<Guardian>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        private static Guardian MapGuardianProfile(SqlDataReader reader)
        {
            return new Guardian
            {
                Id = (int)reader["Id"],
                UserId = (int)reader["UserId"],
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString(),
                NationalId = reader["NationalId"] == DBNull.Value ? null : reader["NationalId"].ToString(),
                PersonalEmail = reader["PersonalEmail"] == DBNull.Value ? null : reader["PersonalEmail"].ToString(),
                Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                City = reader["City"] == DBNull.Value ? null : reader["City"].ToString(),
                Photo = reader["Photo"] == DBNull.Value ? null : reader["Photo"].ToString(),
                relationship = reader["Relationship"] == DBNull.Value ? null : reader["Relationship"].ToString(),
                EntityStatus = reader["EntityStatus"].ToString()!,
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
            };
        }

        public async Task<RepositoryResponse<(List<Guardian> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? status)
        {
            var items = new List<Guardian>();
            var response = new RepositoryResponse<(List<Guardian>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllGuardians", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@Status", (object?)status ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new Guardian
                            {
                                Id = (int)reader["Id"],
                                UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
                                FirstName = reader["FirstName"].ToString()!,
                                LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString(),
                                NationalId = reader["NationalId"] == DBNull.Value ? null : reader["NationalId"].ToString(),
                                PersonalEmail = reader["PersonalEmail"] == DBNull.Value ? null : reader["PersonalEmail"].ToString(),
                                Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                                relationship = reader["Relationship"] == DBNull.Value ? null : reader["Relationship"].ToString(),
                                EntityStatus = reader["EntityStatus"].ToString()!,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                            });
                        }

                        if (await reader.NextResultAsync() && await reader.ReadAsync())
                        {
                            totalRecords = (int)reader["TotalRecords"];
                        }
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
                return new RepositoryResponse<(List<Guardian>, int)>
                {
                    Data = (items, 0),
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        public async Task<RepositoryResponse<EntityStatusResult>> DeactivateAsync(int guardianId, string? reason)
        {
            var resultReturned = new EntityStatusResult();
            var response = new RepositoryResponse<EntityStatusResult>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeactivateGuardian", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GuardianId", guardianId);
                    cmd.Parameters.AddWithValue("@Reason", (object?)reason ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            resultReturned = new EntityStatusResult
                            {
                                Id = (int)reader["Id"],
                                UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
                                FirstName = reader["FirstName"].ToString()!,
                                LastName = reader["LastName"].ToString(),
                                EntityStatus = reader["EntityStatus"].ToString()!,
                                DismissalDate = reader["DismissalDate"] == DBNull.Value ? null : (DateTime?)reader["DismissalDate"],
                                DismissalReason = reader["DismissalReason"] == DBNull.Value ? null : reader["DismissalReason"].ToString(),
                                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                            };
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = resultReturned;
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
                return new RepositoryResponse<EntityStatusResult>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }
    }
}
