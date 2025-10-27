# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos .csproj das camadas para otimizar o cache
COPY FinancasPessoais.Api/FinancasPessoais.Api.csproj FinancasPessoais.Api/
COPY FinancasPessoais.Domain/FinancasPessoais.Domain.csproj FinancasPessoais.Domain/
COPY FinancasPessoais.Application/FinancasPessoais.Application.csproj FinancasPessoais.Application/
COPY FinancasPessoais.Infra.Data/FinancasPessoais.Infra.Data.csproj FinancasPessoais.Infra.Data/

# Restaura dependências
RUN dotnet restore FinancasPessoais.Api/FinancasPessoais.Api.csproj

# Copia todo o código-fonte
COPY . .

# Publica o projeto
RUN dotnet publish FinancasPessoais.Api/FinancasPessoais.Api.csproj -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expor portas
EXPOSE 5000
EXPOSE 5001

# Definir ponto de entrada
ENTRYPOINT ["dotnet", "FinancasPessoais.Api.dll"]
