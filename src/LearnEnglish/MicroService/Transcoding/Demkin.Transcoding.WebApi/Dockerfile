#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN sed -i 's/deb.debian.org/mirrors.ustc.edu.cn/g' /etc/apt/sources.list \
    && apt-get update \
    && apt-get install -y ffmpeg \
    && apt-get clean && apt-get autoclean && apt-get autoremove \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8084
EXPOSE 8184

COPY . .

ENTRYPOINT ["dotnet", "Demkin.Transcoding.WebApi.dll"]