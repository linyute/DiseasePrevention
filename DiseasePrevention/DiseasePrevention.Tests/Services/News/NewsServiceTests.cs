using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using DiseasePrevention.Services.News;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services.News
{
    public class NewsServiceTests
    {
        private readonly ITestOutputHelper _output;

        public NewsServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
            this._netService = new NetService(_httpClient);
        }

        private readonly HttpClient _httpClient;

        private readonly NetService _netService;

        [Fact]
        public async void GetRssReedsAsyncTest()
        {
            // Arrange
            var newsService = new NewsService(_netService);

            // Act
            var items = await newsService.GetRssReedsAsync();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Title);
        }
    }
}
