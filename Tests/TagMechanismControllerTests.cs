using Chemistry_Cafe_API.Controllers;
using Chemistry_Cafe_API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlConnector;

namespace Chemistry_Cafe_API.Tests
{
    [TestClass]
    public class TagMechanismControllerTests
    {
        readonly MySqlDataSource db = new MySqlDataSource("Server=chemisty-cafe.cl8uuceq2rud.us-east-1.rds.amazonaws.com;User ID=cafeadmin;Password=cafeadmin;Port=3306;Database=Testing");
        
        [TestMethod]
        public async Task Get_retrieves_tagmechanism()
        {
            var controller = new TagMechanismController(db);

            var result = await controller.Get() as List<TagMechanism>;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Creates_tagmechanism()
        {
            var controller = new TagMechanismController(db);

            var result = await controller.Create("Test") ;

            var getResult = await controller.Get(result);

            Assert.AreEqual(result, getResult.uuid);
        }

        [TestMethod]
        public async Task Updates_tagmechanism()
        {
            var controller = new TagMechanismController(db);

            var result = await controller.Create("Test");

            var getResult = await controller.Get(result);

            getResult.tag = "Edited";

            await controller.Put(getResult);

            var getEditedResult = await controller.Get(result);

            await controller.Delete(result);

            Assert.AreEqual(getResult.tag, "Edited");
        }
    }
}
