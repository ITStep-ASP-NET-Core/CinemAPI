using CinemAPI.Application.DTO.Actor;
using CinemAPI.Application.Interfaces;
using CinemAPI.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace CinemAPI.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ActorsController : ControllerBase
	{
		private readonly IService<ActorDto> _actorService;

		public ActorsController ( IService<ActorDto> actorService )
		{
			_actorService = actorService;
		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(ActorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetActor ( int id )
		{
			var result = await _actorService.GetAsync(id);
			if(result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[ProducesResponseType(typeof(ICollection<PagedResult<ActorDto>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll ( [FromQuery] int page = 1 )
		{
			var result = await _actorService.GetAllAsync(page);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create ( [FromBody] ActorDto dto )
		{
			var result = await _actorService.AddAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
			return Created();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Edit ( [FromBody] ActorDto dto )
		{
			var result = await _actorService.EditAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete ( int id )
		{
			var result = await _actorService.DeleteAsync(id);
			if(!result.Success)
				return NotFound(result.Error);
			return NoContent();
		}
	}
}