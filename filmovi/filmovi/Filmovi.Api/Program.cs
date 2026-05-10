using Filmovi.Api.Data;
using Filmovi.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=filmovi.db"));

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.AllowAnyOrigin()
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

    db.Database.Migrate();

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