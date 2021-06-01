using Microsoft.AspNetCore.Mvc.Testing;
using Prueba.Tecnica.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Prueba.Tecnica.Test
{
    public class TopSecretTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public TopSecretTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/topsecret", "{\"satellites\":[{\"name\":\"kenobi\",\"distance\":500,\"message\":['este', '', '', 'mensaje', ''] },{\"name\":\"skywalker\",\"distance\":500,\"message\":['', 'es', '', '', 'secreto']},{\"name\":\"sato\",\"distance\":1300,\"message\":['este', '', 'un', '', '']}]}", 200)]
        [InlineData("topsecret", "{\"satellites\":[{\"name\":\"kenobi\",\"distance\":10.5,\"message\":['este', '', '', 'mensaje', ''] },{\"name\":\"skywalker\",\"distance\":9.5,\"message\":['', 'es', '', '', 'secreto']},{\"name\":\"sato\",\"distance\":9.5,\"message\":['este', '', 'un', '', '']}]}", 400)]
        public async Task ExcetutePostTopSecretDataIntegrationTest(string url, string jsonInput, int statusExpected)
        {
            var client = _factory.CreateClient();
            //client.DefaultRequestHeaders.Add("userId", headers);
            StringContent inputData = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, inputData);
            Assert.Equal(statusExpected, (int)response.StatusCode);
        }

    }
}
