using CinemAPI.Application.DTO.Logger;
using CinemAPI.Infrastructure.Entities;

namespace CinemAPI.Application.Mappers
{
	public class LogMapper
	{
		public static LogEntityDto ToDto ( LogEntity log ) => new()
		{
			Method = log.Method,
			Action = log.Action,
			Entity = log.Entity,
			Message = log.Message,
			OccurredAt = log.OccurredAt
		};
	}
}
