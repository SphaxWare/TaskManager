# Use the .NET 9.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /taskmanager

# Copy the solution file
COPY TaskManager.sln ./

# Copy the project folders with their full relative paths
COPY src/TaskManager.API/ src/TaskManager.API/
COPY src/TaskManager.Application/ src/TaskManager.Application/
COPY src/TaskManager.Domain/ src/TaskManager.Domain/
COPY src/TaskManager.Infrastructure/ src/TaskManager.Infrastructure/

# Restore dependencies (this uses the full folder structure, so paths must match .sln)
RUN dotnet restore

# Copy everything else (source code, configs, etc)
COPY . .

# Build the project in Release mode
RUN dotnet publish -c Release -o /app

# Runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /app ./

# Tell ASP.NET to listen on port 5103
ENV ASPNETCORE_URLS=http://+:5103

# Expose port
EXPOSE 5103

ENTRYPOINT ["dotnet", "TaskManager.API.dll"]