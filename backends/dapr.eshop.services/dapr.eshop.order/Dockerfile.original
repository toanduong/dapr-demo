#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backends/dapr.eshop.services/dapr.eshop.order/dapr.eshop.order.csproj", "dapr.eshop.order/"]
RUN dotnet restore "dapr.eshop.order/dapr.eshop.order.csproj"
COPY . .
WORKDIR "/src/backends/dapr.eshop.services/dapr.eshop.order"
RUN dotnet build "dapr.eshop.order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dapr.eshop.order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dapr.eshop.order.dll"]