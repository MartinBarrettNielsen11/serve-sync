using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using ClubAdministrationService.Application;
using ClubAdministrationService.Infrastructure;
using ClubAdministrationService.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(opts => opts.AddServerHeader = false);
builder.Host.UseDefaultServiceProvider((_, opts) =>
{
	opts.ValidateScopes = true;
	opts.ValidateOnBuild = true;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
		.AddMediatorServices()
		.AddServices()
		.AddPersistence(builder.Configuration);
//.AddInfrastructure(builder.Configuration);

builder.Services
		.AddApiVersioning(opts =>
		{
			opts.DefaultApiVersion = new ApiVersion(1);
			opts.ReportApiVersions = true;
			opts.AssumeDefaultVersionWhenUnspecified = true;
			opts.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
																new HeaderApiVersionReader("X-Api-Version"));
		})
		.AddApiExplorer(opts =>
		{
			opts.GroupNameFormat = "'v'V";
			opts.SubstituteApiVersionInUrl = true;
		});

builder.Services.AddEndpoints(typeof(Program).Assembly);

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
								.HasApiVersion(new ApiVersion(1))
								.HasApiVersion(new ApiVersion(2))
								.ReportApiVersions()
								.Build();

RouteGroupBuilder versionedGroup = app.MapGroup("api/v{version:apiVersion}")
									.WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

// app.UseMiddleware<EventualConsistencyMiddleware>(); I'll need this back at some point.

// Configure the HTTP request pipeline for DEVELOPMENT only
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference(opts =>
	{
		opts.Title = Assembly.GetExecutingAssembly().GetName().Name!;
		opts.Theme = ScalarTheme.DeepSpace;
	});
}

if (!app.Environment.IsEnvironment("Testing"))
{
	using IServiceScope scope = app.Services.CreateScope();
	ClubDbContext dbContext = scope.ServiceProvider.GetRequiredService<ClubDbContext>();
	await dbContext.Database.MigrateAsync();
}

app.UseExceptionHandler();
app.AddInfrastructureMiddleware();
await app.RunAsync();
