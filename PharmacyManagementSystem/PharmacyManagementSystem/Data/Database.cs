
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using PharmacyManagementSystem;

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
            $"Data Source={DbPath}";

        public static void Initialize()
        {
            string? folder = Path.GetDirectoryName(DbPath);

            if (folder != null)
            {
                Directory.CreateDirectory(folder);
            }

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Medicines (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Barcode TEXT NOT NULL UNIQUE,
                    Price REAL NOT NULL,
                    Quantity INTEGER NOT NULL,
                    ExpiryDate TEXT NOT NULL
                );
            ";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        public static void SeedMedicines()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string countSql = "SELECT COUNT(*) FROM Medicines;";

            using var countCommand = new SqliteCommand(countSql, connection);

            long count = (long)countCommand.ExecuteScalar()!;

            if (count > 0)
                return;

            string insertSql = @"
                INSERT INTO Medicines
                (Name, Barcode, Price, Quantity, ExpiryDate)
                VALUES
                (@Name, @Barcode, @Price, @Quantity, @ExpiryDate);
            ";

            var medicines = new List<Medicine>
            {
                new Medicine
                {
                    Name = "Paracetamol",
                    Barcode = "100001",
                    Price = 25,
                    Quantity = 100,
                    ExpiryDate = "2027-06-15"
                },
                new Medicine
                {
                    Name = "Amoxicillin",
                    Barcode = "100002",
                    Price = 45,
                    Quantity = 50,
                    ExpiryDate = "2027-09-20"
                },
                new Medicine
                {
                    Name = "Ibuprofen",
                    Barcode = "100003",
                    Price = 30,
                    Quantity = 75,
                    ExpiryDate = "2028-01-10"
                }
            };

            using var transaction = connection.BeginTransaction();

            foreach (var medicine in medicines)
            {
                using var command = new SqliteCommand(
                    insertSql, connection, transaction);

                command.Parameters.AddWithValue("@Name", medicine.Name);
                command.Parameters.AddWithValue("@Barcode", medicine.Barcode);
                command.Parameters.AddWithValue("@Price", medicine.Price);
                command.Parameters.AddWithValue("@Quantity", medicine.Quantity);
                command.Parameters.AddWithValue("@ExpiryDate", medicine.ExpiryDate);

                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        public static List<Medicine> GetMedicines()
        {
            var medicines = new List<Medicine>();

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sql = @"
                SELECT Name, Barcode, Price, Quantity, ExpiryDate
                FROM Medicines;
            ";

            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                medicines.Add(new Medicine
                {
                    Name = reader.GetString(0),
                    Barcode = reader.GetString(1),
                    Price = Convert.ToDecimal(reader.GetDouble(2)),
                    Quantity = reader.GetInt32(3),
                    ExpiryDate = reader.GetString(4)
                });
            }

            return medicines;
        }
    }
}
