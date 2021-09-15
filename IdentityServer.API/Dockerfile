FROM mcr.microsoft.com/dotnet/sdk:3.1 as build

WORKDIR /app

COPY /IdentityServer.API.csproj /app/IdentityServer.API.csproj

RUN dotnet restore

COPY . .

RUN dotnet build \
    && dotnet publish -c Release -o /build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY --from=build /build ./

ENTRYPOINT ["dotnet", "./IdentityServer.API.dll"]