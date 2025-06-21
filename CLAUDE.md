# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 9 Blazor Server application called MealPlanner. It uses interactive server-side rendering with Blazor components.

## Common Commands

- **Build the project**: `dotnet build`
- **Run the application**: `dotnet run`
- **Run in development mode**: `dotnet run --launch-profile Development`
- **Restore packages**: `dotnet restore`
- **Clean build artifacts**: `dotnet clean`

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