namespace Medhub_Backend.Domain.Exceptions;

public class TestResultNotFoundException : Exception
{
    public TestResultNotFoundException(int testResultId) : base($"Test Result with Id {testResultId} not found")
    {
    }
}