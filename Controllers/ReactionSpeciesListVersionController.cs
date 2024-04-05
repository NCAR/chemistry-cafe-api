using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionSpeciesListVersionController : ControllerBase
    {
        private ReactionSpeciesListVersionService reactionSpeciesListVersionService;
        
        //Injects sql data source setup in Program.cs
        public ReactionSpeciesListVersionController([FromServices] MySqlDataSource db)
        {
            this.reactionSpeciesListVersionService = new ReactionSpeciesListVersionService(db);
        }

        // GET: api/ReactionSpeciesListVersion/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<ReactionSpeciesListVersion>> Get()
        {
            return await reactionSpeciesListVersionService.GetReactionSpeciesListVersionsAsync();
        }

        // GET api/ReactionSpeciesListVersion/5
        [HttpGet("{uuid}")]
        public async Task<ReactionSpeciesListVersion?> Get(Guid uuid)
        {
            return await reactionSpeciesListVersionService.GetReactionSpeciesListVersionAsync(uuid);
        }

        // POST api/ReactionSpeciesListVersion/create
        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] ReactionSpeciesListVersion newReactionSpeciesListVersion)
        {
            return await reactionSpeciesListVersionService.CreateReactionSpeciesListVersionAsync(newReactionSpeciesListVersion);
        }

        // PUT api/ReactionSpeciesListVersion/5
        [HttpPut("update")]
        public async Task Put([FromBody] ReactionSpeciesListVersion newReactionSpeciesListVersion)
        {
            await reactionSpeciesListVersionService.UpdateReactionSpeciesListVersionAsync(newReactionSpeciesListVersion);
        }

        // DELETE api/ReactionSpeciesListVersion/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await reactionSpeciesListVersionService.DeleteReactionSpeciesListVersionAsync(uuid);
        }
    }
}
