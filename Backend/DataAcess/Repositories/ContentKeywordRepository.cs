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
    public class ContentKeywordRepository : IContentKeywordRepository
    {

        private readonly string _connectionString;

        public ContentKeywordRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<ContentKeyWord>>> GetByContentAsync(int contentId)
        {
            var items = new List<ContentKeyWord>();
            var response = new RepositoryResponse<List<ContentKeyWord>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetKeywordsByContent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContentId", contentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapContentKeyword(reader));
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
                return new RepositoryResponse<List<ContentKeyWord>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<ContentKeyWord>> AddAsync(int contentId, int? keywordId, string? keywordName)
        {
            var linkReturned = new ContentKeyWord();
            var response = new RepositoryResponse<ContentKeyWord>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_AddKeywordToContent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContentId", contentId);
                    cmd.Parameters.AddWithValue("@KeywordId", (object?)keywordId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@KeywordName", (object?)keywordName ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            linkReturned = MapContentKeyword(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = linkReturned;
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
                return new RepositoryResponse<ContentKeyWord> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<bool>> RemoveAsync(int contentId, int keywordId)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_RemoveKeywordFromContent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContentId", contentId);
                    cmd.Parameters.AddWithValue("@KeywordId", keywordId);
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

        private static ContentKeyWord MapContentKeyword(SqlDataReader reader)
        {
            return new ContentKeyWord
            {
                Id = (int)reader["Id"],
                ContentId = (int)reader["ContentId"],
                KeywordId = (int)reader["KeywordId"],
                KeywordName = reader["KeywordName"].ToString()!
            };
        }
    }
}
