using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Optional: Use controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(settings =>
{
    settings.Title = "Minimal API";
    settings.Version = "v1";
});

var app = builder.Build();
app.UseDeveloperExceptionPage();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapGet("/", (Func<string>)(() => "Hello World!"))
    .WithTags("General");

app.MapGet("/sum/{a}/{b}", (Func<int, int, int>)((a, b) => a + b))
    .WithName("CalculateSum")
    .WithTags("Calculator");

app.MapGet("/abs({a})", (Func<int, int>)(a => Math.Abs(a)))
    .WithName("AbsoluteValue")
    .WithTags("Calculator");

// Optional: Use controllers
app.UseRouting();
app.MapControllers();

app.Run();

// Optional: Use controllers
[ApiController]
[Route("examples")]
public class ExampleController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok("Get Method");
	}
}