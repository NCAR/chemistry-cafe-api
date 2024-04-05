using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionSpeciesListController : ControllerBase
    {
        private ReactionSpeciesListService reactionSpeciesListService;
        
        //Injects sql data source setup in Program.cs
        public ReactionSpeciesListController([FromServices] MySqlDataSource db)
        {
            this.reactionSpeciesListService = new ReactionSpeciesListService(db);
        }

        // GET: api/ReactionSpeciesList/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<ReactionSpeciesList>> Get()
        {
            return await reactionSpeciesListService.GetReactionSpeciesListsAsync();
        }

        // GET api/ReactionSpeciesList/5
        [HttpGet("{uuid}")]
        public async Task<ReactionSpeciesList?> Get(Guid uuid)
        {
            return await reactionSpeciesListService.GetReactionSpeciesListAsync(uuid);
        }

        // POST api/ReactionSpeciesList/create
        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] ReactionSpeciesList newReactionSpeciesList)
        {
            return await reactionSpeciesListService.CreateReactionSpeciesListAsync(newReactionSpeciesList);
        }

        // PUT api/ReactionSpeciesList/5
        [HttpPut("update")]
        public async Task Put([FromBody] ReactionSpeciesList newReactionSpeciesList)
        {
            await reactionSpeciesListService.UpdateReactionSpeciesListAsync(newReactionSpeciesList);
        }

        // DELETE api/ReactionSpeciesList/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await reactionSpeciesListService.DeleteReactionSpeciesListAsync(uuid);
        }
    }
}
