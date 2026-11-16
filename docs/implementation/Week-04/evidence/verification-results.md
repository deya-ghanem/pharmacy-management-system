# Week 4 – Preliminary Verification Results

## 1. Purpose

This document records the preliminary verification results of the Pharmacy Management System prototype developed during Week 4.

The purpose of these checks is to verify the medicine search functionality using medicine names and barcodes, including valid, invalid, and empty search inputs.

## 2. Test Environment

- Application: Pharmacy Management System
- Framework: WPF
- Programming Language: C#
- Database: SQLite
- Database Library: Microsoft.Data.Sqlite
- Test Type: Manual Functional Verification

## 3. Verification Results

| ID | Input / Action | Expected Result | Actual Result | Status |
|---|---|---|---|---|
| P01 | Search for `Para` | Paracetamol should appear in the results. | Paracetamol was displayed correctly. | PASS |
| P02 | Search for barcode `100002` | Amoxicillin should appear in the results. | Amoxicillin was displayed correctly with its medicine details. | PASS |
| P03 | Search for `Aspirin` | No matching medicines should be displayed. | No medicine records were displayed. The application remained operational. | PASS |
| P04 | Leave the search field empty and click Search. | All available medicines should be displayed. | All three medicines were displayed correctly. | PASS |

## 4. Test Summary

- Total test cases: 4
- Passed: 4
- Failed: 0

All four preliminary verification cases passed successfully.

## 5. Evidence

The following screenshots document the actual test results:

- `P01-search-name.png` – Search by partial medicine name.
- `P02-search-barcode.png` – Search by medicine barcode.
- `P03-no-results.png` – Search for a medicine that does not exist.
- `P04-empty-search.png` – Display all medicines with an empty search field.

## 6. Conclusion

The preliminary verification demonstrated that the medicine search functionality works correctly with the SQLite database.

The prototype successfully supports partial medicine name searches, barcode searches, searches with no matching results, and empty search inputs.

These preliminary checks do not replace the full testing and validation activities planned for Week 6.
