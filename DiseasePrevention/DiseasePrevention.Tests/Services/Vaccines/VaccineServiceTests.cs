using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using DiseasePrevention.Services.Vaccines;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services.Vaccines
{
    public class VaccineServiceTests
    {
        private readonly ITestOutputHelper _output;

        public VaccineServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
            this._netService = new NetService(_httpClient);
        }

        private readonly HttpClient _httpClient;

        private readonly NetService _netService;

        [Fact]
        public void GetAdultVaccinesTest()
        {
            // Arrange
            var service = new VaccineService(_netService);

            // Act
            var items = service.GetAdultVaccines();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Vaccine);
        }

        [Fact]
        public async void GetVaccineHospitalsAsyncTest()
        {
            // Arrange
            var service = new VaccineService(_netService);

            // Act
            var items = await service.GetVaccineHospitalsAsync();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().合約醫療院所名稱);
        }
    }
}
