using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;


namespace Chemistry_Cafe_API.Services
{
    public class PropertyListService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<PropertyList>> GetPropertyListsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Property_List";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<PropertyList?> GetPropertyListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Property_List WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreatePropertyListAsync(PropertyList userPreferences)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid userPreferencesID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Property_List (uuid, parent_uuid, version) VALUES (@uuid, @parent_uuid, @version);";

            command.Parameters.AddWithValue("@uuid", userPreferencesID);
            command.Parameters.AddWithValue("@parent_uuid", userPreferences.parent_uuid);
            command.Parameters.AddWithValue("@version", userPreferences.version);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdatePropertyListAsync(PropertyList userPreferences)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Property_List SET parent_uuid = @parent_uuid, version = @version, isDel = @isDel WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", userPreferences.uuid);
            command.Parameters.AddWithValue("@parent_uuid", userPreferences.parent_uuid);
            command.Parameters.AddWithValue("@version", userPreferences.version);
            command.Parameters.AddWithValue("@isDel", userPreferences.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeletePropertyListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Property_List SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);

            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<PropertyList>> ReadAllAsync(DbDataReader reader)
        {
            var propertyList = new List<PropertyList>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var property = new PropertyList
                    {
                        uuid = reader.GetGuid(0),
                        parent_uuid = reader.GetGuid(1),
                        version = reader.GetString(2),
                        isDel = reader.GetBoolean(3),
                    };
                    propertyList.Add(property);
                }
            }
            return propertyList;
        }
    }
}
