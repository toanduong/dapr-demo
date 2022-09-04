#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["backends/dapr.eshop.services/dapr.eshop.catalog/dapr.eshop.catalog.csproj", "dapr.eshop.catalog/"]
COPY ["backends/dapr.eshop.services/dapr.eshop.healthcheck/dapr.eshop.healthcheck.csproj", "dapr.eshop.healthcheck/"]

RUN dotnet restore "dapr.eshop.catalog/dapr.eshop.catalog.csproj"
COPY . .
WORKDIR "/src/backends/dapr.eshop.services/dapr.eshop.catalog"
RUN dotnet build "dapr.eshop.catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dapr.eshop.catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dapr.eshop.catalog.dll"]