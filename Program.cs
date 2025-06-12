using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using pgapp;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from environment variables
builder.Configuration.AddEnvironmentVariables(prefix: "CoreApp_");

// Add services to the container
builder.Services.AddControllers();

// Configure database connection
string connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? 
    builder.Configuration["ConnectionString"];
var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseNpgsql(npgsqlBuilder.ConnectionString));

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// app.UseHttpsRedirection(); // Commented out as in original

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Run database migrations on app start
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dataContext.Database.Migrate();
}

app.Run();