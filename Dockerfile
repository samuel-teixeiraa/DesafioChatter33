# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Estágio Final / Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

# Define a porta dinâmica exigida pelo Render e inicia a aplicação
ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "DesafioChatter33.dll"]
