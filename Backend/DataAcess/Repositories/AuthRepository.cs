using Core;
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
    public class AuthRepository: IAuthRepository
    {

        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<User>> GetByEmailAsync(string email)
        {
            var userReturned = new User();
            var response = new RepositoryResponse<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetUserByEmail", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userReturned.Id = (int)reader["Id"];
                            userReturned.Email = reader["Email"].ToString()!;
                            userReturned.PasswordHash = reader["PasswordHash"].ToString()!;
                            userReturned.IsActive = (bool)reader["IsActive"];
                            userReturned.Roles = reader["Roles"] == DBNull.Value
                                ? new List<string>()
                                : reader["Roles"].ToString()!
                                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(r => r.Trim())
                                    .ToList();
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = userReturned;
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
                return new RepositoryResponse<User>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        public async Task<RepositoryResponse<LinkCodeInfo>> GetLinkCodeInfoAsync(string code)
        {
            var linkCodeReturned = new LinkCodeInfo();
            var response = new RepositoryResponse<LinkCodeInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetLinkCodeInfo", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            linkCodeReturned.Id = (int)reader["Id"];
                            linkCodeReturned.Code = reader["Code"].ToString()!;
                            linkCodeReturned.Purpose = reader["Purpose"].ToString()!;
                            linkCodeReturned.Status = reader["Status"].ToString()!;
                            linkCodeReturned.IssuedById = reader["IssuedById"] == DBNull.Value ? null : (int?)reader["IssuedById"];
                            linkCodeReturned.ExpiresAt = reader["ExpiresAt"] == DBNull.Value ? null : (DateTime?)reader["ExpiresAt"];
                            linkCodeReturned.UsedById = reader["UsedById"] == DBNull.Value ? null : (int?)reader["UsedById"];
                            linkCodeReturned.UsedAt = reader["UsedAt"] == DBNull.Value ? null : (DateTime?)reader["UsedAt"];
                            linkCodeReturned.CreatedAt = (DateTime)reader["CreatedAt"];
                            linkCodeReturned.UpdateAt = reader["UpdateAt"] == DBNull.Value ? null : (DateTime?)reader["UpdateAt"];
                            linkCodeReturned.TargetEntityType = reader["TargetEntityType"] == DBNull.Value ? null : reader["TargetEntityType"].ToString();
                            linkCodeReturned.TargetEntityId = reader["TargetEntityId"] == DBNull.Value ? null : (int?)reader["TargetEntityId"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = linkCodeReturned;
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
                return new RepositoryResponse<LinkCodeInfo>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        public async Task<RepositoryResponse<User>> RegisterWithLinkCodeAsync(string code, string email, string passwordHash)
        {
            var userReturned = new User();
            var response = new RepositoryResponse<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_RegisterUserWithLinkCode", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userReturned.Id = (int)reader["Id"];
                            userReturned.Email = reader["Email"].ToString()!;
                            userReturned.IsActive = (bool)reader["IsActive"];
                            userReturned.Roles = reader["Roles"] == DBNull.Value
                                ? new List<string>()
                                : reader["Roles"].ToString()!
                                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(r => r.Trim())
                                    .ToList();
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = userReturned;
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
                return new RepositoryResponse<User>
                {
                    Data = null,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }
    }
}
