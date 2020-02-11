FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY bin/Debug/netcoreapp3.1/publish .

ENTRYPOINT ["dotnet", "Testnt.Main.Api.Rest.dll"]