using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using StatePharmaceuticalCooperation.Model;
using System.Web.Script.Serialization;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class DrugInformationManagement : Form
    {
        public DrugInformationManagement()
        {
            InitializeComponent();
            loadData();
            lblID.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void dgvDrugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                lblID.Text = dgvDrugs.Rows[r].Cells[1].Value.ToString();
                txtName.Text = dgvDrugs.Rows[r].Cells[2].Value.ToString();
                txtDescription.Text = dgvDrugs.Rows[r].Cells[3].Value.ToString();
                txtStock.Text = dgvDrugs.Rows[r].Cells[4].Value.ToString();
                txtPrice.Text = dgvDrugs.Rows[r].Cells[5].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://localhost:44335/api/Drug";
                HttpClient client = new HttpClient();
                Drug drug = new Drug();
                drug.Name = txtName.Text;
                drug.Price = decimal.Parse(txtPrice.Text);
                drug.Stock = int.Parse(txtStock.Text);
                drug.Manufacturer = txtManufecturer.Text;
                drug.Description = txtDescription.Text;

                string info = (new JavaScriptSerializer()).Serialize(drug);
                var content = new StringContent(info, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;

                // Debugging: Check the response status code
                //MessageBox.Show("Response Status Code: " + response.StatusCode.ToString());

                if (response.IsSuccessStatusCode)
                {
                  MessageBox.Show("Drug Added","Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);
                    loadData();
                }
                else
                {
                    // Read the response content for any error details
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Failed to Add Item. Server response: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }


        private void loadData()
        {
            string url = "https://localhost:44335/api/Drug";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var Drugs = read.Result;
                dgvDrugs.DataSource = null;
                dgvDrugs.DataSource = (new JavaScriptSerializer()).Deserialize<List<Drug>>(Drugs);
            }


        }



     

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               
                string input = Interaction.InputBox("Enter Drug ID to update:", "Update Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input); 
                    string url = $"https://localhost:44335/api/Drug/{id}";
                    HttpClient client = new HttpClient();

                  
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var itemJson = response.Content.ReadAsStringAsync().Result;
                        var selectedItem = (new JavaScriptSerializer()).Deserialize<Drug>(itemJson);

                        txtName.Text = selectedItem.Name;
                        txtPrice.Text = selectedItem.Price.ToString();
                        txtDescription.Text = selectedItem.Description;
                        txtStock.Text = selectedItem.Stock.ToString();
                        txtManufecturer.Text = selectedItem.Manufacturer.ToString();

                       
                        txtName.ReadOnly = false;
                        txtPrice.ReadOnly = false;
                        txtDescription.ReadOnly = false;
                        txtStock.ReadOnly = false;
                        txtManufecturer.ReadOnly = false;

                        
                        lblID.Text = id.ToString(); 
                    }
                    else
                    {
                        MessageBox.Show("Drug not found. Please check the ID.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                       
                    }
                }
                else
                {
                 
                    MessageBox.Show("No Drug ID entered.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions that occur
            }

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the user has entered valid data in the text fields
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStock.Text))
                {
                    MessageBox.Show("Please make sure all fields are filled out.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                    return;
                }

                // Retrieve the Item ID from the label or hidden field where it's stored
                int DrugId = Convert.ToInt32(lblID.Text); // Retrieve from label or hidden TextBox

                // Create the updated item from the text fields
                Drug updatedDrug = new Drug()
                {
                    Id = DrugId, 
                    Name = txtName.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    Description = txtDescription.Text,
                    Stock = int.Parse(txtStock.Text),
                    Manufacturer = txtManufecturer.Text
                };

                string url = $"https://localhost:44335/api/Drug/{updatedDrug.Id}";
                HttpClient client = new HttpClient();

                // Step 1: Serialize the updated item to JSON
                string updatedDrugJson = (new JavaScriptSerializer()).Serialize(updatedDrug);
                var content = new StringContent(updatedDrugJson, Encoding.UTF8, "application/json");

                // Step 2: Send a PUT request to update the item
                var response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Drug Updated Successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                    loadData(); // Reload the data to show the updated item in the list
                }
                else
                {
                    MessageBox.Show("Failed to Update Drug. Please try again.");

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
                string input = Interaction.InputBox("Enter Item ID to delete:", "Delete Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input);
                    string url = $"https://localhost:44335/api/Drug/{id}";
                    HttpClient client = new HttpClient();

                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                      
                        MessageBox.Show("Drug Deleted", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Delete Drug");
                    }
                }
                else
                {
                    MessageBox.Show("No Drug ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Menue menue = new Menue();
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtManufecturer.Clear();
            txtDescription.Clear();
            txtName.Focus();
        }

        private void txtStock_ValueChanged(object sender, EventArgs e)
        {

        }
    }



    }

