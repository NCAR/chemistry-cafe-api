using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagMechanismController : ControllerBase
    {
        private TagMechanismService tagTagMechanismService;

        //Injects sql data source setup in Program.cs
        public TagMechanismController([FromServices] MySqlDataSource db)
        {
            this.tagTagMechanismService = new TagMechanismService(db);
        }

        // GET: api/TagMechanism/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<TagMechanism>> Get()
        {
            return await tagTagMechanismService.GetTagMechanismsAsync();
        }

        // GET api/TagMechanism/5
        [HttpGet("{uuid}")]
        public async Task<TagMechanism?> Get(Guid uuid)
        {
            return await tagTagMechanismService.GetTagMechanismAsync(uuid);
        }

        // POST api/TagMechanism/create
        [HttpPost("create")]
        public async Task Create([FromBody] string tag)
        {
            await tagTagMechanismService.CreateTagMechanismAsync(tag);
        }

        // PUT api/TagMechanism/5
        [HttpPut("update")]
        public async Task Put([FromBody] TagMechanism tagMechanism)
        {
            await tagTagMechanismService.UpdateTagMechanismAsync(tagMechanism);
        }

        // DELETE api/TagMechanism/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await tagTagMechanismService.DeleteTagMechanismAsync(uuid);
        }
    }
}
