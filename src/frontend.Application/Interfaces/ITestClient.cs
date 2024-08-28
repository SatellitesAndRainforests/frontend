using frontend.Domain.Responses;

namespace frontend.Application.Interfaces
{
    public interface ITestClient
    {
        Task<TestResonpse> MakeTestApiCall();
    }
}
