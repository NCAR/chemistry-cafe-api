using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechTagMechListVersionController : ControllerBase
    {
        private MechTagMechListVersionService mechTagMechListVersionService;
        
        //Injects sql data source setup in Program.cs
        public MechTagMechListVersionController([FromServices] MySqlDataSource db)
        {
            this.mechTagMechListVersionService = new MechTagMechListVersionService(db);
        }

        // GET: api/MechTagMechListVersion/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<MechTagMechListVersion>> Get()
        {
            return await mechTagMechListVersionService.GetMechTagMechListVersionsAsync();
        }

        // GET api/MechTagMechListVersion/5
        [HttpGet("{uuid}")]
        public async Task<MechTagMechListVersion?> Get(Guid uuid)
        {
            return await mechTagMechListVersionService.GetMechTagMechListVersionAsync(uuid);
        }

        // POST api/MechTagMechListVersion/create
        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] MechTagMechListVersion newMechTagMechListVersion)
        {
            return await mechTagMechListVersionService.CreateMechTagMechListVersionAsync(newMechTagMechListVersion);
        }

        // PUT api/MechTagMechListVersion/5
        [HttpPut("update")]
        public async Task Put([FromBody] MechTagMechListVersion newMechTagMechListVersion)
        {
            await mechTagMechListVersionService.UpdateMechTagMechListVersionAsync(newMechTagMechListVersion);
        }

        // DELETE api/MechTagMechListVersion/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await mechTagMechListVersionService.DeleteMechTagMechListVersionAsync(uuid);
        }
    }
}
