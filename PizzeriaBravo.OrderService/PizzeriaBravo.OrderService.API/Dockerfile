# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Local build
#COPY ["PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
#COPY ["PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]

RUN dotnet restore

COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess", "PizzeriaBravo.OrderService.DataAccess/"]

WORKDIR "/App/PizzeriaBravo.OrderService.API"
RUN dotnet publish -c Release -o out

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/PizzeriaBravo.OrderService.API/out .
EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
