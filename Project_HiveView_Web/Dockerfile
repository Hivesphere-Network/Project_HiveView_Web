#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Project_HiveView_Web.csproj", "Project_HiveView_Web/"]
RUN dotnet restore "./Project_HiveView_Web/Project_HiveView_Web.csproj"

WORKDIR "/src/Project_HiveView_Web"
COPY . .
RUN dotnet build "Project_HiveView_Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project_HiveView_Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project_HiveView_Web.dll"]