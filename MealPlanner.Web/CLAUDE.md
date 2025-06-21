# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 9 Blazor Server application called MealPlanner organized as a solution with multiple projects:
- **MealPlanner.Web**: Main Blazor Server application with interactive server-side rendering
- **MealPlanner.Tests**: Integration tests using Puppeteer for browser automation

## Common Commands

### Solution-level commands (run from root directory):
- **Build all projects**: `dotnet build`
- **Restore packages for all projects**: `dotnet restore`
- **Run tests**: `dotnet test`
- **Clean all projects**: `dotnet clean`

### Web application commands (run from MealPlanner.Web directory or specify project):
- **Run the web application**: `dotnet run --project MealPlanner.Web`
- **Run in development mode**: `dotnet run --project MealPlanner.Web --launch-profile Development`

### Test commands:
- **Run integration tests**: `dotnet test MealPlanner.Tests`
- **Run specific test**: `dotnet test MealPlanner.Tests --filter "TestMethodName"`

## Architecture

- **Framework**: ASP.NET Core 9.0 with Blazor Server
- **Rendering**: Interactive server-side rendering mode
- **Project Structure**:
  - `Program.cs`: Application entry point and service configuration
  - `Components/`: Contains all Blazor components
    - `App.razor`: Root application component with HTML structure
    - `Routes.razor`: Router configuration
    - `Layout/`: Layout components including MainLayout and NavMenu
    - `Pages/`: Page components (Home, Counter, Weather, Error)
  - `wwwroot/`: Static assets including CSS, images, and libraries
  - Uses Bootstrap for styling

## Development Notes

- The application runs on HTTPS (port 7111) and HTTP (port 5178) by default
- Static assets are mapped using the new .NET 9 static asset system
- Antiforgery tokens are enabled for security
- Interactive server rendering mode is configured for all components
- The application includes rating images (good.png, great.png, meh.png, ok.png, yuck.png) suggesting meal rating functionality