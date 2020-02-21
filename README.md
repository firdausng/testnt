# testnt
Example Multi-tenant Test Management system Project using 
1. ASP.NET Core 3.1 (Clean Architecture)
2. IdentityServer4
3. EF Core using PostgreSQL

identityserver4 with docker ref https://brainwipe.github.io/docker/dotnet/oauth/identityserver/2017/10/30/oauth-on-docker-part2/

## Domain Driven Design - Clean Architecture
inspired by https://github.com/JasonGT/NorthwindTraders

## Run Server from Visual Studio 2019
1. set startup project to docker-compose
2. run 

## Run Server from shell
1. build dockerfile from root directory(from root solution folder)
```sh
docker build -f src/Testnt.Main.Api.Rest/Dockerfile -t testnt/rest-server .
docker build -f src/Testnt.Main.Api.Rest/ClientApp/Dockerfile -t testnt/client .
docker build -f src/Testnt.IdentityServer/Dockerfile -t testnt/identity-server .

```
2. Run docker compose(from root solution folder)
```sh
docker-compose  -f "docker-compose.yml" -f "docker-compose.override.yml"   --no-ansi up -d --no-build
```