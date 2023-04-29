#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pizzengineering.App/Pizzengineering.App.csproj", "Pizzengineering.App/"]
RUN dotnet restore "Pizzengineering.App/Pizzengineering.App.csproj"
COPY . .
WORKDIR "/src/Pizzengineering.App"
RUN dotnet build "Pizzengineering.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pizzengineering.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pizzengineering.App.dll"]