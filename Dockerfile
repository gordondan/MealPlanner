# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["MealPlanner.csproj", "."]
RUN dotnet restore "./MealPlanner.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MealPlanner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MealPlanner.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MealPlanner.dll"]