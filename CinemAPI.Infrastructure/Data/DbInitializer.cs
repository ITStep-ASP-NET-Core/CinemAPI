using CinemAPI.Domain.Entities;

namespace CinemAPI.Infrastructure.Data
{
	public static class DbInitializer
	{
		public static async Task InitializeAsync ( ApplicationContext context )
		{
			if(!context.Actors.Any())
				await SeedActorsAsync(context);

			if(!context.Genres.Any())
				await SeedGenresAsync(context);

			if(!context.Movies.Any())
				await SeedMoviesAsync(context);
		}


		private static async Task SeedActorsAsync ( ApplicationContext context )
		{
			var actors = new List<Actor>
			{
				new() {
					Name = "Christian Bale",
					Biography = "English actor born in Haverfordwest, Wales. " +
								"Known for his extreme physical transformations for roles. " +
								"Had his breakthrough at 13 in Spielberg's 'Empire of the Sun' (1987). " +
								"Rose to global fame as Bruce Wayne/Batman in Christopher Nolan's " +
								"Dark Knight Trilogy (2005–2012). Won an Academy Award for " +
								"'The Fighter' (2010). Other notable roles include " +
								"'American Psycho', 'The Machinist', and 'Ford v Ferrari'.",
					BirthDate = new DateOnly(1974, 1, 30)
				},
				new() {
					Name = "Heath Ledger",
					Biography = "Australian actor born in Perth, Western Australia. " +
								"Began his career in Australian TV before moving to Hollywood. " +
								"Gained critical acclaim for 'Brokeback Mountain' (2005). " +
								"His portrayal of the Joker in 'The Dark Knight' (2008) " +
								"is considered one of the greatest performances in cinema history — " +
								"he was posthumously awarded the Academy Award for Best Supporting Actor. " +
								"Ledger passed away on January 22, 2008, before the film's release.",
					BirthDate = new DateOnly(1979, 4, 4)
				},
				new() {
					Name = "Matthew McConaughey",
					Biography = "American actor, screenwriter and producer born in Uvalde, Texas. " +
								"His father owned a gas station and his mother worked as a kindergarten teacher. " +
								"Broke through with 'Dazed and Confused' (1993) after being discovered in a bar " +
								"by producer Don Phillips. Won the Academy Award for Best Actor for " +
								"'Dallas Buyers Club' (2013). Played astronaut Cooper in Christopher Nolan's " +
								"'Interstellar' (2014), a father who leaves Earth through a wormhole " +
								"in search of a new home for humanity. Also known for 'True Detective', " +
								"'The Gentlemen', and 'Mud'.",
					BirthDate = new DateOnly(1969, 11, 4)
				},
				new() {
					Name = "Anne Hathaway",
					Biography = "American actress born in Brooklyn, New York. " +
								"Her mother Kate McCaley was a stage actress, inspiring Anne's love of performing " +
								"from an early age. She trained in vocals and graduated from Millburn High School " +
								"before pursuing acting full-time. Winner of the Academy Award, Golden Globe, " +
								"BAFTA, and SAG Award for 'Les Misérables' (2012). Played biologist " +
								"Dr. Amelia Brand in 'Interstellar' (2014). Also celebrated for " +
								"'The Devil Wears Prada', 'The Dark Knight Rises', and 'Bride Wars'.",
					BirthDate = new DateOnly(1982, 11, 12)
				},
				new() {
					Name = "Ryan Gosling",
					Biography = "Canadian actor and musician born in London, Ontario, Canada. " +
								"Began his career at age 13 as a member of the Mickey Mouse Club. " +
								"Raised by his mother and sister after his parents divorced when he was 13. " +
								"Earned widespread critical acclaim for 'The Notebook' (2004), 'Drive' (2011), " +
								"and 'La La Land' (2016), for which he received an Academy Award nomination. " +
								"Plays astronaut Ryland Grace in 'Project Hail Mary' (2026), a scientist " +
								"who wakes alone on a spacecraft and must save humanity. " +
								"Also known for 'Blade Runner 2049' and 'Barbie'.",
					BirthDate = new DateOnly(1980, 11, 12)
				},
			};

			context.Actors.AddRange(actors);
			await context.SaveChangesAsync();
		}

		private static async Task SeedGenresAsync ( ApplicationContext context )
		{
			var genres = new List<Genre>
			{
				new() {
					Name = "Action",
					Description = "Films driven by high-energy sequences including fights, chases, " +
								  "and large-scale confrontations. Typically centers on a hero overcoming " +
								  "a powerful antagonist through physical prowess and determination.",
				},
				new() {
					Name = "Science Fiction",
					Description = "Stories grounded in speculative science and futuristic concepts such as " +
								  "space travel, artificial intelligence, and alternate realities. " +
								  "Explores the impact of technology and the unknown on humanity.",
				},
				new() {
					Name = "Drama",
					Description = "Character-driven narratives focused on emotional depth, moral conflict, " +
								  "and personal growth. Prioritizes realistic storytelling and nuanced " +
								  "performances over spectacle.",
				},
				new() {
					Name = "Thriller",
					Description = "Films designed to build suspense and tension, keeping the audience " +
								  "on edge through mystery, danger, and unexpected twists. " +
								  "Often involves protagonists facing life-threatening situations.",
				},
				new() {
					Name = "Superhero",
					Description = "Stories centered on individuals with extraordinary abilities or resources " +
								  "who use them to protect others and fight evil. Combines action, " +
								  "mythology, and moral themes within large-scale narratives.",
				},
				new() {
					Name = "Mystery",
					Description = "Narratives built around the investigation of a crime, secret, or " +
								  "unexplained event. Engages audiences through clues, red herrings, " +
								  "and the gradual revelation of truth.",
				},
				new() {
					Name = "Adventure",
					Description = "Stories following characters on journeys into unfamiliar or dangerous " +
								  "territory, whether physical or psychological. Emphasizes exploration, " +
								  "discovery, and overcoming extraordinary challenges.",
				}
			};

			context.Genres.AddRange(genres);
			await context.SaveChangesAsync();
		}

		private static async Task SeedMoviesAsync ( ApplicationContext context )
		{
			var actionGenre = context.Genres.FirstOrDefault(g => g.Name == "Action");
			var sciFiGenre = context.Genres.FirstOrDefault(g => g.Name == "Science Fiction");
			var dramaGenre = context.Genres.FirstOrDefault(g => g.Name == "Drama");
			var thrillerGenre = context.Genres.FirstOrDefault(g => g.Name == "Thriller");
			var superheroGenre = context.Genres.FirstOrDefault(g => g.Name == "Superhero");
			var mysteryGenre = context.Genres.FirstOrDefault(g => g.Name == "Mystery");
			var adventureGenre = context.Genres.FirstOrDefault(g => g.Name == "Adventure");

			var christianBale = context.Actors.FirstOrDefault(a => a.Name == "Christian Bale");
			var heathLedger = context.Actors.FirstOrDefault(a => a.Name == "Heath Ledger");
			var matthewMcCon = context.Actors.FirstOrDefault(a => a.Name == "Matthew McConaughey");
			var anneHathaway = context.Actors.FirstOrDefault(a => a.Name == "Anne Hathaway");
			var ryanGosling = context.Actors.FirstOrDefault(a => a.Name == "Ryan Gosling");

			var movies = new List<Movie>
			{
				new() {
					Title = "The Dark Knight",
					Description = "Batman raises the stakes in his war on crime. With the help of " +
								  "Lieutenant Jim Gordon and District Attorney Harvey Dent, Batman sets " +
								  "out to dismantle the remaining criminal organizations that plague Gotham. " +
								  "The partnership proves effective, but soon they find themselves prey to " +
								  "a reign of chaos unleashed by the Joker, a criminal mastermind who " +
								  "wants to plunge the city into anarchy and test the limits of " +
								  "Batman's sense of justice.",
					ReleaseYear = new DateOnly(2008, 7, 18),
					Genres = [
						actionGenre!,
						thrillerGenre!,
						superheroGenre!,
						mysteryGenre!
					],
					Actors = [
						christianBale!,
						heathLedger!
					]
				},
				new () {
					Title = "Interstellar",
					Description = "In a near-future Earth devastated by crop blights and dust storms, " +
								  "former NASA pilot Cooper is recruited for a last-ditch mission to find " +
								  "a habitable planet for humanity. Travelling through a wormhole near " +
								  "Saturn with a small crew including biologist Dr. Amelia Brand, " +
								  "they must navigate the extremes of space and time — while Cooper " +
								  "grapples with the prospect of never seeing his children again.",
					ReleaseYear = new DateOnly(2014, 11, 7),
					Genres = [
						sciFiGenre!,
						dramaGenre!,
						adventureGenre!
					],
					Actors = [
						matthewMcCon!,
						anneHathaway!
					]
				},
				new () {
					Title = "Project Hail Mary",
					Description = "Ryland Grace wakes up alone on a spacecraft millions of miles from " +
								  "Earth with no memory of how he got there. As his memory slowly returns, " +
								  "he realizes he is humanity's last hope — sent on a solo mission to " +
								  "find a solution to a dying sun that threatens all life on Earth. " +
								  "Against all odds, he discovers he is not entirely alone in the universe.",
					ReleaseYear = new DateOnly(2026, 3, 20),
					Genres = [
						sciFiGenre!,
						dramaGenre!,
						adventureGenre!,
						thrillerGenre!
					],
					Actors = [
						ryanGosling!
					]
				}
			};

			context.Movies.AddRange(movies);
			await context.SaveChangesAsync();
		}
	}
}