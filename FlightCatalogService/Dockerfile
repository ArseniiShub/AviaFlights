FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FlightCatalogService/FlightCatalogService.csproj", "FlightCatalogService/"]
RUN dotnet restore "FlightCatalogService/FlightCatalogService.csproj"
COPY . .
WORKDIR "/src/FlightCatalogService"
RUN dotnet build "FlightCatalogService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlightCatalogService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightCatalogService.dll"]

# docker build -f FlightCatalogService\Dockerfile -t eyndrahena/flight-catalog-service . ; docker push eyndrahena/flight-catalog-service ; kubectl rollout restart deploy flight-catalog-service-depl
