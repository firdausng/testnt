# testnt

## To create database 
```sh
Update-Database -Context PersistedGrantDbContext -Verbose
Update-Database -Context ConfigurationDbContext -Verbose
```

## To seed data
1. Go to ```appsettings.Development.json```
2. set **Seed** to true

## To run IdentityServer
1. set **Testnt.IdentityServer** as startup project 
2. run ```dotnet run```
