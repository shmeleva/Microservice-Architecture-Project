FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y unzip

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

#RUN wget https://releases.hashicorp.com/consul-template/0.20.0/consul-#template_0.20.0_linux_arm.zip -O consul-template.zip
#RUN unzip consul-template.zip
#RUN touch appsettings.tpl
#RUN echo "{{ key \"Carsharing/appsettings.json\" }}" > appsettings.tpl
#RUN ./consul-template -consul-addr "consul:8500" -template "appsettings.tpl:appsettings.json"
