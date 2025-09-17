using AspNetCore.Swagger.Themes;
using HomeWork.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<LoggingMiddleware>();
builder.Services.AddTransient<ApiKeyMiddleware>();
builder.Services.AddTransient<ErrorHandlingMiddleware>();
builder.Services.AddTransient<RoleMiddleware>();
builder.Services.AddTransient<NightModeMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(Style.Dark, options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<RoleMiddleware>();
app.UseMiddleware<NightModeMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
