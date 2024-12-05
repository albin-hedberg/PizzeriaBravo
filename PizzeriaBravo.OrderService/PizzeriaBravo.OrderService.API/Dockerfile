# Stage 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# docker-compose build
#COPY ["PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
#COPY ["PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]

# Restore dependencies
RUN dotnet restore "PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"

# Copy full solution
COPY . .
#COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API", "PizzeriaBravo.OrderService.API/"]
#COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess", "PizzeriaBravo.OrderService.DataAccess/"]

# Build solution
RUN dotnet build "PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj" -c Release

# Stage 2: Publish WebShop (target PizzaService project)
FROM build AS publish
WORKDIR /src/PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API
RUN dotnet publish "PizzeriaBravo.OrderService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
