# Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Local build path
#COPY ["PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
#COPY ["PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]

# GitHub Actions path
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]

# Restore dependencies
RUN dotnet restore "PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"

# Copy full solution
COPY . .

# Build solution
# Local build path
#RUN dotnet build "PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj" -c Release
# GitHub Actions path
RUN dotnet build "PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj" -c Release

# Publish
FROM build AS publish

# Local build path
#WORKDIR /src/PizzeriaBravo.OrderService.API

# GitHub Actions path
WORKDIR /src/PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API

RUN dotnet publish "PizzeriaBravo.OrderService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
