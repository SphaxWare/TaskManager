# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj files and restore dependencies
COPY *.sln .
COPY TaskManager.API/*.csproj ./TaskManager.API/
COPY TaskManager.Application/*.csproj ./TaskManager.Application/
COPY TaskManager.Domain/*.csproj ./TaskManager.Domain/
COPY TaskManager.Infrastructure/*.csproj ./TaskManager.Infrastructure/
RUN dotnet restore

# Copy everything else and publish
COPY . .
WORKDIR /app/TaskManager.API
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/TaskManager.API/out .
EXPOSE 5103
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]