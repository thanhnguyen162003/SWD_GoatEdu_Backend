#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoatEdu.API/GoatEdu.API.csproj", "GoatEdu.API/"]
COPY ["GoatEdu.Core/GoatEdu.Core.csproj", "GoatEdu.Core/"]
COPY ["GoatEdu.Infrastructure/GoatEdu.Infrastructure.csproj", "GoatEdu.Infrastructure/"]
RUN dotnet restore "GoatEdu.API/GoatEdu.API.csproj"
COPY . .
WORKDIR "/src/GoatEdu.API"
RUN dotnet build "GoatEdu.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoatEdu.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoatEdu.API.dll"]