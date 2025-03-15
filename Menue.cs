using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatePharmaceuticalCooperation.Model;

using System.Windows.Forms;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class Menue : Form
    {
        public Menue()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DrugInformationManagement drugInformationManagement = new DrugInformationManagement();
            drugInformationManagement.Show();
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            SupplierInformationManagement supplierInformationManagement = new SupplierInformationManagement();
            supplierInformationManagement.Show();
         
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close( );
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
              TenderManagement tenderManagement = new TenderManagement();
              tenderManagement.Show();
        
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            OrderManagemnet orderManagemnet = new OrderManagemnet();
            orderManagemnet.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

  
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            pharmacyManagement pharmacyManagement = new pharmacyManagement();
            pharmacyManagement.Show();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            PurchasingManagement purchasingManagement = new PurchasingManagement();
            purchasingManagement.Show();
        }
    }
}
