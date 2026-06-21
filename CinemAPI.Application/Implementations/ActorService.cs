using CinemAPI.Application.Common;
using CinemAPI.Application.DTO.Actor;
using CinemAPI.Application.Interfaces;
using CinemAPI.Application.Mappers;
using CinemAPI.Domain.Common;
using CinemAPI.Infrastructure.Interfaces;

namespace CinemAPI.Application.Implementations
{
    public class ActorService : IService<ActorDto>
    {
        private readonly IUnitOfWork _uow;

        public ActorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<ActorDto>> GetAllAsync(int page)
        {
            var source = await _uow.Actors.GetAllAsync(page);
			var actors = source.Items.Select(ActorMapper.ToDto).ToList();

			return new PagedResult<ActorDto>
			{
				Items = actors,
				PageNumber = page,
				PageSize = source.PageSize,
				TotalCount = source.TotalCount,
			};
		}

        public async Task<ActorDto?> GetAsync(int id)
        {
            var actor = await _uow.Actors.GetByIdAsync(id);
            if (actor == null) return null;
            return ActorMapper.ToDto(actor);
        }

        public async Task<Result> AddAsync (ActorDto dto)
        {
            try {
				var entity = ActorMapper.ToEntity(dto);
				await _uow.Actors.AddAsync(entity);
				await _uow.SaveChangesAsync();
			}
            catch(Exception)
			{
                return Result.Fail("The actor failed to add");
			}
            return Result.Ok();
		}

		public async Task<Result> EditAsync (ActorDto dto)
        {
			try
			{
				var entity = await _uow.Actors.GetByIdAsync((int)dto.Id);

				if(entity == null)
					return Result.Fail("The actor does not exist");

				entity.Name = dto.Name;
				entity.Biography = dto.Biography ?? "None";
				entity.BirthDate = dto.BirthDate ?? DateOnly.FromDateTime(DateTime.Now);
				_uow.Actors.Update(entity);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The actor failed to edit");
			}
			return Result.Ok();
		}

        public async Task<Result> DeleteAsync (int id)
        {
			try
			{
				var entity = await _uow.Actors.GetByIdAsync(id);

				if(entity == null)
					return Result.Fail("The actor does not exist");

				_uow.Actors.Delete(entity);
				await _uow.SaveChangesAsync();
			}
			catch(Exception)
			{
				return Result.Fail("The actor failed to delete");
			}
			return Result.Ok();
		}
    }
}