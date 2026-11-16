
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PharmacyManagementSystem
{
    public partial class MainWindow : Window
    {
        private List<Medicine> medicines;

        public MainWindow()
        {
            InitializeComponent();

            medicines = new List<Medicine>
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

            dgMedicines.ItemsSource = medicines;

            btnSearch.Click += BtnSearch_Click;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var results = medicines.Where(m =>
                m.Name.ToLower().Contains(searchText) ||
                m.Barcode.Contains(searchText)
            ).ToList();

            dgMedicines.ItemsSource = results;
        }
    }

    public class Medicine
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ExpiryDate { get; set; }
    }
}
