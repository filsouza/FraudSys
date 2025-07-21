using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using FraudSys.Application.UseCases;
using FraudSys.Application.UseCases.Interfaces;
using FraudSys.Domain.Interfaces;
using FraudSys.Infrastructure.Config;
using FraudSys.Infrastructure.Dynamo;
using FraudSys.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<AWSSettings>(builder.Configuration.GetSection("AWS"));

builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<AWSSettings>>().Value);

builder.Services.AddSingleton<DynamoDbContext>();

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAWSService<AmazonDynamoDBClient>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddScoped<ICadastrarClienteUseCase, CadastrarClienteUseCase>();
builder.Services.AddScoped<IConsultarClienteUseCase, ConsultarClienteUseCase>();
builder.Services.AddScoped<IAtualizarLimiteUseCase, AtualizarLimiteUseCase>();
builder.Services.AddScoped<IRemoverClienteUseCase, RemoverClienteUseCase>();


builder.Services.AddScoped<RealizarTransacaoPixUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
