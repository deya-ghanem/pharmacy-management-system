
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem
{
    public partial class MainWindow : Window
    {
        private List<Medicine> medicines;

        public MainWindow()
        {
            InitializeComponent();

            Database.Initialize();
            Database.SeedMedicines();

            medicines = Database.GetMedicines();

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
