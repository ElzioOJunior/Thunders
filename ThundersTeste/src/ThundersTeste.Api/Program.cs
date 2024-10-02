using ThundersTeste.Application.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using MediatR;
using ThundersTeste.Infra.CrossCutting.IoC.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer();
builder.Services.AddSwaggerSetup();
builder.Services.AddDependencyInjectionSetup(builder.Configuration);
builder.Services.AddMediatR(typeof(CommandHandler));
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(corsBuilder =>
{
    corsBuilder.WithOrigins("*");
    corsBuilder.AllowAnyOrigin();
    corsBuilder.AllowAnyMethod();
    corsBuilder.AllowAnyHeader();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerSetup(apiVersionDescriptionProvider);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
