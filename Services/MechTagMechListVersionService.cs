using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class MechTagMechListVersionService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<MechTagMechListVersion>> GetMechTagMechListVersionsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Mechanism_TagMechanism_List_Version";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<MechTagMechListVersion?> GetMechTagMechListVersionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Mechanism_TagMechanism_List_Version WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task<Guid> CreateMechTagMechListVersionAsync(MechTagMechListVersion newMechTagMechListVersion)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid mechTagMechListVersionID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Mechanism_TagMechanism_List_Version (uuid, mechanism_uuid, tag_mechanism_uuid, frozen_version, action, user_uuid, datetime) VALUES (@uuid, @mechanism_uuid, @tag_mechanism_uuid, @frozen_version, @action, @user_uuid, @datetime);";

            command.Parameters.AddWithValue("@uuid", mechTagMechListVersionID);
            command.Parameters.AddWithValue("@mechanism_uuid", newMechTagMechListVersion.mechanism_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", newMechTagMechListVersion.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@frozen_version", newMechTagMechListVersion.frozen_version);
            command.Parameters.AddWithValue("@action", newMechTagMechListVersion.action);
            command.Parameters.AddWithValue("@user_uuid", newMechTagMechListVersion.user_uuid);
            command.Parameters.AddWithValue("@datetime", newMechTagMechListVersion.datetime);

            await command.ExecuteNonQueryAsync();

            return mechTagMechListVersionID;
        }
        public async Task UpdateMechTagMechListVersionAsync(MechTagMechListVersion mechTagMechListVersion)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism_TagMechanism_List_Version SET mechanism_uuid = @mechanism_uuid, tag_mechanism_uuid = @tag_mechanism_uuid, frozen_version = @frozen_version, action = @action, user_uuid = @user_uuid, datetime = @datetime, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", mechTagMechListVersion.uuid);
            command.Parameters.AddWithValue("@mechanism_uuid", mechTagMechListVersion.mechanism_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", mechTagMechListVersion.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@frozen_version", mechTagMechListVersion.frozen_version);
            command.Parameters.AddWithValue("@action", mechTagMechListVersion.action);
            command.Parameters.AddWithValue("@user_uuid", mechTagMechListVersion.user_uuid);
            command.Parameters.AddWithValue("@datetime", mechTagMechListVersion.datetime);
            command.Parameters.AddWithValue("@isDel", mechTagMechListVersion.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteMechTagMechListVersionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Mechanism_TagMechanism_List_Version SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<MechTagMechListVersion>> ReadAllAsync(DbDataReader reader)
        {
            var mechTagMechListVersion = new List<MechTagMechListVersion>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var familyMechVersion = new MechTagMechListVersion
                    {
                        uuid = reader.GetGuid(0),
                        mechanism_uuid = reader.GetGuid(1),
                        tag_mechanism_uuid = reader.GetGuid(2),
                        frozen_version = reader.GetString(3),
                        action = reader.GetString(4),
                        user_uuid = reader.GetGuid(5),
                        datetime = reader.GetDateTime(6),
                        isDel = reader.GetBoolean(7),
                    };
                    mechTagMechListVersion.Add(familyMechVersion);
                }
            }
            return mechTagMechListVersion;
        }
    }
}
