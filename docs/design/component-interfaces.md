# Component Responsibilities and Interfaces

## System boundary
Initial release: Windows desktop application on one pharmacy computer with a local SQLite database. No external API, cloud service, or remote multi-branch synchronization is included.

| Component | Responsibility / input | Output / connection |
|---|---|---|
| WPF Presentation | Search text, sale lines, user actions | Displays results, totals, confirmations and validation messages |
| Application Services | UI commands and current-user context | Coordinates use cases, authorization and calls domain/data layers |
| Domain Logic | Medicine, quantity and sale inputs | Validated sale, calculated total and stock rules |
| Data Access Layer | Queries and persistence requests | SQLite reads/writes, transactions and mapped records |
| SQLite Database | SQL statements | Stores medicines, sales, sale items and users |

## Planned application interfaces
- `SearchMedicines(query: string) -> MedicineSearchResult[]`
- `CreateSale(items: SaleItemInput[], userId: int) -> SaleResult`
- `UpdateMedicine(medicine: MedicineRecord) -> OperationResult`

These are conceptual application interfaces, not HTTP endpoints. The first release has no REST API.

## Data consistency
Sale header, sale lines, and inventory decrements must be committed in one transaction. If validation or a database operation fails before commit, roll back so stock remains unchanged.

## Security and validation
Require authentication for privileged actions; authorize medicine edits and stock adjustments by role; validate positive quantities and non-negative stock; store password hashes, never plaintext passwords. Database access belongs in the data-access layer, not UI event handlers.
