using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Scalar.AspNetCore;
using SessionBookingService.Application;
using SessionBookingService.Infrastructure;
using SessionBookingService.WebApi.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Host.UseDefaultServiceProvider((_, options) =>
	{
		options.ValidateScopes = true;
		options.ValidateOnBuild = true;
	}
);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.Services
	.AddMediatorServices()
	.AddServices()
	.AddInfrastructure(builder.Configuration);

builder.Services.AddApiVersioning(options =>
	{
		options.DefaultApiVersion = new ApiVersion(1);
		options.ReportApiVersions = true;
		options.AssumeDefaultVersionWhenUnspecified = true;
		options.ApiVersionReader = ApiVersionReader.Combine(
			new UrlSegmentApiVersionReader(),
			new HeaderApiVersionReader("X-Api-Version"));
	})
	.AddApiExplorer(options =>
	{
		options.GroupNameFormat = "'v'V";
		options.SubstituteApiVersionInUrl = true;
	});

builder.Services.AddEndpoints(typeof(Program).Assembly);

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .HasApiVersion(new ApiVersion(2))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(routeGroupBuilder: versionedGroup);


// app.UseMiddleware<EventualConsistencyMiddleware>(); // I'll need this back some day

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
/*
if (!app.Environment.IsEnvironment("Testing"))
{
	using IServiceScope scope = app.Services.CreateScope();
	SessionBookingDbContext dbContext = scope.ServiceProvider.GetRequiredService<SessionBookingDbContext>();
	await dbContext.Database.MigrateAsync();
}*/

app.AddInfrastructureMiddleware();
app.MapControllers();
await app.RunAsync();
