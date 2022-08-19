var builder = WebApplication.CreateBuilder(args);

var keys = await new FirebaseSigningKeysGenerator().GetKeysAsync();

builder.Services.AddFirebaseAuthentication(keys)
    .AddFirebaseAuthorization()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<OrganizationService>()
    .AddControllers();

var app = builder.Build();

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