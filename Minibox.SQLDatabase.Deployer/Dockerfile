# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Minibox.SQLDatabase.Deployer/Minibox.SQLDatabase.Deployer.csproj", "Minibox.SQLDatabase.Deployer/"]
RUN dotnet restore "./Minibox.SQLDatabase.Deployer/Minibox.SQLDatabase.Deployer.csproj"
COPY . . 
WORKDIR "/src/Minibox.SQLDatabase.Deployer"
RUN dotnet build "./Minibox.SQLDatabase.Deployer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Minibox.SQLDatabase.Deployer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Switch to root user before installing SQLPackage
USER root

# Install SQLPackage for database deployment
RUN apt-get clean && apt-get update --allow-releaseinfo-change && apt-get install -y unzip curl && \
    curl -o sqlpackage.zip https://aka.ms/sqlpackage-linux && \
    unzip sqlpackage.zip -d /opt/sqlpackage && \
    chmod +x /opt/sqlpackage/sqlpackage

# Switch back to a non-root user for security
USER app

ENTRYPOINT ["dotnet", "Minibox.SQLDatabase.Deployer.dll"]
