# GuacActings

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Overview

GuacActings is a web API for managing movie castings. It provides endpoints for creating, updating, and retrieving castings, as well as managing the actors and movies associated with the castings.

## Prerequisites

Before running the application, you will need to have the following installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started)

## Getting Started

1. Clone the repository:

```bash
git clone https://github.com/yourusername/guacactings.git
```

2. Create a '.env' file in the root directory of the project and add the following environment variables:

```bash
SA_PASSWORD=yourStrong(!)Password
```
Replace "yourStrong(!)Password" with a strong password of your choice.

3. Create a 'appsettings.json' file in the root directory of the project and add the following configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=guacactings;User=sa;Password=IveBeenPirated76!;TrustServerCertificate=true;MultipleActiveResultSets=True"
  },
  "apiKey": "yourApiKey"
}
```
Replace "yourStrong(!)Password" with the password you chose in step 2.
Replace "yourApiKey" with a strong API key of your choice.

4. Build and run the Docker container for the SQL Server database:

```bash
docker-compose up -d
```

5. Navigate to the Swagger UI at [https://localhost:8080/swagger/index.html](https://localhost:8080/swagger/index.html) to test the API.


## Development

To run the application in development mode, run the following command:

```bash
dotnet watch run
```

This will run the application and automatically restart it when changes are made to the source code.

## Testing

To run the unit tests, run the following command:

```bash
dotnet test
```