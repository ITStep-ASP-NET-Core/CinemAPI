using CinemAPI.Application.DTO.Logger;
using CinemAPI.Application.DTO.Movie;
using CinemAPI.Application.Interfaces;
using CinemAPI.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace CinemAPI.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LogsController : ControllerBase
	{
		private readonly ILogService _logService;

		public LogsController ( ILogService logService )
		{
			_logService = logService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(PagedResult<MovieDto>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetLogs ( [FromQuery] LogFilterDto? filter, [FromQuery] string? continuationToken = null, CancellationToken ct = default )
		{
			var result = await _logService.GetAllAsync(filter, continuationToken, ct);
			return Ok(result);
		}
	}
}