namespace MedHub_Backend.Service.TestRequest;

public interface ITestRequestService
{
    Task<List<Model.TestRequest>> GetAllTestRequestsAsync();
    Task<Model.TestRequest?> GetTestRequestByIdAsync(int testRequestId);
    Task<Model.TestRequest> CreateNewTestRequestAsync(Model.TestRequest testRequest, List<Model.TestType> testTypes);
    Task<Model.TestRequest> UpdateTestRequestAsync(Model.TestRequest testRequest);
    Task<bool> DeleteTestRequestAsync(int testRequestId);
    Task<List<Model.TestRequest>> GetAllTestRequestsOfUserAsync(int userId);
    Task<List<int>> GetExistingTestTypeIdsForTestRequestAsync(int testRequestId);
}