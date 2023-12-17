namespace Elearning.Contracts.Repositories;

using Elearning.Entittes.Models;

public interface ICustomLoggerRepository
{
    void Log(Log log, Exception exception);
    void LogError(Log log, Exception exception);
    void LogInfo(Log log, Exception exception);
    void LogDebug(Log log, Exception exception);
    void LogWarn(Log log, Exception exception);
    void LogTrace(Log log, Exception exception);
    void LogToDatabase(Log log, Exception exception);
}
