using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class MechTagMechListService (MySqlDataSource database)
    {
        public async Task<IReadOnlyList<MechTagMechList>> GetMechTagMechsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Mechanism_TagMechanism";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<MechTagMechList?> GetMechTagMechAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Mechanism_TagMechanism WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreateMechTagMechAsync(MechTagMechList newMechTagMech)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid mechTagMechID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Mechanism_TagMechanism (uuid, mechanism_uuid, tag_mechanism_uuid, version) VALUES (@uuid, @mechanism_uuid, @tag_mechanism_uuid, @version);";

            command.Parameters.AddWithValue("@uuid", mechTagMechID);
            command.Parameters.AddWithValue("@mechanism_uuid", newMechTagMech.mechanism_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", newMechTagMech.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@version", newMechTagMech.version);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateMechTagMechAsync(MechTagMechList mechTagMech)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism_TagMechanism SET mechanism_uuid = @mechanism_uuid, tag_mechanism_uuid = @tag_mechanism_uuid, version = @version, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", mechTagMech.uuid);
            command.Parameters.AddWithValue("@mechanism_uuid", mechTagMech.mechanism_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", mechTagMech.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@version", mechTagMech.version);
            command.Parameters.AddWithValue("@isDel", mechTagMech.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteMechTagMechAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism_TagMechanism SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<MechTagMechList>> ReadAllAsync(DbDataReader reader)
        {
            var mechTagMech = new List<MechTagMechList>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var mechTag = new MechTagMechList
                    {
                        uuid = reader.GetGuid(0),
                        mechanism_uuid = reader.GetGuid(1),
                        tag_mechanism_uuid = reader.GetGuid(2),
                        version = reader.GetString(3),
                        isDel = reader.GetBoolean(4),
                    };
                    mechTagMech.Add(mechTag);
                }
            }
            return mechTagMech;
        }
    }
}
