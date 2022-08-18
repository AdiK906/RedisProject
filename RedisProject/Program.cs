using Infrastructure.Services;
using Redis.OM;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connection string do redis
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));

//Tworzenie indexów
builder.Services.AddHostedService<IndexCreationService>();

//mediatr dependency injection
builder.Services.AddRedisApplication();

var app = builder.Build();

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
