using Moq;
using PokemonManagerAPP.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokemonManagerAPP.Tests.TestHelpers
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _mockResponse;

        public MockHttpMessageHandler(HttpResponseMessage mockResponse)
        {
            _mockResponse = mockResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mockResponse);
        }
    }

    public static class MockHttpClientFactory
    {
        public static HttpClient CreateHttpClient(string responseContent, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseContent)
            };

            var handler = new MockHttpMessageHandler(mockResponse);
            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://mockapi.com")
            };
        }
    }

}
