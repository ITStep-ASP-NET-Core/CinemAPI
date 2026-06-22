using CinemAPI.Application.DTO.Genre;
using CinemAPI.Application.Interfaces;
using CinemAPI.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace CinemAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IService<GenreDto> _genreService;

        public GenresController ( IService<GenreDto> genreService )
        {
			_genreService = genreService;
        }

		[HttpGet("{id:int}")]
		[ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetGenre ( int id )
		{
			var result = await _genreService.GetAsync(id);
			if(result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
        [ProducesResponseType(typeof(ICollection<PagedResult<GenreDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll ( [FromQuery] int page = 1 )
        {
            var result = await _genreService.GetAllAsync(page);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create ( [FromBody] GenreDto dto )
        {
			var result = await _genreService.AddAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
            return Created();
        }

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Edit ( [FromBody] GenreDto dto )
		{
            var result = await _genreService.EditAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);
			return NoContent();
		}

		[HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete ( int id )
        {
            var result = await _genreService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result.Error);
            return NoContent();
        }
    }
}