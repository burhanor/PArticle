
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

string[] allowedOrigins = builder.Configuration
	.GetSection("AllowedOrigins")
	.Get<string[]>()!;
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
			.WithOrigins(allowedOrigins) 
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials());
});

var app = builder.Build();

app.UseStaticFiles();// Configure the HTTP request pipeline.
app.UseRouting();
app.MapOpenApi();
app.MapScalarApiReference();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();

app.MapControllers();

app.Run();
