#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FinTracker.HttpApi.Host/FinTracker.HttpApi.Host.csproj", "src/FinTracker.HttpApi.Host/"]
COPY ["src/FinTracker.HttpApi/FinTracker.HttpApi.csproj", "src/FinTracker.HttpApi/"]
COPY ["src/FinTracker.Application.Contracts/FinTracker.Application.Contracts.csproj", "src/FinTracker.Application.Contracts/"]
COPY ["src/FinTracker.EntityFrameworkCore/FinTracker.EntityFrameworkCore.csproj", "src/FinTracker.EntityFrameworkCore/"]
COPY ["src/FinTracker.Domain/FinTracker.Domain.csproj", "src/FinTracker.Domain/"]
COPY ["src/FinTracker.Domain.Shared/FinTracker.Domain.Shared.csproj", "src/FinTracker.Domain.Shared/"]
RUN dotnet restore "./src/FinTracker.HttpApi.Host/./FinTracker.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/FinTracker.HttpApi.Host"
RUN dotnet build "./FinTracker.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinTracker.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinTracker.HttpApi.Host.dll"]