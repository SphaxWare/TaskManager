# Use .NET 9 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./ 
COPY ../TaskManager.Application/*.csproj ../TaskManager.Application/
COPY ../TaskManager.Domain/*.csproj ../TaskManager.Domain/
COPY ../TaskManager.Infrastructure/*.csproj ../TaskManager.Infrastructure/
RUN dotnet restore

# Copy everything else and build
COPY . .
COPY ../TaskManager.Application ../TaskManager.Application
COPY ../TaskManager.Domain ../TaskManager.Domain
COPY ../TaskManager.Infrastructure ../TaskManager.Infrastructure
RUN dotnet publish -c Release -o out

# Use ASP.NET runtime for running
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port
EXPOSE 5103

# Environment variable for SQLite
ENV ConnectionStrings__DefaultConnection="Data Source=tasks.db"

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]