#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProjectName.WebApi/ProjectName.WebApi.csproj", "src/ProjectName.WebApi/"]
COPY ["src/ProjectName.Infrastructure/ProjectName.Infrastructure.csproj", "src/ProjectName.Infrastructure/"]
COPY ["src/ProjectName.Domain/ProjectName.Domain.csproj", "src/ProjectName.Domain/"]
COPY ["src/ProjectName.Domain.Shared/ProjectName.Domain.Shared.csproj", "src/ProjectName.Domain.Shared/"]
RUN dotnet restore "src/ProjectName.WebApi/ProjectName.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ProjectName.WebApi"
RUN dotnet build "ProjectName.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProjectName.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectName.WebApi.dll"]
