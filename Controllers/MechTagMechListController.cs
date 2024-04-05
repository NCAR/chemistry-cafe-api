using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechTagMechListController : ControllerBase
    {
        private MechTagMechListService mechTagMechService;
        
        //Injects sql data source setup in Program.cs
        public MechTagMechListController([FromServices] MySqlDataSource db)
        {
            this.mechTagMechService = new MechTagMechListService(db);
        }

        // GET: api/MechTagMech/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<MechTagMechList>> Get()
        {
            return await mechTagMechService.GetMechTagMechsAsync();
        }

        // GET api/MechTagMech/5
        [HttpGet("{uuid}")]
        public async Task<MechTagMechList?> Get(Guid uuid)
        {
            return await mechTagMechService.GetMechTagMechAsync(uuid);
        }

        // POST api/MechTagMech/create
        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] MechTagMechList newMechTagMech)
        {
            return await mechTagMechService.CreateMechTagMechAsync(newMechTagMech);
        }

        // PUT api/MechTagMech/5
        [HttpPut("update")]
        public async Task Put([FromBody] MechTagMechList newMechTagMech)
        {
            await mechTagMechService.UpdateMechTagMechAsync(newMechTagMech);
        }

        // DELETE api/MechTagMech/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await mechTagMechService.DeleteMechTagMechAsync(uuid);
        }
    }
}
