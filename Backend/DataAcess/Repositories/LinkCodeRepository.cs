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
    public class LinkCodeRepository : ILinkCodeRepository
    {
        private readonly string _connectionString;

        public LinkCodeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<LinkCodeInfo>> CreateForTeacherAsync(int teacherId, int? issuedById, DateTime? expiresAt)
        {
            var linkCodeReturned = new LinkCodeInfo();
            var response = new RepositoryResponse<LinkCodeInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_CreateTeacherLinkCode", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("@IssuedById", (object?)issuedById ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ExpiresAt", (object?)expiresAt ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            linkCodeReturned = MapLinkCodeInfo(reader);
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

        public async Task<RepositoryResponse<LinkCodeInfo>> CreateForGuardianAsync(int guardianId, int? issuedById, DateTime? expiresAt)
        {
            var linkCodeReturned = new LinkCodeInfo();
            var response = new RepositoryResponse<LinkCodeInfo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_CreateGuardianLinkCode", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GuardianId", guardianId);
                    cmd.Parameters.AddWithValue("@IssuedById", (object?)issuedById ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ExpiresAt", (object?)expiresAt ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            linkCodeReturned = MapLinkCodeInfo(reader);
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

        public async Task<RepositoryResponse<bool>> RevokeAsync(string code)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_RevokeLinkCode", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", code);
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
                return new RepositoryResponse<bool>
                {
                    Data = false,
                    OperationStatusCode = -1,
                    Message = ex.Message
                };
            }
        }

        private static LinkCodeInfo MapLinkCodeInfo(SqlDataReader reader)
        {
            return new LinkCodeInfo
            {
                Id = (int)reader["Id"],
                Code = reader["Code"].ToString()!,
                Purpose = reader["Purpose"].ToString()!,
                Status = reader["Status"].ToString()!,
                IssuedById = reader["IssuedById"] == DBNull.Value ? null : (int?)reader["IssuedById"],
                ExpiresAt = reader["ExpiresAt"] == DBNull.Value ? null : (DateTime?)reader["ExpiresAt"],
                UsedById = reader["UsedById"] == DBNull.Value ? null : (int?)reader["UsedById"],
                UsedAt = reader["UsedAt"] == DBNull.Value ? null : (DateTime?)reader["UsedAt"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdateAt = reader["UpdateAt"] == DBNull.Value ? null : (DateTime?)reader["UpdateAt"],
                TargetEntityType = reader["TargetEntityType"] == DBNull.Value ? null : reader["TargetEntityType"].ToString(),
                TargetEntityId = reader["TargetEntityId"] == DBNull.Value ? null : (int?)reader["TargetEntityId"]
            };
        }
    }
}
