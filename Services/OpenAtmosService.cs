﻿using Chemistry_Cafe_API.Models;
using System.Data.Common;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Chemistry_Cafe_API.Services
{
    public class OpenAtmosService(MySqlDataSource database)
    {
        public async Task<string> Get(Guid tag_mechanism_uuid)
        {
            ReactionService reactionService = new ReactionService(database);
            SpeciesService speciesService = new SpeciesService(database);
            TagMechanismService tagMechanismService = new TagMechanismService(database);
            PropertyListService propertyListService = new PropertyListService(database);
            ReactantProductListService reactantProductListService = new ReactantProductListService(database);

            var mechanism = tagMechanismService.GetTagMechanismAsync(tag_mechanism_uuid).Result;
            
            string JSON = "{\n" +
                "  \"version\": \"1.0.0\",\n" +
                "  \"name\": \"" + mechanism.tag + "\", \n";
            
            using var connection = await database.OpenConnectionAsync();
            using var command = connection.CreateCommand();

            var reactionList = reactionService.GetTags(tag_mechanism_uuid).Result;
            var speciesList = speciesService.GetTags(tag_mechanism_uuid).Result;

            JSON += "  \"species\": [ \n";


            foreach ( var species in speciesList )
            {
                JSON += "    {\n";
                JSON += "      \"name\": \"" + species.type + "\", \n";
                var properties = propertyListService.GetPropertiesAsync(species.uuid).Result;
                foreach ( var property in properties )
                {
                    JSON += "      \"" + property.name + " " + property.units + "\": ";

                    if (property.float_value.HasValue)
                    {
                        JSON += property.float_value.ToString();
                    }
                    else if(property.double_value.HasValue)
                    {
                        JSON += property.double_value.ToString();
                    }
                    else if (property.int_value.HasValue)
                    {
                        JSON += property.int_value.ToString();
                    }
                    else if(property.string_value != null)
                    {
                        JSON += property.string_value;
                    }

                    JSON += " , \n";
                }
                JSON.Remove(JSON.LastIndexOf(','));
                JSON += "    }, \n";
            }
            JSON.Remove(JSON.LastIndexOf(','));

            JSON += "  ],\n" +
                "  \"phases\": [ \n" +
                "    { \n" +
                "      \"name\": \"gas\", \n" +
                "      \"species\": [ \n";
            foreach(Species species in speciesList)
            {
                JSON += "        \"" + species.type + "\", \n";
            }
            JSON.Remove(JSON.LastIndexOf(','));
            JSON += "      ] \n" +
                "    } \n" +
                "  ],\n" +
                "  \"reactions\": [ \n";

            foreach (var reaction in reactionList)
            {
                JSON += "    { \n";
                JSON += "      \"type\" : \"" + reaction.type.ToUpper() + "\", \n"; 
                var properties = propertyListService.GetPropertiesAsync(reaction.uuid).Result;
                foreach (var property in properties)
                {
                    if (property.units != null)
                    {
                        JSON += "      \"" + property.name + " " + property.units + "\": ";
                    }
                    else
                    {
                        JSON += "      \"" + property.name + "\": ";
                    }

                    if (property.float_value.HasValue)
                    {
                        JSON += property.float_value.ToString();
                    }
                    else if (property.double_value.HasValue)
                    {
                        JSON += property.double_value.ToString();
                    }
                    else if (property.int_value.HasValue)
                    {
                        JSON += property.int_value.ToString();
                    }
                    else if (property.string_value != null)
                    {
                        JSON += "\"" + property.string_value + "\"";
                    }

                    JSON += " , \n";
                }
                var reactants = reactantProductListService.GetReactantsAsync(reaction.reactant_list_uuid).Result;
                JSON += "      \"reactants\": [ \n" +
                    "        {\n";
                foreach (ReactantsProducts reactant in reactants)
                {
                    JSON += "          \"species name\": \"" + reactant.type + "\", \n";
                    JSON += "          \"coefficient\": \"" + reactant.quantity + "\" \n";
                }
                JSON += "        }\n" +
                    "      ], \n";

                var products = reactantProductListService.GetProductsAsync(reaction.product_list_uuid).Result;
                JSON += "      \"products\": [ \n" +
                    "        {\n";
                foreach (ReactantsProducts product in products)
                {
                    JSON += "          \"species name\": \"" + product.type + "\", \n";
                    JSON += "          \"coefficient\": \"" + product.quantity + "\" \n";
                }
                JSON += "        }\n" +
                    "      ], \n";
                JSON += "    }\n";
            }

            JSON += "  ]\n}";
            return JSON;
        }

    }  
}
