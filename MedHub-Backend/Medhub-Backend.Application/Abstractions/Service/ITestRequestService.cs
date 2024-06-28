using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface ITestRequestService
{
    IQueryable<TestRequest> GetAllAsync();
    Task<TestRequest?> GetByIdAsync(int testRequestId);
    Task<TestRequest> CreateAsync(TestRequest testRequest, Clinic clinic);
    Task<TestRequest> UpdateAsync(TestRequest testRequest);
    Task<bool> DeleteAsync(int testRequestId);
    Task<List<TestRequest>> GetAllTestRequestsOfUserAsync(int userId);
    IQueryable<TestRequest> GetAllTestRequestsOfUserInClinicAsync(int userId, int clinicId);
    Task<List<TestType>> GetRemainingTestTypesAsync(int testRequestId);
}