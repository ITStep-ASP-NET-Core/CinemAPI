using CinemAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CinemaController : ControllerBase
	{
		private static readonly List<Genre> _genres =
		[
			new Genre { Id = 1, Name = "Action", Description = "Action genre" },
			new Genre { Id = 2, Name = "Comedy", Description = "Comedy genre" },
			new Genre { Id = 3, Name = "Drama", Description = "Drama genre" },
			new Genre { Id = 4, Name = "Horror", Description = "Horror genre" },
			new Genre { Id = 5, Name = "Sci-Fi", Description = "Sci-Fi genre" }
		];

		private static readonly List<Movie> _movies =
		[
			new Movie { Id = 1, Title = "Movie 1", Description = "Description for Movie 1", Genres = [_genres.FirstOrDefault(g => g.Id == 1), _genres.FirstOrDefault(g => g.Id == 2)], ReleaseYear = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) },
			new Movie { Id = 2, Title = "Movie 2", Description = "Description for Movie 2", Genres = [_genres.FirstOrDefault(g => g.Id == 2)], ReleaseYear = DateOnly.FromDateTime(DateTime.Now.AddDays(2)) },
			new Movie { Id = 3, Title = "Movie 3", Description = "Description for Movie 3", Genres = [_genres.FirstOrDefault(g => g.Id == 3)], ReleaseYear = DateOnly.FromDateTime(DateTime.Now.AddDays(3)) },
			new Movie { Id = 4, Title = "Movie 4", Description = "Description for Movie 4", Genres = [_genres.FirstOrDefault(g => g.Id == 5), _genres.FirstOrDefault(g => g.Id == 3)], ReleaseYear = DateOnly.FromDateTime(DateTime.Now.AddDays(4)) },
			new Movie { Id = 5, Title = "Movie 5", Description = "Description for Movie 5", Genres = [_genres.FirstOrDefault(g => g.Id == 2), _genres.FirstOrDefault(g => g.Id == 4)], ReleaseYear = DateOnly.FromDateTime(DateTime.Now.AddDays(5)) }
		];

		/// <summary>
		/// Get all movies.
		/// </summary>
		/// <remarks>
		/// GET /api/movies
		/// </remarks>
		[HttpGet("movies")]
		public IActionResult GetMovies ( ) => Ok(_movies.OrderBy(m => m.Id));


		/// <summary>
		/// Get a movie by ID.
		/// </summary>
		/// <remarks>
		/// GET /api/movies/{id}:int
		/// </remarks>
		[HttpGet("movies/{id:int}")]
		public IActionResult GetMovie (int id) {
			var movie = _movies.FirstOrDefault(m => m.Id == id);
			return movie == null ? NotFound() : Ok(movie);
		}


		/// <summary>
		/// Create a new movie.
		/// </summary>
		/// <remarks>
		/// POST /api/movies
		/// </remarks>
		[HttpPost("movies")]
		public IActionResult AddMovie ( [FromBody] Movie movie )
		{
			movie.Id = _movies.Count == 0 ? 1 : _movies.Max(m => m.Id) + 1;
			_movies.Add(movie);
			return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
		}


		/// <summary>
		/// Edit an existing movie.
		/// </summary>
		/// <remarks>
		/// PUT /api/movies
		/// </remarks>
		[HttpPut("movies")]
		public IActionResult EditMovie ( [FromBody] Movie newMovie )
		{
			var movie = _movies.FirstOrDefault(m => m.Id == newMovie.Id);
			if (movie == null)
			{
				return NotFound();
			}

			_movies.Remove(movie);
			_movies.Add(newMovie);
			return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, newMovie);
		}


		/// <summary>
		/// Delete an existing movie.
		/// </summary>
		/// <remarks>
		/// DELETE /api/movies
		/// </remarks>
		[HttpDelete("movies/{id:int}")]
		public IActionResult DeleteMovie ( int id )
		{
			var movie = _movies.FirstOrDefault(m => m.Id == id);
			if(movie == null)
			{
				return NotFound();
			}

			_movies.Remove(movie);
			return Ok();
		}
	}
}
