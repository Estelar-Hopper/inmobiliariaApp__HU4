FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["inmobiliariaApp.Api/inmobiliariaApp.Api.csproj", "inmobiliariaApp.Api/"]
COPY ["inmobiliariaApp.Application/inmobiliariaApp.Application.csproj", "inmobiliariaApp.Application/"]
COPY ["inmobiliariaApp.Domain/inmobiliariaApp.Domain.csproj", "inmobiliariaApp.Domain/"]
COPY ["inmobiliariaApp.Infrastructure/inmobiliariaApp.Infrastructure.csproj", "inmobiliariaApp.Infrastructure/"]
RUN dotnet restore "inmobiliariaApp.Api/inmobiliariaApp.Api.csproj"
COPY . .
WORKDIR "/src/inmobiliariaApp.Api"
RUN dotnet build "./inmobiliariaApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./inmobiliariaApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "inmobiliariaApp.Api.dll"]
