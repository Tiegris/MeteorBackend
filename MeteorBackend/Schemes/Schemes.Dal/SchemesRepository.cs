using System.Collections.Generic;
using System.Threading.Tasks;
using Schemes.Dal.Data;

using System.Data;
using MySql.Data.MySqlClient;
using System;

namespace Schemes.Dal
{
    public class SchemesRepository : ISchemesRepository
    {
        private const string CONN_STRING = "server=mysql; userid=root; pwd=alma; port=3306; database=scheme_db;";

        public async Task<bool> Delete(int id) {
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

        public async Task<Scheme> Get(int id) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "SELECT * FROM schemes WHERE scheme_id = @id", conn);
            command.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return new Scheme() {
                Id = Convert.ToInt32(reader["scheme_id"]),
                Name = Convert.ToString(reader["name"]),
                Version = Convert.ToInt32(reader["version"]),
                Swarm1 = Convert.ToString(reader["swarm_1"]),
                Swarm2 = Convert.ToString(reader["swarm_2"]),
                Swarm3 = Convert.ToString(reader["swarm_3"]),
                Swarm4 = Convert.ToString(reader["swarm_4"]),
                Swarm5 = Convert.ToString(reader["swarm_5"]),
                Swarm6 = Convert.ToString(reader["swarm_6"])
            }.Normalize();            
        }

        public async Task<List<Scheme>> GetAll() {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "SELECT * FROM schemes", conn);
            await conn.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            List<Scheme> result = new List<Scheme>();
            while (await reader.ReadAsync()) 
                result.Add(new Scheme() {
                    Id = Convert.ToInt32(reader["scheme_id"]),
                    Name = Convert.ToString(reader["name"]),
                    Version = Convert.ToInt32(reader["version"]),
                    Swarm1 = Convert.ToString(reader["swarm_1"]),
                    Swarm2 = Convert.ToString(reader["swarm_2"]),
                    Swarm3 = Convert.ToString(reader["swarm_3"]),
                    Swarm4 = Convert.ToString(reader["swarm_4"]),
                    Swarm5 = Convert.ToString(reader["swarm_5"]),
                    Swarm6 = Convert.ToString(reader["swarm_6"])
                }.Normalize());
            return result;
        }

        public async Task<Scheme> Insert(Scheme scheme) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "INSERT INTO " +
                "schemes(name, version, date, swarm_1, swarm_2, swarm_3, swarm_4, swarm_5, swarm_6) " +
                "VALUES(@name, @ver, curdate(), " +
                "@swarm_1, @swarm_2, @swarm_3, @swarm_4, @swarm_5, @swarm_6);" +
                "SELECT last_insert_id() AS id",
                conn);

            command.Parameters.AddWithValue("@name", scheme.Name);
            command.Parameters.AddWithValue("@ver", scheme.Version);

            command.Parameters.AddWithValue("@swarm_1", scheme.Swarm1);
            command.Parameters.AddWithValue("@swarm_2", scheme.Swarm2);
            command.Parameters.AddWithValue("@swarm_3", scheme.Swarm3);
            command.Parameters.AddWithValue("@swarm_4", scheme.Swarm4);
            command.Parameters.AddWithValue("@swarm_5", scheme.Swarm5);
            command.Parameters.AddWithValue("@swarm_6", scheme.Swarm6);
            await conn.OpenAsync();

            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            int insertedID = Convert.ToInt32(reader["id"]);    
            return await Get(insertedID);
        }

        public async Task<Scheme> Update(int id, Scheme scheme) {
            using var conn = new MySqlConnection(CONN_STRING);
            using var command = new MySqlCommand(
                "UPDATE schemes SET " +
                "name = @name, version = @ver, date = curdate(), " +
                "swarm_1 = @swarm_1, swarm_2 = @swarm_2, swarm_3 = @swarm_3, " +
                "swarm_4 = @swarm_4, swarm_5 = @swarm_5, swarm_6 = @swarm_6 " +
                "WHERE scheme_id = @id",
                conn);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", scheme.Name);
            command.Parameters.AddWithValue("@ver", scheme.Version);

            command.Parameters.AddWithValue("@swarm_1", scheme.Swarm1);
            command.Parameters.AddWithValue("@swarm_2", scheme.Swarm2);
            command.Parameters.AddWithValue("@swarm_3", scheme.Swarm3);
            command.Parameters.AddWithValue("@swarm_4", scheme.Swarm4);
            command.Parameters.AddWithValue("@swarm_5", scheme.Swarm5);
            command.Parameters.AddWithValue("@swarm_6", scheme.Swarm6);
            await conn.OpenAsync();

            int affectedRows = await command.ExecuteNonQueryAsync();
            if (affectedRows > 0) {
                scheme.Id = id;
                return scheme;
            } else {
                return null;
            }
        }

    }
}
