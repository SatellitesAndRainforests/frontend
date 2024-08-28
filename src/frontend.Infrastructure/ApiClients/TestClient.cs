using frontend.Application.Interfaces;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using frontend.Domain.Responses;
using System.Data;

namespace frontend.Infrastructure.ApiClients
{
    public class TestClient : ITestClient
    {

        private readonly HttpClient _httpClient;

        public TestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<TestResonpse> MakeTestApiCall()
        {
            var url = "test";

            //var serializedTestRequest = JsonConvert.SerializeObject(testRequest);
            //var content = new StringContent(serializedTestRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseMessage = await response.Content.ReadAsStringAsync();
            var testResponse = JsonConvert.DeserializeObject<TestResonpse>(responseMessage);

            if (testResponse == null) throw new InvalidOperationException("null object not expected!");

            return testResponse;

        }

    }
}
