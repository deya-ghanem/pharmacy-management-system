
# Development Stage 2 – Medicine Management

**Date:** 17 November 2026

## Objective
Implement the core medicine management functionality and allow users to manage medicine records stored in the SQLite database.

## Requirement ID(s)
To be linked to the approved project requirements.

## Implemented Functionality
- Add new medicine records.
- Update existing medicine information.
- Delete medicine records.
- Search medicines by name or barcode.
- Display medicine information in the DataGrid.

## Main Files / Modules
- Models/Medicine.cs
- Data/Database.cs
- MainWindow.xaml
- MainWindow.xaml.cs

## Engineering Decision
Separated the Medicine model from the main window code and implemented database operations in the Database class to improve modularity and separation of concerns.

## Technical Problem
The original prototype supported medicine searching but did not provide complete medicine management operations.

## Solution
Implemented AddMedicine, UpdateMedicine, and DeleteMedicine methods using parameterized SQLite commands and connected them to the WPF interface.


## Screenshot / Evidence
- [Medicine List](Evidence/01_Medicine_List.png)
- [Add Medicine](Evidence/02_Add_Medicine.png)
- [Update Medicine](Evidence/03_Update_Medicine.png)
- [Search Medicine](Evidence/04_Search_Medicine.png)

## GitHub Commit Hash / URL
To be added after committing the implementation.

## Next Step
Integrate medicine management with the sales workflow and automatic stock updates.
