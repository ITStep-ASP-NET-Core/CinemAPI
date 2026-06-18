
namespace CinemAPI.WebApi
{
	public class Program
	{
		public static void Main ( string[] args )
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddOpenApi();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if(app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI();

			app.MapControllers();

			app.Run();
		}
	}
}
