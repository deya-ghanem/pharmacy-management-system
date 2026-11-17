
# Development Stage 3 – Sales and Inventory Integration

**Date:** 17 November 2026

## Objective
Implement the main sales workflow and integrate it with medicine inventory management.

## Requirement ID(s)
To be linked to the approved project requirements.

## Implemented Functionality
- Select a medicine from the available inventory.
- Enter the quantity to be sold.
- Calculate the sale total using the medicine price.
- Save sales information in the SQLite database.
- Store individual sale items in the SaleItems table.
- Automatically decrease medicine stock after a successful sale.
- Reject invalid quantities and sales exceeding available stock.

## Main Files / Modules
- Data/Database.cs
- Models/Medicine.cs
- MainWindow.xaml
- MainWindow.xaml.cs

## Engineering Decision
Used a database transaction to ensure that the sale record, sale item, and inventory update are handled as one operation. This helps prevent incomplete sales and inconsistent stock information.

## Technical Problem
The medicine management functionality was not connected to a complete sales workflow. Stock quantities also needed to remain consistent after sales.

## Solution
Implemented the CreateSale method using SQLite transactions. The method checks medicine availability, records the sale, saves its details, and updates the medicine quantity.

## Screenshot / Evidence
- [Successful Sale](Evidence/05_Successful_Sale.png)
- [Stock Update](Evidence/06_Stock_Update.png)
- [Invalid Sale](Evidence/07_Invalid_Sale.png)

## GitHub Commit Hash / URL
To be added after committing the implementation.

## Next Step
Perform systematic testing, validate boundary conditions, and improve the reliability of the application.
