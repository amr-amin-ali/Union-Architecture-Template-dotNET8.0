#pragma warning disable CS8604 // Possible null reference argument.
namespace Elearning.Repositiores;
using Elearning.Contracts.Repositories;
using Elearning.Entittes.Models;
using Elearning.Utils;

using Microsoft.Extensions.Configuration;

using NLog;

using System;
using System.Text;

public class CustomLoggerRepository : ICustomLoggerRepository
{
    private readonly Logger _logger;
    private LoggerConfigurations LoggerConfigurations { get; set; } = new LoggerConfigurations();

    public CustomLoggerRepository(IConfiguration configuration)
    {
        _logger = LogManager.GetCurrentClassLogger();

        configuration.GetSection("LoggerConfigurations").Bind(LoggerConfigurations);

    }

    public void Log(Log log, Exception exception)
    {
        if (LoggerConfigurations.AllowLogging)
        {
            var message = BuildFileLoggingMessage(log);

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Trace.ToString()) && log.Level == CustomLogLevelUtility.Trace.ToString())
            {
                _logger.Trace(message);
            }

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Debug.ToString()) && log.Level == CustomLogLevelUtility.Debug.ToString())
            {
                _logger.Debug(message);
            }

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Info.ToString()) && log.Level == CustomLogLevelUtility.Info.ToString())
            {
                _logger.Info(message);
            }

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Error.ToString()) && log.Level == CustomLogLevelUtility.Error.ToString())
            {
                //LogEventInfo logEventWithNoException = BuildDatabaseLoggingEvent(log: log, exception: exception);
                //_logger.Error(logEventWithNoException);
                try
                {
                    throw new Exception("Intended Exception");
                }
                catch (Exception e)
                {
                    LogEventInfo logEvent = BuildDatabaseLoggingEvent(log: log, exception: e);
                    _logger.Error(logEvent);
                }
            }

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Warn.ToString()) && log.Level == CustomLogLevelUtility.Warn.ToString())
            {
                _logger.Warn(message);
            }

            if (LoggerConfigurations.AllowedLevels.Contains(CustomLogLevelUtility.Fatal.ToString()) && log.Level == CustomLogLevelUtility.Fatal.ToString())
            {
                _logger.Fatal(message);
            }
        }
    }

    public void LogError(Log log, Exception exception)
    {
        var message = BuildFileLoggingMessage(log);
        _logger.Error(exception: exception, message: message);
    }

    public void LogInfo(Log log, Exception exception)
    {
        var message = BuildFileLoggingMessage(log);
        _logger.Info(exception: exception, message: message);
    }

    public void LogDebug(Log log, Exception exception)
    {
        var message = BuildFileLoggingMessage(log);
        _logger.Debug(exception: exception, message: message);
    }

    public void LogWarn(Log log, Exception exception)
    {
        var message = BuildFileLoggingMessage(log);
        _logger.Warn(exception: exception, message: message);
    }

    public void LogTrace(Log log, Exception exception)
    {
        var message = BuildFileLoggingMessage(log);
        _logger.Trace(exception: exception, message: message);
    }

    public void LogToDatabase(Log log, Exception exception)
    {
        LogEventInfo logEventWithNoException = BuildDatabaseLoggingEvent(log: log, exception: exception);
        _logger.Error(logEventWithNoException);
    }

    private LogEventInfo BuildDatabaseLoggingEvent(Log log, Exception? exception = null)
    {
        LogLevel logLevel = log.Level switch
        {
            CustomLogLevelUtility.Info => LogLevel.Info,
            CustomLogLevelUtility.Trace => LogLevel.Trace,
            CustomLogLevelUtility.Warn => LogLevel.Warn,
            CustomLogLevelUtility.Debug => LogLevel.Debug,
            _ => LogLevel.Error,
        };

        LogEventInfo logEvent = new LogEventInfo(logLevel, "dataBase", message: log.Message, exception: exception, formatProvider: null, parameters: null);

        logEvent.Properties["UserId"] = log.UserId;
        logEvent.Properties["Controller"] = log.Controller;
        logEvent.Properties["Action"] = log.Action;
        logEvent.Properties["Message"] = logEvent.FormattedMessage;
        logEvent.Properties["FnParameter"] = log.FnParameter;
        logEvent.Properties["CreatedAt"] = log.CreatedAt;
        if (exception != null)
        {
            logEvent.Properties["Exception"] = exception;
        }

        return logEvent;
    }
    private string BuildFileLoggingMessage(Log log)
    {
        var message = new StringBuilder();

        //message.Append(log.Logger?.Trim().Length > 0 ? $"Logger: {log.Logger}, " : null);
        if (log.FnParameter?.Trim().Length > 0)
        {
            message.Append($"Fn Parameter: {log.FnParameter} | ");
        }
        if (log.Message?.Trim().Length > 0)
        {
            message.Append($"Message: {log.Message}");
        }


        return message.ToString();

    }
}
