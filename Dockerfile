FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BelenLibraryNowAPI/BelenLibraryNowAPI.csproj", "BelenLibraryNowAPI/"]
RUN dotnet restore "BelenLibraryNowAPI/BelenLibraryNowAPI.csproj"
COPY . .
WORKDIR "/src/BelenLibraryNowAPI"
RUN dotnet build "BelenLibraryNowAPI.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR /src
COPY --from=build /app/build .
RUN dotnet publish "BelenLibraryNowAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "BelenLibraryNowAPI.dll"]
