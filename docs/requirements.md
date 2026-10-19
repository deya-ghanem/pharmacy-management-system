# System Requirements

## Functional Requirements

| ID | Requirement | Priority |
|---|---|---|
| FR-01 | The system shall allow authorized users to search for medicines by name or barcode. | Must |
| FR-02 | The system shall display available medicine details, including price, quantity, and expiry date when recorded. | Must |
| FR-03 | The system shall allow users to add medicines and quantities to a sale and calculate the total price. | Must |
| FR-04 | The system shall save a confirmed sale and update the corresponding inventory quantities. | Must |
| FR-05 | The system shall allow authorized users to add, edit, and deactivate medicine records. | Should |
| FR-06 | The system shall provide low-stock and expiry alerts. | Should |

## Non-Functional Requirements

| ID | Requirement | Verification |
|---|---|---|
| NFR-01 | Medicine searches shall return results within 2 seconds with 5,000 medicine records. | Test 10 searches using a dataset of 5,000 records. |
| NFR-02 | A confirmed sale shall update the transaction and inventory consistently. Cancelled or failed sales shall not reduce stock. | Test successful, cancelled, and failed sale scenarios. |
| NFR-03 | A trained user shall be able to search for a medicine, add it to a sale, and reach confirmation within 5 main interactions. | Observe 3 trial runs and count interactions. |

## Initial Constraints

- The initial version targets Windows desktop.
- The project has limited development time and resources.
- The programming language, GUI framework, and database have not yet been finalized.

## Assumption

The pharmacy will maintain medicine records and stock quantities accurately enough for the system to provide useful inventory information.

## Current Status

These requirements are preliminary and may be refined after technology selection and prototype testing.
