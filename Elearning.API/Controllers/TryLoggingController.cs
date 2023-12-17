namespace Elearning.API.Controllers;

using Elearning.Contracts.Common;
using Elearning.Contracts.Repositories;
using Elearning.Contracts.Services;
using Elearning.Services.Common;
using Elearning.Shared.DTOs;
using Elearning.Utils;
using Elearning.Utils.Contracts;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TryLoggingController : BaseController
{
    private readonly ICustomLoggerService _logger;
    public TryLoggingController(ICustomLoggerService logger)
    {
        _logger = logger;
    }
    [HttpGet("Tracing")]
    public async Task<IActionResult> LogTrace()
    {
        _logger.LogTrace(new LogDto
        {
            Action = "Tr. Action",
            Controller = "Tr. Controller",
            FnParameter = "Tracing parameter",
            Message = " Tracing message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Trace
        }, null);
        return Ok();
    }
    [HttpGet("Error")]
    public async Task<IActionResult> LogError()
    {
        _logger.LogError(new LogDto
        {
            Action = "Er. Action",
            Controller = "Er. Controller",
            FnParameter = "Error parameter",
            Message = " Error message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Error
        }, null);


        return Ok();
    }
    [HttpGet("Debug")]
    public async Task<IActionResult> LogDebug()
    {
        _logger.LogDebug(new LogDto
        {
            Action = "De. Action",
            Controller = "De. Controller",
            FnParameter = "Debugging parameter",
            Message = " Debugging message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Debug
        }, null);


        return Ok();
    }
    [HttpGet("Info")]
    public async Task<IActionResult> LogInfo()
    {
        _logger.LogInfo(new LogDto
        {
            Action = "Info. Action",
            Controller = "Info. Controller",
            FnParameter = "Info parameter",
            Message = " Info message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Info
        }, null);


        return Ok();
    }
    [HttpGet("Warn")]
    public async Task<IActionResult> LogWarn()
    {
        _logger.LogWarn(new LogDto
        {
            Action = "Warn. Action",
            Controller = "Warn. Controller",
            FnParameter = "Warning parameter",
            Message = " Warning message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Warn
        }, null);


        return Ok();
    }
    [HttpGet("Database")]
    public async Task<IActionResult> LogToDatabase()
    {
        _logger.LogToDatabase(new LogDto
        {
            Action = "Database. Action",
            Controller = "Database. Controller",
            FnParameter = "Database parameter",
            Message = " Database message ",
            UserId = "UserId: 898959292",
            Level = CustomLogLevelUtility.Warn
        }, null);


        return Ok();
    }


    //[HttpPost("api/Tasks")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    //public async Task<IActionResult> Add([FromForm] UserTaskDTO? taskDto, [FromForm] IFormFileCollection attachments)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return UnprocessableEntity(ModelState);
    //    }
    //    try
    //    {
    //        var data = await _taskService.AddAsync(taskDto, attachments);
    //        if (data.StatusCode == StatusCodes.Status200OK)
    //        {
    //            return Created($"/api/Tasks/{data.Data.Id}", data);
    //        }
    //        if (data.StatusCode == StatusCodes.Status400BadRequest)
    //        {
    //            return BadRequest(data);
    //        }
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    //[HttpPut("api/Tasks")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    //public async Task<IActionResult> Update([FromForm] UserTaskDTO? taskDto, [FromForm] IFormFileCollection attachments)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return UnprocessableEntity(ModelState);
    //    }
    //    try
    //    {
    //        var data = await _taskService.UpdateAsync(taskDto, attachments);

    //        if (data.StatusCode == StatusCodes.Status200OK)
    //        {
    //            return Ok(data);
    //        }

    //        return BadRequest(data);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    //[HttpDelete("api/Tasks/{id}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<IActionResult> Delete(long id)
    //{
    //    try
    //    {
    //        var result = await _taskService.DeleteAsync(id);
    //        if (result)
    //        {
    //            return NoContent();
    //        }

    //        return BadRequest();
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    //[HttpGet("api/Tasks")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public async Task<IActionResult> GetAll()
    //{
    //    try
    //    {
    //        var data = await _taskService.GetAllTasks();
    //        if (data.StatusCode == StatusCodes.Status200OK)
    //        {
    //            return Ok(data);
    //        }
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}

    //[HttpGet("api/Tasks/{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    //public async Task<IActionResult> GetById(long id)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return UnprocessableEntity(ModelState);
    //    }
    //    try
    //    {
    //        var data = await _taskService.GetByIDAsync(id);
    //        if (data.StatusCode == StatusCodes.Status200OK)
    //        {
    //            return Ok();
    //        }
    //        if (data.StatusCode == StatusCodes.Status404NotFound)
    //        {
    //            return NotFound();
    //        }
    //        if (data.StatusCode == StatusCodes.Status400BadRequest)
    //        {
    //            return BadRequest(data);
    //        }
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(StatusCodes.Status500InternalServerError);
    //    }
    //}


}

