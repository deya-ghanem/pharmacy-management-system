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

## Implemented Features

The following functionality is currently implemented and demonstrated:

- Search medicines by name or barcode.
- View medicine details, including price, quantity, and expiry date.
- Add new medicine records.
- Update existing medicine records.
- Delete medicine records.
- Validate medicine input fields.
- Create a sale by selecting a medicine and entering a quantity.
- Calculate the sale total based on the medicine price and quantity.
- Save sales and sale-item records in SQLite.
- Automatically update medicine stock after a successful sale.
- Reject invalid sale quantities and sales exceeding available stock.
- Use SQLite transactions during the sale workflow.

## Planned Features

The following features remain part of the future development scope:

- User authentication and role-based permissions.
- Supplier management.
- Customer or patient management.
- Purchase management.
- Low-stock and expiry alerts.
- Sales and inventory reports.
- Backup and restore functionality.
- Additional systematic testing and performance evaluation.

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

The project has progressed from the initial Week 4 medicine-search prototype to a working Week 5 core application.

Completed milestones:

- Project definition, scope, and requirements documented.
- Initial architecture and technical design documented in Week 3.
- Week 4: SQLite integration and medicine search by name or barcode.
- Week 5 Stage 2: Medicine management operations, including add, update, delete, and search.
- Week 5 Stage 3: Sales workflow integrated with SQLite inventory updates.
- Medicine model separated into its own Models module.
- Database operations organized in the Database class.
- Input validation and error handling added to the main workflows.
- Development records and screenshot evidence added under `docs/Week5/`.

The application has passed the basic manual demonstrations performed during Week 5. Comprehensive testing, performance evaluation, and validation under broader conditions are planned for Week 6.

## System Architecture

The application currently uses:

- **Presentation Layer:** WPF interface implemented in MainWindow.xaml.
- **UI Coordination:** MainWindow.xaml.cs handles interface events, user input, and coordination of operations.
- **Medicine Model:** Models/Medicine.cs represents medicine records.
- **Data Access Layer:** Data/Database.cs handles SQLite initialization, medicine CRUD operations, and sales persistence.
- **Database:** SQLite stores medicine records, sales, and sale items.

The current implementation does not yet contain separate Application Services or Domain Logic layers. Some validation and workflow coordination remain in the window code-behind and database component. These layers may be introduced in a future refactoring stage.

The sale workflow uses a database transaction to record the sale, store its items, and update medicine stock. If the operation fails, the transaction is rolled back.

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
## Launch Instructions

### Requirements
- Windows operating system
- Visual Studio 2022 or later
- .NET SDK compatible with the project
- NuGet package restore support

### Run the Application Using Visual Studio
1. Clone or download the repository.
2. Open `PharmacyManagementSystem.slnx` in Visual Studio.
3. Allow Visual Studio to restore the NuGet packages.
4. Set `PharmacyManagementSystem` as the startup project.
5. Press **F5** or click **Start** to run the application.

### Run Using the .NET CLI
Open a terminal in the project directory and execute:

```bash
dotnet restore
dotnet build
dotnet run
```

### Database Initialization
The application uses SQLite for local data storage.

- The database is created automatically when the application starts.
- The database file is stored in the user's local application data directory:
  `%LOCALAPPDATA%/PharmacyManagementSystem/pharmacy.db`
- Sample medicine records are inserted automatically when the database is empty.

### Current Prototype Features
- Search medicines by name.
- Search medicines by barcode.
- Display available medicine records.
- Store medicine data locally using SQLite.

### Development Documentation

- Week 5 Stage 2: `PharmacyManagementSystem/docs/Week5/Development_Stage_2.md`
- Week 5 Stage 3: `PharmacyManagementSystem/docs/Week5/Development_Stage_3.md`
- Week 5 Evidence: `PharmacyManagementSystem/docs/Week5/evidence/`

  
**Note:** This is an initial working prototype. Sales management, inventory transactions, and other planned features have not yet been implemented.
