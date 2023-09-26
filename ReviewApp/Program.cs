using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddTransient<Seed>();
    var ConnectionString = builder.Configuration.GetConnectionString("sqlConnection");
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(ConnectionString));

    var app = builder.Build();

    if (args.Length == 1 && args[0].ToLower() == "seeddata")
        SeedData(app);

    void SeedData(IHost app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using (var scope = scopedFactory.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<Seed>();
            service.SeedDataContext();
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    Log.Error(ex, $"Unhandled Exception: {ex.Message}");
}
finally
{
    Log.Information("Shutdown Completed");
    Log.CloseAndFlush();
}


