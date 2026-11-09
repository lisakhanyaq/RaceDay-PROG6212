# RaceDay

## Project Overview
RaceDay is a web application for managing sporting events (runs, walks and cycles). Organisers create and manage events, categories and results. Participants browse events, enter them, and view their results.

**Student:** [Lisakhanya Qwaka] | **Student number:** [ST10485274] | **Module:** PROG6212 (Programming 2B) | **Part:** 3 / Final PoE

## User Roles

### Organiser
- Dashboard of their events and enrolment counts
- Create, edit and delete events (with banner image)
- Manage categories for each event
- View enrolments and their statuses
- Capture results (finish time and finishing position)

### Participant
- Browse and filter upcoming events
- View event details and categories
- Enter events and view My Events
- View My Results
- Update their profile and profile picture

## Solution Structure
```
RaceDay
|-- RaceDay.Api          (Part 2: REST API, business logic, database, Azure Blob upload)
|-- RaceDay.Contracts    (shared DTOs/request models)
|-- RaceDay.Api.Tests    (unit tests)
|-- RaceDay.Mvc          (Part 3: ASP.NET Core MVC web front end)
|-- .github/workflows    (CI/CD)
`-- README.md
```

## Architecture
Browser -> **RaceDay.Mvc** -> **RaceDay.Api** -> SQL Server / Azure Blob Storage

RaceDay.Mvc does not connect to the database or to Azure Storage directly. It sends all data operations to RaceDay.Api over HTTP.

## Prerequisites
- Visual Studio 2022 (ASP.NET and web development workload)
- .NET 8 SDK
- SQL Server (or [your database setup])
- Docker Desktop (Linux containers, WSL 2)
- An Azure Storage account [or Azurite, if that is what you used]

## Database Setup
1. Open `RaceDay.Api/appsettings.json` and set the SQL Server connection string.
2. In Package Manager Console, run `Update-Database`.

## How to Run RaceDay.Api
1. Open `RaceDay.sln` in Visual Studio.
2. Set **RaceDay.Api** as the startup project and press F5.
3. Swagger opens at [https://localhost:7282/swagger/index.html].

## How to Run RaceDay.Mvc Locally
1. Make sure `ApiSettings:BaseUrl` in `RaceDay.Mvc/appsettings.json` matches the address the API is running on [http://localhost:5129/`].
2. Start both projects (Solution > Properties > Multiple startup projects: RaceDay.Api and RaceDay.Mvc), then press F5.

## Azure Blob Storage Setup
1. Create a storage account and a container named `raceday-images` in the Azure portal.
2. Store the connection string in the API's **User Secrets** (right-click RaceDay.Api > Manage User Secrets):
```json
{
  "AzureBlobStorage": {
    "ConnectionString": "<your connection string>",
    "ContainerName": "raceday-images"
  }
}
```
3. The connection string is never committed to GitHub.

Image uploads travel **MVC -> API -> Azure Blob Storage**. The API saves the blob URL in the database and MVC displays the image from that URL.

## Docker Setup
Docker packages the RaceDay.Mvc web application in a container. The API runs on the host computer.

### Build Image
Run from the folder containing `RaceDay.sln`:
```
docker build -f RaceDay.Mvc/Dockerfile -t raceday-mvc:part3 .
```

### Start the API for Docker
```
dotnet run --project RaceDay.Api --urls http://0.0.0.0:5000
```

### Run Container
```
docker run -d --name raceday-mvc -p 8080:8080 -e ApiSettings__BaseUrl=http://host.docker.internal:5000/ raceday-mvc:part3
```
Then open **http://localhost:8080**.

The API must be running and reachable at the configured base URL. Inside the container, `localhost` refers to the container itself, so `host.docker.internal` is used to reach the API on the host.

## Authentication and Session Flow
1. The user registers or logs in through MVC, which calls the API.
2. MVC stores the user's ID, role and display name in its session.
3. MVC also stores the API's session cookie and sends it with later API requests.
4. Menus and page access depend on the role. Unauthenticated users are redirected to the login page. Logout clears the session.

## CI/CD
GitHub Actions restores, builds and tests the whole solution (including RaceDay.Mvc) on every push.

## GitHub Actions Screenshot
![CI build]

## Video Presentation
[Paste your video link here]
