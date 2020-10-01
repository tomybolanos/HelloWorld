using Xunit;


namespace HelloWorld.API.UnitTest
{
    public class HelloWorldControllerUnitTest
    {
        [Fact]
        public void TestHelloWorld()
        {
            var controller = new WebAPI.Controllers.HelloWorldController();
            var response = controller.Get();

            Assert.True(response == "Hello World");
        }
    }
}
