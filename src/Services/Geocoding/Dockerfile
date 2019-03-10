FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Geocoding/Geocoding.csproj src/Services/Geocoding/
RUN dotnet restore src/Services/Geocoding/Geocoding.csproj
COPY . .
WORKDIR /src/src/Services/Geocoding
RUN dotnet build Geocoding.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Geocoding.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Geocoding.dll"]
