#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoordsTelegram.API/CoordsTelegram.API.csproj", "CoordsTelegram.API/"]
COPY ["CoordsTelegram.Domain/CoordsTelegram.Domain.csproj", "CoordsTelegram.Domain/"]
COPY ["CoordsTelegram.App/CoordsTelegram.App.csproj", "CoordsTelegram.App/"]
COPY ["CoordsTelegram.EF_Core/CoordsTelegram.EF_Core.csproj", "CoordsTelegram.EF_Core/"]
COPY ["CoordsTelegram.Infrastructure/CoordsTelegram.Infrastructure.csproj", "CoordsTelegram.Infrastructure/"]
COPY ["CoordsTelegram.Notifications/CoordsTelegram.Notifications.csproj", "CoordsTelegram.Notifications/"]
COPY ["CoordsTelegram.RabbitMQ/CoordsTelegram.RabbitMQ.csproj", "CoordsTelegram.RabbitMQ/"]
COPY ["CoordsTelegram.Telegram/CoordsTelegram.TelegramBot.csproj", "CoordsTelegram.TelegramBot/"]
RUN dotnet restore "CoordsTelegram.API/CoordsTelegram.API.csproj"
COPY . .
WORKDIR "/src/CoordsTelegram.API"
RUN dotnet build "CoordsTelegram.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoordsTelegram.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoordsTelegram.API.dll"]