using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class MechanismService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<Mechanism>> GetMechanismsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Mechanism";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<Mechanism?> GetMechanismAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Mechanism WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyList<Mechanism>> GetFamilyMechanismsAsync(Guid family_uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT Mechanism.uuid, Mechanism.name, Mechanism.isDel FROM Family_Mechanism_List LEFT JOIN Mechanism ON mechanism_uuid = Mechanism.uuid WHERE family_uuid = @id;";
            command.Parameters.AddWithValue("@id", family_uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task CreateMechanismAsync(string name)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid mechanismID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Mechanism (uuid, name) VALUES (@uuid, @name);";

            command.Parameters.AddWithValue("@uuid", mechanismID);
            command.Parameters.AddWithValue("@name", name);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateMechanismAsync(Mechanism mechanism)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism SET name = @name, isDel = @isDel WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", mechanism.uuid);
            command.Parameters.AddWithValue("@name", mechanism.name);
            command.Parameters.AddWithValue("@isDel", mechanism.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteMechanismAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);

            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<Mechanism>> ReadAllAsync(DbDataReader reader)
        {
            var mechanisms = new List<Mechanism>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var mechanism = new Mechanism
                    {
                        uuid = reader.GetGuid(0),
                        name = reader.GetString(1),
                        isDel = reader.GetBoolean(2),
                    };
                    mechanisms.Add(mechanism);
                }
            }
            return mechanisms;
        }
    }
}
