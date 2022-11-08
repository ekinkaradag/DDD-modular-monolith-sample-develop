using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Serilog;
using TestApp.Api;
using TestApp.Api.Validation;
using TestApp.BuildingBlocks.Application;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.BuildingBlocks.Domain;
using TestApp.BuildingBlocks.Module;
using TestApp.ModuleIntegration.EntryPoint;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    var modules = AppAssemblies.Discover(
                          name => name.Name is not null
                                  && name.Name.StartsWith("TestApp"))
                      .ToArray() ??
                  throw new InvalidOperationException(
                      "Can not load the referenced assemblies for dependency injection");

    var connectionString = builder.Configuration.GetConnectionString("local");

    var moduleIntegrationModule = new ModuleIntegrationModule(connectionString, modules);

    containerBuilder.RegisterModule(moduleIntegrationModule);
}));

builder.Host.UseSerilog((context, configuration) =>
    configuration.WriteTo.ColoredConsole());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails(x =>
{
    x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
    x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
    x.Map<NotFoundException>(ex => new NotFoundExceptionProblemDetails(ex));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var initializers = app.Services.GetAutofacRoot().Resolve<IEnumerable<IModuleInitializer>>();

foreach (var initializer in initializers)
{
    await initializer.Run();
}

app.Run();