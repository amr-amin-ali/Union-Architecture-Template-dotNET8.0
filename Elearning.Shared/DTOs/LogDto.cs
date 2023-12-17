namespace Elearning.Shared.DTOs;

using Elearning.Entittes.Models;
using Elearning.Utils;

public class LogDto
{
    public string? UserId { get; set; }
    public string? Controller { get; set; }
    public string? Action { get; set; }
    public string? Message { get; set; }
    public string? FnParameter { get; set; }
    public string? Level { get; set; } = CustomLogLevelUtility.Info;

    public Log ToLog()
    {
        return new Log
        {
            CreatedAt = DateTime.Now,
            FnParameter = this.FnParameter!,
            Level = this.Level!,
            Message = this.Message!,
            Action = this.Action!,
            Controller = this.Controller!,
            UserId = this.UserId!
        };
    }
}
