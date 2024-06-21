namespace MedHub_Backend.Service.TestResult;

public interface ITestResultService
{
    Task<List<Model.TestResult>> GetAllTestResultsAsync();
    Task<Model.TestResult?> GetTestResultByIdAsync(int testResultId);
    Task<bool> DeleteTestResultAsync(int testResultId);
    Task<Model.TestResult> CreateTestResultAsync(Model.TestResult testResult);
    Task<Model.TestResult> UploadResult(Model.TestResult testResult, Model.TestRequest testRequest, IFormFile formFile);
    Task<Model.TestResult> AddTestTypesAsync(Model.TestResult createdTestResult, List<Model.TestType> testTypes);
}