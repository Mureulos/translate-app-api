FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["translate-app-api.sln", "./"]
COPY ["translate-app-api/translate-app.Api.csproj", "translate-app-api/"]
COPY ["translate-app.domain/translate-app.Domain.csproj", "translate-app.domain/"]
COPY ["translate-app.application/translate-app.Application.csproj", "translate-app.application/"]
COPY ["translate-app.Infrastructure/translate-app.Infrastructure.csproj", "translate-app.Infrastructure/"]
RUN dotnet restore "translate-app-api.sln"

COPY . .
WORKDIR "/src/translate-app-api"
RUN dotnet build "translate-app.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "translate-app.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "translate-app.Api.dll"]