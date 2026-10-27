# Architecture and Technology Decisions
Status: proposed baseline for Week 3; review before implementation.

## ADR-01 — Desktop UI
**Selected:** C# + WPF on a currently supported .NET release.  
**Alternative:** Windows Forms or Python + PySide6.  
**Rationale:** WPF is Windows-only, matching the first release, and supports XAML, data binding, layouts and controls. This helps separate presentation from application behavior. WinForms may be simpler for basic forms; Python/PySide6 is also viable but uses a different ecosystem. This is a project recommendation, not an instructor-imposed requirement.  
**Official reference:** https://learn.microsoft.com/en-us/dotnet/desktop-wpf/overview/index

## ADR-02 — Database
**Selected:** SQLite through a dedicated data-access layer.  
**Alternative:** PostgreSQL or SQL Server.  
**Rationale:** SQLite supports ACID transactions, allowing sale records and stock changes to be committed together or rolled back together. A local database avoids requiring a separate database server for the single-PC prototype. If multi-branch or high concurrent-write needs appear, reassess the choice.  
**Official reference:** https://www.sqlite.org/transactional.html

## Architecture pattern
A layered monolithic desktop application: WPF presentation, application services, domain logic, and data access. This is simpler than introducing a separate backend server for the initial scope.

## Limitations
Initial assumption: one Windows PC and one local database. Backup/restore must be tested before real operational use. This academic prototype is not certified for production pharmacy or regulatory use.
