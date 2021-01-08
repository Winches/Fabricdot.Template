#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/FabricdotApp.WebApi/FabricdotApp.WebApi.csproj", "src/FabricdotApp.WebApi/"]
COPY ["src/FabricdotApp.Infrastructure/FabricdotApp.Infrastructure.csproj", "src/FabricdotApp.Infrastructure/"]
COPY ["src/FabricdotApp.Domain/FabricdotApp.Domain.csproj", "src/FabricdotApp.Domain/"]
COPY ["src/FabricdotApp.Domain.Shared/FabricdotApp.Domain.Shared.csproj", "src/FabricdotApp.Domain.Shared/"]
RUN dotnet restore "src/FabricdotApp.WebApi/FabricdotApp.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FabricdotApp.WebApi"
RUN dotnet build "FabricdotApp.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FabricdotApp.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FabricdotApp.WebApi.dll"]
