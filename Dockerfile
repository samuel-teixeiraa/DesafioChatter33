# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Estágio Final / Execução (usando o runtime do .NET 6 compatível com o seu projeto)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "DesafioTP04.dll"]
