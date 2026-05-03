# Mini Dynamic Survey Application - Development Status

This document outlines the features, architecture, and components that have been developed so far in the project.

## Project Structure
The project is divided into two main parts:
- **Backend**: Built with .NET 10 (ASP.NET Core Web API) following the Clean Architecture pattern.
- **Frontend**: Currently an empty directory (Not started yet).

---

## Backend (`/backend`)

The backend follows Clean Architecture, organized into four main layers:

### 1. Domain Layer (`SurveyApp.Domain`)
Contains the core business logic and domain models.
- **Entities**:
  - `User`: Represents authentication/user details.
  - `Survey`: Represents a survey created by a user.
  - `Question`: Represents a question belonging to a survey.
  - `QuestionOption`: Represents choices for multiple-choice or similar question types.
  - `Response`: Represents a participant's submission for a survey.
  - `Answer`: Represents a participant's answer to a specific question.
- **Enums**:
  - `QuestionType`: Defines types of questions (e.g., text, multiple choice, etc.).
  - `SurveyStatus`: Defines the state of the survey (e.g., Draft, Published, Closed).
- **Value Objects**:
  - `SurveySlug`: Custom type to handle survey URL slugs.

### 2. Application Layer (`SurveyApp.Application`)
Contains application-specific business rules, use cases, and interfaces.
- **Interfaces**:
  - **Repositories**: `IBaseRepository`, `IUserRepository`, `ISurveyRepository`, `IQuestionRepository`, `IResponseRepository`, `IAnswerRepository`.
  - **Services**: `IAuthService`, `ISurveyService`, `IQuestionService`, `IResponseService`, `IAnalyticsService`.
- **Services (Implementations)**:
  - `AuthService`: Handles user registration and login.
  - `SurveyService`: Handles survey creation, modification, and retrieval.
  - `QuestionService`: Manages questions within surveys.
  - `ResponseService`: Manages user responses to surveys.
  - `AnalyticsService`: Provides data and metrics on survey responses.
- **DTOs (Data Transfer Objects)**:
  - **Auth**: `AuthResponseDto`, `LoginRequestDto`, `RegisterRequestDto`.
  - **Survey**: `CreateSurveyDto`, `UpdateSurveyDto`, `SurveyResponseDto`.
  - **Question**: `CreateQuestionDto`, `UpdateQuestionDto`, `QuestionResponseDto`.
  - **Response**: `SubmitResponseDto`, `ResponseDetailDto`.
- **Validators** (FluentValidation):
  - Validations for creating surveys, creating questions, submitting responses, login, and registration.

### 3. Infrastructure Layer (`SurveyApp.Infrastructure`)
Contains implementations for external concerns like database access.
- **Data Access**:
  - Uses Entity Framework Core (`SurveyDbContext`).
  - **Configurations**: Fluent API configurations for all domain entities (`UserConfiguration`, `SurveyConfiguration`, etc.).
  - **Migrations**: Initial database migration has been generated (`InitialCreate`).
- **Repositories**:
  - Implementations for `BaseRepository`, `UserRepository`, `SurveyRepository`, `QuestionRepository`, `ResponseRepository`, `AnswerRepository`.
- **Dependency Injection**: Extension methods to register infrastructure services (`DependencyInjection.cs`).

### 4. API Layer (`SurveyApp.API`)
The entry point of the backend, exposing RESTful endpoints.
- **Controllers**:
  - `AuthController`: Endpoints for user registration and authentication.
  - `SurveysController`: Endpoints for managing surveys.
  - `QuestionsController`: Endpoints for managing questions within a survey.
  - `ResponsesController`: Endpoints for submitting and viewing survey responses.
  - `AnalyticsController`: Endpoints for retrieving survey analytics.
- **Middleware**: Custom middleware for request pipeline handling (e.g., error handling).
- **Configuration**: Standard ASP.NET Core setup in `Program.cs` and `appsettings.json`.

---

## Frontend (`/frontend`)
- **Status**: Initialized and actively being built.
- **Framework**: Next.js 15 (App Router), React 19, TypeScript.
- **Architecture**: Clean Architecture structure adapted for frontend.
  - **`src/domain/`**: Contains core business entities (`Survey.ts`, `Question.ts`) and repository interfaces (`ISurveyRepository`).
  - **`src/infrastructure/`**: Handles external communications, including `apiClient.ts` for interacting with the .NET Backend.
  - **`src/presentation/`**: Reusable UI components (e.g., `Button.tsx`).
  - **`src/app/`**: Next.js pages and routing, acting as the entry points and controllers.
- **Styling**: Vanilla CSS (`globals.css` and CSS Modules) with a modern, dark-mode, glassmorphism design. No Tailwind CSS is used.

---

## Next Steps
- Implement full `ApiSurveyRepository` conforming to `ISurveyRepository`.
- Create the Survey Builder interface with dynamic question forms.
- Integrate the frontend API client completely with the .NET Core backend.
