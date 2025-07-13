FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/RadioManager.Api/RadioManager.Api.csproj", "RadioManager.Api/"]
COPY ["src/RadioManager.Application/RadioManager.Application.csproj", "RadioManager.Application/"]
COPY ["src/RadioManager.Domain/RadioManager.Domain.csproj", "RadioManager.Domain/"]
COPY ["src/RadioManager.Infrastructure/RadioManager.Infrastructure.csproj", "RadioManager.Infrastructure/"]

RUN dotnet restore "RadioManager.Api/RadioManager.Api.csproj"

COPY src/. .

WORKDIR "/src/RadioManager.Api"
RUN dotnet build "RadioManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadioManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadioManager.Api.dll"]