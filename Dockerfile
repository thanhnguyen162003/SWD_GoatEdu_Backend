# Use the official .NET Core SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the .csproj file and restore any dependencies
COPY *.sln .
COPY GoatEdu.API/*.csproj ./GoatEdu.API/
COPY GoatEdu.Core/*.csproj ./GoatEdu.Core/
COPY GoatEdu.Infrastructure/*.csproj ./GoatEdu.Infrastructure/
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build artifacts from the build image
COPY --from=build /app/out .

# Expose the port that the application runs on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "GoatEdu.API.dll"]
