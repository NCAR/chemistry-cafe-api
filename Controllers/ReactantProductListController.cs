using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactantProductListController : ControllerBase
    {
        private ReactantProductListService userpreferencesService;

        //Injects sql data source setup in Program.cs
        public ReactantProductListController([FromServices] MySqlDataSource db)
        {
            this.userpreferencesService = new ReactantProductListService(db);
        }

        // GET: api/ReactantProductList/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<ReactantProductList>> Get()
        {
            return await userpreferencesService.GetReactantProductListsAsync();
        }

        // GET api/ReactantProductList/5
        [HttpGet("{uuid}")]
        public async Task<ReactantProductList?> Get(Guid uuid)
        {
            return await userpreferencesService.GetReactantProductListAsync(uuid);
        }

        // POST api/ReactantProductList/create
        [HttpPost("create")]
        public async Task Create([FromBody] ReactantProductList reactantProduct)
        {
            await userpreferencesService.CreateReactantProductListAsync(reactantProduct);
        }

        // PUT api/ReactantProductList/5
        [HttpPut("update")]
        public async Task Put([FromBody] ReactantProductList userpreferences)
        {
            await userpreferencesService.UpdateReactantProductListAsync(userpreferences);
        }

        // DELETE api/ReactantProductList/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await userpreferencesService.DeleteReactantProductListAsync(uuid);
        }
    }
}
