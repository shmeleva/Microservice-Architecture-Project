FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/Identity/Identity.csproj src/Services/Identity/
RUN dotnet restore src/Services/Identity/Identity.csproj
COPY . .
WORKDIR /src/src/Services/Identity
RUN dotnet build Identity.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Identity.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.dll"]
