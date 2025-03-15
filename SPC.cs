using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace State_Pharmaceutical_Cooperation__System
{
    public partial class SPC : Form
    {
        public SPC()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          this.Close();
        }

        private void SPC_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            String username = txtUserName.Text;
            String password = txtPassword.Text;

            try
            {
                if (username == "Admin" || password == "123")
                {
                    Menue menue = new Menue();
                    MessageBox.Show("Admin Logged Successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, 0, true);


                    menue.Show();
                
                    

                }

                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
