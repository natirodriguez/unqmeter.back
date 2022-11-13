#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=https://+:443;http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UnqMeterAPI/UnqMeterAPI.csproj", "UnqMeterAPI/"]
RUN dotnet restore "UnqMeterAPI/UnqMeterAPI.csproj"
COPY . .
WORKDIR "/src/UnqMeterAPI"
RUN dotnet build "UnqMeterAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnqMeterAPI.csproj" -c Release -o /app/publish

RUN dotnet test

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnqMeterAPI.dll"]
