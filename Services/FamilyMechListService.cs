﻿using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class FamilyMechListService (MySqlDataSource database)
    {
        public async Task<IReadOnlyList<FamilyMechList>> GetFamilyMechListsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Family_Mechanism_List";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<FamilyMechList?> GetFamilyMechListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Family_Mechanism_List WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task<Guid> CreateFamilyMechListAsync(FamilyMechList newFamilyMechList)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid familyMechListID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Family_Mechanism_List (uuid, family_uuid, tag_mechanism_uuid, version) VALUES (@uuid, @family_uuid, @tag_mechanism_uuid, @version);";

            command.Parameters.AddWithValue("@uuid", familyMechListID);
            command.Parameters.AddWithValue("@family_uuid", newFamilyMechList.family_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", newFamilyMechList.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@version", newFamilyMechList.version);

            await command.ExecuteNonQueryAsync();

            return familyMechListID;
        }
        public async Task UpdateFamilyMechListAsync(FamilyMechList familyMechList)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Family_Mechanism_List SET family_uuid = @family_uuid, tag_mechanism_uuid = @tag_mechanism_uuid, version = @version, isDel = @isDel WHERE uuid = @uuid;";
            
            command.Parameters.AddWithValue("@uuid", familyMechList.uuid);
            command.Parameters.AddWithValue("@family_uuid", familyMechList.family_uuid);
            command.Parameters.AddWithValue("@tag_mechanism_uuid", familyMechList.tag_mechanism_uuid);
            command.Parameters.AddWithValue("@version", familyMechList.version);
            command.Parameters.AddWithValue("@isDel", familyMechList.isDel);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteFamilyMechListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Family_Mechanism_List SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);
            
            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<FamilyMechList>> ReadAllAsync(DbDataReader reader)
        {
            var familyMechList = new List<FamilyMechList>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var familyMech = new FamilyMechList
                    {
                        uuid = reader.GetGuid(0),
                        family_uuid = reader.GetGuid(1),
                        tag_mechanism_uuid = reader.GetGuid(2),
                        version = reader.GetString(3),
                        isDel = reader.GetBoolean(4),
                    };
                    familyMechList.Add(familyMech);
                }
            }
            return familyMechList;
        }
    }
}
