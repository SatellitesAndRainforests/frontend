using AutoMapper;
using frontend.Application.Interfaces;
using frontend.Application.Mappings;
using frontend.Application.Services;
using frontend.Domain.Models;
using frontend.Domain.Responses;
using Moq;

namespace frontend.Application.Tests.Tests;

public class TestServiceTests
{


    private readonly Mock<ITestClient> _testClient;
    private readonly ITestService _testService;

    public TestServiceTests()
    {
        var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        var mapper = mappingConfig.CreateMapper();
        _testClient = new Mock<ITestClient>();
        _testService = new TestService(_testClient.Object, mapper);
    }



    [Fact]
    public async void MakeApiCall_Returns_TempModel()
    {
        // Arrange 
        var testResponse = new TestResonpse() { TestString = "test A" };
        _testClient.Setup(x => x.MakeTestApiCall()).ReturnsAsync(testResponse);

        // Act 
        var result = await _testService.MakeApiCall();

        // Assert
        Assert.IsType<TestModel>(result);
        Assert.Equal("test A", result.TestString);
    }




    [Fact]
    public async void MakeApiCall_ThrowsException_WhenTestClientException()
    {
        // Arrange 
        _testClient.Setup(x => x.MakeTestApiCall()).Throws<Exception>();

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(() => _testService.MakeApiCall());
    }


















}