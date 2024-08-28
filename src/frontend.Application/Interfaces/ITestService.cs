using frontend.Domain.Models;

namespace frontend.Application.Interfaces
{
    public interface ITestService
    {
        Task<TestModel> MakeApiCall();
    }
}
