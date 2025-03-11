#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Minibox.App.Api/Minibox.App.Api.csproj", "Minibox.App.Api/"]
COPY ["Minibox.Core.Data/Minibox.Core.Data.csproj", "Minibox.Core.Data/"]
COPY ["Minibox.Shared.Library/Minibox.Shared.Library.csproj", "Minibox.Shared.Library/"]
COPY ["Minibox.Core.Service/Minibox.Core.Service.csproj", "Minibox.Core.Service/"]
COPY ["Minibox.Shared.Model/Minibox.Shared.Model.csproj", "Minibox.Shared.Model/"]
COPY ["Minibox.Shared.Module.Logging/Minibox.Shared.Module.Logging.csproj", "Minibox.Shared.Module.Logging/"]
COPY ["Minibox.Shared.Module.Mapping/Minibox.Shared.Module.Mapping.csproj", "Minibox.Shared.Module.Mapping/"]
RUN dotnet restore "./Minibox.App.Api/Minibox.App.Api.csproj"
COPY . .
WORKDIR "/src/Minibox.App.Api"
RUN dotnet build "./Minibox.App.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Minibox.App.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Minibox.App.Api.dll"]