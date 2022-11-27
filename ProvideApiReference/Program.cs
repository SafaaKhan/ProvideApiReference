
using ProvideApiReference_DataAccess.DbInitializer;
using ProvideApiReference_Models.ValidateModelAttributes;
using ProvideApiReference_Utilities.Extensions;
using ProvideApiReference_Utilities.Helpers.ExceptionsHandling;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);
        // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
        result.ContentTypes.Add(MediaTypeNames.Application.Json);
        return result;
    };
});

//Extensions
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

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
