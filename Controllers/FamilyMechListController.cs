﻿using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMechListController : ControllerBase
    {
        private FamilyMechListService familyMechListService;
        
        //Injects sql data source setup in Program.cs
        public FamilyMechListController([FromServices] MySqlDataSource db)
        {
            this.familyMechListService = new FamilyMechListService(db);
        }

        // GET: api/FamilyMechList/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<FamilyMechList>> Get()
        {
            return await familyMechListService.GetFamilyMechListsAsync();
        }

        // GET api/FamilyMechList/5
        [HttpGet("{uuid}")]
        public async Task<FamilyMechList?> Get(Guid uuid)
        {
            return await familyMechListService.GetFamilyMechListAsync(uuid);
        }

        // POST api/FamilyMechList/create
        [HttpPost("create")]
        public async Task Create([FromBody] FamilyMechList newFamilyMechList)
        {
            await familyMechListService.CreateFamilyMechListAsync(newFamilyMechList);
        }

        // PUT api/FamilyMechList/5
        [HttpPut("update")]
        public async Task Put([FromBody] FamilyMechList newFamilyMechList)
        {
            await familyMechListService.UpdateFamilyMechListAsync(newFamilyMechList);
        }

        // DELETE api/FamilyMechList/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await familyMechListService.DeleteFamilyMechListAsync(uuid);
        }
    }
}
