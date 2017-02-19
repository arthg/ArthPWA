using NUnit.Framework;
using Nancy.Testing;
using Nancy;
using SharpTestsEx;
using ArthPWA.Api.Weights;

namespace ArthPWA.Tests.Api.Weights
{
    [TestFixture]
    public class WeightsModuleTests
    {
        private Browser _browser;

        [SetUp]
        public void PrepareTestBrowser()
        {
            _browser = new Browser(with => with.Module(new WeightsModule()));
        }

        public sealed class GetWeightsUrl : WeightsModuleTests
        {
            [Test]
            public void Should_return_status_code_Ok()
            {
                //act
                var response = _browser.Get("/weights");

                //assert
                response.StatusCode.Should().Be.EqualTo(HttpStatusCode.OK);
            }
        }

        public sealed class PostWeightsUrl : WeightsModuleTests
        {
            [Test]
            public void Should_return_status_code_Created()
            {
                //act
                var response = _browser.Post("/weights", with =>             
                    with.Header("Accept", "application/json"));

                //assert
                response.StatusCode.Should().Be.EqualTo(HttpStatusCode.Created);
            }
        }
    }
}
