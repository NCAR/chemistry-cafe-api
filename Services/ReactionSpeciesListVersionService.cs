using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class ReactionSpeciesListVersionService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<ReactionSpeciesListVersion>> GetReactionSpeciesListVersionsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Reaction_Species_List_Version";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<ReactionSpeciesListVersion?> GetReactionSpeciesListVersionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Reaction_Species_List_Version WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task<Guid> CreateReactionSpeciesListVersionAsync(ReactionSpeciesListVersion newReactionSpeciesListVersion)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid reactionSpeciesListVersionID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Reaction_Species_List_Version (uuid, reaction_uuid, species_uuid, frozen_version, action, user_uuid, datetime) VALUES (@uuid, @reaction_uuid, @species_uuid, @frozen_version, @action, @user_uuid, @datetime);";

            command.Parameters.AddWithValue("@uuid", reactionSpeciesListVersionID);
            command.Parameters.AddWithValue("@reaction_uuid", newReactionSpeciesListVersion.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", newReactionSpeciesListVersion.species_uuid);
            command.Parameters.AddWithValue("@frozen_version", newReactionSpeciesListVersion.frozen_version);
            command.Parameters.AddWithValue("@action", newReactionSpeciesListVersion.action);
            command.Parameters.AddWithValue("@user_uuid", newReactionSpeciesListVersion.user_uuid);
            command.Parameters.AddWithValue("@datetime", newReactionSpeciesListVersion.datetime);

            await command.ExecuteNonQueryAsync();

            return reactionSpeciesListVersionID;
        }
        public async Task UpdateReactionSpeciesListVersionAsync(ReactionSpeciesListVersion reactionSpeciesListVersion)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction_Species_List_Version SET reaction_uuid = @reaction_uuid, species_uuid = @species_uuid, frozen_version = @frozen_version, action = @action, user_uuid = @user_uuid, datetime = @datetime, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", reactionSpeciesListVersion.uuid);
            command.Parameters.AddWithValue("@reaction_uuid", reactionSpeciesListVersion.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", reactionSpeciesListVersion.species_uuid);
            command.Parameters.AddWithValue("@frozen_version", reactionSpeciesListVersion.frozen_version);
            command.Parameters.AddWithValue("@action", reactionSpeciesListVersion.action);
            command.Parameters.AddWithValue("@user_uuid", reactionSpeciesListVersion.user_uuid);
            command.Parameters.AddWithValue("@datetime", reactionSpeciesListVersion.datetime);
            command.Parameters.AddWithValue("@isDel", reactionSpeciesListVersion.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteReactionSpeciesListVersionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction_Species_List_Version SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<ReactionSpeciesListVersion>> ReadAllAsync(DbDataReader reader)
        {
            var reactionSpeciesListVersion = new List<ReactionSpeciesListVersion>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var familyMechVersion = new ReactionSpeciesListVersion
                    {
                        uuid = reader.GetGuid(0),
                        reaction_uuid = reader.GetGuid(1),
                        species_uuid = reader.GetGuid(2),
                        frozen_version = reader.GetString(3),
                        action = reader.GetString(4),
                        user_uuid = reader.GetGuid(5),
                        datetime = reader.GetDateTime(6),
                        isDel = reader.GetBoolean(7),
                    };
                    reactionSpeciesListVersion.Add(familyMechVersion);
                }
            }
            return reactionSpeciesListVersion;
        }
    }
}
