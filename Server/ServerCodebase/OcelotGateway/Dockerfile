FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OcelotGateway/OcelotGateway.csproj", "OcelotGateway/"]
RUN dotnet restore "OcelotGateway/OcelotGateway.csproj"
COPY . .
WORKDIR "/src/OcelotGateway"
RUN dotnet build "OcelotGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OcelotGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 7000/tcp
ENV ASPNETCORE_URLS http://*:7000

ENTRYPOINT ["dotnet", "OcelotGateway.dll"]
