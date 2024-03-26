using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMechListVersionController : ControllerBase
    {
        private FamilyMechListVersionService familyMechListVersionService;
        
        //Injects sql data source setup in Program.cs
        public FamilyMechListVersionController([FromServices] MySqlDataSource db)
        {
            this.familyMechListVersionService = new FamilyMechListVersionService(db);
        }

        // GET: api/FamilyMechListVersion/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<FamilyMechListVersion>> Get()
        {
            return await familyMechListVersionService.GetFamilyMechListVersionsAsync();
        }

        // GET api/FamilyMechListVersion/5
        [HttpGet("{uuid}")]
        public async Task<FamilyMechListVersion?> Get(Guid uuid)
        {
            return await familyMechListVersionService.GetFamilyMechListVersionAsync(uuid);
        }

        // POST api/FamilyMechListVersion/create
        [HttpPost("create")]
        public async Task Create([FromBody] FamilyMechListVersion newFamilyMechListVersion)
        {
            await familyMechListVersionService.CreateFamilyMechListVersionAsync(newFamilyMechListVersion);
        }

        // PUT api/FamilyMechListVersion/5
        [HttpPut("update")]
        public async Task Put([FromBody] FamilyMechListVersion newFamilyMechListVersion)
        {
            await familyMechListVersionService.UpdateFamilyMechListVersionAsync(newFamilyMechListVersion);
        }

        // DELETE api/FamilyMechListVersion/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await familyMechListVersionService.DeleteFamilyMechListVersionAsync(uuid);
        }
    }
}
