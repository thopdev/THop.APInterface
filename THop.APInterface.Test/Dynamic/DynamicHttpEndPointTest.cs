using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ImpromptuInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using THop.APInterface.Dynamic;
using THop.APInterface.Services;
using Xunit;
using Xunit.Abstractions;

namespace THop.APInterface.Test.Dynamic
{
    public class DynamicHttpEndpointTest
    {

        private readonly Mock<IHttpClientService> _httpClientMock;

        public DynamicHttpEndpointTest()
        {
            _httpClientMock = new Mock<IHttpClientService>(MockBehavior.Strict);
        }



        [Fact]
        public async Task TestSimpleGetFunctionAsync()
        {
            var dynamicHttpEndpoint = new DynamicHttpEndpoint(_httpClientMock.Object, typeof(ITestEndpoint)).ActLike<ITestEndpoint>();

            _httpClientMock.Setup(setup => setup.GetRequestAsync<object>(It.IsAny<string>())).ReturnsAsync(null).Verifiable();

            await dynamicHttpEndpoint.Get();

            _httpClientMock.Verify(x => x.GetRequestAsync<object>("Test"), Times.Once);
        }

        [Fact]
        public async Task TestSimplePostFunctionAsync()
        {
            var dynamicHttpEndpoint = new DynamicHttpEndpoint(_httpClientMock.Object, typeof(ITestEndpoint)).ActLike<ITestEndpoint>();

            _httpClientMock.Setup(setup => setup.PostRequestAsync<object, object>(It.IsAny<string>(), It.IsAny<object>())).ReturnsAsync(null).Verifiable();

            var obj = "emptyObject";
            await dynamicHttpEndpoint.Post(obj);

            _httpClientMock.Verify(x => x.PostRequestAsync<object, object>("Test", obj), Times.Once);
        }

        [Fact]
        public async Task TestSimpleDeleteFunctionAsync()
        {
            var dynamicHttpEndpoint = new DynamicHttpEndpoint(_httpClientMock.Object, typeof(ITestEndpoint)).ActLike<ITestEndpoint>();

            _httpClientMock.Setup(setup => setup.DeleteRequestAsync(It.IsAny<string>())).Returns(Task.CompletedTask).Verifiable();

            await dynamicHttpEndpoint.Delete();

            _httpClientMock.Verify(x => x.DeleteRequestAsync("Test"), Times.Once);
        }

        [Fact]
        public async Task TestWithExtraRouteAsync()
        {
            var dynamicHttpEndpoint = new DynamicHttpEndpoint(_httpClientMock.Object, typeof(ITestEndpoint)).ActLike<ITestEndpoint>();

            _httpClientMock.Setup(setup => setup.GetRequestAsync<object>(It.IsAny<string>())).ReturnsAsync(null).Verifiable();

            await dynamicHttpEndpoint.Get("5");

            _httpClientMock.Verify(x => x.GetRequestAsync<object>("Test/5"), Times.Once);
        }

        [Fact]
        public async Task TestWithSimpleQueryAsync()
        {
            var dynamicHttpEndpoint = new DynamicHttpEndpoint(_httpClientMock.Object, typeof(ITestEndpoint)).ActLike<ITestEndpoint>();

            _httpClientMock.Setup(setup => setup.GetRequestAsync<object>(It.IsAny<string>())).ReturnsAsync(null).Verifiable();

            await dynamicHttpEndpoint.Get(5);

            _httpClientMock.Verify(x => x.GetRequestAsync<object>("Test?number=5"), Times.Once);
        }

    }

    public interface ITestEndpoint
    {
        [HttpGet]
        Task<object> Get();

        [HttpGet("{id}")]
        Task<object> Get(string id);

        [HttpGet]
        Task<object> Get([FromQuery] int number);

        [HttpPost]
        Task<object> Post([FromBody]object obj);

        [HttpDelete]
        Task Delete();
    }



}
