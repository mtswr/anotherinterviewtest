# Interview .NET test 

A Task Management System RESTful API built with .NET 8 that helps teams manage their projects and tasks efficiently. This API provides a robust foundation for task management, allowing teams to organize work, track progress, and collaborate effectively.

## What's Inside?

- 📋 **Project Management**: Create and manage projects with ease
- ✅ **Task Management**: Organize tasks with priorities and status tracking
- 📊 **Task Priority System**: Assign and manage task priorities
- 📝 **Task History**: Track all changes made to tasks
- 📈 **Performance Reports**: Get insights into team productivity
- 💬 **Task Comments**: Enable team collaboration through comments

## Tech Stack

- .NET 8
- Entity Framework Core
- SQL Server
- Docker
- xUnit for testing

## Project Structure

The solution is organized into four main projects:

- **TaskManagement.API**: The web API layer with controllers and endpoints
- **TaskManagement.Core**: Contains domain models and business logic
- **TaskManagement.Infrastructure**: Handles data access and external services
- **TaskManagement.Tests**: Houses all unit and integration tests

## Getting Started

### Prerequisites

- .NET 8 SDK
- Docker and Docker Compose
- SQL Server (for local development)

### Quick Start with Docker

1. Build the containers:
```bash
docker-compose build
```

2. Start the application:
```bash
docker-compose up
```

The API will be available at `http://localhost:5000`

### Local Development

1. Install dependencies:
```bash
dotnet restore
```

2. Start the API:
```bash
cd TaskManagement.API
dotnet run
```

## API Endpoints

### Projects
- `GET /api/projects` - Get all projects
- `POST /api/projects` - Create a new project
- `GET /api/projects/{id}` - Get project details
- `GET /api/projects/{id}/tasks` - List project tasks

### Tasks
- `POST /api/projects/{projectId}/tasks` - Add a new task
- `PUT /api/tasks/{id}` - Update a task
- `DELETE /api/tasks/{id}` - Remove a task
- `POST /api/tasks/{id}/comments` - Add a comment

## Key Features

1. **Smart Task Management**
   - Tasks have three priority levels (low, medium, high)
   - Priority is locked after task creation
   - Maximum 20 tasks per project

2. **Project Safety**
   - Projects with pending tasks cannot be deleted
   - Clear error messages guide users to complete or remove tasks first

3. **Change Tracking**
   - Every task update is recorded with timestamp and user info
   - Full history of changes is maintained

4. **Team Collaboration**
   - Comment system for task discussions
   - Performance reports for managers
   - Role-based access control

## Future Considerations

### Questions for Product Owner

1. **User Experience**
   - Should we add email notifications for task updates?
   - Would task templates be useful for recurring work?
   - Do we need to support task dependencies?

2. **Team Management**
   - How should we handle team hierarchies?
   - Should we implement team-specific views?
   - Do we need to support external team members?

3. **Integration**
   - Should we integrate with popular tools like Jira or Trello?
   - Do we need webhook support for external systems?
   - Should we implement an API key system?

### Technical Roadmap

1. **Architecture Improvements**
   - Implement CQRS for better scalability
   - Add event sourcing for enhanced audit trails
   - Consider microservices for larger deployments

2. **Performance Enhancements**
   - Add Redis caching for frequent queries
   - Implement pagination for large datasets
   - Add background job processing

3. **Security & Monitoring**
   - Implement JWT authentication
   - Add rate limiting
   - Set up application insights
   - Implement health checks

4. **DevOps**
   - Set up CI/CD pipeline
   - Implement infrastructure as code
   - Add automated deployment scripts 