using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Travels;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services.Travels
{
    public class TravelServiceTests
    {
        private readonly ITestOutputHelper _output;

        public TravelServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
            this._netService = new NetService(_httpClient);
        }

        private readonly HttpClient _httpClient;

        private readonly NetService _netService;

        [Fact]
        public async void GetTravelAlertInfoAsyncTest()
        {
            // Arrange
            var newsService = new TravelService(_netService);

            // Act
            var items = await newsService.GetTravelAlertsAsync("國際重要疫情");

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Headline);
        }
    }
}
