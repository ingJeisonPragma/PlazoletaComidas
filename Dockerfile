#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 7160
ENV ASPNETCORE_URLS=http://*:7160

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Food.Api/Food.Api.csproj", "Food.Api/"]
COPY ["./Food.Transversal/Food.Transversal.csproj", "Food.Transversal/"]
COPY ["./Food.DataBase/Food.DataBase.csproj", "Food.DataBase/"]
COPY ["./Food.Domain.Interface/Food.Domain.Interface.csproj", "Food.Domain.Interface/"]
COPY ["./Food.Domain.Business/Food.Domain.Business.csproj", "Food.Domain.Business/"]
COPY ["./Food.Domain.Services/Food.Domain.Services.csproj", "Food.Domain.Services/"]
RUN dotnet restore "Food.Api/Food.Api.csproj"
COPY . .
WORKDIR "/src/Food.Api"
RUN dotnet build "Food.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Food.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Food.Api.dll"]