#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BricsSocial.Api/BricsSocial.Api.csproj", "src/BricsSocial.Api/"]
COPY ["src/BricsSocial.Application/BricsSocial.Application.csproj", "src/BricsSocial.Application/"]
COPY ["src/BricsSocial.Domain/BricsSocial.Domain.csproj", "src/BricsSocial.Domain/"]
COPY ["src/BricsSocial.Infrastructure/BricsSocial.Infrastructure.csproj", "src/BricsSocial.Infrastructure/"]
RUN dotnet restore "src/BricsSocial.Api/BricsSocial.Api.csproj"
COPY . .
WORKDIR "/src/src/BricsSocial.Api"
RUN dotnet build "BricsSocial.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BricsSocial.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BricsSocial.Api.dll"]