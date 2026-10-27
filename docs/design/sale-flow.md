# Complete Sale Flow (Mermaid Source)

```mermaid
sequenceDiagram
    actor Staff
    participant UI as WPF UI
    participant App as Application Service
    participant Domain as Domain Logic
    participant DAL as Data Access
    participant DB as SQLite
    Staff->>UI: Search by name or barcode
    UI->>App: SearchMedicines(query)
    App->>DAL: Query medicines
    DAL->>DB: SELECT matching records
    DB-->>UI: Matching medicines and stock
    Staff->>UI: Select medicine and quantity
    UI->>App: CreateSale(items, userId)
    App->>Domain: Validate quantities and total
    Domain-->>App: Validated sale
    App->>DAL: Begin transaction
    DAL->>DB: INSERT Sale and SaleItems; UPDATE stock
    alt all writes succeed
        DAL->>DB: COMMIT
        DB-->>UI: Confirmation and updated stock
    else any write fails
        DAL->>DB: ROLLBACK
        DB-->>UI: Error; stock unchanged
    end
```
