using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<NumberConverter>();

var app = builder.Build();

app.MapGet("/", () => "Hello, World!");
app.MapGet("/convert", (
    [FromQuery] decimal number,
    [FromServices] NumberConverter converter) =>
    {
        if (number < 0 || number >= 1_000_000_000)
            return Results.BadRequest($"Invalid number \"{number}\".");

        var words = converter.Convert(number);

        return Results.Ok(words);
    });

app.Run();
