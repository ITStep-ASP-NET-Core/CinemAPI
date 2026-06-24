using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Genre;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Entities;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
	public class GenreService : IService<GenreDto>
	{
		private readonly ILogRepository _logs;
		private readonly IUnitOfWork _uow;

		public GenreService ( IUnitOfWork uow, ILogRepository logs )
		{
			_uow = uow;
			_logs = logs;
		}

		public async Task<PagedResult<GenreDto>> GetAllAsync ( int page )
		{	
			var source = await _uow.Genres.GetAllAsync(Math.Max(0, page - 1));
			var Genres = source.Items.Select(GenreMapper.ToDto).ToList();

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "genre",
				Method = "view",
				Action = "GetAllAsync",
				Entity = "genre",
				Message = $"Genres on page {page} viewed"
			});

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

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "genre",
				Method = "view",
				Action = "GetAsync",
				Entity = "genre",
				Message = $"Genre {id} viewed"
			});


			return GenreMapper.ToDto(Genre);
		}

		public async Task<Result> AddAsync ( GenreDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "genre",
				Method = "create",
				Action = "AddAsync",
				Entity = "genre",
			};
			try
			{
				var entity = GenreMapper.ToEntity(dto);
				await _uow.Genres.AddAsync(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Genre {dto.Name} created";
			}
			catch(Exception ex)
			{
				log.Message = $"Error adding genre {dto.Name}: {ex.Message}";
				return Result.Fail("The Genre failed to add");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> EditAsync ( GenreDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "genre",
				Method = "edit",
				Action = "EditAsync",
				Entity = "genre",
			};
			try
			{
				var entity = await _uow.Genres.GetByIdAsync((int)dto.Id);

				if(entity == null)
					return Result.Fail("The Genre does not exist");

				entity.Name = dto.Name;
				entity.Description = dto.Description ?? "None";
				_uow.Genres.Update(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Genre {dto.Name} edited";
			}
			catch(Exception ex)
			{
				log.Message = $"Error editing genre {dto.Id}: {ex.Message}";
				return Result.Fail("The Genre failed to edit");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> DeleteAsync ( int id )
		{
			var log = new LogEntity
			{
				PartitionKey = "genre",
				Method = "delete",
				Action = "DeleteAsync",
				Entity = "genre",
			};
			try
			{
				var entity = await _uow.Genres.GetByIdAsync(id);

				if(entity == null)
					return Result.Fail("The Genre does not exist");

				_uow.Genres.Delete(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Genre {id} deleted";
			}
			catch(Exception ex)
			{
				log.Message = $"Error deleting genre {id}: {ex.Message}";
				return Result.Fail("The Genre failed to delete");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}
	}
}