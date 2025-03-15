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
using System.Xml.Linq;
using Microsoft.VisualBasic;
using State_Pharmaceutical_Cooperation__System.Model;
using StatePharmaceuticalCooperation.Model;
using StatePharmaceuticalCooperationPharmacy;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class OrderManagemnet : Form
    {
        private List<OrderSys> allOrders = new List<OrderSys>();


        public OrderManagemnet()
        {
            InitializeComponent();
            //loadDataall();
            loadData();
        }

        private void dgvDrugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (c == 0 && r >= 0)
            {
                lblOrderID.Text = dgvDrugs.Rows[r].Cells["OrderId"].Value.ToString();  // Assuming you have an OrderId column
                txtDrugName.Text = dgvDrugs.Rows[r].Cells["DrugName"].Value.ToString();
                txtStock.Text = dgvDrugs.Rows[r].Cells["Stock"].Value.ToString();
                cmbStatus.SelectedItem = Enum.Parse(typeof(OrderStatus), dgvDrugs.Rows[r].Cells["Status"].Value.ToString());
            }
        }
            
        

        private void loadDatawithouttime()
        {
            string url = "https://localhost:44335/api/Order/GetOrderr";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                var read = result.Content.ReadAsStringAsync();
                response.Wait();
                var order = read.Result;
                dgvDrugs.DataSource = null;
                dgvDrugs.DataSource = (new JavaScriptSerializer()).Deserialize<List<Order>>(order);
                dgvDrugs.DataSource = null;
                dgvDrugs.DataSource = order;
            }
        }

        private void loadData()
        {
            string url = "https://localhost:44335/api/Order/GetOrder";
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var ordersData = response.Content.ReadAsStringAsync().Result;
                var orders = (new JavaScriptSerializer()).Deserialize<List<Order>>(ordersData);

                dgvDrugs.DataSource = null;
                dgvDrugs.DataSource = orders; // Set the updated orders list to the DataGridView
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                // Validate if required fields are not empty
                if (string.IsNullOrEmpty(txtDrugName.Text) || string.IsNullOrEmpty(txtStock.Text))
                {
                    MessageBox.Show("Please make sure all fields are filled out.");
                    return;
                }

                // Try parsing the Stock value and handle invalid inputs
                if (!int.TryParse(txtStock.Text, out int stock))
                {
                    MessageBox.Show("Invalid stock quantity. Please enter a valid number.");
                    return;
                }

                // Validate the selected status
                if (cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a status.");
                    return;
                }

                OrderStatus status;
                if (!Enum.TryParse(cmbStatus.SelectedItem.ToString(), out status))
                {
                    MessageBox.Show("Invalid status selected.");
                    return;
                }

                // Retrieve the Order ID from the label
                int orderId = Convert.ToInt32(lblOrderID.Text);

                // Get the current date for OrderDate
                DateTime orderDate = DateTime.Now;

                // Create the updated order object
                Order updatedOrder = new Order()
                {
                    OrderId = orderId,
                    DrugName = txtDrugName.Text,
                    Stock = stock,
                    Status = status,
                    OrderDate = orderDate  // Pass DateTime object directly
                };

                string url = $"https://localhost:44335/api/Order/{orderId}";
                HttpClient client = new HttpClient();

                // Serialize the updated order data
                string updatedOrderJson = (new JavaScriptSerializer()).Serialize(updatedOrder);
                var content = new StringContent(updatedOrderJson, Encoding.UTF8, "application/json");

                // Send the PUT request to update the order
                var response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Order Updated Successfully!");
                    loadData(); // Reload the data to show the updated order in the list
                }
                else
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Failed to Update Order. Response: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (txtDrugName  == null)
            {
                MessageBox.Show("Please select a drug.");
                return;
            }

            if (string.IsNullOrEmpty(txtStock.Text) || !int.TryParse(txtStock.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.");
                return;
            }

            var selectedStatus = (OrderStatus)cmbStatus.SelectedItem;

            var orderData = new
            {
             
                Stock = quantity,
  
                Status = selectedStatus // Include the status
            };

            string jsonOrder = new JavaScriptSerializer().Serialize(orderData);

            string url = "https://localhost:44335/api/Order/Order"; // API endpoint
            HttpClient client = new HttpClient();
            var content = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Order placed successfully!");

                loadData(); // Refresh drug list and stock display
            }
            else
            {
                MessageBox.Show("Failed to place the order. Please try again.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
         
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                string input = Interaction.InputBox("Enter Order ID to update:", "Update Order", "", -1, -1);

                if (!string.IsNullOrEmpty(input))
                {
                    int orderId = Convert.ToInt32(input); // Convert the input to OrderId
                    string url = $"https://localhost:44335/api/Order/{orderId}"; // URL to fetch the specific order
                    HttpClient client = new HttpClient();

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var orderJson = response.Content.ReadAsStringAsync().Result;
                        var selectedOrder = (new JavaScriptSerializer()).Deserialize<Order>(orderJson);

                        txtDrugName.Text = selectedOrder.DrugName;
                        txtStock.Text = selectedOrder.Stock.ToString();
                        cmbStatus.SelectedItem = Enum.Parse(typeof(OrderStatus), selectedOrder.Status.ToString());

                        txtDrugName.ReadOnly = false;
                        txtStock.ReadOnly = false;
                        cmbStatus.Enabled = true;

                        lblOrderID.Text = orderId.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Order not found. Please check the Order ID.");
                    }
                }
                else
                {
                    MessageBox.Show("No Order ID entered.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Display any exceptions
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Prompt the user for the Order ID to delete
                string input = Interaction.InputBox("Enter Order ID to delete:", "Delete Order", "", -1, -1);

                // If input is not empty, proceed to delete the order
                if (!string.IsNullOrEmpty(input))
                {
                    int orderId = Convert.ToInt32(input); // Convert the input to OrderId
                    string url = $"https://localhost:44335/api/Order/{orderId}"; // URL to delete the specific order
                    HttpClient client = new HttpClient();

                    // Send DELETE request to remove the order
                    var response = client.DeleteAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Order deleted successfully!");
                        loadData(); // Reload the data to reflect the deleted order
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the order. Please try again.");
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtDrugName.Clear();
            txtStock.Clear();
        
            txtDrugName.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ViewPurchaseDetails viewPurchaseDetails = new ViewPurchaseDetails();
            viewPurchaseDetails.Show();
        }
    }
}
