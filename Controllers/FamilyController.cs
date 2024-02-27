using Chemistry_Cafe_API.Models;
using Chemistry_Cafe_API.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chemistry_Cafe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        private FamilyService familyService;
        
        //Injects sql data source setup in Program.cs
        public FamilyController([FromServices] MySqlDataSource db)
        {
            this.familyService = new FamilyService(db);
        }
        

        //TEMPLATE CODE, UNIMPLEMENTED FUNCTIONS BELOW

        // GET: api/Family
        [HttpGet]
        public async Task<IReadOnlyList<Family>> Get()
        {
            return await familyService.GetFamiliesAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
