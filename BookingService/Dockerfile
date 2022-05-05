FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookingService/BookingService.csproj", "BookingService/"]
RUN dotnet restore "BookingService/BookingService.csproj"
COPY . .
WORKDIR "/src/BookingService"
RUN dotnet build "BookingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.dll"]

# docker build -f BookingService\Dockerfile -t eyndrahena/booking-service . ; docker push eyndrahena/booking-service ; kubectl rollout restart deploy booking-service-deploy
