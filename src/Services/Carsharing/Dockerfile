FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Carsharing/Carsharing.csproj src/Services/Carsharing/
RUN dotnet restore src/Services/Carsharing/Carsharing.csproj
COPY . .
WORKDIR /src/src/Services/Carsharing
RUN dotnet build Carsharing.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Carsharing.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Carsharing.dll"]
