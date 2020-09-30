#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Fabricdot.WebApi/Fabricdot.WebApi.csproj", "src/Fabricdot.WebApi/"]
COPY ["src/Fabricdot.Infrastructure/Fabricdot.Infrastructure.csproj", "src/Fabricdot.Infrastructure/"]
COPY ["src/Fabricdot.Domain/Fabricdot.Domain.csproj", "src/Fabricdot.Domain/"]
COPY ["src/Fabricdot.Domain.Shared/Fabricdot.Domain.Shared.csproj", "src/Fabricdot.Domain.Shared/"]
RUN dotnet restore "src/Fabricdot.WebApi/Fabricdot.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Fabricdot.WebApi"
RUN dotnet build "Fabricdot.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fabricdot.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fabricdot.WebApi.dll"]
