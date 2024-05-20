FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env
WORKDIR /src

ARG NUGET_API_KEY

COPY . ./

RUN dotnet nuget add source https://nuget.pkg.github.com/trumpee/index.json \
    --name github \
    --username trumpee \
    --password $NUGET_API_KEY \
    --store-password-in-clear-text

RUN dotnet restore Gateway.sln \
    --source "https://api.nuget.org/v3/index.json" \
    --source "https://nuget.pkg.github.com/trumpee/index.json"

RUN dotnet build Gateway.sln -c Debug
RUN dotnet publish ./src/Api/Api.csproj -c Debug -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine 
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "Api.dll"]
