using AutoMapper;
using frontend.Application.Interfaces;
using frontend.Domain.Responses;
using frontend.Domain.Models;

namespace frontend.Application.Services
{
    public class TestService : ITestService
    {

        private readonly ITestClient _testClient;
        private readonly IMapper _automapper;

        public TestService(ITestClient testClient, IMapper mapper) 
        { 
            _testClient = testClient;
            _automapper = mapper;
        }


        public async Task<TestModel> MakeApiCall()
        {
            TestResonpse testResponse = await _testClient.MakeTestApiCall();

            TestModel testModel = _automapper.Map<TestModel>(testResponse);

            return testModel;
        }

    }
}
