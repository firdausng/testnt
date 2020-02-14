#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:12.16.0 AS clientApp
WORKDIR /src
RUN npm install -g @angular/cli
COPY ["src/Testnt.Main.Api.Rest/ClientApp/package.json", "."]
RUN npm install
COPY ["src/Testnt.Main.Api.Rest/ClientApp", "."]
RUN npm run build
#COPY ["src/Testnt.Main.Api.Rest/ClientApp/dist/ClientApp/", "dist/"]



FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Testnt.Main.Api.Rest/Testnt.Main.Api.Rest.csproj", "src/Testnt.Main.Api.Rest/"]
COPY ["src/Testnt.Main.Application/Testnt.Main.Application.csproj", "src/Testnt.Main.Application/"]
COPY ["src/Testnt.Common/Testnt.Common.csproj", "src/Testnt.Common/"]
COPY ["src/Testnt.Main.Domain/Testnt.Main.Domain.csproj", "src/Testnt.Main.Domain/"]
COPY ["src/Testnt.Main.Infrastructure/Testnt.Main.Infrastructure.csproj", "src/Testnt.Main.Infrastructure/"]
RUN dotnet restore "src/Testnt.Main.Api.Rest/Testnt.Main.Api.Rest.csproj"
COPY --from=clientApp "src/dist/ClientApp/" "src/Testnt.Main.Api.Rest/wwwroot/"
COPY . .
WORKDIR "/src/src/Testnt.Main.Api.Rest"
RUN dotnet build "Testnt.Main.Api.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Testnt.Main.Api.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Testnt.Main.Api.Rest.dll"]