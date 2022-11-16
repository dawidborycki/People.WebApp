using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using People.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PeopleDbContext
builder.Services.AddDbContext<PeopleDbContext>(options =>
{
    if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        options.UseInMemoryDatabase("People");
    }
    else
    {
        options.UseSqlServer(builder.Configuration.
            GetConnectionString("PeopleDbConnection"));
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "People API",
    Description = "An ASP.NET Core Web API for managing people items"
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");        
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(s =>
    //{
    //    s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //});
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
