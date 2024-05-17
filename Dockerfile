# STAGE 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env
WORKDIR /src

ARG NUGET_API_KEY

# Copy the project files first for better layer caching (if only code changes)
COPY *.sln ./
COPY Gateway.* ./Gateway/
COPY Api.* ./Api/

# Add the custom NuGet source
RUN dotnet nuget add source https://nuget.pkg.github.com/trumpee/index.json --name github --username trumpee --password $NUGET_API_KEY --store-password-in-clear-text

# Restore NuGet packages from both sources
RUN dotnet restore Gateway.sln --source "https://api.nuget.org/v3/index.json" --source "https://nuget.pkg.github.com/trumpee/index.json"

# Build the solution
RUN dotnet build Gateway.sln -c Release

# Publish the Api project only (assuming this is the main entry point)
RUN dotnet publish ./Api/Api.csproj -c Release -o /app

# STAGE 2: Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine 
WORKDIR /app
COPY --from=build-env /app .

# Expose the port your application listens on (replace 80 if needed)
EXPOSE 80

# Define the entry point for your application
ENTRYPOINT ["dotnet", "Api.dll"]
