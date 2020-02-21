
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
