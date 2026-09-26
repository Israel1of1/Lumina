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
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<(List<Student> Items, int TotalRecords)>> GetByGroupAsync(int groupId, int pageNumber, int pageSize, bool onlyActive)
        {
            var items = new List<Student>();
            var response = new RepositoryResponse<(List<Student>, int)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetStudentsByGroup", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GroupId", groupId);
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@OnlyActive", onlyActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    int totalRecords = 0;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapStudent(reader));

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
                return new RepositoryResponse<(List<Student>, int)> { Data = (items, 0), OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<Student>> GetByIdAsync(int id)
            => await ExecuteSingle("USP_GetStudentById", cmd => cmd.Parameters.AddWithValue("@Id", id));

        public async Task<RepositoryResponse<Student>> CreateAsync(Student student)
            => await ExecuteSingle("USP_CreateStudent", cmd =>
            {
                cmd.Parameters.AddWithValue("@GroupId", student.GroupId);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", (object?)student.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UniqueNumber", (object?)student.UniqueNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", (object?)student.BirthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", (object?)student.Gender ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<Student>> UpdateAsync(int id, Student student)
            => await ExecuteSingle("USP_UpdateStudent", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@GroupId", student.GroupId);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", (object?)student.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UniqueNumber", (object?)student.UniqueNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", (object?)student.BirthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", (object?)student.Gender ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LanguageLevel", (object?)student.LanguageLevel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ClinicalInfo", (object?)student.ClinicalInfo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Observations", (object?)student.Observations ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<Student>> SetActiveAsync(int id, bool isActive)
            => await ExecuteSingle("USP_SetStudentActive", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
            });

        private async Task<RepositoryResponse<Student>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var studentReturned = new Student();
            var response = new RepositoryResponse<Student>();

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
                            studentReturned = MapStudent(reader);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = studentReturned;
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
                return new RepositoryResponse<Student> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static Student MapStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = (int)reader["Id"],
                GroupId = (int)reader["GroupId"],
                UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
                FirstName = reader["FirstName"].ToString()!,
                LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString(),
                UniqueNumber = reader["UniqueNumber"] == DBNull.Value ? null : reader["UniqueNumber"].ToString(),
                BirthDate = reader["BirthDate"] == DBNull.Value ? null : (DateTime?)reader["BirthDate"],
                Gender = reader["Gender"] == DBNull.Value ? null : reader["Gender"].ToString(),
                LanguageLevel = reader["LanguageLevel"] == DBNull.Value ? null : reader["LanguageLevel"].ToString(),
                ClinicalInfo = reader["ClinicalInfo"] == DBNull.Value ? null : reader["ClinicalInfo"].ToString(),
                Observations = reader["Observations"] == DBNull.Value ? null : reader["Observations"].ToString(),
                IsActive = (bool)reader["IsActive"],
                CreatedAt = (DateTime)reader["CreatedAt"],
                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
            };
        }
    }
}
