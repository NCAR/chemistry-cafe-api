using Chemistry_Cafe_API.Controllers;
using Chemistry_Cafe_API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlConnector;

namespace Chemistry_Cafe_API.Tests
{
    [TestClass]
    public class FamilyControllerTests
    {
        readonly MySqlDataSource db = new MySqlDataSource("Server=chemisty-cafe.cl8uuceq2rud.us-east-1.rds.amazonaws.com;User ID=cafeadmin;Password=cafeadmin;Port=3306;Database=Testing");
        
        [TestMethod]
        public async Task Get_retrieves_family()
        {
            var controller = new FamilyController(db);

            var result = await controller.Get() as List<Family>;

            Assert.IsNotNull(result);
        }
    }
}
