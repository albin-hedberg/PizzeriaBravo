# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]
RUN dotnet restore "./PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"
COPY . .
WORKDIR "/src/PizzeriaBravo.OrderService.API"
RUN dotnet build "./PizzeriaBravo.OrderService.API.csproj" -c Release -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
RUN dotnet publish "./PizzeriaBravo.OrderService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
