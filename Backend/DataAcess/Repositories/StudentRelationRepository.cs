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
    public class StudentRelationRepository : IStudentRelationRepository
    {
        private readonly string _connectionString;

        public StudentRelationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<List<EntityStudentRelation>>> GetByStudentAsync(int studentId, bool onlyActive)
        {
            var items = new List<EntityStudentRelation>();
            var response = new RepositoryResponse<List<EntityStudentRelation>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetRelationsByStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@OnlyActive", onlyActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                            items.Add(MapRelation(reader));
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
                return new RepositoryResponse<List<EntityStudentRelation>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<List<StudentForEntity>>> GetActiveStudentsForEntityAsync(int entityId, string entityType)
        {
            var items = new List<StudentForEntity>();
            var response = new RepositoryResponse<List<StudentForEntity>>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_GetActiveStudentsForEntity", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EntityId", entityId);
                    cmd.Parameters.AddWithValue("@EntityType", entityType);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            items.Add(new StudentForEntity
                            {
                                RelationId = (int)reader["RelationId"],
                                RelationType = reader["RelationType"].ToString()!,
                                AssignedAt = reader["AssignedAt"] == DBNull.Value ? null : (DateTime?)reader["AssignedAt"],
                                Id = (int)reader["Id"],
                                GroupId = (int)reader["GroupId"],
                                FirstName = reader["FirstName"].ToString()!,
                                LastName = reader["LastName"] == DBNull.Value ? null : reader["LastName"].ToString(),
                                UniqueNumber = reader["UniqueNumber"] == DBNull.Value ? null : reader["UniqueNumber"].ToString(),
                                BirthDate = reader["BirthDate"] == DBNull.Value ? null : (DateTime?)reader["BirthDate"],
                                Gender = reader["Gender"] == DBNull.Value ? null : reader["Gender"].ToString(),
                                LanguageLevel = reader["LanguageLevel"] == DBNull.Value ? null : reader["LanguageLevel"].ToString(),
                                IsActive = (bool)reader["IsActive"]
                            });
                        }
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
                return new RepositoryResponse<List<StudentForEntity>> { Data = items, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        public async Task<RepositoryResponse<EntityStudentRelation>> CreateAsync(int entityId, string entityType, int studentId, string relationType, DateTime? assignedAt)
            => await ExecuteSingle("USP_CreateStudentRelation", cmd =>
            {
                cmd.Parameters.AddWithValue("@EntityId", entityId);
                cmd.Parameters.AddWithValue("@EntityType", entityType);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@RelationType", relationType);
                cmd.Parameters.AddWithValue("@AssignedAt", (object?)assignedAt ?? DBNull.Value);
            });

        public async Task<RepositoryResponse<EntityStudentRelation>> EndAsync(int id)
            => await ExecuteSingle("USP_EndStudentRelation", cmd => cmd.Parameters.AddWithValue("@Id", id));

        private async Task<RepositoryResponse<EntityStudentRelation>> ExecuteSingle(string spName, Action<SqlCommand> bindParams)
        {
            var itemReturned = new EntityStudentRelation();
            var response = new RepositoryResponse<EntityStudentRelation>();

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
                            itemReturned = MapRelation(reader);
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
                return new RepositoryResponse<EntityStudentRelation> { Data = null, OperationStatusCode = -1, Message = ex.Message };
            }
        }

        private static EntityStudentRelation MapRelation(SqlDataReader reader)
        {
            bool ContainsColumn(string name)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    if (string.Equals(reader.GetName(i), name, StringComparison.OrdinalIgnoreCase))
                        return true;
                return false;
            }

            return new EntityStudentRelation
            {
                Id = (int)reader["Id"],
                EntityId = (int)reader["EntityId"],
                EntityType = reader["EntityType"].ToString()!,
                StudentId = (int)reader["StudentId"],
                RelationType = reader["RelationType"].ToString()!,
                IsActive = (bool)reader["IsActive"],
                AssignedAt = reader["AssignedAt"] == DBNull.Value ? null : (DateTime?)reader["AssignedAt"],
                EndDate = reader["EndDate"] == DBNull.Value ? null : (DateTime?)reader["EndDate"],
                EntityFirstName = ContainsColumn("EntityFirstName") && reader["EntityFirstName"] != DBNull.Value ? reader["EntityFirstName"].ToString() : null,
                EntityLastName = ContainsColumn("EntityLastName") && reader["EntityLastName"] != DBNull.Value ? reader["EntityLastName"].ToString() : null
            };
        }
    }
}
