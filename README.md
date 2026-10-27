# Pharmacy Management System

## Project Description

The Pharmacy Management System is a Windows desktop application designed to help pharmacies manage their daily operations through one organized and user-friendly system.

The system aims to make pharmacy operations faster and more efficient, especially when searching for medicines, checking their availability, completing sales, and managing inventory.

## Objective

The main objective of this project is to develop a fast, organized, and easy-to-use pharmacy management system that brings the main pharmacy operations together in one application.

The system will help pharmacists and pharmacy employees manage medicines, inventory, sales, purchases, suppliers, customers or patients, users, and reports.

## Target Users

The main target users are:

- Pharmacists
- Pharmacy Employees
- Administrators

Each user type will have different permissions according to their responsibilities.

## Selected Technologies

- **Platform:** Windows Desktop
- **Programming Language:** C#
- **GUI Framework:** WPF
- **Runtime:** Modern .NET
- **Database:** SQLite
- **Version Control:** Git and GitHub

These technologies are the current proposed baseline for the project and may be reassessed if requirements change.

## Planned Features

- Search for medicines by name or barcode.
- View medicine details, including price, quantity, and expiry date when available.
- Create sales transactions and calculate the total price.
- Automatically update inventory after a confirmed sale.
- Manage medicine records and stock quantities.
- Provide low-stock and expiry alerts.
- Generate basic sales and inventory reports.

## Scope

### In Scope

- Windows desktop application.
- Medicine search and information management.
- Sales and inventory management.
- Basic stock alerts and reports.

### Out of Scope

- AI-based demand forecasting.
- Online pharmacy and e-commerce functionality.
- External integrations with healthcare systems.
- Cross-platform support in the initial version.

## Current Status

- Project definition and initial scope documented.
- Functional and non-functional requirements documented.
- Initial architecture and technical design documented in Week 3.
- Implementation and testing will proceed incrementally according to the project milestones.

## System Architecture

The Pharmacy Management System is planned as a Windows desktop application using C# and WPF on modern .NET, with SQLite for local data storage.

The proposed architecture consists of four logical layers:

- **Presentation Layer:** Provides the user interface for medicine search, sales, and inventory management.
- **Application Services Layer:** Coordinates application use cases and validation.
- **Domain Logic Layer:** Applies business rules, validates sale quantities, checks stock availability, and calculates totals.
- **Data Access Layer:** Handles database queries, persistence, and transaction management.

SQLite stores medicine, sales, sale-item, and user records locally. During a sale, the system is designed to save the sale details and update stock within a single transaction. If the operation fails, the transaction should be rolled back to prevent inconsistent inventory data.

### Design Artifacts

- **Architecture Diagram:** `docs/design/architecture.png`
- **Editable Architecture Diagram:** `docs/design/architecture-source.drawio`
- **Technical Decisions:** `docs/design/design-decisions.md`
- **Component Interfaces:** `docs/design/component-interfaces.md`
- **Sale Flow:** `docs/design/sale-flow.md`
- **Week 3 Progress Log:** `docs/weekly-progress/week03.md`

### Initial Deployment Scope

The initial version targets one Windows computer in a pharmacy. External APIs, cloud dependencies, and multi-branch synchronization are outside the initial scope.

The architecture represents the proposed design. The full application is not claimed to be implemented at this stage.
