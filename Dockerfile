FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build-env
WORKDIR src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "./Api.dll"]
