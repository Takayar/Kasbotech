#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kasbotech_AvgCandle/Kasbotech_AvgCandle.csproj", "Kasbotech_AvgCandle/"]
RUN dotnet restore "./Kasbotech_AvgCandle/Kasbotech_AvgCandle.csproj"
COPY . .
WORKDIR "/src/Kasbotech_AvgCandle"
RUN dotnet build "./Kasbotech_AvgCandle.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kasbotech_AvgCandle.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kasbotech_AvgCandle.dll"]