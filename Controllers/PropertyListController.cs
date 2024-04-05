using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyListController : ControllerBase
    {
        private PropertyListService userpreferencesService;

        //Injects sql data source setup in Program.cs
        public PropertyListController([FromServices] MySqlDataSource db)
        {
            this.userpreferencesService = new PropertyListService(db);
        }

        // GET: api/PropertyList/all
        [HttpGet("all")]
        public async Task<IReadOnlyList<PropertyList>> Get()
        {
            return await userpreferencesService.GetPropertyListsAsync();
        }

        // GET api/PropertyList/5
        [HttpGet("{uuid}")]
        public async Task<PropertyList?> Get(Guid uuid)
        {
            return await userpreferencesService.GetPropertyListAsync(uuid);
        }

        // POST api/PropertyList/create
        [HttpPost("create")]
        public async Task<Guid> Create([FromBody] PropertyList userPreferneces)
        {
            return await userpreferencesService.CreatePropertyListAsync(userPreferneces);
        }

        // PUT api/PropertyList/5
        [HttpPut("update")]
        public async Task Put([FromBody] PropertyList userpreferences)
        {
            await userpreferencesService.UpdatePropertyListAsync(userpreferences);
        }

        // DELETE api/PropertyList/delete/5
        [HttpDelete("delete/{uuid}")]
        public async Task Delete(Guid uuid)
        {
            await userpreferencesService.DeletePropertyListAsync(uuid);
        }
    }
}
