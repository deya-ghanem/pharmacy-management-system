
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Data
{
    internal class Database
    {
        private static readonly string DbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PharmacyManagementSystem",
                "pharmacy.db"
            );

        private static string ConnectionString =>
            $"Data Source={DbPath};Foreign Keys=True";

        public static void Initialize()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath)!);

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Medicines (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Barcode TEXT NOT NULL UNIQUE,
                    Price REAL NOT NULL CHECK(Price >= 0),
                    Quantity INTEGER NOT NULL CHECK(Quantity >= 0),
                    ExpiryDate TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Sales (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SaleDate TEXT NOT NULL,
                    Total REAL NOT NULL
                );

                CREATE TABLE IF NOT EXISTS SaleItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SaleId INTEGER NOT NULL,
                    MedicineId INTEGER NOT NULL,
                    Quantity INTEGER NOT NULL,
                    UnitPrice REAL NOT NULL,
                    FOREIGN KEY(SaleId) REFERENCES Sales(Id),
                    FOREIGN KEY(MedicineId) REFERENCES Medicines(Id)
                );
            ";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static void SeedMedicines()
        {
            if (GetMedicines().Count > 0)
                return;

            AddMedicine(new Medicine
            {
                Name = "Paracetamol",
                Barcode = "100001",
                Price = 25,
                Quantity = 100,
                ExpiryDate = "2027-06-15"
            });

            AddMedicine(new Medicine
            {
                Name = "Amoxicillin",
                Barcode = "100002",
                Price = 45,
                Quantity = 50,
                ExpiryDate = "2027-09-20"
            });

            AddMedicine(new Medicine
            {
                Name = "Ibuprofen",
                Barcode = "100003",
                Price = 30,
                Quantity = 75,
                ExpiryDate = "2028-01-10"
            });
        }

        public static List<Medicine> GetMedicines()
        {
            var medicines = new List<Medicine>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                SELECT Id, Name, Barcode, Price, Quantity, ExpiryDate
                FROM Medicines;
            ";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                medicines.Add(new Medicine
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Barcode = reader.GetString(2),
                    Price = Convert.ToDecimal(reader.GetDouble(3)),
                    Quantity = reader.GetInt32(4),
                    ExpiryDate = reader.GetString(5)
                });
            }

            return medicines;
        }

        public static void AddMedicine(Medicine medicine)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                INSERT INTO Medicines
                (Name, Barcode, Price, Quantity, ExpiryDate)
                VALUES
                (@Name, @Barcode, @Price, @Quantity, @ExpiryDate);
            ";

            using var command = new SqliteCommand(sql, connection);

            command.Parameters.AddWithValue("@Name", medicine.Name);
            command.Parameters.AddWithValue("@Barcode", medicine.Barcode);
            command.Parameters.AddWithValue("@Price", medicine.Price);
            command.Parameters.AddWithValue("@Quantity", medicine.Quantity);
            command.Parameters.AddWithValue("@ExpiryDate", medicine.ExpiryDate);

            command.ExecuteNonQuery();
        }

        public static void UpdateMedicine(Medicine medicine)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                UPDATE Medicines
                SET Name = @Name,
                    Barcode = @Barcode,
                    Price = @Price,
                    Quantity = @Quantity,
                    ExpiryDate = @ExpiryDate
                WHERE Id = @Id;
            ";

            using var command = new SqliteCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", medicine.Id);
            command.Parameters.AddWithValue("@Name", medicine.Name);
            command.Parameters.AddWithValue("@Barcode", medicine.Barcode);
            command.Parameters.AddWithValue("@Price", medicine.Price);
            command.Parameters.AddWithValue("@Quantity", medicine.Quantity);
            command.Parameters.AddWithValue("@ExpiryDate", medicine.ExpiryDate);

            command.ExecuteNonQuery();
        }

        public static void DeleteMedicine(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = "DELETE FROM Medicines WHERE Id = @Id;";

            using var command = new SqliteCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public static void CreateSale(int medicineId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string selectSql = @"
                    SELECT Price, Quantity
                    FROM Medicines
                    WHERE Id = @Id;
                ";

                using var selectCommand =
                    new SqliteCommand(selectSql, connection, transaction);

                selectCommand.Parameters.AddWithValue("@Id", medicineId);

                using var reader = selectCommand.ExecuteReader();

                if (!reader.Read())
                    throw new InvalidOperationException("Medicine not found.");

                decimal price = Convert.ToDecimal(reader.GetDouble(0));
                int available = reader.GetInt32(1);
                reader.Close();

                if (available < quantity)
                    throw new InvalidOperationException("Insufficient stock.");

                decimal total = price * quantity;

                string saleSql = @"
                    INSERT INTO Sales (SaleDate, Total)
                    VALUES (@Date, @Total);
                    SELECT last_insert_rowid();
                ";

                using var saleCommand =
                    new SqliteCommand(saleSql, connection, transaction);

                saleCommand.Parameters.AddWithValue(
                    "@Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                saleCommand.Parameters.AddWithValue("@Total", total);

                long saleId = (long)saleCommand.ExecuteScalar()!;

                string itemSql = @"
                    INSERT INTO SaleItems
                    (SaleId, MedicineId, Quantity, UnitPrice)
                    VALUES (@SaleId, @MedicineId, @Quantity, @Price);
                ";

                using var itemCommand =
                    new SqliteCommand(itemSql, connection, transaction);

                itemCommand.Parameters.AddWithValue("@SaleId", saleId);
                itemCommand.Parameters.AddWithValue("@MedicineId", medicineId);
                itemCommand.Parameters.AddWithValue("@Quantity", quantity);
                itemCommand.Parameters.AddWithValue("@Price", price);
                itemCommand.ExecuteNonQuery();

                string updateSql = @"
                    UPDATE Medicines
                    SET Quantity = Quantity - @Quantity
                    WHERE Id = @Id AND Quantity >= @Quantity;
                ";

                using var updateCommand =
                    new SqliteCommand(updateSql, connection, transaction);

                updateCommand.Parameters.AddWithValue("@Quantity", quantity);
                updateCommand.Parameters.AddWithValue("@Id", medicineId);

                if (updateCommand.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Stock update failed.");

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
