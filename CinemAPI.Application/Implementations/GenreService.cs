using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Genre;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
	public class GenreService : IService<GenreDto>
	{
		private readonly IUnitOfWork _uow;

		public GenreService ( IUnitOfWork uow )
		{
			_uow = uow;
		}

		public async Task<PagedResult<GenreDto>> GetAllAsync ( int page )
		{
			var source = await _uow.Genres.GetAllAsync(page);
			var Genres = source.Items.Select(GenreMapper.ToDto).ToList();

			return new PagedResult<GenreDto>
			{
				Items = Genres,
				PageNumber = page,
				PageSize = source.PageSize,
				TotalCount = source.TotalCount,
			};
		}

		public async Task<GenreDto?> GetAsync ( int id )
		{
			var Genre = await _uow.Genres.GetByIdAsync(id);
			if(Genre == null) return null;
			return GenreMapper.ToDto(Genre);
		}

		public async Task<Result> AddAsync ( GenreDto dto )
		{
			try
			{
				var entity = GenreMapper.ToEntity(dto);
				await _uow.Genres.AddAsync(entity);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Genre failed to add");
			}
			return Result.Ok();
		}

		public async Task<Result> EditAsync ( GenreDto dto )
		{
			try
			{
				var entity = await _uow.Genres.GetByIdAsync((int)dto.Id);

				if(entity == null)
					return Result.Fail("The Genre does not exist");

				entity.Name = dto.Name;
				entity.Description = dto.Description ?? "None";
				_uow.Genres.Update(entity);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Genre failed to edit");
			}
			return Result.Ok();
		}

		public async Task<Result> DeleteAsync ( int id )
		{
			try
			{
				var entity = await _uow.Genres.GetByIdAsync(id);

				if(entity == null)
					return Result.Fail("The Genre does not exist");

				_uow.Genres.Delete(entity);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The Genre failed to delete");
			}
			return Result.Ok();
		}
	}
}