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
    public class StudentInterestRepository : IStudentInterestRepository
    {
        private readonly string _connectionString;

        public StudentInterestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<StudentInterest>>> GetByStudentAsync(int studentId)
        {
            var items = new List<StudentInterest>();
            var response = new RepositoryResponse<List<StudentInterest>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetStudentInterestsByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapStudentInterest(reader));
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
                return new RepositoryResponse<List<StudentInterest>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<StudentInterest>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetStudentInterestById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<StudentInterest>> CreateAsync(StudentInterest interest)
            => await ExecuteSingle("USP_CreateStudentInterest", cmd =>
            {
                cmd.Parameters.AddWithValue("@StudentId", interest.StudentId);
                cmd.Parameters.AddWithValue("@Name", interest.Name);
                cmd.Parameters.AddWithValue("@Description", (object?)interest.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<StudentInterest>> UpdateAsync(int id, StudentInterest interest)
            => await ExecuteSingle("USP_UpdateStudentInterest", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", interest.Name);
                cmd.Parameters.AddWithValue("@Description", (object?)interest.Description ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<bool>> DeleteAsync(int id)
        {
            var response = new RepositoryResponse<bool>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeleteStudentInterest", connection);
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

        private async Task<RepositoryResponse<StudentInterest>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var interestReturned = new StudentInterest();
            var response = new RepositoryResponse<StudentInterest>();

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
                            interestReturned = MapStudentInterest(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = interestReturned;
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
                return new RepositoryResponse<StudentInterest> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static StudentInterest MapStudentInterest(SqlDataReader reader)
        {
            return new StudentInterest
            {
                Id = (int)reader["id"],
                StudentId = (int)reader["studentId"],
                Name = reader["name"] == DBNull.Value ? null : reader["name"].ToString(),
                Description = reader["description"] == DBNull.Value ? null : reader["description"].ToString(),
                CreatedAt = (DateTime)reader["createdAt"],
                UpdatedAt = reader["updatedAt"] == DBNull.Value ? null : (DateTime?)reader["updatedAt"]
            };
        }
    }
}