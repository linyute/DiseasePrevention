using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Serums;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services.Serums
{
    public class SerumServiceTests
    {
        private readonly ITestOutputHelper _output;

        public SerumServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
            this._netService = new NetService(_httpClient);
        }

        private readonly HttpClient _httpClient;

        private readonly NetService _netService;

        [Fact]
        public async void GetVaccineHospitalsAsyncTest()
        {
            // Arrange
            var service = new SerumService(_netService);

            // Act
            var items = await service.GetSerumHospitalsAsync();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().醫療院所名稱);
        }
    }
}
