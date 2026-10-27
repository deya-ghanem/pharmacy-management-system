# Week 3 Progress — Architecture and Technical Design

**Project:** Pharmacy Management System  
**Student:** Deya Ghanem  
**Repository:** https://github.com/deya-ghanem/pharmacy-management-system  
**Status:** Draft package prepared; student review and GitHub push still required.

## 1. Requirements-to-design traceability

| ID | Requirement | Design response |
|---|---|---|
| FR-01 | Search medicines by name/barcode | WPF search calls application service; data-access layer queries indexed fields. |
| FR-03 | Add medicines to sale and calculate total | Sales UI collects lines; domain logic validates quantities and calculates total. |
| FR-04 | Save confirmed sale and update stock | Sale, sale items and stock deductions occur in one SQLite transaction; rollback on failure. |
| NFR-01 | Search within 2 seconds with 5,000 records | Index name/barcode as appropriate; test 5,000 records and time ten searches. |
| NFR-02 | Sale and stock consistency | Test success, cancellation, insufficient stock, database error and repeated confirmation. |

## 2. End-to-end sale flow
1. Staff searches by medicine name or barcode.
2. UI sends query to application services.
3. Data access queries SQLite and returns matching medicine records.
4. Staff selects medicine and quantity; domain logic validates quantity and calculates total.
5. On confirmation, one transaction inserts sale header and sale lines and decrements stock.
6. Success commits and shows confirmation. Failure rolls back and shows an error; stock must remain unchanged.

## 3. Preliminary data dictionary
- **Medicine:** `MedicineId` (PK), `Name` (required), `Barcode` (optional/unique when present), `Strength`, `Form`, `UnitPrice`, `QuantityInStock` (integer >= 0), `ExpiryDate`, `IsActive`.
- **Sale:** `SaleId` (PK), `CreatedAt`, `UserId` (FK), `TotalAmount`, `Status`.
- **SaleItem:** `SaleItemId` (PK), `SaleId` (FK), `MedicineId` (FK), `Quantity` (>0), `UnitPriceAtSale`, `LineTotal`.
- **User:** `UserId` (PK), `Username` (unique), `PasswordHash`, `Role`, `IsActive`.

Currency, tax and discount rules were not specified in Week 2 and should be confirmed before implementation. This is a preliminary entity sketch, not a finalized schema.

## 4. Design risks and mitigation
| Risk | Impact | Mitigation / verification |
|---|---|---|
| Partial sale persistence or duplicate stock deduction | Incorrect sales and inventory | One transaction; test forced write failure, cancellation, insufficient stock and repeated confirmation. |
| Slow or ambiguous search | Checkout delay or wrong medicine | Index search fields; validate barcode uniqueness where present; test 5,000 records and time ten searches. |

## 5. Week 4 prototype acceptance goal
Demonstrate a Windows desktop screen where a user searches a seeded medicine by name or barcode, selects it, enters quantity, confirms a test sale, and sees a saved sale plus reduced stock. Evidence: screenshot of the UI and database output before/after. If full integration takes longer, prove the transaction through a minimal UI or test harness.

## 6. References
1. Microsoft Learn, WPF overview: https://learn.microsoft.com/en-us/dotnet/desktop-wpf/overview/index
2. SQLite, SQLite Is Transactional: https://www.sqlite.org/transactional.html
3. Microsoft .NET, releases and support: https://learn.microsoft.com/en-us/dotnet/core/releases-and-support

## 7. Actual evidence to add after pushing
- Commit URLs/hashes: pending.
- Screenshot of architecture diagram and repository changes: add if requested.
- Update the cumulative graduation project report, not only this weekly progress file.
