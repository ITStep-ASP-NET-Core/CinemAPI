
using CinemAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemAPI.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Actor>(entity =>
			{
				entity.HasKey(s => s.Id);
			});

			modelBuilder.Entity<Movie>(entity =>
			{
				entity.HasKey(m => m.Id);

				entity.HasMany(m => m.Actors)
					  .WithMany(a => a.Movies); ;

				entity.HasMany(m => m.Genres)
					  .WithMany(g => g.Movies); ;
			});

			modelBuilder.Entity<Genre>(entity =>
			{
				entity.HasKey(g => g.Id);
			});
		}
    }
}