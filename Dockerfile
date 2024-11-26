FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY src/CashFlow.Api/*.csproj ./CashFlow.Api/
COPY src/CashFlow.Application/*.csproj ./CashFlow.Application/
COPY src/CashFlow.Communication/*.csproj ./CashFlow.Communication/
COPY src/CashFlow.Domain/*.csproj ./CashFlow.Domain/
COPY src/CashFlow.Exception/*.csproj ./CashFlow.Exception/
COPY src/CashFlow.Infrastructure/*.csproj ./CashFlow.Infrastructure/

WORKDIR /app/CashFlow.Api
RUN dotnet restore

WORKDIR /app
COPY src/ .

WORKDIR /app/CashFlow.Api
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CashFlow.Api.dll"]
