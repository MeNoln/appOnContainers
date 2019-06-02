FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["IdentityApi/IdentityApi.csproj", "IdentityApi/"]
RUN dotnet restore "IdentityApi/IdentityApi.csproj"
COPY . .
WORKDIR "/src/IdentityApi"
RUN dotnet build "IdentityApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IdentityApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 6000/tcp
ENV ASPNETCORE_URLS http://*:6000

ENTRYPOINT ["dotnet", "IdentityApi.dll"]
