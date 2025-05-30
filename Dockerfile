FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CaixasAPI.API/CaixasAPI.API.csproj", "CaixasAPI.API/"]
COPY ["CaixasAPI.Domain/CaixasAPI.Domain.csproj", "CaixasAPI.Domain/"]
COPY ["CaixasAPI.Infrastructure/CaixasAPI.Infrastructure.csproj", "CaixasAPI.Infrastructure/"]
RUN dotnet restore "CaixasAPI.API/CaixasAPI.API.csproj"
COPY . .
WORKDIR "/src/CaixasAPI.API"
RUN dotnet build "CaixasAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CaixasAPI.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaixasAPI.API.dll"]
