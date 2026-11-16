# Pharmacy Management System — Graduation Project Report

**Institution:** AltinbasUniversity  
**Department:** Software Engineering  
**Student:** Deya Ghanem  
**Academic year:** 2026–2027  
**Repository:** https://github.com/deya-ghanem/pharmacy-management-system

> **Document status:** Cumulative report updated through the Week 4 first working prototype. It records the implemented medicine-search workflow and preliminary manual verification. It does not claim that the full application has been implemented, tested, or deployed.

## 1. Introduction

The Pharmacy Management System is planned as a Windows desktop application intended to help pharmacies organize routine operations through one system. The project focuses on medicine search and information management, sales, and inventory control. The broader project vision also mentions purchases, suppliers, customers or patients, user management, and reports; these functions require further requirements clarification and may be implemented in later stages.

### 1.1 Problem Statement
Pharmacy staff need to locate medicines, check availability, complete sales, and maintain stock information. The project aims to bring these operations together in an organized interface. Specific current-state problems and baseline performance have not yet been measured, so quantified improvements are not claimed.

### 1.2 Objective
Develop a fast, organized, and easy-to-use pharmacy management system supporting medicine search, sales, and inventory management, with room to extend the system as requirements are confirmed.

### 1.3 Target Users
Pharmacists, pharmacy employees, and administrators. The detailed role-permission matrix remains to be specified and tested.

## 2. Proposed System

The initial proposed system is a Windows desktop application for one pharmacy computer. Its initial focus is searching medicines by name or barcode, viewing medicine details, adding items to a sale, calculating totals, saving sales, and updating stock. Low-stock alerts, expiry alerts, and basic reports are planned features whose detailed acceptance criteria remain to be finalized.

### 2.1 Scope
**Initial scope:** Windows desktop UI; medicine search and information management; sales and inventory management; foundation for stock alerts and basic reports.  
**Out of scope for the initial version:** AI-based demand forecasting, online pharmacy/e-commerce, external healthcare-system integrations, cross-platform support, cloud dependencies, and multi-branch synchronization.

### 2.2 Assumptions and Constraints
The current design assumes one Windows computer and local SQLite storage. Technology choices are a proposed baseline. Currency, tax, discount, supplier/purchase workflows, and customer/patient data requirements remain to be confirmed.

## 3. Requirements Baseline

| ID | Requirement | Planned verification |
|---|---|---|
| FR-01 | Search by medicine name or barcode. | Seed test records and verify matches and no-match behavior. |
| FR-03 | Add sale items and calculate the total. | Test multiple items, quantity boundaries, and total calculations. |
| FR-04 | Save a sale and update stock. | Test successful sale, insufficient stock, and forced database failure. |
| NFR-01 | Search within 2 seconds with 5,000 records. | Seed 5,000 records and time ten searches; record results after testing. |
| NFR-02 | Maintain sale and inventory consistency. | Test success, cancellation, insufficient stock, database error, and repeat confirmation. |

These tests are planned, not reported as completed. See `docs/requirements.md` for the requirements baseline.

## 4. Methodology

The project is being developed incrementally using weekly milestones, written requirements, architecture/design artifacts, GitHub issues, and version-control commits. Each milestone should produce reviewable evidence such as documentation, diagrams, tests, or a working prototype. The Week 4 goal is a small end-to-end vertical slice rather than the complete application.

Requirements should be linked to design elements and verified with explicit tests. Planned behavior is distinguished from implemented and verified behavior.

## 5. System Architecture

The proposed architecture is a layered monolithic desktop application with presentation, application services, domain logic, and data access layers. SQLite is the local persistence store. The architecture aims to separate UI code from business rules and database operations.

### 5.1 Layer Responsibilities
- **Presentation (WPF):** Displays search, sales, and inventory screens and shows results, totals, confirmations, and errors.
- **Application Services:** Coordinates use cases and calls domain and data-access components.
- **Domain Logic:** Applies medicine, stock, and sale rules; validates quantities and calculates totals.
- **Data Access:** Encapsulates SQLite queries, persistence, and transaction handling.
- **SQLite Database:** Stores medicine, sale, sale-item, and user records locally.

### 5.2 Sale Data Flow
1. Staff searches by medicine name or barcode.
2. The UI sends the query to an application service; data access queries SQLite.
3. Matching medicines and quantities return to the UI.
4. Staff selects a medicine and quantity; domain logic validates stock and calculates totals.
5. On confirmation, one transaction inserts the sale header and lines and decrements stock.
6. Success is displayed only after commit. On validation/database failure, the operation fails and the transaction is rolled back where applicable; stock must remain unchanged after a failed sale.

### 5.3 Preliminary Data Model
- **Medicine:** MedicineId, Name, Barcode, Strength, Form, UnitPrice, QuantityInStock, ExpiryDate, IsActive.
- **Sale:** SaleId, CreatedAt, UserId, TotalAmount, Status.
- **SaleItem:** SaleItemId, SaleId, MedicineId, Quantity, UnitPriceAtSale, LineTotal.
- **User:** UserId, Username, PasswordHash, Role, IsActive.

Each SaleItem belongs to a Sale and references a Medicine. This is a preliminary sketch, not a final schema. Currency, tax, discount, and other business rules must be confirmed.

## 6. Technology Selection

C# with WPF on modern .NET is the proposed desktop baseline. WPF supports XAML, layout controls, and data binding and fits the initial Windows target. Alternatives considered include Windows Forms and Python/PySide6. The trade-off is a Windows-specific UI.

SQLite is the proposed local database. It supports transactions and avoids a separate server for a single-computer prototype. PostgreSQL and SQL Server were considered alternatives. Reassess if multi-branch or high-concurrency needs emerge.

Git and GitHub are used for version control. Confirm the exact .NET version and package compatibility during implementation. These choices remain proposed until validated in a working prototype.

## 7. Design Decisions

- **ADR-01 — C# + WPF on modern .NET:** Chosen for a Windows-first multi-screen desktop application; alternatives are Windows Forms and Python/PySide6; trade-off is Windows-specific UI.
- **ADR-02 — SQLite:** Chosen for local persistence and transactions without a separate server; alternatives are PostgreSQL and SQL Server; reassess if centralized, multi-branch, or high-concurrency operation becomes necessary.

## 8. Security, Failure Handling, and Risks

The design calls for authentication for privileged actions, role-based authorization for edits, validation of required fields and quantities, non-negative stock, password hashing, transaction rollback on sale failures, and tested backup/restore. These are design requirements, not claims of completed implementation.

| Risk | Impact | Mitigation |
|---|---|---|
| Partial sale persistence or duplicate stock deduction | Incorrect sales records or inventory quantities. | One transaction; test forced failure, cancellation, insufficient stock, and repeated confirmation. |
| Slow or ambiguous medicine search | Checkout delay or wrong medicine selection. | Index suitable fields, validate barcode uniqueness, and test the 5,000-record / 2-second target. |

## 9. Week 4 Prototype Goal and Outcome

The Week 4 prototype goal was to demonstrate a meaningful database-application workflow connected to FR-01: searching medicine records by medicine name or barcode. The implemented workflow uses a WPF screen, C# application code, and a local SQLite database. The prototype reads medicine records and displays matching results in a DataGrid.

The implemented scope is limited to medicine retrieval and search. The planned sale workflow (selecting a medicine, entering a quantity, confirming a sale, saving the sale, and reducing stock) was not implemented in this Week 4 prototype and is not claimed as complete.

Preliminary manual verification was performed for partial-name search, barcode search, a query with no matching record, and an empty query. All four observed cases passed. The detailed results are recorded in `docs/implementation/Week-04/verification-results.md`.

## 10. Project Status and Next Steps

**Documented and implemented:** Project definition and scope; requirements baseline; Week 3 design artifacts; a working WPF medicine-search prototype connected to SQLite; a `Medicines` table; three seeded sample medicine records; and four preliminary manual search checks.

**Not claimed complete:** Sales processing, stock deduction through sales, medicine create/update/delete workflows, authentication and role authorization, supplier/customer management, reports, performance verification against 5,000 records, full transaction testing, deployment, or validation with real pharmacy workflows.

**Next steps:** Complete Week 4 repository documentation and progress evidence; review the current prototype and its limitations; then select and implement the next approved requirement for Week 5. Full performance and transaction validation remain planned for later testing stages.

## References
1. Microsoft Learn, WPF overview: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/index
2. SQLite, SQLite Is Transactional: https://www.sqlite.org/transactional.html
3. Microsoft .NET, releases and support: https://learn.microsoft.com/en-us/dotnet/core/releases-and-support

## 11. Implementation and Development Process — Week 4

### 11.1 Implementation Overview

The first working prototype implements the medicine-search functionality associated with FR-01. A user can enter a partial medicine name or a barcode in the WPF search field and view matching medicine records in a DataGrid. The prototype uses C# with WPF for the desktop interface and SQLite for local persistence through the `Microsoft.Data.Sqlite` package.

The implemented code currently consists of the WPF presentation (`MainWindow.xaml`), its code-behind (`MainWindow.xaml.cs`), and the database helper (`Data/Database.cs`). During startup, the application initializes the database and the `Medicines` table, inserts three sample records if the table is empty, and loads the records. The search event filters the loaded medicine collection by name or barcode and updates the DataGrid. This is a working first vertical slice, not the complete layered architecture proposed in Week 3. The current prototype does not implement sales, stock deduction through sales, medicine editing, user authentication, or other planned modules.

### 11.2 Development Environment and Technologies

| Component | Actual technology/version | Role and reason for selection |
|---|---|---|
| Language | C# (language version not independently recorded) | Implements application behavior and search logic. |
| Desktop framework | Windows Presentation Foundation (WPF) | Provides the Windows desktop UI and XAML-based layout. |
| Runtime observed during execution | .NET 11.0.0 | The Visual Studio Output window showed `Microsoft.NETCore.App\\11.0.0`. |
| Database | SQLite | Local database suitable for the single-computer prototype. |
| Data access library | Microsoft.Data.Sqlite 11.0.0 | Provides .NET access to the SQLite database; package version was visible in NuGet. |
| IDE | Microsoft Visual Studio (exact product version not recorded) | Used to edit, build, run, and debug the WPF application. |
| Version control | Git and GitHub | Used to track project changes and publish the repository. |
| Target environment | Windows desktop | Matches the project's initial desktop scope. |

The exact Visual Studio product version and C# language version were not recorded in the available evidence and are therefore not stated. The runtime version above is the version shown in the application execution output; it is not presented as a separately verified target-framework value.

### 11.3 Prototype Implementation

**Feature name:** Medicine Search  
**Requirement ID:** FR-01 — Search by medicine name or barcode.

**Input:** Text entered in the medicine-search field. The input may be a partial medicine name, a barcode, a non-matching term, or an empty string.

**Processing:** On application startup, `Database.Initialize()` creates the local database directory and the `Medicines` table if they do not already exist. `Database.SeedMedicines()` inserts three sample records only when the table is empty. `Database.GetMedicines()` reads the records into a C# collection. When the Search button is clicked, `BtnSearch_Click` trims and lowercases the entered text, filters the loaded collection by medicine name or barcode, and assigns the resulting list to the DataGrid's `ItemsSource`.

**Output:** Matching medicine rows are displayed with name, barcode, price, quantity, and expiry date. A non-matching query produces an empty result list. An empty query returns all loaded medicine records because every string contains the empty string.

**Database table:** `Medicines` contains `Id` (INTEGER primary key, auto-increment), `Name` (TEXT, required), `Barcode` (TEXT, required and unique), `Price` (REAL, required), `Quantity` (INTEGER, required), and `ExpiryDate` (TEXT, required). The database file is stored under the user's local application data directory in a `PharmacyManagementSystem` folder.

**Architecture relationship:** The WPF view is the presentation component. `MainWindow.xaml.cs` currently handles the button event and filtering logic. `Database.cs` handles SQLite initialization, sample-data insertion, and retrieval. SQLite provides persistence. This prototype demonstrates a simplified implementation of the Week 3 presentation/data-access separation; dedicated application-service and domain layers remain future architectural work.

**Figure evidence:** Insert the four authentic screenshots from `docs/implementation/Week-04/evidence/` as Figures 4.1–4.4 (or renumber them to match the university report template). The screenshots show partial-name search, barcode search, no-match behavior, and empty-query behavior. The evidence files are `P01.png` through `P04.png` in `docs/implementation/Week-04/evidence/`.

#### Preliminary Verification

| ID | Input/action | Expected result | Actual result | Status |
|---|---|---|---|---|
| P01 | Search for `Para` | Paracetamol appears. | Paracetamol was displayed. | PASS |
| P02 | Search for barcode `100002` | Amoxicillin appears. | Amoxicillin was displayed with its medicine details. | PASS |
| P03 | Search for `Aspirin` | No matching medicine rows appear. | No medicine records were displayed; the application remained open. | PASS |
| P04 | Leave the search field empty and click Search. | All available sample medicines appear. | Paracetamol, Amoxicillin, and Ibuprofen were displayed. | PASS |

These were manual preliminary checks of the demonstrated search behavior. They do not replace the full testing and validation work scheduled for Week 6. No performance claim for 5,000 records was tested.

### 11.4 Technical Challenges and Solutions

**Challenge 1 — NuGet did not recognize a project while Visual Studio was in Folder View.**  
**Problem:** The NuGet interface reported that no projects were supported by NuGet in the solution.  
**Diagnosis:** The project was initially opened in Folder View rather than through its solution file.  
**Solution:** The `.slnx` solution was opened in Visual Studio, after which the solution-level NuGet interface recognized the project and `Microsoft.Data.Sqlite` 11.0.0 was shown as installed.  
**Outcome:** The SQLite package was available to the application and the database code compiled and ran.  
**Evidence:** Visual Studio NuGet screenshot showing `Microsoft.Data.Sqlite` 11.0.0 installed; retain it in the Week 4 evidence folder if available.

**Challenge 2 — Git initially treated the project files as untracked and the first commit workflow showed a pipe-ended message.**  
**Problem:** The Git repository root and project folder structure needed checking; an early Git Changes view displayed “The pipe has been ended.”  
**Diagnosis and solution:** The repository root was checked with `git rev-parse --show-toplevel`, and `git status --short --untracked-files=all` was used to inspect the tracked/untracked state. The `.gitignore` file was corrected and the intended project files were staged. A later Visual Studio Git Changes view showed a successful push to `origin/main`.  
**Outcome:** The repository displayed a successful push and no unstaged changes at the time of the screenshot. The exact commit hash for the SQLite implementation was not visible in the available evidence and must be copied from Git history before final submission.

No additional technical defects are claimed. The four preliminary search checks passed; no bug-fix commit is reported for those cases.

### 11.5 Development Process — Stage 1

| Field | Stage 1 record |
|---|---|
| Date | 17 November 2026 (report preparation date; adjust if the actual coding session date differs). |
| Objective | Build and demonstrate the first working database-backed prototype for FR-01. |
| Implemented functionality | WPF medicine search by partial name or barcode, backed by SQLite records. |
| Important technical decision | Use SQLite with `Microsoft.Data.Sqlite` 11.0.0 for local persistence in the single-computer desktop prototype. |
| Problem encountered | NuGet project recognition while in Folder View; Git repository/staging workflow required troubleshooting. |
| Solution attempted | Opened the solution file in Visual Studio; verified repository root and Git status; corrected `.gitignore`; installed the SQLite package. |
| Related GitHub commit | The Git Changes panel showed a successful push to `origin/main`; insert the exact commit hash/URL for the SQLite implementation from GitHub commit history before submission. |
| Screenshot/other evidence | `P01.png`, `P02.png`, `P03.png`, and `P04.png` in the Week 4 evidence folder; include the application/database and NuGet screenshots if retained. |
| Next development step | Complete Week 4 documentation and README instructions; then select the next approved Week 5 requirement. |

### 11.6 Current Limitations and Next Steps

The current prototype is limited to reading the medicine records at startup and filtering the loaded collection in memory. It does not yet perform a database query for each search request. The sample database contains three seeded medicine records; the search performance target of 2 seconds with 5,000 records has not been evaluated.

The prototype does not yet support creating, editing, or deleting medicines; sales; sale-item calculations; stock deduction; suppliers; customers; authentication; role-based permissions; reporting; or backup/restore. The implemented prototype also uses a simple code-behind structure; dedicated application-service and domain layers have not yet been introduced.

The next development stage should be selected from the approved requirements and the Week 5 assignment. Any additional functionality must be implemented, demonstrated, tested, and documented before it is described as complete.
