

using Microsoft.EntityFrameworkCore;
using ProvideApiReference_DataAccess.Data;
using ProvideApiReference_DataAccess.DbInitializer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>
  options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
SeedDatabase();

app.UseAuthorization();

app.MapControllers();

app.Run();

async void SeedDatabase()
{
    using (var scope=app.Services.CreateScope())
    {
        var dbInitializer=scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.Initialize();
    }
}
