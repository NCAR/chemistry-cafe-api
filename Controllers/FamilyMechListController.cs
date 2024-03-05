using Chemistry_Cafe_API.Models;
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
        private FamilyMechListService familyService;
        
        //Injects sql data source setup in Program.cs
        public FamilyMechListController([FromServices] MySqlDataSource db)
        {
            this.familyService = new FamilyMechListService(db);
        }

        // GET: api/FamilyMechList/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<FamilyMechList>> Get()
        {
            return await familyService.GetFamiliesAsync();
        }

        // GET api/Family/5
        [HttpGet("{uuid}")]
        public async Task<FamilyMechList?> GetFamilyAsync(Guid uuid)
        {
            return await familyService.GetFamilyMechListAsync(uuid);
        }

        // POST api/Family/create
        [HttpPost("create")]
        public async Task CreateFamily([FromBody] FamilyMechList newFamilyMechList)
        {
            await familyService.CreateFamilyMechListAsync(newFamilyMechList);
        }

        // PUT api/Family/5
        [HttpPut("update")]
        public async Task Put([FromBody] FamilyMechList newFamilyMechList)
        {
            await familyService.UpdateFamilyMechListAsync(newFamilyMechList);
        }

        // DELETE api/Family/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await familyService.DeleteFamilyMechListAsync(uuid);
        }
    }
}
