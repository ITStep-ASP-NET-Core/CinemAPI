using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Actor;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Entities;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
    public class ActorService : IService<ActorDto>
    {
		private readonly ILogRepository _logs;
		private readonly IUnitOfWork _uow;

		public ActorService ( IUnitOfWork uow, ILogRepository logs )
		{
			_uow = uow;
			_logs = logs;
		}

		public async Task<PagedResult<ActorDto>> GetAllAsync ( int page )
		{
			var source = await _uow.Actors.GetAllAsync(Math.Max(0, page - 1));
			var actors = source.Items.Select(ActorMapper.ToDto).ToList();

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "actor",
				Method = "view",
				Action = "GetAllAsync",
				Entity = "actor",
				Message = $"Actors on page {page} viewed"
			});

			return new PagedResult<ActorDto>
			{
				Items = actors,
				PageNumber = page,
				PageSize = source.PageSize,
				TotalCount = source.TotalCount,
			};
		}

		public async Task<ActorDto?> GetAsync ( int id )
		{
			var actor = await _uow.Actors.GetByIdAsync(id);
			if(actor == null) return null;

			_ = _logs.LogAsync(new LogEntity
			{
				PartitionKey = "actor",
				Method = "view",
				Action = "GetAsync",
				Entity = "actor",
				Message = $"Actor {id} viewed"
			});

			return ActorMapper.ToDto(actor);
		}

		public async Task<Result> AddAsync ( ActorDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "actor",
				Method = "create",
				Action = "AddAsync",
				Entity = "actor",
			};
			try
			{
				var entity = ActorMapper.ToEntity(dto);
				await _uow.Actors.AddAsync(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Actor {dto.Name} created";
			}
			catch(Exception ex)
			{
				log.Message = $"Error adding actor {dto.Name}: {ex.Message}";
				return Result.Fail("The actor failed to add");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}

		public async Task<Result> EditAsync ( ActorDto dto )
		{
			var log = new LogEntity
			{
				PartitionKey = "actor",
				Method = "edit",
				Action = "EditAsync",
				Entity = "actor",
			};
			try
			{
				var entity = await _uow.Actors.GetByIdAsync((int)dto.Id);
				if(entity == null) return Result.Fail("The actor does not exist");

				entity.Name = dto.Name;
				entity.Biography = dto.Biography ?? "None";
				entity.BirthDate = dto.BirthDate ?? DateOnly.FromDateTime(DateTime.Now);

				_uow.Actors.Update(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Actor {entity.Id} edited";
			}
			catch(Exception ex)
			{
				log.Message = $"Error editing actor {dto.Id}: {ex.Message}";
				return Result.Fail("The actor failed to edit");
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
				PartitionKey = "actor",
				Method = "delete",
				Action = "DeleteAsync",
				Entity = "actor",
			};
			try
			{
				var entity = await _uow.Actors.GetByIdAsync(id);
				if(entity == null) return Result.Fail("The actor does not exist");

				_uow.Actors.Delete(entity);
				await _uow.SaveChangesAsync();

				log.Message = $"Actor {id} deleted";
			}
			catch(Exception ex)
			{
				log.Message = $"Error deleting actor {id}: {ex.Message}";
				return Result.Fail("The actor failed to delete");
			}
			finally
			{
				_ = _logs.LogAsync(log, CancellationToken.None);
			}
			return Result.Ok();
		}
	}
}