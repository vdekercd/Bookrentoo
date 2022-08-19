#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Clients/Organization/DamienVDK.Bookrentoo.OrganizationApp/DamienVDK.Bookrentoo.OrganizationApp.csproj", "Clients/Organization/DamienVDK.Bookrentoo.OrganizationApp/"]
RUN dotnet restore "Clients/Organization/DamienVDK.Bookrentoo.OrganizationApp/DamienVDK.Bookrentoo.OrganizationApp.csproj"
COPY . .
WORKDIR "/src/Clients/Organization/DamienVDK.Bookrentoo.OrganizationApp"
RUN dotnet build "DamienVDK.Bookrentoo.OrganizationApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DamienVDK.Bookrentoo.OrganizationApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DamienVDK.Bookrentoo.OrganizationApp.dll"]