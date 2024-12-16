FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj", "PizzeriaBravo.OrderService.API/"]
COPY ["PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.DataAccess/PizzeriaBravo.OrderService.DataAccess.csproj", "PizzeriaBravo.OrderService.DataAccess/"]

RUN dotnet restore "PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"

COPY . .

RUN dotnet build "PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj" -c Release

FROM build AS publish
WORKDIR /src/PizzeriaBravo.OrderService/PizzeriaBravo.OrderService.API

RUN dotnet publish "PizzeriaBravo.OrderService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003
ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
