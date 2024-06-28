using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Service.Interface;

public interface ITestRequestService
{
    IQueryable<TestRequest> GetAllTestRequestsAsync();
    Task<TestRequest?> GetTestRequestByIdAsync(int testRequestId);
    Task<TestRequest> CreateNewTestRequestAsync(TestRequest testRequest, Clinic clinic);
    Task<TestRequest> UpdateTestRequestAsync(TestRequest testRequest);
    Task<bool> DeleteTestRequestAsync(int testRequestId);
    Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId);
    IQueryable<TestRequest> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId);
    Task<List<TestType>> GetRemainingTestTypesAsync(int testRequestId);
}