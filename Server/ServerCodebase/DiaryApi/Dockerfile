FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DiaryApi/DiaryApi.csproj", "DiaryApi/"]
RUN dotnet restore "DiaryApi/DiaryApi.csproj"
COPY . .
WORKDIR "/src/DiaryApi"
RUN dotnet build "DiaryApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DiaryApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 7100/tcp
ENV ASPNETCORE_URLS http://*:7100
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:7100/hc || exit 1

ENTRYPOINT ["dotnet", "DiaryApi.dll"]
