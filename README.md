# testnt
Example Multi-tenant Test Management system Project using 
1. ASP.NET Core 3.1 (Clean Architecture)
2. IdentityServer4
3. EF Core using PostgreSQL

identityserver4 with docker ref https://brainwipe.github.io/docker/dotnet/oauth/identityserver/2017/10/30/oauth-on-docker-part2/

## Domain Driven Design - Clean Architecture
inspired by https://github.com/JasonGT/NorthwindTraders


## To create database 
```sh
Update-Database -Context TestntIdentityDbContext -Verbose
Update-Database -Context PersistedGrantDbContext -Verbose
Update-Database -Context ConfigurationDbContext -Verbose
```

## To seed data for Identityserver
1. Go to ```appsettings.Development.json```
2. set **Seed** to true

## To run IdentityServer
1. set **Testnt.IdentityServer** as startup project 
2. run ```dotnet run```


## To rerun Migration for Identityserver
1. open **Package Manager Console** on Visual Studio
```sh
Drop-Database -c TestntIdentityDbContext -Verbose
Drop-Database -c PersistedGrantDbContext -Verbose
Drop-Database -c ConfigurationDbContext -Verbose

Remove-Migration -c TestntIdentityDbContext -Verbose
Remove-Migration -c PersistedGrantDbContext -Verbose
Remove-Migration -c ConfigurationDbContext -Verbose

Add-Migration InitialTestntDbMigration -c TestntIdentityDbContext -o Data/Migrations/Main/TestntMainDb -Verbose
Add-Migration InitialPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb -Verbose
Add-Migration InitialConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb -Verbose

Update-Database -Context TestntIdentityDbContext -Verbose
Update-Database -Context PersistedGrantDbContext -Verbose
Update-Database -Context ConfigurationDbContext -Verbose

```


## To run Rest API Server
1. set **Testnt.Main.Api.Rest** as startup project 
2. run ```dotnet run```


## To rerun Migration for Rest API Server
1. open **Package Manager Console** on Visual Studio
```sh
Drop-Database -c TestntDbContext -Verbose
Remove-Migration -c TestntDbContext -Verbose
Add-Migration addtenantIdConstructor -c TestntDbContext -o Data/Migrations -Verbose
Update-Database -Context TestntDbContext -Verbose


```

## Docker Support
1. build dockerfile from root directory(root solution folder)
```sh
docker build -f src/Testnt.Main.Api.Rest/Dockerfile -t testnt/rest-server .
docker build -f src/Testnt.IdentityServer/Dockerfile -t testnt/identity-server .

```
2. Run container
```sh
docker network create testnt_network
docker run --network=testnt_network --name postgres-server -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres
docker run --network=testnt_network --name identity-server -e "Client:Ip:0=http://rest-server:7000" -e "ConnectionStrings:DefaultConnection=Host=postgres-server;Database=TestntIdentity;Username=postgres;Password=postgres" -p 5000:80 -d testnt/identity-server
docker run --network=testnt_network --name rest-server -e "IdentityServer:Url:http://identity-server:5000" -e "ConnectionStrings:PostgresTestntMainConnectionString=Host=postgres-server;Database=Testnt;Username=postgres;Password=postgres" -p 7000:80 -d testnt/rest-server

```