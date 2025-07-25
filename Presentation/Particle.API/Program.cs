
using PArticle.Infrastructure;
using PArticle.Persistence;
using PArticle.Application;
using Scalar.AspNetCore;
using Particle.API.Transformers;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
	.AddJsonFile("appsettings.deployment.json", optional: true, reloadOnChange: true);

Env.Load();

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
	options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
}); builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		builder => builder
			.WithOrigins("http://localhost:3000") 
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials());
});

var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
