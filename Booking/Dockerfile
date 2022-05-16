FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Booking/Booking.csproj", "Booking/"]
RUN dotnet restore "Booking/Booking.csproj"
COPY . .
WORKDIR "/src/Booking"
RUN dotnet build "Booking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Booking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Booking.dll"]

# docker build -f Booking\Dockerfile -t eyndrahena/booking . ; docker push eyndrahena/booking ; kubectl rollout restart deploy booking-deploy
