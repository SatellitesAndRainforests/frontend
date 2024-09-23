# Use the official .NET 8 image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET 8 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and all project files
COPY ["frontend.UI/frontend.UI.csproj", "frontend/src/frontend.UI/"]
COPY ["frontend.Application/frontend.Application.csproj", "frontend/src/frontend.Application/"]
COPY ["frontend.Infrastructure/frontend.Infrastructure.csproj", "frontend/src/frontend.Infrastructure/"]
COPY ["frontend.Domain/frontend.Domain.csproj", "frontend/src/frontend.Domain/"]

# Restore dependencies
RUN dotnet restore "frontend/src/frontend.UI/frontend.UI.csproj"

# Copy the entire source code to the container
COPY . .

# Build the project
WORKDIR "/src/frontend/src/frontend.UI"
RUN dotnet build "frontend.UI.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "frontend.UI.csproj" -c Release -o /app/publish

# Final stage: use runtime-only environment
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "frontend.UI.dll"]

