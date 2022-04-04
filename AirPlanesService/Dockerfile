FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AirplanesService/AirplanesService.csproj", "AirplanesService/"]
RUN dotnet restore "AirplanesService/AirplanesService.csproj"
COPY . .
WORKDIR "/src/AirplanesService"
RUN dotnet build "AirplanesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirplanesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirplanesService.dll"]

# docker build -f AirplanesService\Dockerfile -t eyndrahena/airplanes-service .
# docker run -p 8080:80 -d eyndrahena/platforms-service
# docker push eyndrahena/airplanes-service
