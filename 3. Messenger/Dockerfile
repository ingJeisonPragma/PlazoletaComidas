FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 7264
ENV ASPNETCORE_URLS=http://*:7264

#Imagen Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Messenger.Api/Messenger.Api.csproj", "Messenger.Api/"]
COPY ["./Messenger.Domain.Business/Messenger.Domain.Business.csproj", "Messenger.Domain.Business/"]
COPY ["./Messenger.Domain.Services/Messenger.Domain.Services.csproj", "Messenger.Domain.Services/"]
RUN dotnet restore "Messenger.Api/Messenger.Api.csproj"
COPY . .
WORKDIR "/src/Messenger.Api"
RUN dotnet build "Messenger.Api.csproj" -c Release -o /app/build

#Imagen Publish
FROM build AS publish
RUN dotnet publish "Messenger.Api.csproj" -c Release -o /app/publish

#Publish a Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messenger.Api.dll"]