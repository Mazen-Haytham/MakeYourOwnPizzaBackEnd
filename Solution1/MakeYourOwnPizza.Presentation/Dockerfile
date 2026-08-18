# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files for restore caching
COPY ["MakeYourOwnPizza.Presentation/MakeYourOwnPizza.Presentation.csproj", "MakeYourOwnPizza.Presentation/"]
COPY ["MakeYourOwnPizza.Application/MakeYourOwnPizza.Application.csproj", "MakeYourOwnPizza.Application/"]
COPY ["MakeYourOwnPizza.Infrastructure/MakeYourOwnPizza.Infrastructure.csproj", "MakeYourOwnPizza.Infrastructure/"]
COPY ["MakeYourOwnPizza.Domain/MakeYourOwnPizza.Domain.csproj", "MakeYourOwnPizza.Domain/"]
RUN dotnet restore "MakeYourOwnPizza.Presentation/MakeYourOwnPizza.Presentation.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/MakeYourOwnPizza.Presentation"
RUN dotnet build "MakeYourOwnPizza.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MakeYourOwnPizza.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MakeYourOwnPizza.Presentation.dll"]