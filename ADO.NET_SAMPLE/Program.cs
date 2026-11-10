using ADO.NET_SAMPLE.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//taking connection string
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//register depenceinjection
builder.Services.AddScoped<DbConnectionFactory>(
    _ => new DbConnectionFactory(ConnectionString!));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
