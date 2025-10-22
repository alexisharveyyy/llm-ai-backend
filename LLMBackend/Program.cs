using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LLMBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register services (no interfaces)
builder.Services.AddHttpClient();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<KernelService>();
builder.Services.AddScoped<SearchService>();

var app = builder.Build();

// Configure pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();