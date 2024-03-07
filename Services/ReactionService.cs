﻿using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;


namespace Chemistry_Cafe_API.Services
{
    public class ReactionService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<Reaction>> GetReactionsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Reaction";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<Reaction?> GetReactionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Reaction WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreateReactionAsync(string type)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid reactionID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Reaction (uuid, type) VALUES (@uuid, @type);";

            command.Parameters.AddWithValue("@uuid", reactionID);
            command.Parameters.AddWithValue("@type", type);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateReactionAsync(Reaction reaction)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction SET type = @type, isDel = @isDel WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", reaction.uuid);
            command.Parameters.AddWithValue("@type", reaction.type);
            command.Parameters.AddWithValue("@isDel", reaction.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteReactionAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reaction SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);

            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<Reaction>> ReadAllAsync(DbDataReader reader)
        {
            var reactions = new List<Reaction>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var reaction = new Reaction
                    {
                        uuid = reader.GetGuid(0),
                        type = reader.GetString(1),
                        isDel = reader.GetBoolean(2),
                    };
                    reactions.Add(reaction);
                }
            }
            return reactions;
        }
    }
}
