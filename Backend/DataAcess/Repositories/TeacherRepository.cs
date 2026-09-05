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

    public class TeacherRepository : ITeacherRepository
    {
        private readonly string _connectionString;

        public TeacherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<Teacher>> GetByUserIdAsync(int userId)
        {
            var teacherReturned = new Teacher();
            var response = new RepositoryResponse<Teacher>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetTeacherByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            teacherReturned = MapTeacher(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = teacherReturned;
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
                return new RepositoryResponse<Teacher> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Teacher>> PatchProfileAsync(int userId, Teacher profile)
        {
            var teacherReturned = new Teacher();
            var response = new RepositoryResponse<Teacher>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_PatchTeacherProfile", connection);
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
                    cmd.Parameters.AddWithValue("@Specialty", (object?)profile.Specialty ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Degree", (object?)profile.Degree ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            teacherReturned = MapTeacher(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = teacherReturned;
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
                return new RepositoryResponse<Teacher> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }
        public async Task<RepositoryResponse<Teacher>> UpdateProfileAsync(int userId, Teacher profile)
        {
            var teacherReturned = new Teacher();
            var response = new RepositoryResponse<Teacher>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_UpdateTeacherProfile", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", profile.LastName);
                    cmd.Parameters.AddWithValue("@NationalId", (object?)profile.NationalId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PersonalEmail", (object?)profile.PersonalEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object?)profile.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object?)profile.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object?)profile.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Photo", (object?)profile.Photo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Specialty", (object?)profile.Specialty ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Degree", (object?)profile.Degree ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            teacherReturned = MapTeacher(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = teacherReturned;
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
                return new RepositoryResponse<Teacher> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }



        public async Task<RepositoryResponse<(List<TeacherWithAccount> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? status)
        {
            var items = new List<TeacherWithAccount>();
            var response = new RepositoryResponse<(List<TeacherWithAccount>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetAllTeachers", connection);
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
                            items.Add(new TeacherWithAccount
                            {
                                Teacher = MapTeacher(reader),
                                AccountEmail = reader["AccountEmail"] == DBNull.Value ? null : reader["AccountEmail"].ToString(),
                                AccountIsActive = reader["AccountIsActive"] == DBNull.Value ? null : (bool?)reader["AccountIsActive"]
                            });
                        }

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
                return new RepositoryResponse<(List<TeacherWithAccount>, int)> { Data = (items, 0), OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Teacher>> DeactivateAsync(int teacherId, string? reason)
        {
            var teacherReturned = new Teacher();
            var response = new RepositoryResponse<Teacher>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DeactivateTeacher", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                    cmd.Parameters.AddWithValue("@Reason", (object?)reason ?? DBNull.Value);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            teacherReturned = MapTeacher(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = teacherReturned;
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
                return new RepositoryResponse<Teacher> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static Teacher MapTeacher(SqlDataReader reader)
        {
            return new Teacher
            {
                Id = (int)reader["Id"],
                UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"].ToString()!,
                NationalId = reader["NationalId"] == DBNull.Value ? null : reader["NationalId"].ToString(),
                PersonalEmail = reader["PersonalEmail"] == DBNull.Value ? null : reader["PersonalEmail"].ToString(),
                Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                City = reader["City"] == DBNull.Value ? null : reader["City"].ToString(),
                Photo = reader["Photo"] == DBNull.Value ? null : reader["Photo"].ToString(),
                Specialty = reader["Specialty"] == DBNull.Value ? null : reader["Specialty"].ToString(),
                Degree = reader["Degree"] == DBNull.Value ? null : reader["Degree"].ToString(),
                EntityStatus = reader["EntityStatus"].ToString()!,
                DismissalDate = reader["DismissalDate"] == DBNull.Value ? null : (DateTime?)reader["DismissalDate"],
                DismissalReason = reader["DismissalReason"] == DBNull.Value ? null : reader["DismissalReason"].ToString(),
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdateAt = reader["UpdateAt"] == DBNull.Value ? null : (DateTime?)reader["UpdateAt"]
            };
        }
    }
}
