using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;


namespace Chemistry_Cafe_API.Services
{
    public class ReactantProductListService(MySqlDataSource database)
    {
        public async Task<IReadOnlyList<ReactantProductList>> GetReactantProductListsAsync()
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Reactant_Product_List";
            return await ReadAllAsync(await command.ExecuteReaderAsync());
        }

        public async Task<ReactantProductList?> GetReactantProductListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"SELECT * FROM Reactant_Product_List WHERE uuid = @id";
            command.Parameters.AddWithValue("@id", uuid);

            var result = await ReadAllAsync(await command.ExecuteReaderAsync());
            return result.FirstOrDefault();
        }

        public async Task CreateReactantProductListAsync(ReactantProductList reactantProduct)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            Guid reactantProductID = Guid.NewGuid();

            command.CommandText = @"INSERT INTO Reactant_Product_List (uuid, reaction_uuid, species_uuid, quantity) VALUES (@uuid, @reaction_uuid, @species_uuid, @quantity);";

            command.Parameters.AddWithValue("@uuid", reactantProductID);
            command.Parameters.AddWithValue("@reaction_uuid", reactantProduct.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", reactantProduct.species_uuid);
            command.Parameters.AddWithValue("@quantity", reactantProduct.quantity);

            await command.ExecuteNonQueryAsync();
        }
        public async Task UpdateReactantProductListAsync(ReactantProductList reactantProduct)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reactant_Product_List SET reaction_uuid = @reaction_uuid, quantity = @quantity, isDel = @isDel WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", reactantProduct.reactant_product_uuid);
            command.Parameters.AddWithValue("@reaction_uuid", reactantProduct.reaction_uuid);
            command.Parameters.AddWithValue("@species_uuid", reactantProduct.species_uuid);
            command.Parameters.AddWithValue("@quantity", reactantProduct.quantity);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteReactantProductListAsync(Guid uuid)
        {
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            command.CommandText = @"UPDATE Reactant_Product_List SET isDel = 1 WHERE uuid = @uuid;";

            command.Parameters.AddWithValue("@uuid", uuid);

            await command.ExecuteNonQueryAsync();
        }

        private async Task<IReadOnlyList<ReactantProductList>> ReadAllAsync(DbDataReader reader)
        {
            var reactantProductList = new List<ReactantProductList>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var property = new ReactantProductList
                    {
                        reactant_product_uuid = reader.GetGuid(0),
                        reaction_uuid = reader.GetGuid(1),
                        species_uuid = reader.GetGuid(2),
                        quantity = reader.GetInt32(3)
                    };
                    reactantProductList.Add(property);
                }
            }
            return reactantProductList;
        }
    }
}
