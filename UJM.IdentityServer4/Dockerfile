#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../UJM.IdentityServer4/UJM.IdentityServer4.csproj", "../UJM.IdentityServer4/"]
RUN dotnet restore "../UJM.IdentityServer4/UJM.IdentityServer4.csproj"
COPY . .
WORKDIR "/src/../UJM.IdentityServer4"
RUN dotnet build "UJM.IdentityServer4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UJM.IdentityServer4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UJM.IdentityServer4.dll"]