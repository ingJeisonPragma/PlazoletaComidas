FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 7270
ENV ASPNETCORE_URLS=http://*:7270

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./User.Api/User.Api.csproj", "User.Api/"]
COPY ["./User.Transversal/User.Transversal.csproj", "User.Transversal/"]
COPY ["./User.DataBase/User.DataBase.csproj", "User.DataBase/"]
COPY ["./User.Domain.Interface/User.Domain.Interface.csproj", "User.Domain.Interface/"]
COPY ["./UserDomain.Business/User.Domain.Business.csproj", "UserDomain.Business/"]
COPY ["./User.Domain.Services/User.Domain.Services.csproj", "User.Domain.Services/"]
RUN dotnet restore "User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]