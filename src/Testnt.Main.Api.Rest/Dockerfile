# Build Image Stage
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
ENV ASPNETCORE_ENVIRONMENT=Production
#ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish src/Testnt.Main.Api.Rest -o /publish


# Runtime Image Stage
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Testnt.Main.Api.Rest.dll"]