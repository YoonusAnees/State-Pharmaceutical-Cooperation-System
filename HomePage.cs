using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using State_Pharmaceutical_Cooperation__System;

namespace StatePharmaceuticalCooperationPharmacy
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
    
        

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
       Environment.Exit(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SPC sPC = new SPC();
            sPC.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
       
        }
    }
}
