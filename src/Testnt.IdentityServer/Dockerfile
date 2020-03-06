#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Testnt.IdentityServer/Testnt.IdentityServer.csproj", "src/Testnt.IdentityServer/"]
COPY ["src/Testnt.Idp.App/Testnt.Idp.App.csproj", "src/Testnt.Idp.App/"]
COPY ["src/Testnt.Idp.Domain/Testnt.Idp.Domain.csproj", "src/Testnt.Idp.App/"]
COPY ["src/Testnt.Idp.Infra/Testnt.Idp.Infra.csproj", "src/Testnt.Idp.Infra/"]
COPY ["src/Testnt.PolicyProvider/Testnt.PolicyProvider.csproj", "src/Testnt.PolicyProvider/"]
RUN dotnet restore "src/Testnt.IdentityServer/Testnt.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Testnt.IdentityServer"
RUN dotnet build "Testnt.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testnt.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testnt.IdentityServer.dll"]