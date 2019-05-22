FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TodoListApi/TodoListApi.csproj", "TodoListApi/"]
RUN dotnet restore "TodoListApi/TodoListApi.csproj"
COPY . .
WORKDIR "/src/TodoListApi"
RUN dotnet build "TodoListApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TodoListApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

EXPOSE 7200/tcp
ENV ASPNETCORE_URLS http://*:7200
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:7200/hc || exit 1

ENTRYPOINT ["dotnet", "TodoListApi.dll"]
