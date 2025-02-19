STAR-Events (Event Management System)
ASP.NET Core MVC Project
Built as part of SoftUni's ASP.NET Advanced Course
.NET EF Core Blazor

Event Management System Banner

📌 Overview
A full-stack event management platform where users can:
✅ Organize events, join activities, and discuss via comments.
✅ Secure authentication/authorization using ASP.NET Identity roles (Admin/User).
✅ Admin dashboard for managing users, events, and content.
✅ Image storage via Azure Blob Storage (toggleable via app settings).
✅ Real-time notifications for event updates and interactions.

🛠️ Technical Stack
Backend: ASP.NET Core MVC, Entity Framework Core (Code-First), SQL Server (SSMS/T-SQL)

Frontend: Razor Pages, Blazor Components, Bootstrap

Architecture: Clean Architecture, Repository Pattern, Dependency Injection

Testing: NUnit, Moq (80% code coverage)

Tools: AutoMapper, Azure Blob Storage (images), ASP.NET Identity

🔥 Key Features
Core Functionality
CRUD Operations: Users create/join events; Admins manage users/events.

Role-Based Access: Admin dashboard with elevated privileges.

Image Handling: Upload event/user images to Azure Blob Storage (configurable).

Notifications: In-app alerts for event changes or comments.

Code Quality
SOLID Principles: Services for business logic (decoupled from controllers).

Unit Tests: Mocked dependencies using Moq, focused on auth/services.

Scalable Design: Clean Architecture ensures separation of concerns.

🧠 Challenges & Solutions 
Authentication/Testing Hurdles
Problem: Mocking SignInManager/UserManager for unit tests was complex.
Solution: Abstracted auth logic into an IAuthService interface, injected via DI. Enabled seamless mocking and cleaner test suites.

Azure Blob Storage Integration
Flexibility: Designed a toggleable system (local/Azure) via appsettings.json.

Security: Stored API keys as user secrets to avoid hardcoding.

📊 Future Improvements
Add SignalR for real-time comment updates.

Implement OAuth (Google/Facebook login).

Containerize with Docker for easier deployment.

Note to Employers:

This project demonstrates my ability to build scalable ASP.NET Core solutions with clean architecture, testing, and Azure integration. While it’s my first large-scale MVC project, I focused on maintainable code and SOLID principles. I’d love to discuss how these skills can translate to your backend/full-stack .NET team!

