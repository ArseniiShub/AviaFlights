﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Management/Management.csproj", "Management/"]
RUN dotnet restore "Management/Management.csproj"
COPY . .
WORKDIR "/src/Management"
RUN dotnet build "Management.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.dll"]

# docker build -f Management\Dockerfile -t eyndrahena/management . ; docker push eyndrahena/management ; kubectl rollout restart deploy management-deploy
