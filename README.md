# Movie Booking App

This is a movie booking application built with .NET Core and MongoDB.

## Prerequisites

- Docker https://docs.docker.com/engine/install/
- .NET Core SDK/runtime https://dotnet.microsoft.com/en-us/download/dotnet/8.0

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Installation

1. Clone the repository:

```
git clone https://github.com/deva8907/booking-app.git
```

2. Run dockers containers

```
docker-compose up --build
```

3. Access the application at http://localhost:5011

## Description

The application consists of the following services:

```booking-app```: This is the main application service. It's a .NET Core application that serves the movie booking API.

```mongo```: This is the MongoDB database service.

The database connection string and logging configurations are maintained in the appsettings.json file. The application uses a Docker network named movieBookingNetwork for communication between the booking-app and mongo services.

System data like Cinema, Movie and City data are preloaded in the application startup

### Architecture

Vertical slice architecture is followed in creating Classess, Services and Repositories. Folder structure is created based on features rather than technical layers like Service, Repository etc.

Business logic like validations and creation of entity is pushed into the Domain class itself following domain modelling principles, example- MovieShow domain model

## Usage

1. Follow the [Installation](#installation) section to start the application
2. Import the postman collection- movie-booking-app-postman-collection.json in the root directory. 
3. Show management folder in the collection has endpoints to manage shows for admin users
4. System folder has only the GET endpoints for system data like Cinema, Movie and City
5. Movie booking folder currently only has Search show endpoint. Future scope would be to add endpoint for booking a show

## To do

1. Write unit and integration tests for the app
2. Introduce Read and Update endpoints for managing system data Cinema, City and Movie
3. Introduce endpoint for booking a ticket
