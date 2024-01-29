# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy csproj and restore dependencies.
COPY src/ ./src/
RUN dotnet restore ./src/

# Copy the rest of the working directory contents into the container.
COPY . ./

# Build the app.
RUN dotnet publish ./src/MovieBooking.sln -c Release -o out

# Build runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 5011 for the app.
EXPOSE 5011

# Define the command to start the app.
ENTRYPOINT ["dotnet", "MovieBooking.Api.dll"]
