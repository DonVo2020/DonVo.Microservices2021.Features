#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DonVo.EventSourcing.MongoDB.SourcingAPI/DonVo.EventSourcing.MongoDB.SourcingAPI.csproj", "DonVo.EventSourcing.MongoDB.SourcingAPI/"]
COPY ["DonVo.EventSourcing.EventBusRabbitMQ/DonVo.EventSourcing.EventBusRabbitMQ.csproj", "DonVo.EventSourcing.EventBusRabbitMQ/"]
RUN dotnet restore "DonVo.EventSourcing.MongoDB.SourcingAPI/DonVo.EventSourcing.MongoDB.SourcingAPI.csproj"
COPY . .
WORKDIR "/src/DonVo.EventSourcing.MongoDB.SourcingAPI"
RUN dotnet build "DonVo.EventSourcing.MongoDB.SourcingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DonVo.EventSourcing.MongoDB.SourcingAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DonVo.EventSourcing.MongoDB.SourcingAPI.dll"]
