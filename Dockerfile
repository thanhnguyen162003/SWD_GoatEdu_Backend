# Use the official .NET SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /SWD_GoatEdu_Backend

# Copy the solution file and project files
COPY GoatEdu.sln .
COPY GoatEdu.API/GoatEdu.API.csproj GoatEdu.API/
COPY GoatEdu.Core/GoatEdu.Core.csproj GoatEdu.Core/
COPY GoatEdu.Infrastructure/GoatEdu.Infrastructure.csproj GoatEdu.Infrastructure/


# Set environment to Development to enable Swagger
ENV ASPNETCORE_ENVIRONMENT=Development

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR /SWD_GoatEdu_Backend/GoatEdu.API
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image as the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build artifacts from the build image
COPY --from=build /app/publish .

# Expose the port that the application runs on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "GoatEdu.API.dll", "--environment=Development"]
