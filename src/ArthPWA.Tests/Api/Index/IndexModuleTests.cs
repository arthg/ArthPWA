using NUnit.Framework;
using Nancy.Testing;
using Nancy.ViewEngines;
using Nancy;
using SharpTestsEx;
using ArthPWA.Api.Index;
using Moq;
using System.IO;

namespace ArthPWA.Tests.Api.Index
{
    [TestFixture]
    public class IndexModuleTests
    {
        private Browser _browser;
        private Mock<IViewResolver> _viewResolver;

        [SetUp]
        public void PrepareTestBrowser()
        {
            _viewResolver = new Mock<IViewResolver>();
            _viewResolver.Setup(m => m.GetViewLocation(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<ViewLocationContext>()))
                         .Returns(new ViewLocationResult("", "", "html", () => new StringReader("")));
            _browser = new Browser(with => with.Module(new IndexModule())
                            .ViewFactory<TestingViewFactory>()
                            .ViewResolver(_viewResolver.Object));
        }

        public sealed class RootUrl : IndexModuleTests
        {
            [Test]
            public void Should_return_index_view_and_status_code_Ok()
            {
                //act
                var response = _browser.Get("/");

                //assert
                response.Satisfies(r =>
                   r.StatusCode == HttpStatusCode.OK &&
                   r.GetViewName() == "Web/index.html");
            }
        }
    }
}
