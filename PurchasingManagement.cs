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
    public partial class PurchasingManagement : Form
    {
        public PurchasingManagement()
        {
            InitializeComponent();
            loadData();
            lblID.Visible = false;

        }

        private void dgvDrugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                dgvDrugs.Rows[r].Cells[1].Value.ToString();
                dgvDrugs.Rows[r].Cells[2].Value.ToString();
                dgvDrugs.Rows[r].Cells[3].Value.ToString();
                dgvDrugs.Rows[r].Cells[4].Value.ToString();
                dgvDrugs.Rows[r].Cells[5].Value.ToString();
            }
        }


        private void loadData()
        {
            string url = "https://localhost:44335/api/Order/purchasesGet";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var purchase = read.Result;
                dgvDrugs.DataSource = null;
                dgvDrugs.DataSource = (new JavaScriptSerializer()).Deserialize<List<Purchase>>(purchase);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {


            try
            {
                // Prompt the user for the Order ID to delete
                string input = Interaction.InputBox("Enter Order ID to delete:", "Delete Purchase", "", -1, -1);

                // If input is not empty, proceed to delete the order
                if (!string.IsNullOrEmpty(input))
                {
                    int pID = Convert.ToInt32(input); // Convert the input to OrderId
                    string url = $"https://localhost:44335/api/Order/purchase/{pID}"; // URL to delete the specific order
                    HttpClient client = new HttpClient();

                    // Send DELETE request to remove the order
                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Purchase deleted successfully!");
                        loadData(); // Reload the data to reflect the deleted order
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the Purchase. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("No Order ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions that occur
            }
        }
    }
}
