using CinemAPI.Application.ServiceProviderExtensions;
using CinemAPI.Infrastructure.Data;
using DotNetEnv;

namespace CinemAPI.WebApi
{
	public class Program
	{
		public static async Task Main ( string[] args )
		{
			Env.Load();

			var builder = WebApplication.CreateBuilder(args);

			var sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found");
			
			builder.Services.AddApplicationContext(sqlConnection);

			var storageConnection = builder.Configuration.GetConnectionString("StorageConnection")
				?? throw new InvalidOperationException("Connection string 'StorageConnection' not found");

			builder.Services.AddStorageContext(storageConnection);

			var chatEndpoint = builder.Configuration.GetConnectionString("ChatEndpoint")
				?? throw new InvalidOperationException("Connection string 'ChatEndpoint' not found");

			var chatKey = builder.Configuration.GetConnectionString("ChatKey")
				?? throw new InvalidOperationException("Connection string 'ChatKey' not found");

			var chatModel = builder.Configuration.GetConnectionString("ChatModel")
				?? throw new InvalidOperationException("Connection string 'ChatModel' not found");

			builder.Services.AddChatContext(chatEndpoint, chatKey, chatModel);

			builder.Services.AddUnitOfWork();
			builder.Services.AddApplicationServices();

			builder.Services.AddControllers();

			builder.Services.AddOpenApi();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if(app.Environment.IsDevelopment())
			{
				app.MapOpenApi();

				using(var scope = app.Services.CreateScope())
				{
					var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
					await DbInitializer.InitializeAsync(context);
				}
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