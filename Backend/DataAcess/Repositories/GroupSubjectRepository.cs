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
    public class GroupSubjectRepository : IGroupSubjectRepository
    {
        private readonly string _connectionString;

        public GroupSubjectRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<GroupSubject>>> GetByGroupAsync(int groupId)
            => await ExecuteList("USP_GetGroupSubjectsByGroup", cmd => cmd.Parameters.AddWithValue("@GroupId", groupId), hasGroupName: false);

        public async Task<RepositoryResponse<List<GroupSubject>>> GetByTeacherAsync(int teacherId)
            => await ExecuteList("USP_GetGroupSubjectsByTeacher", cmd => cmd.Parameters.AddWithValue("@TeacherId", teacherId), hasGroupName: true);

        public async Task<RepositoryResponse<GroupSubject>> CreateAsync(int groupId, int subjectId, int teacherId, DateTime? assignmentDate)
            => await ExecuteSingle("USP_CreateGroupSubject", cmd =>
            {
                cmd.Parameters.AddWithValue("@GroupId", groupId);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                cmd.Parameters.AddWithValue("@AssignmentDate", (object?)assignmentDate ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<GroupSubject>> SetActiveAsync(int id, bool isActive)
            => await ExecuteSingle("USP_SetGroupSubjectActive", cmd =>
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
            });

        private async Task<RepositoryResponse<List<GroupSubject>>> ExecuteList(string spName, Action<SqlCommand> bindParams, bool hasGroupName)
        {
            var items = new List<GroupSubject>();
            var response = new RepositoryResponse<List<GroupSubject>>();

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
                        while (await reader.ReadAsync())
                            items.Add(MapGroupSubject(reader, hasGroupName));
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
                return new RepositoryResponse<List<GroupSubject>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private async Task<RepositoryResponse<GroupSubject>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new GroupSubject();
            var response = new RepositoryResponse<GroupSubject>();

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
                            itemReturned = MapGroupSubject(reader, hasGroupName: false);
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = itemReturned;
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
                return new RepositoryResponse<GroupSubject> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static GroupSubject MapGroupSubject(SqlDataReader reader, bool hasGroupName)
        {
            var columns = reader.GetSchemaTable();
            bool ContainsColumn(string name)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    if (string.Equals(reader.GetName(i), name, StringComparison.OrdinalIgnoreCase))
                        return true;
                return false;
            }

            return new GroupSubject
            {
                Id = (int)reader["Id"],
                GroupId = (int)reader["GroupId"],
                SubjectId = (int)reader["SubjectId"],
                TeacherId = (int)reader["TeacherId"],
                IsActive = (bool)reader["IsActive"],
                AssignmentDate = reader["AssignmentDate"] == DBNull.Value ? null : (DateTime?)reader["AssignmentDate"],
                EndDate = reader["EndDate"] == DBNull.Value ? null : (DateTime?)reader["EndDate"],
                SubjectName = reader["SubjectName"].ToString()!,
                TeacherFirstName = ContainsColumn("TeacherFirstName") && reader["TeacherFirstName"] != DBNull.Value ? reader["TeacherFirstName"].ToString() : null,
                TeacherLastName = ContainsColumn("TeacherLastName") && reader["TeacherLastName"] != DBNull.Value ? reader["TeacherLastName"].ToString() : null,
                GroupName = ContainsColumn("GroupName") && reader["GroupName"] != DBNull.Value ? reader["GroupName"].ToString() : null
            };
        }
    }
}
