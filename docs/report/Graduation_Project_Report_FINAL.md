# Pharmacy Management System — Graduation Project Report

**Institution:** Altınbaş University  
**Department:** Software Engineering  
**Student:** Deya Ghanem  
**Academic year:** 2026–2027  
**Repository:** https://github.com/deya-ghanem/pharmacy-management-system

> **Document status:** Cumulative report for the project stage documented to date. It consolidates the project definition, requirements baseline, and Week 3 design. It does not claim that the full application has been implemented, tested, or deployed.

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

## 9. Week 4 Prototype Goal

Demonstrate a Windows desktop screen that searches seeded medicine records by name or barcode, lets a user select a medicine and enter a quantity, confirms a test sale, and displays the saved sale with reduced stock. Evidence should include a UI screenshot and database output before and after the sale. If needed, first prove transaction behavior through a minimal UI or test harness.

## 10. Project Status and Next Steps

**Documented:** Project definition and scope in README; requirements in `docs/requirements.md`; proposed architecture, requirement traceability, component responsibilities, sale flow, preliminary data model, two technology decisions, risks, and Week 4 goal in Week 3 design artifacts.

**Not claimed complete:** Full application implementation, execution of performance and transaction tests, deployment, or validation with real pharmacy workflows.

**Next steps:** Confirm open business rules; implement the Week 4 vertical slice; test search performance and sale rollback; update this report with implementation and test evidence; keep README, progress logs, and GitHub commits aligned with actual progress.

## References
1. Microsoft Learn, WPF overview: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/index
2. SQLite, SQLite Is Transactional: https://www.sqlite.org/transactional.html
3. Microsoft .NET, releases and support: https://learn.microsoft.com/en-us/dotnet/core/releases-and-support
