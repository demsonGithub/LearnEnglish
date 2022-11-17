#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroService/System/Demkin.System.WebApi/Demkin.System.WebApi.csproj", "MicroService/System/Demkin.System.WebApi/"]
COPY ["MicroService/System/Demkin.System.Infrastructure/Demkin.System.Infrastructure.csproj", "MicroService/System/Demkin.System.Infrastructure/"]
COPY ["Shared/Demkin.Infrastructure.Core/Demkin.Infrastructure.Core.csproj", "Shared/Demkin.Infrastructure.Core/"]
COPY ["Shared/Demkin.Domain.Abstraction/Demkin.Domain.Abstraction.csproj", "Shared/Demkin.Domain.Abstraction/"]
COPY ["MicroService/System/Demkin.System.Domain/Demkin.System.Domain.csproj", "MicroService/System/Demkin.System.Domain/"]
COPY ["Shared/Demkin.Utils/Demkin.Utils.csproj", "Shared/Demkin.Utils/"]
COPY ["Shared/Demkin.Core/Demkin.Core.csproj", "Shared/Demkin.Core/"]
RUN dotnet restore "MicroService/System/Demkin.System.WebApi/Demkin.System.WebApi.csproj"
COPY . .
WORKDIR "/src/MicroService/System/Demkin.System.WebApi"
RUN dotnet build "Demkin.System.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demkin.System.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demkin.System.WebApi.dll"]