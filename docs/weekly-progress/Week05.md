
# Weekly Progress Log — Week 5

**Project:** Pharmacy Management System  
**Student:** Deya Ghanem  
**Student ID:** 220513098  
**Institution:** AltinbasUniversity  
**Department:** Software Engineering  
**Week:** 5  
**Date:** 17 November 2026

## 1. Weekly Objective

The main objective of Week 5 was to extend the Week 4 medicine-search prototype into a functional pharmacy application by implementing medicine management and integrating a basic sales workflow with inventory updates.

## 2. Completed Work

### Development Stage 2 — Medicine Management

- Created a separate Medicine model in `Models/Medicine.cs`.
- Implemented adding new medicine records.
- Implemented updating existing medicine records.
- Implemented deleting medicine records.
- Maintained medicine search by name and barcode.
- Connected the medicine-management interface to SQLite.
- Added input validation and error handling.

### Development Stage 3 — Sales and Inventory Integration

- Added a sales interface to the WPF application.
- Implemented medicine selection and sale quantity input.
- Implemented sale total calculation.
- Created the Sales and SaleItems database tables.
- Implemented the CreateSale database operation.
- Used SQLite transactions to maintain consistency during sales.
- Automatically decreased medicine stock after successful sales.
- Rejected invalid quantities and sales exceeding available stock.

## 3. Software Quality Improvements

- Separated the Medicine model from the main window code.
- Organized database operations inside the Database class.
- Used parameterized SQL commands.
- Added validation for medicine information and sale quantities.
- Added error handling for database operations.
- Used transaction rollback when a sale operation fails.

## 4. Verification and Evidence

The main application workflows were manually demonstrated.

| Test / Demonstration | Expected Result | Observed Result |
|---|---|---|
| Add medicine | New record is saved | Passed |
| Update medicine | Existing record is updated | Passed |
| Search medicine | Matching record is displayed | Passed |
| Delete medicine | Selected record is removed | Passed |
| Successful sale | Sale is recorded | Passed |
| Stock update | Quantity decreases after sale | Passed |
| Insufficient stock | Sale is rejected | Passed |

A successful sale of two units from an initial stock quantity of 20 resulted in a remaining quantity of 18.

Screenshots are stored in `PharmacyManagementSystem/docs/Week5/evidence/`.

## 5. Technical Challenges and Decisions

**Challenge:** The original prototype supported medicine search only and did not provide complete medicine-management or sales operations.

**Solution:** Expanded the database functionality and connected the new operations to the WPF interface.

**Engineering decision:** Used an SQLite transaction for the sales workflow so that sale records, sale items, and stock updates are handled as one operation.

**Code organization decision:** Separated the Medicine model and grouped database operations in the Database class to improve maintainability.

## 6. GitHub Progress

- Implemented medicine management and sales functionality.
- Added Week 5 development records.
- Added screenshot evidence.
- Updated the repository and pushed the Week 5 work to GitHub.

Repository:
https://github.com/deya-ghanem/pharmacy-management-system

Latest reported push:
`c76928d`

## 7. Remaining Work

- Update and finalize project documentation.
- Review the implemented architecture against the original design.
- Perform systematic testing during Week 6.
- Evaluate search performance with 5,000 records.
- Test database failure and transaction rollback conditions.
- Continue implementing the remaining approved requirements.

## 8. Overall Weekly Outcome

Week 5 successfully extended the project beyond the initial medicine-search prototype. The application now supports medicine management and a basic sales workflow integrated with SQLite inventory updates.

The core functionality has been demonstrated through manual checks. Comprehensive testing and broader system validation remain scheduled for Week 6.
