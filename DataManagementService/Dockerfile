FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataManagementService/DataManagementService.csproj", "DataManagementService/"]
RUN dotnet restore "DataManagementService/DataManagementService.csproj"
COPY . .
WORKDIR "/src/DataManagementService"
RUN dotnet build "DataManagementService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataManagementService.dll"]

# docker build -f DataManagementService\Dockerfile -t eyndrahena/data-mng-service . ; docker push eyndrahena/data-mng-service ; kubectl rollout restart deploy data-mng-service-depl
