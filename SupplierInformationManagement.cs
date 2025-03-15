using System;
using System.Collections.Generic;
using System.ComponentModel;
using StatePharmaceuticalCooperation.Model;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class SupplierInformationManagement : Form
    {
        public SupplierInformationManagement()
        {
            InitializeComponent();
            loadData();
            lblID.Visible = false;  
        }

        private void dgvSuuplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                lblID.Text = dgvSuuplier.Rows[r].Cells[1].Value.ToString();
                txtName.Text = dgvSuuplier.Rows[r].Cells[2].Value.ToString();
                txtContactNumber.Text = dgvSuuplier.Rows[r].Cells[3].Value.ToString();
                txtEmail.Text = dgvSuuplier.Rows[r].Cells[4].Value.ToString();
                txtAddress.Text = dgvSuuplier.Rows[r].Cells[5].Value.ToString();
             
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://localhost:44335/api/Supplier";
                HttpClient client = new HttpClient();
                Supplier supplier = new Supplier();
                supplier.Name = txtName.Text;
                supplier.ContactNumber = txtContactNumber.Text;
                supplier.Email = txtEmail.Text;
                supplier.Address = txtAddress.Text;

                string info = (new JavaScriptSerializer()).Serialize(supplier);
                var content = new StringContent(info, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

              

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier Added");
                    loadData();
                }
                else
                {
                   
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Failed to Supplier. Server response: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }


        private void loadData()
        {
            string url = "https://localhost:44335/api/Supplier";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var Suppliers = read.Result;
                dgvSuuplier.DataSource = null;
                dgvSuuplier.DataSource = (new JavaScriptSerializer()).Deserialize<List<Supplier>>(Suppliers);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
           Menue menue = new Menue();
          


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string input = Interaction.InputBox("Enter Supplier ID to update:", "Supplier Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input);
                    string url = $"https://localhost:44335/api/Supplier/{id}";
                    HttpClient client = new HttpClient();


                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var supplierJson = response.Content.ReadAsStringAsync().Result;
                        var selectedItem = (new JavaScriptSerializer()).Deserialize<Supplier>(supplierJson);

                        txtName.Text = selectedItem.Name;
                        txtContactNumber.Text = selectedItem.ContactNumber;
                        txtEmail.Text = selectedItem.Email;
                        txtAddress.Text = selectedItem.Address;
                      


                        txtName.ReadOnly = false;
                        txtContactNumber.ReadOnly = false;
                        txtEmail.ReadOnly = false;
                        txtAddress.ReadOnly = false;
                  


                        lblID.Text = id.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Supplier not found. Please check the ID.");
                    }
                }
                else
                {
                    MessageBox.Show("No Supplier ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); 
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtContactNumber.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Please make sure all fields are filled out.");
                    return;
                }

               
                int SupplierId = Convert.ToInt32(lblID.Text); 

                // Create the updated item from the text fields
                Supplier updatedSupplier = new Supplier()
                {
                    SupplierId = SupplierId,
                    Name = txtName.Text,
                    ContactNumber = txtContactNumber.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,

                    //Price = decimal.Parse(txtPrice.Text),
                    //Description = txtDescription.Text,
                    //Stock = int.Parse(txtStock.Text),
                    //Manufacturer = txtManufecturer.Text
                };

                string url = $"https://localhost:44335/api/Supplier/{updatedSupplier.SupplierId}";
                HttpClient client = new HttpClient();

                // Step 1: Serialize the updated item to JSON
                string updatedSupplierJson = (new JavaScriptSerializer()).Serialize(updatedSupplier);
                var content = new StringContent(updatedSupplierJson, Encoding.UTF8, "application/json");

                // Step 2: Send a PUT request to update the item
                var response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Supplier Updated Successfully!");
                    loadData(); // Reload the data to show the updated item in the list
                }
                else
                {
                    MessageBox.Show("Failed to Update Supplier. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions that occur
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Show input dialog to get Item ID to delete
                string input = Interaction.InputBox("Enter Supplier ID to delete:", "Supplier Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input);
                    string url = $"https://localhost:44335/api/Supplier/{id}";
                    HttpClient client = new HttpClient();

                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Supplier Deleted");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Delete Supplier");
                    }
                }
                else
                {
                    MessageBox.Show("No Supplier ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtContactNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear(); 
            txtName.Focus();
        }
    }
}
