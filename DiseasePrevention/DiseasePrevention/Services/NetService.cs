using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Services
{
    public class NetService
    {
        public NetService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var headers = _httpClient.DefaultRequestHeaders;
            headers.Accept.TryParseAdd("text/csv");
            headers.Accept.TryParseAdd("text/html");
            headers.Accept.TryParseAdd("text/plain");
            headers.Accept.TryParseAdd("application/json");
            headers.Accept.TryParseAdd("application/xml");
            headers.Accept.TryParseAdd("application/xhtml+xml");
            headers.AcceptLanguage.TryParseAdd("zh-TW");
            headers.AcceptEncoding.TryParseAdd("gzip, deflate");
            headers.UserAgent.TryParseAdd("Edge");
        }

        private readonly HttpClient _httpClient;

        /// <summary>
        /// 取得檔案大小
        /// </summary>
        /// <param name="uri">網址</param>
        /// <returns>檔案大小</returns>
        public async Task<long> GetContentLengthAsync(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, uri);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response.Content.Headers.ContentLength ?? 0;
        }

        /// <summary>
        /// 取得字串
        /// </summary>
        /// <param name="uri">網址</param>
        /// <returns>字串</returns>
        public async Task<string> GetStringAsync(Uri uri)
        {
            return await _httpClient.GetStringAsync(uri);
        }

        /// <summary>
        /// 取得字串
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public async Task<string> GetEncodingStringAsync(Uri uri, string encoding = "utf-8")
        {
            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var content = Encoding.GetEncoding(encoding).GetString(byteArray, 0, byteArray.Length);

            return content;
        }

        /// <summary>
        /// 取得字串
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public async Task<NetContent<string>> GetEncodingStringContentAsync(Uri uri, string encoding = "utf-8")
        {
            var response = await _httpClient.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var length = response.Content.Headers.ContentLength;

            var byteArray = await response.Content.ReadAsByteArrayAsync();
            var content = Encoding.GetEncoding(encoding).GetString(byteArray, 0, byteArray.Length);


            return new NetContent<string>()
            {
                ResponsLength = length.Value,
                Content = content
            };
        }
    }
}
