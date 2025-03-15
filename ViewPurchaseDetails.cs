using State_Pharmaceutical_Cooperation__System;
using StatePharmaceuticalCooperation.Model;
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

namespace StatePharmaceuticalCooperationPharmacy
{
    public partial class ViewPurchaseDetails : Form
    {
        public ViewPurchaseDetails()
        {
            InitializeComponent();
            loadData();
        
        }

        private void dgvViewPurchase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                dgvViewPurchase.Rows[r].Cells[1].Value.ToString();
                dgvViewPurchase.Rows[r].Cells[2].Value.ToString();
                dgvViewPurchase.Rows[r].Cells[3].Value.ToString();
            }
        }

        //private void loadData()
        //{
        //    string url = "https://localhost:44335/api/Order/GetOrder";
        //    HttpClient client = new HttpClient();
        //    var response = client.GetAsync(url);
        //    response.Wait();
        //    var result = response.Result;
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var read = result.Content.ReadAsStringAsync();
        //        response.Wait();
        //        var purchase = read.Result;
        //        dgvViewPurchase.DataSource = null;
        //        dgvViewPurchase.DataSource = (new JavaScriptSerializer()).Deserialize<List<Order>>(purchase);
        //    }


        //}

        private void loadData()
        {
            string pharmacyUrl = "https://localhost:44335/api/Pharmacies"; // API endpoint to get pharmacies
            HttpClient client = new HttpClient();
            var pharmacyResponse = client.GetAsync(pharmacyUrl).Result;

            if (pharmacyResponse.IsSuccessStatusCode)
            {
                var pharmacyData = pharmacyResponse.Content.ReadAsStringAsync().Result;
                var allPharmacies = new JavaScriptSerializer().Deserialize<List<Pharmacy>>(pharmacyData);
                cmbPharmacyName.DataSource = allPharmacies;
                cmbPharmacyName.DisplayMember = "PharmacyName"; // Show pharmacy name
                cmbPharmacyName.ValueMember = "PharmacyId"; // Store pharmacy ID in ComboBox ValueMember
            }
            else
            {
                MessageBox.Show("Failed to load pharmacy data.");
            }
        }




        private void loadDataByPharmacy(int pharmacyId)
        {
            string url = $"https://localhost:44335/api/Order/GetOrderByPharmacy/{pharmacyId}";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var read = response.Content.ReadAsStringAsync().Result;
                var purchase = new JavaScriptSerializer().Deserialize<List<Order>>(read);
                dgvViewPurchase.DataSource = null;
                dgvViewPurchase.DataSource = purchase;
            }
            else
            {
                MessageBox.Show("Failed to load purchase data for selected pharmacy.");
            }
        }


        private void lblUserNme_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPharmacyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPharmacyName.SelectedItem != null)
            {
                // Get the selected pharmacy
                var selectedPharmacy = (Pharmacy)cmbPharmacyName.SelectedItem;
                loadDataByPharmacy(selectedPharmacy.PharmacyId); // Filter orders by pharmacy ID
            }
        }

    }
}
