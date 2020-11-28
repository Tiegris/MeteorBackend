using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Schemes.Domain.Requests;
using Schemes.Domain.Results;

namespace Schemes.Dal
{
    public class SchemesRepository : ISchemesRepository
    {
        private const string CONN_STRING = "server=mysql; userid=root; pwd=alma; port=3306; database=scheme_db;";

        public async Task<bool> DeleteSingle(int id) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "DELETE FROM schemes WHERE scheme_id = @id", conn);
            command.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            int affectedRows = await command.ExecuteNonQueryAsync();
            return affectedRows > 0;
        }

        public async Task DeleteAll() {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "TRUNCATE TABLE schemes", conn);
            conn.Open();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<SchemeResult> GetSingle(int id) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "SELECT * FROM schemes WHERE scheme_id = @id", conn);
            command.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();

            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return Converters.ParseScheme(reader);           
        }

        public async Task<List<SchemeResult>> GetLatest(int limit = 6) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "SELECT * FROM schemes ORDER BY dt DESC, version DESC LIMIT @limit", conn);
            command.Parameters.AddWithValue("@limit", limit);
            await conn.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            List<SchemeResult> result = new List<SchemeResult>(limit);
            while (await reader.ReadAsync()) 
                result.Add(Converters.ParseScheme(reader));
            return result;
        }

        public async Task<SchemeResult> Insert(UpdateSchemeRequest request) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "INSERT INTO " +
                "schemes(name, version, dt, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) " +
                "VALUES(@name, @ver, now(), " +
                "@swarm_1, @swarm_2, @swarm_3, @swarm_4, @swarm_5, @swarm_6);" +
                "SELECT last_insert_id() AS id",
                conn);

            command.Parameters.AddWithValue("@name", request.Name);
            command.Parameters.AddWithValue("@ver", request.Version);

            command.Parameters.AddWithValue("@swarm_1", request.Swarm1);
            command.Parameters.AddWithValue("@swarm_2", request.Swarm2);
            command.Parameters.AddWithValue("@swarm_3", request.Swarm3);
            command.Parameters.AddWithValue("@swarm_4", request.Swarm4);
            command.Parameters.AddWithValue("@swarm_5", request.Swarm5);
            command.Parameters.AddWithValue("@swarm_6", request.Swarm6);
            await conn.OpenAsync();

            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            int insertedID = Convert.ToInt32(reader["id"]);    
            return await GetSingle(insertedID);
        }

        public async Task<SchemeResult> Update(int id, UpdateSchemeRequest request) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "UPDATE schemes SET " +
                "name = @name, version = @ver, dt = now(), " +
                "swarm_1 = @swarm_1, swarm_2 = @swarm_2, swarm_3 = @swarm_3, " +
                "swarm_4 = @swarm_4, swarm_5 = @swarm_5, swarm_6 = @swarm_6 " +
                "WHERE scheme_id = @id;" +
                "SELECT * FROM schemes" +
                "ORDER BY dt LIMIT 1",
                conn);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", request.Name);
            command.Parameters.AddWithValue("@ver", request.Version);

            command.Parameters.AddWithValue("@swarm_1", request.Swarm1);
            command.Parameters.AddWithValue("@swarm_2", request.Swarm2);
            command.Parameters.AddWithValue("@swarm_3", request.Swarm3);
            command.Parameters.AddWithValue("@swarm_4", request.Swarm4);
            command.Parameters.AddWithValue("@swarm_5", request.Swarm5);
            command.Parameters.AddWithValue("@swarm_6", request.Swarm6);
            await conn.OpenAsync();

            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return Converters.ParseScheme(reader);
        }

    }
}
