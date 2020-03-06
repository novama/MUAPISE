using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Muapise.Common.Rest
{
    public class WebApiClientProcessor
    {
        private readonly HttpClient _httpClient;

        public WebApiClientProcessor(HttpClient httpClient, string defaultApiBaseUrl)
        {
            DefaultApiBaseUrl = defaultApiBaseUrl;
            _httpClient = httpClient;
        }

        public string DefaultApiBaseUrl { get; }

        public async Task<HttpResponseMessage> GetResponseMessage(string resourceName,
            string parameters = "")
        {
            return await GetResponseMessage(DefaultApiBaseUrl, resourceName, parameters);
        }

        public async Task<HttpResponseMessage> GetResponseMessage(string apiBaseUrl, string resourceName,
            string parameters)
        {
            var getRequest = new StringBuilder();
            getRequest.Append(resourceName);
            if (!string.IsNullOrEmpty(parameters))
            {
                getRequest.Append("/");
                getRequest.Append(parameters);
            }

            var url = new Uri(apiBaseUrl + getRequest);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson));
            var responseGet = await _httpClient.GetAsync(url);
            return responseGet;
        }

        public async Task<string> GetResponseContent(string resourceName, string parameters = "")
        {
            return await GetResponseContent(DefaultApiBaseUrl, resourceName, parameters);
        }

        public async Task<string> GetResponseContent(string apiBaseUrl, string resourceName, string parameters)
        {
            var result = await GetResponseMessage(apiBaseUrl, resourceName, parameters);
            return result.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> PostResponseContent(string resourceName, string content)
        {
            return await PostResponseContent(DefaultApiBaseUrl, resourceName, content);
        }

        public async Task<string> PostResponseContent(string apiBaseUrl, string resourceName, string content)
        {
            // Encoding content
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            //Set the content type to let the API know this is JSON.
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson);

            var url = new Uri(apiBaseUrl + resourceName);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson));
            var responsePost = await _httpClient.PostAsync(url, byteContent);
            responsePost.EnsureSuccessStatusCode();
            return responsePost.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> PutResponseContent(string resourceName, string parameters, string content)
        {
            return await PutResponseContent(DefaultApiBaseUrl, resourceName, parameters, content);
        }

        public async Task<string> PutResponseContent(string apiBaseUrl, string resourceName, string parameters,
            string content)
        {
            // Encoding content
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            //Set the content type to let the API know this is JSON.
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson);

            var putRequest = new StringBuilder();
            putRequest.Append(resourceName);
            if (!string.IsNullOrEmpty(parameters))
            {
                putRequest.Append("/");
                putRequest.Append(parameters);
            }

            var url = new Uri(apiBaseUrl + putRequest);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson));
            var responsePut = await _httpClient.PutAsync(url, byteContent);
            responsePut.EnsureSuccessStatusCode();
            return responsePut.Content.ReadAsStringAsync().Result;
        }

        public async Task<string> DeleteResponseContent(string resourceName, string parameters = "")
        {
            return await DeleteResponseContent(DefaultApiBaseUrl, resourceName, parameters);
        }

        public async Task<string> DeleteResponseContent(string apiBaseUrl, string resourceName, string parameters)
        {
            var deleteRequest = new StringBuilder();
            deleteRequest.Append(resourceName);
            if (!string.IsNullOrEmpty(parameters))
            {
                deleteRequest.Append("/");
                deleteRequest.Append(parameters);
            }

            var url = new Uri(apiBaseUrl + deleteRequest);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(GlobalConstants.MediaTypeNames.ApplicationJson));
            var responseDelete = await _httpClient.DeleteAsync(url);
            responseDelete.EnsureSuccessStatusCode();
            return responseDelete.Content.ReadAsStringAsync().Result;
        }
    }
}