using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanismController : ControllerBase
    {
        private MechanismService mechanismService;

        //Injects sql data source setup in Program.cs
        public MechanismController([FromServices] MySqlDataSource db)
        {
            this.mechanismService = new MechanismService(db);
        }

        // GET: api/Mechanism/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<Mechanism>> Get()
        {
            return await mechanismService.GetMechanismsAsync();
        }

        // GET api/Mechanism/5
        [HttpGet("{uuid}")]
        public async Task<Mechanism?> Get(Guid uuid)
        {
            return await mechanismService.GetMechanismAsync(uuid);
        }

        // POST api/Mechanism/create
        [HttpPost("create")]
        public async Task Create([FromBody] string name)
        {
            await mechanismService.CreateMechanismAsync(name);
        }

        // PUT api/Mechanism/5
        [HttpPut("update")]
        public async Task Put([FromBody] Mechanism mechanism)
        {
            await mechanismService.UpdateMechanismAsync(mechanism);
        }

        // DELETE api/Mechanism/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await mechanismService.DeleteMechanismAsync(uuid);
        }
    }
}
