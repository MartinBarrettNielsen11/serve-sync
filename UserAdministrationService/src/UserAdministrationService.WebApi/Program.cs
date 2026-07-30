using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Scalar.AspNetCore;
using UserAdministrationService.Application;
using UserAdministrationService.Infrastructure;
using UserAdministrationService.WebApi.Extensions;

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

app.MapEndpoints(versionedGroup);

//app.UseMiddleware<EventualConsistencyMiddleware>(); //I'll need this back some day

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi().AllowAnonymous();
	app.MapScalarApiReference(opts =>
	{
		opts.Title = Assembly.GetExecutingAssembly().GetName().Name!;
		opts.Theme = ScalarTheme.Kepler;
	}).AllowAnonymous();
}

/*
if (!app.Environment.IsEnvironment("Testing"))
{
	using IServiceScope scope = app.Services.CreateScope();
	UserDbContext dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
	await dbContext.Database.MigrateAsync();
}*/

app.AddInfrastructureMiddleware();
app.MapControllers();
await app.RunAsync();
