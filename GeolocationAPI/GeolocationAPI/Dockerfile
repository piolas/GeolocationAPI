FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["GeolocationAPI/GeolocationAPI.csproj", "GeolocationAPI/"]
RUN dotnet restore "GeolocationAPI/GeolocationAPI.csproj"
COPY . .
WORKDIR "/src/GeolocationAPI"
RUN dotnet build "GeolocationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeolocationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeolocationAPI.dll"]