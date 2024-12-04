FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY . ./

RUN dotnet restore "PizzeriaBravo.OrderService.API/PizzeriaBravo.OrderService.API.csproj"
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

COPY --from=build-env /App/out ./

EXPOSE 3003
ENV ASPNETCORE_URLS=http://+:3003

ENTRYPOINT ["dotnet", "PizzeriaBravo.OrderService.API.dll"]
