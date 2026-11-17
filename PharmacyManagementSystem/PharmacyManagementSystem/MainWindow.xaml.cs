
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem
{
    public partial class MainWindow : Window
    {
        private List<Medicine> medicines = new();

        public MainWindow()
        {
            InitializeComponent();

            Database.Initialize();
            Database.SeedMedicines();

            LoadMedicines();

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearFields();
            btnSearch.Click += BtnSearch_Click;
            btnCreateSale.Click += BtnCreateSale_Click;

            dgMedicines.SelectionChanged += DgMedicines_SelectionChanged;
        }

        private void LoadMedicines()
        {
            medicines = Database.GetMedicines();
            dgMedicines.ItemsSource = null;
            dgMedicines.ItemsSource = medicines;

            cmbMedicine.ItemsSource = null;
            cmbMedicine.ItemsSource = medicines;
        }

        private bool ValidateMedicine(out Medicine medicine)
        {
            medicine = new Medicine();

            string name = txtName.Text.Trim();
            string barcode = txtBarcode.Text.Trim();
            string expiry = txtExpiryDate.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(barcode))
            {
                MessageBox.Show("Name and barcode are required.");
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Number,
                    CultureInfo.CurrentCulture, out decimal price) || price < 0)
            {
                MessageBox.Show("Enter a valid non-negative price.");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Enter a valid non-negative quantity.");
                return false;
            }

            if (!DateTime.TryParseExact(expiry, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Expiry date must use YYYY-MM-DD.");
                return false;
            }

            medicine = new Medicine
            {
                Name = name,
                Barcode = barcode,
                Price = price,
                Quantity = quantity,
                ExpiryDate = expiry
            };

            return true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateMedicine(out Medicine medicine))
                return;

            try
            {
                Database.AddMedicine(medicine);
                LoadMedicines();
                ClearFields();
                MessageBox.Show("Medicine added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not add medicine: " + ex.Message);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicines.SelectedItem is not Medicine selected)
            {
                MessageBox.Show("Select a medicine first.");
                return;
            }

            if (!ValidateMedicine(out Medicine medicine))
                return;

            medicine.Id = selected.Id;

            try
            {
                Database.UpdateMedicine(medicine);
                LoadMedicines();
                ClearFields();
                MessageBox.Show("Medicine updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update medicine: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicines.SelectedItem is not Medicine selected)
            {
                MessageBox.Show("Select a medicine first.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this medicine?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                Database.DeleteMedicine(selected.Id);
                LoadMedicines();
                ClearFields();
                MessageBox.Show("Medicine deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not delete medicine: " + ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text.Trim();

            var results = medicines.Where(m =>
                m.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                m.Barcode.Contains(search, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            dgMedicines.ItemsSource = results;
        }

        private void DgMedicines_SelectionChanged(
            object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgMedicines.SelectedItem is Medicine selected)
            {
                txtName.Text = selected.Name;
                txtBarcode.Text = selected.Barcode;
                txtPrice.Text = selected.Price.ToString(CultureInfo.CurrentCulture);
                txtQuantity.Text = selected.Quantity.ToString();
                txtExpiryDate.Text = selected.ExpiryDate;
            }
        }

        private void BtnCreateSale_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMedicine.SelectedItem is not Medicine selected)
            {
                MessageBox.Show("Select a medicine.");
                return;
            }

            if (!int.TryParse(txtSaleQuantity.Text, out int quantity) ||
                quantity <= 0)
            {
                MessageBox.Show("Enter a quantity greater than zero.");
                return;
            }

            try
            {
                Database.CreateSale(selected.Id, quantity);

                decimal total = selected.Price * quantity;

                LoadMedicines();

                txtSaleQuantity.Clear();

                txtSaleStatus.Text =
                    $"Sale completed successfully!\nTotal: {total:F2}";

                MessageBox.Show("Sale saved and stock updated.");
            }
            catch (Exception ex)
            {
                txtSaleStatus.Text = "Sale failed: " + ex.Message;
                MessageBox.Show("Sale failed: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtBarcode.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtExpiryDate.Clear();
            dgMedicines.SelectedItem = null;
        }
    }
}
