using DamienVDK.Bookrentoo.Common;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using DamienVDK.Bookrentoo.OrganizationApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var keys = await new FirebaseSigningKeysGenerator().GetKeysAsync();

builder.AddNpgsqlDbContext<OrganizationDbContext>(Components.PostgreSqlDatabase);
builder.Services.AddFirebaseAuthentication(keys)
    .AddFirebaseAuthorization()
    .AddAutoMapper(typeof(MappingProfile))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();