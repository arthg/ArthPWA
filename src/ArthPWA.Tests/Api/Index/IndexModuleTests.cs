using NUnit.Framework;
using Nancy.Testing;
using Nancy;
using SharpTestsEx;
using ArthPWA.Api.Index;

namespace ArthPWA.Tests.Api.Index
{
    [TestFixture]
    public class IndexModuleTests
    {
        private Browser _browser;
        
        [SetUp]
        public void PrepareTestBrowser()
        {          
            _browser = new Browser(with => with.Module(new IndexModule()));
        }

        public sealed class RootUrl : IndexModuleTests
        {
            [Test]
            public void Should_return_status_code_200()
            {
                //act
                var response = _browser.Get("/");

                //assert
                response.StatusCode.Should().Be.EqualTo(HttpStatusCode.OK);
            }
        }
    }
}
