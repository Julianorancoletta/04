# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Delivery.Email/Delivery.Email/Delivery.Email.Worker.csproj", "Delivery.Email/Delivery.Email/"]
COPY ["Delivery.Email/Delivery.Email.Core/Delivery.Email.Core.csproj", "Delivery.Email/Delivery.Email.Core/"]
COPY ["Delivery.Email/Delivery.Email.Infra/Delivery.Email.Infra.csproj", "Delivery.Email/Delivery.Email.Infra/"]
RUN dotnet restore "./Delivery.Email/Delivery.Email/Delivery.Email.Worker.csproj"
COPY . .
WORKDIR "/src/Delivery.Email/Delivery.Email"
RUN dotnet build "./Delivery.Email.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Delivery.Email.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery.Email.Worker.dll"]