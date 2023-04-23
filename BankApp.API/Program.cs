using BankApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();
app.MapControllers();

using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await service.Database.MigrateAsync();

app.Run();
