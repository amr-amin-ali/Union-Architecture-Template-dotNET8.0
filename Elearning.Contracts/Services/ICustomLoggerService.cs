namespace Elearning.Contracts.Services;
using Elearning.Shared.DTOs;

public interface ICustomLoggerService
{
    public void Log(LogDto logDto, Exception exception);
    public void LogError(LogDto logDto, Exception exception);
    public void LogInfo(LogDto logDto, Exception exception);
    public void LogDebug(LogDto logDto, Exception exception);
    public void LogWarn(LogDto logDto, Exception exception);
    public void LogTrace(LogDto logDto, Exception exception);
    public void LogToDatabase(LogDto logDto, Exception exception);

}
