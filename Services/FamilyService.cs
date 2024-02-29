using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class FamilyService (MySqlDataSource database)
    {
        public async Task<IReadOnlyList<Family>> GetFamiliesAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Family";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<Family?> GetFamilyAsync(int uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Family WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreateFamilyAsync(string name)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"INSERT INTO Family (name) VALUES (@name);";
            command.Parameters.AddWithValue("@name", name);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateFamilyAsync(Family family)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Family SET name = @name, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", family.uuid);
            command.Parameters.AddWithValue("@name", family.name);
            command.Parameters.AddWithValue("@isDel", family.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteFamilyAsync(int uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Family SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<Family>> ReadAllAsync(DbDataReader reader)
        {
            var families = new List<Family>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var family = new Family
                    {
                        uuid = reader.GetInt32(0),
                        name = reader.GetString(1),
                        isDel = reader.GetBoolean(2),
                    };
                    families.Add(family);
                }
            }
            return families;
        }
    }
}
