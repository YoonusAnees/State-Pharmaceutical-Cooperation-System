using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using State_Pharmaceutical_Cooperation__System.Model;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class pharmacyManagement : Form
    {
        public pharmacyManagement()
        {
            InitializeComponent();
            loadData();
            lblID.Visible = false;
        }

        private void txtPharmacyName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContactNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int r = e.RowIndex;
            //int c = e.ColumnIndex;
            //if (c == 0 && r >= 0)
            //{
            //    //lblID.Text = dgvp.Rows[r].Cells[1].Value.ToString();
            //    txtPharmacyName.Text = dgvp.Rows[r].Cells[2].Value.ToString();
            //    txtAddress.Text = dgvp.Rows[r].Cells[3].Value.ToString();
            //    txtContactNumber.Text = dgvp.Rows[r].Cells[4].Value.ToString();
            //    txtusername.Text = dgvp.Rows[r].Cells[5].Value.ToString();
            //    txtusername.Text = dgvp.Rows[r].Cells[6].Value.ToString();
            //}
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://localhost:44335/api/Pharmacies";
                HttpClient client = new HttpClient();
                PharmacyInforation pharmacy = new PharmacyInforation();
                pharmacy.PharmacyName = txtPharmacyName.Text;
                pharmacy.Address = txtAddress.Text;
                pharmacy.ContactNumber = txtContactNumber.Text;
                pharmacy.Username = txtUsername.Text;
                pharmacy.Password = txtPassword.Text;


                string info = (new JavaScriptSerializer()).Serialize(pharmacy);
                var content = new StringContent(info, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).Result;



                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registered Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                    
                    loadData();
                }
                else
                {

                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Failed to Registered. Server response: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void loadData()
        {
            string url = "https://localhost:44335/api/Pharmacies";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var Pharmacy = read.Result;
                dgvPharmacy.DataSource = null;
                dgvPharmacy.DataSource = (new JavaScriptSerializer()).Deserialize<List<PharmacyInforation>>(Pharmacy);
            }
        }

        private void dgvPharmacy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                //lblID.Text = dgvp.Rows[r].Cells[1].Value.ToString();
                txtPharmacyName.Text = dgvPharmacy.Rows[r].Cells[2].Value.ToString();
                txtAddress.Text = dgvPharmacy.Rows[r].Cells[3].Value.ToString();
                txtContactNumber.Text = dgvPharmacy.Rows[r].Cells[4].Value.ToString();
                txtUsername.Text = dgvPharmacy.Rows[r].Cells[5].Value.ToString();
                txtPassword.Text = dgvPharmacy.Rows[r].Cells[6].Value.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the user has entered valid data in the text fields
                if (string.IsNullOrEmpty(txtPharmacyName.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtContactNumber.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Please make sure all fields are filled out.");
                    return;
                }

                // Retrieve the Pharmacy ID from the label (if updating)
                int pharmacyId = string.IsNullOrEmpty(lblID.Text) ? 0 : Convert.ToInt32(lblID.Text);

                // Create the pharmacy object from the text fields
                PharmacyInforation updatedPharmacy = new PharmacyInforation()
                {
                    PharmacyId = pharmacyId,
                    PharmacyName = txtPharmacyName.Text,
                    Address = txtAddress.Text,
                    ContactNumber = txtContactNumber.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text
                };

                // Determine whether to perform a POST (add new) or PUT (update) request
                HttpClient client = new HttpClient();
                string url;

                if (pharmacyId == 0) // If there's no PharmacyId, it's a new pharmacy (POST)
                {
                    url = "https://localhost:44335/api/Pharmacies";
                    string pharmacyJson = (new JavaScriptSerializer()).Serialize(updatedPharmacy);
                    var content = new StringContent(pharmacyJson, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Pharmacy Registered Successfully!");
                        loadData(); // Reload the pharmacy list
                    }
                    else
                    {
                        MessageBox.Show("Failed to Register Pharmacy. Server response: " + response.Content.ReadAsStringAsync().Result);
                    }
                }
                else // If there's a PharmacyId, it's an update (PUT)
                {
                    url = $"https://localhost:44335/api/Pharmacies/{pharmacyId}";
                    string pharmacyJson = (new JavaScriptSerializer()).Serialize(updatedPharmacy);
                    var content = new StringContent(pharmacyJson, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Updated Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);
                        loadData(); // Reload the pharmacy list to show the updated pharmacy
                    }
                    else
                    {
                        MessageBox.Show("Failed to Update Pharmacy. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions that occur
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string input = Interaction.InputBox("Enter Pharmacy ID to update:", "Update Pharmacy", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int pharmacyId = Convert.ToInt32(input); // Convert the input to PharmacyId
                    string url = $"https://localhost:44335/api/Pharmacies/{pharmacyId}"; // URL to fetch the specific pharmacy
                    HttpClient client = new HttpClient();

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var pharmacyJson = response.Content.ReadAsStringAsync().Result;
                        var selectedPharmacy = (new JavaScriptSerializer()).Deserialize<PharmacyInforation>(pharmacyJson);

                        txtPharmacyName.Text = selectedPharmacy.PharmacyName;
                        txtAddress.Text = selectedPharmacy.Address;
                        txtContactNumber.Text = selectedPharmacy.ContactNumber;
                        txtUsername.Text = selectedPharmacy.Username;
                        txtPassword.Text = selectedPharmacy.Password;

                        txtPharmacyName.ReadOnly = false;
                        txtAddress.ReadOnly = false;
                        txtContactNumber.ReadOnly = false;
                        txtUsername.ReadOnly = false;
                        txtPassword.ReadOnly = false;

                        lblID.Text = pharmacyId.ToString(); // Show the PharmacyId in the label for identification


                    }
                    else
                    {
                        MessageBox.Show("Pharmacy not found. Please check the Pharmacy ID.");
                    }
                }
                else
                {
                    MessageBox.Show("No Pharmacy ID entered.");
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
                // Show input dialog to get Pharmacy ID to delete
                string input = Interaction.InputBox("Enter Pharmacy ID to delete:", "Delete Pharmacy", "", -1, -1);

                // If input is not empty, proceed to delete the pharmacy
                if (!string.IsNullOrEmpty(input))
                {
                    int pharmacyId = Convert.ToInt32(input); // Convert the input to PharmacyId
                    string url = $"https://localhost:44335/api/Pharmacies/{pharmacyId}"; // URL to delete the specific pharmacy
                    HttpClient client = new HttpClient();

                    // Send DELETE request to remove the pharmacy
                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Pharmacy Deleted Successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);

                        loadData(); // Reload the pharmacy list after deletion
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete pharmacy.");
                    }
                }
                else
                {
                    MessageBox.Show("No Pharmacy ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions that occur
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPharmacyName.Clear();
            txtAddress.Clear();
            txtContactNumber.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtPharmacyName.Focus();
        }
    }

    }

