# Base image for running the application (ASP.NET Core Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Image for building the application (SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Install Entity Framework tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Copy only the UserService project files
COPY ["UserService/UserService.csproj", "UserService/"]
RUN dotnet restore "UserService/UserService.csproj"

# Copy only the UserService source code
COPY UserService/ UserService/
WORKDIR "/src/UserService"
RUN dotnet build "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: use the runtime image and add migrations
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entry point to run migrations and start the app
ENTRYPOINT ["bash", "-c", "dotnet ef database update --no-build && dotnet UserService.dll"]
