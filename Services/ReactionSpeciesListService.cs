using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class ReactionSpeciesListService (MySqlDataSource database)
    {
        public async Task<IReadOnlyList<ReactionSpeciesList>> GetReactionSpeciesListsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Reaction_Species_List";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<ReactionSpeciesList?> GetReactionSpeciesListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Reaction_Species_List WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreateReactionSpeciesListAsync(ReactionSpeciesList newReactionSpeciesList)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid reactionSpeciesListID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Reaction_Species_List (uuid, reaction_uuid, species_uuid, version) VALUES (@uuid, @reaction_uuid, @species_uuid, @version);";

            command.Parameters.AddWithValue("@uuid", reactionSpeciesListID);
            command.Parameters.AddWithValue("@reaction_uuid", newReactionSpeciesList.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", newReactionSpeciesList.species_uuid);
            command.Parameters.AddWithValue("@version", newReactionSpeciesList.version);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateReactionSpeciesListAsync(ReactionSpeciesList reactionSpeciesList)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction_Species_List SET reaction_uuid = @reaction_uuid, species_uuid = @species_uuid, version = @version, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", reactionSpeciesList.uuid);
            command.Parameters.AddWithValue("@reaction_uuid", reactionSpeciesList.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", reactionSpeciesList.species_uuid);
            command.Parameters.AddWithValue("@version", reactionSpeciesList.version);
            command.Parameters.AddWithValue("@isDel", reactionSpeciesList.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteReactionSpeciesListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction_Species_List SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<ReactionSpeciesList>> ReadAllAsync(DbDataReader reader)
        {
            var reactionSpeciesList = new List<ReactionSpeciesList>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var reactionSpecies = new ReactionSpeciesList
                    {
                        uuid = reader.GetGuid(0),
                        reaction_uuid = reader.GetGuid(1),
                        species_uuid = reader.GetGuid(2),
                        version = reader.GetString(3),
                        isDel = reader.GetBoolean(4),
                    };
                    reactionSpeciesList.Add(reactionSpecies);
                }
            }
            return reactionSpeciesList;
        }
    }
}
