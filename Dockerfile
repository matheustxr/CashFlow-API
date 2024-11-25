# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia os arquivos .csproj para restaurar dependências
COPY src/CashFlow.API/*.csproj ./CashFlow.API/
COPY src/CashFlow.Application/*.csproj ./CashFlow.Application/
COPY src/CashFlow.Communication/*.csproj ./CashFlow.Communication/
COPY src/CashFlow.Domain/*.csproj ./CashFlow.Domain/
COPY src/CashFlow.Exception/*.csproj ./CashFlow.Exception/
COPY src/CashFlow.Infrastructure/*.csproj ./CashFlow.Infrastructure/

# Restaura dependências
WORKDIR /app/CashFlow.API
RUN dotnet restore

# Copia o restante do código e publica o projeto
WORKDIR /app
COPY src/ .
WORKDIR /app/CashFlow.API
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os binários gerados na etapa anterior
COPY --from=build-env /app/out .

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "CashFlow.Api.dll"]

