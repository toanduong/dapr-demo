#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dapr.eshop.product/dapr.eshop.product.csproj", "dapr.eshop.product/"]
RUN dotnet restore "dapr.eshop.product/dapr.eshop.product.csproj"
COPY . .
WORKDIR "/src/dapr.eshop.product"
RUN dotnet build "dapr.eshop.product.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dapr.eshop.product.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dapr.eshop.product.dll"]