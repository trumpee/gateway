using Core;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGatewayCore(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddAuthorization(x =>
    x.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAssertion(_ => true)
        .Build());

builder.Services.AddFastEndpoints(cfg =>
{
    cfg.IncludeAbstractValidators = true;
}).SwaggerDocument(opt =>
{
    opt.DocumentSettings = s =>
    {
        s.GenerateExamples = true;
        s.Version = "v1";
        s.Title = "gateway";
    };

    opt.ShortSchemaNames = true;
    opt.EnableJWTBearerAuth = false;
    opt.RemoveEmptyRequestSchema = true;
});

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();