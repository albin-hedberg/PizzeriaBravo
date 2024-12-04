# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy the project files
COPY PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj ./PizzeriaBravo.OrderService.API/
COPY PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj ./PizzeriaBravo.OrderService.DataAccess/

# Restore dependencies
RUN dotnet restore "./PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"

# Copy the rest of the files
COPY . ./

# Publish the application
RUN dotnet publish "./PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj" -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copy the build output
COPY --from=build-env /App/out .

# Expose port and set environment variable
EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003

# Set the entry point
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API.dll"]
