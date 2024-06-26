using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface ITestRequestService
{
    Task<List<TestRequest>> GetAllTestRequestsAsync();
    Task<TestRequest?> GetTestRequestByIdAsync(int testRequestId);
    Task<TestRequest> CreateNewTestRequestAsync(TestRequest testRequest);
    Task<TestRequest> UpdateTestRequestAsync(TestRequest testRequest);
    Task<bool> DeleteTestRequestAsync(int testRequestId);
    Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId);
    Task<List<TestRequest>> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId);
    Task<List<int>> GetExistingTestTypeIdsForTestRequestAsync(int testRequestId);
}