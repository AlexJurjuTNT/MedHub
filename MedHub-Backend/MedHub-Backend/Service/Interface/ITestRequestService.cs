using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface ITestRequestService
{
    Task<List<TestRequest>> GetAllTestRequestsAsync();
    Task<TestRequest?> GetTestRequestByIdAsync(int testRequestId);
    Task<TestRequest> CreateNewTestRequestAsync(TestRequest testRequest);
    Task<TestRequest> UpdateTestRequestAsync(TestRequest testRequest);
    Task<bool> DeleteTestRequestAsync(int testRequestId);
    Task<TestRequest> AddTestTypesAsync(TestRequest testRequest, List<TestType> testTypes);
}