using Aspire.Hosting;
using DamienVDK.Bookrentoo.Common;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgresConnection(Components.PostgreSqlDatabase, "postgres://postgres:mysecretpassword@127.0.0.1:5432/");

var organizationApi = builder.AddProject<Projects.DamienVDK_Bookrentoo_OrganizationApi>(Components.OrganizationApi);

builder.AddProject<Projects.DamienVDK_Bookrentoo_OrganizationApp>(Components.OrganizationApp)
    .WithReference(organizationApi);

builder.Build().Run();
