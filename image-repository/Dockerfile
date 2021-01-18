#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["image-repository/image-repository.csproj", "image-repository/"]
RUN dotnet restore "image-repository/image-repository.csproj"
COPY . .
WORKDIR "/src/image-repository"
RUN dotnet build "image-repository.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "image-repository.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "image-repository.dll"]