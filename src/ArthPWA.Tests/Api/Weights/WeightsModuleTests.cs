using NUnit.Framework;
using Nancy.Testing;
using Nancy;
using SharpTestsEx;
using ArthPWA.Api.Weights;
using ArthPWA.Tests.Common.Helpers;
using Moq;

namespace ArthPWA.Tests.Api.Weights
{
    [TestFixture]
    public class WeightsModuleTests
    {
        private Browser _browser;
        private Mock<IWeightsService> _weightsService;

        [SetUp]
        public void PrepareTestBrowser()
        {
            _weightsService = new Mock<IWeightsService>(MockBehavior.Strict);
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Module<WeightsModule>();
                with.Dependencies(_weightsService.Object);
            });
            _browser = new Browser(bootstrapper);
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
            public void Should_call_service_and_return_status_code_Created()
            {
                //arrange
                var newId = RandomData.GetString(10);
                _weightsService
                    .Setup(s => s.Create())
                    .Returns(newId);

                //act
                var response = _browser.Post("/weights", with =>             
                    with.Header("Accept", "application/json"));

                //assert
                response.StatusCode.Should().Be.EqualTo(HttpStatusCode.Created);
                response.Headers["Location"].Should().Be.EqualTo("/weights/" + newId);
                ((string)response.BodyAsDynamic().id).Should().Be.EqualTo(newId);
            }
        }
    }
}
