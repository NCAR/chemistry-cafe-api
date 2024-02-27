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
