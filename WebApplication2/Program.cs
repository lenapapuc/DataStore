using Microsoft.EntityFrameworkCore;
using WebApplication2;
using WebApplication2.Controllers;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddDbContext<Storage>(opt => opt.UseInMemoryDatabase("Storage"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

new Controller(app);

app.Run();