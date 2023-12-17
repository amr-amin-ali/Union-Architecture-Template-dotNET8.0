namespace Taskaty.Services;
using Elearning.Contracts.Repositories;
using Elearning.Contracts.Services;
using Elearning.Shared.DTOs;

public class CustomLoggerService : ICustomLoggerService
{
    private readonly IRepositoryWrapper _repoWrapper;
    public CustomLoggerService(IRepositoryWrapper repoWrapper)
    {
        _repoWrapper = repoWrapper;
    }

    public void Log(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.Log(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogError(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogError(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogInfo(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogInfo(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogDebug(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogDebug(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogWarn(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogWarn(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogTrace(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogTrace(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }

    public void LogToDatabase(LogDto logDto, Exception exception)
    {
        try
        {
            var log = logDto.ToLog();
            _repoWrapper.CustomLoggerRepository.LogToDatabase(log, exception);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EXCEPTION: " + ex.Message);
            throw;
        }
    }
}
