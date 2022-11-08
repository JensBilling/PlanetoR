#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./PlanetoR/PlanetoR.csproj" --disable-parallel
RUN dotnet publish "./PlanetoR/PlanetoR.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app .
EXPOSE 5199
ENTRYPOINT ["dotnet", "PlanetoR.dll"]

