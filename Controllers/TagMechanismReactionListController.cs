using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagMechanismReactionListController : ControllerBase
    {
        private TagMechanismReactionListService mechTagMechService;
        
        //Injects sql data source setup in Program.cs
        public TagMechanismReactionListController([FromServices] MySqlDataSource db)
        {
            this.mechTagMechService = new TagMechanismReactionListService(db);
        }

        // GET: api/TagMechanismReaction/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<TagMechanismReactionList>> Get()
        {
            return await mechTagMechService.GetTagMechanismReactionsAsync();
        }

        // GET api/TagMechanismReaction/5
        [HttpGet("{uuid}")]
        public async Task<TagMechanismReactionList?> Get(Guid uuid)
        {
            return await mechTagMechService.GetTagMechanismReactionAsync(uuid);
        }

        // POST api/TagMechanismReaction/create
        [HttpPost("create")]
        public async Task Create([FromBody] TagMechanismReactionList newTagMechanismReaction)
        {
            await mechTagMechService.CreateTagMechanismReactionAsync(newTagMechanismReaction);
        }

        // PUT api/TagMechanismReaction/5
        [HttpPut("update")]
        public async Task Put([FromBody] TagMechanismReactionList newTagMechanismReaction)
        {
            await mechTagMechService.UpdateTagMechanismReactionAsync(newTagMechanismReaction);
        }

        // DELETE api/TagMechanismReaction/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await mechTagMechService.DeleteTagMechanismReactionAsync(uuid);
        }
    }
}
