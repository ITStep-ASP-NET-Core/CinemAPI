using CinemAPI.Application.DTO.Movie;
using CinemAPI.Application.Interfaces;
using CinemAPI.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasMovies.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MoviesController ( IMovieService movieService )
		{
			_movieService = movieService;
		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(MovieDetailDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetMovie ( int id )
		{
			var result = await _movieService.GetMovieByIdAsync(id);
			if(result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		[ProducesResponseType(typeof(ICollection<PagedResult<MovieDto>>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll ( [FromQuery] int page = 1 )
		{
			var result = await _movieService.GetMoviesAsync(page);
			return Ok(result);
		}

		[HttpGet("filter")]
		[ProducesResponseType(typeof(PagedResult<MovieDto>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetMoviesByFilters ( [FromQuery] MovieFilterDto filter, [FromQuery] int page = 1 )
		{
			var result = await _movieService.GetMoviesByFiltersAsync(filter, page);
			return Ok(result);
		}

		[HttpPost("{id}/poster")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UploadPoster ( int id, IFormFile poster, CancellationToken ct )
		{
			if(poster.ContentType is not ("image/jpeg" or "image/png"))
				return BadRequest("Only JPEG or PNG");

			if(poster.Length > 10 * 1024 * 1024)
				return BadRequest("Maximum size - 10 MB");

			var uploadFile = new UploadFile
			{
				Content = poster.OpenReadStream(),
				ContentType = poster.ContentType,
				FileName = poster.FileName,
				FileExtension = Path.GetExtension(poster.FileName)
			};

			var result = await _movieService.UploadMoviePosterAsync(id, uploadFile, ct);
			if(!result.Success)
				return BadRequest(result.Error);
			return NoContent();
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create ( [FromBody] MovieCreateDto dto )
		{
			var result = await _movieService.CreateMovieAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
			return Created();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Edit ( [FromBody] MovieEditDto dto )
		{
			var result = await _movieService.EditMovieAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete ( int id )
		{
			var result = await _movieService.DeleteMovieAsync(id);
			if(!result.Success)
				return NotFound(result.Error);
			return NoContent();
		}
	}
}