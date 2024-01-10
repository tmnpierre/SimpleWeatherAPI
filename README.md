### SimpleWeatherAPI Documentation

**Introduction**
SimpleWeatherAPI is a RESTful service built with ASP.NET Core, designed to provide weather information. It interfaces with public weather APIs to retrieve current weather data.

**Getting Started**
To run SimpleWeatherAPI locally:
- Clone the repository.
- Ensure .NET 6.0 SDK is installed.
- Execute `dotnet run` within the project directory.

**Endpoints**
`GET /weather/current`: Fetches current weather data.

**Deployment**
To deploy using Docker:
- Ensure Docker is installed.
- Run `docker build -t simpleweatherapi-container .`
- Start the container with `docker run -d -p 8080:80 simpleweatherapi-container`

**Running Tests**
- Use `dotnet test` to run the automated tests.