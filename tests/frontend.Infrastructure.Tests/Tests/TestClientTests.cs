using frontend.Domain.Responses;
using frontend.Infrastructure.ApiClients;
using Moq;
using System.Net;

namespace frontend.Infrastructure.Tests.Tests
{

    public class TestClientTests
    {

        private readonly HttpClient _httpClient;
        private readonly MockHttpMessageHandler _mockMessageHandler;

        public TestClientTests()
        {
            _mockMessageHandler = new MockHttpMessageHandler();

            _httpClient = new HttpClient(_mockMessageHandler.Object)
            {
                BaseAddress = new Uri("http://example.com")
            };
        }


        [Fact]
        public async Task MakeTestApiCall_Returns_ValidResponse()
        {

            // Arrange
            string responseData = File.ReadAllText(Path.Combine("TestJson", "test-response.json"));
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseData)
            };

            _mockMessageHandler.MockSendAsync(responseMessage);
            var testClient = new TestClient(_httpClient);

            // Act
            var response = await testClient.MakeTestApiCall();

            // Assert
            Assert.NotNull(response);
            Assert.IsType<TestResonpse>(response);
            Assert.Equal("test A", response.TestString);

        }



        [Fact]
        public async Task MakeTestApiCall_ThrowsException_OnNonSuccessStatusCode()
        {

            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(string.Empty)
            };

            _mockMessageHandler.MockSendAsync(responseMessage);
            var testClient = new TestClient(_httpClient);

            // Act + Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => testClient.MakeTestApiCall());

        }






    }
}
