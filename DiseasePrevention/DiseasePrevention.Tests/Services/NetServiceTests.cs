using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services
{
    public class NetServiceTests
    {
        private readonly ITestOutputHelper _output;
        
        public NetServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
        }

        private readonly HttpClient _httpClient;

        private readonly Uri _uri = new Uri(@"http://61.57.41.133/rss.aspx?v=0&type=news");


        [Fact]
        public async void GetContentLengthAsyncTest()
        {
            // Arrange
            var netService = new NetService(_httpClient);

            // Act
            var result = await netService.GetContentLengthAsync(_uri);

            // Assert
            Assert.True(result > 0);

            this._output.WriteLine($"內容長度:{result}");
        }

        [Fact]
        public async void GetStringAsyncTest()
        {
            // Arrange
            var netService = new NetService(_httpClient);

            // Act
            var result = await netService.GetStringAsync(_uri);

            // Assert
            Assert.False(string.IsNullOrEmpty(result));

            this._output.WriteLine(result);
        }

        [Fact]
        public async void GetEncodingStringAsyncTest()
        {
            // Arrange
            var netService = new NetService(_httpClient);

            // Act
            var result = await netService.GetEncodingStringAsync(_uri);

            // Assert
            Assert.False(string.IsNullOrEmpty(result));

            this._output.WriteLine(result);
        }

        [Fact]
        public async void GetEncodingStringContentAsyncTest()
        {
            // Arrange
            var netService = new NetService(_httpClient);

            // Act
            var result = await netService.GetEncodingStringContentAsync(_uri);

            // Assert
            Assert.True(result.ResponsLength > 0);
            Assert.False(string.IsNullOrEmpty(result.Content));

            this._output.WriteLine($"內容長度:{result.ResponsLength}");
            this._output.WriteLine(result.Content);
        }
    }
}
