#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["src/ProjectName.WebApi/ProjectName.WebApi.csproj", "src/ProjectName.WebApi/"]
COPY ["src/ProjectName.Infrastructure/ProjectName.Infrastructure.csproj", "src/ProjectName.Infrastructure/"]
COPY ["src/ProjectName.Domain/ProjectName.Domain.csproj", "src/ProjectName.Domain/"]
COPY ["src/ProjectName.Domain.Shared/ProjectName.Domain.Shared.csproj", "src/ProjectName.Domain.Shared/"]
RUN dotnet restore "src/ProjectName.WebApi/ProjectName.WebApi.csproj"
COPY . .
WORKDIR "/src/src/ProjectName.WebApi"
RUN dotnet build "ProjectName.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectName.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectName.WebApi.dll"]
