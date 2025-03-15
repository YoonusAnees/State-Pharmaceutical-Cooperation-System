using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using StatePharmaceuticalCooperation.Model;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net.Http;
using Microsoft.VisualBasic;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class TenderManagement : Form
    {
        public TenderManagement()
        {
            InitializeComponent();
            loadData();
            loadSuppliers();
            loadStatus();

        }

        private void dgvTender_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                dgvTender.Rows[r].Cells[0].Value.ToString();
                dgvTender.Rows[r].Cells[1].Value.ToString();
                dgvTender.Rows[r].Cells[2].Value.ToString();
                dgvTender.Rows[r].Cells[3].Value.ToString();
                dgvTender.Rows[r].Cells[4].Value.ToString();
                dgvTender.Rows[r].Cells[5].Value.ToString();
                dgvTender.Rows[r].Cells[6].Value.ToString();

            }
        }

        private void loadData()
        {
            string url = "https://localhost:44335/api/Tenders";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var tender = read.Result;
                dgvTender.DataSource = null;
                dgvTender.DataSource = (new JavaScriptSerializer()).Deserialize<List<Tender>>(tender);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string input = Interaction.InputBox("Enter Tender ID to update:", "Tender Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input);
                    string url = $"https://localhost:44335/api/Tenders/{id}";
                    HttpClient client = new HttpClient();

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var supplierJson = response.Content.ReadAsStringAsync().Result;
                        var selectedItem = (new JavaScriptSerializer()).Deserialize<Tender>(supplierJson);

                        txtTitle.Text = selectedItem.Title;
                        txtDescription.Text = selectedItem.Description;
                        txtBudget.Text = selectedItem.Budget.ToString();

                        // Set the status in ComboBox to the current Tender's status
                        cmdStatus.SelectedItem = selectedItem.Status;

                        txtTitle.ReadOnly = false;
                        txtDescription.ReadOnly = false;
                        txtBudget.ReadOnly = false;
                        lblID.Text = id.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Tender not found. Please check the ID.");
                    }
                }
                else
                {
                    MessageBox.Show("No Tender ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtBudget.Text))
                {
                    MessageBox.Show("Please make sure all fields are filled out.");
                    return;
                }

                if (!decimal.TryParse(txtBudget.Text, out decimal budget))
                {
                    MessageBox.Show("Please enter a valid numeric value for the Budget.");
                    return;
                }

                int id = Convert.ToInt32(lblID.Text);

                // Get the selected status from the ComboBox
                TenderStatus tenderStatus = (TenderStatus)cmdStatus.SelectedItem;

                var updatedTender = new
                {
                    id = id,
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    Budget = budget,
                    SupplierName = CmbSupplierName.Text,
                    Status = tenderStatus,
                    Deadline = dtpDeadline.Checked ? dtpDeadline.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null
                };

                string url = $"https://localhost:44335/api/Tenders/{updatedTender.id}";
                using (HttpClient client = new HttpClient())
                {
                    string updatedTenderJson = new JavaScriptSerializer().Serialize(updatedTender);
                    var content = new StringContent(updatedTenderJson, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Tender Updated Successfully!");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Update Tender. Please try again.");
                    }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string input = Interaction.InputBox("Enter Tender ID to delete:", "Delete Drug", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int id = Convert.ToInt32(input);
                    string url = $"https://localhost:44335/api/Tenders/{id}";
                    HttpClient client = new HttpClient();

                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Tender Deleted");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Delete Tender");
                    }
                }
                else
                {
                    MessageBox.Show("No Tender ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtDescription.Clear();
            txtBudget.Clear();
            cmdStatus.SelectedIndex = -1; // Clear the status ComboBox
            txtTitle.Focus();
        }

        private void dtpDeadline_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CmbSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrEmpty(txtBudget.Text) || CmbSupplierName.SelectedItem == null)
                {
                    MessageBox.Show("Please make sure all fields are filled out.");
                    return;
                }

                if (!decimal.TryParse(txtBudget.Text, out decimal budget))
                {
                    MessageBox.Show("Please enter a valid numeric value for the Budget.");
                    return;
                }

                var selectedSupplier = (Supplier)CmbSupplierName.SelectedItem;

                // Create the tender object with the status set to TenderStatus.Pending
                var newTender = new
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    Budget = budget,
                    SupplierId = selectedSupplier.SupplierId,
                    SupplierName = selectedSupplier.Name,
                    Status = TenderStatus.Pending,
                    Deadline = dtpDeadline.Checked ? dtpDeadline.Value.ToString("yyyy-MM-ddTHH:mm:ss") : (string)null
                };

                string url = "https://localhost:44335/api/Tenders";
                using (HttpClient client = new HttpClient())
                {
                    string tenderJson = new JavaScriptSerializer().Serialize(newTender);
                    var content = new StringContent(tenderJson, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Tender requested successfully!");
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to request tender. Status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void loadSuppliers()
        {
            string url = "https://localhost:44335/api/Supplier";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync().Result;
                var suppliers = new JavaScriptSerializer().Deserialize<List<Supplier>>(read);
                CmbSupplierName.DataSource = suppliers;
                CmbSupplierName.DisplayMember = "Name";
                CmbSupplierName.ValueMember = "SupplierId";
            }
            else
            {
                MessageBox.Show("Failed to load suppliers.");
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadStatus()
        {
            cmdStatus.DataSource = Enum.GetValues(typeof(TenderStatus));
        }
    }
}


