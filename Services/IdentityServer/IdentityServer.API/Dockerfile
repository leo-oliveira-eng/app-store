#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/IdentityServer/IdentityServer.API/IdentityServer.API.csproj", "Services/IdentityServer/IdentityServer.API/"]
COPY ["Services/IdentityServer/IdentityServer.Data/IdentityServer.Data.csproj", "Services/IdentityServer/IdentityServer.Data/"]
COPY ["Services/IdentityServer/IdentityServer.Domain/IdentityServer.Domain.csproj", "Services/IdentityServer/IdentityServer.Domain/"]
COPY ["Services/IdentityServer/IdentityServer.CrossCutting/IdentityServer.CrossCutting.csproj", "Services/IdentityServer/IdentityServer.CrossCutting/"]
COPY ["Services/IdentityServer/IdentityServer.ExternalServices/IdentityServer.ExternalServices.csproj", "Services/IdentityServer/IdentityServer.ExternalServices/"]
COPY ["Services/IdentityServer/IdentityServer.Application/IdentityServer.Application.csproj", "Services/IdentityServer/IdentityServer.Application/"]
COPY ["Services/IdentityServer/IdentityServer.Messaging/IdentityServer.Messaging.csproj", "Services/IdentityServer/IdentityServer.Messaging/"]
RUN dotnet restore "Services/IdentityServer/IdentityServer.API/IdentityServer.API.csproj"
COPY . .
WORKDIR "/src/Services/IdentityServer/IdentityServer.API"
RUN dotnet build "IdentityServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.API.dll"]