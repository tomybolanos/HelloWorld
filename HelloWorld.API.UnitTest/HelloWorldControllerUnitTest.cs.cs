using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
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
