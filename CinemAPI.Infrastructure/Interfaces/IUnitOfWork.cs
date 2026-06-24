using CinemAPI.Domain.Entities;

namespace CinemAPI.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Actor> Actors { get; }
        IGenericRepository<Genre> Genres { get; }
        IMovieRepository Movies { get; }
        IStorageRepository Storage { get; }
        ITranslatorRepository Translator { get; }
        ILogRepository Logs { get; }

		Task SaveChangesAsync();
    }
}