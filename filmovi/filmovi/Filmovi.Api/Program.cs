using Filmovi.Api.Data;
using Filmovi.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5152",
            "https://localhost:5152",
            "https://mango-wave-0af19c103.4.azurestaticapps.net"
        )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowClient");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Kina.Any())
    {
        db.Kina.AddRange(
            new Kino { Naziv = "Cinestar" },
            new Kino { Naziv = "Kino Europa" },
            new Kino { Naziv = "Arena Cineplex" },
            new Kino { Naziv = "Kino Tuškanac" }
        );

        db.SaveChanges();
    }
}

app.Run();