using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        [Fact]
        public void GetAdultVaccinesTest()
        {
            // Arrange
            var service = new VaccineService();

            // Act
            var items = service.GetAdultVaccines();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Vaccine);
        }
    }
}
