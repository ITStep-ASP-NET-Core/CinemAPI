using CinemAPI.Application.DTO.Actor;
using CinemAPI.Domain.Entities;

namespace CinemAPI.Application.Mappers
{
	public class ActorMapper
	{
		public static ActorDto ToDto ( Actor actor ) => new()
		{
			Id = actor.Id,
			Name = actor.Name,
			Biography = actor.Biography,
			BirthDate = actor.BirthDate,
		};

		public static Actor ToEntity ( ActorDto actorDto ) => new()
		{
			Name = actorDto.Name,
			Biography = actorDto.Biography ?? "None",
			BirthDate = actorDto.BirthDate ?? DateOnly.FromDateTime(DateTime.Now)
		};
	}
}
