#引入镜像，低版本 docker 去掉  AS base
#FROM registry.cn-chengdu.aliyuncs.com/jimlicatpub/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#配置工作目录
WORKDIR /app
#暴露容器端口，此端口与程序运行路径一致，可
EXPOSE 80

#复制文件到工作目录
COPY . .


#启动程序
ENTRYPOINT ["dotnet", "Demkin.System.WebApi.dll"]