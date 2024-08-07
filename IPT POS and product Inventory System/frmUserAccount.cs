using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IPT_POS_and_product_Inventory_System
{
    public partial class frmUserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmUserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void Clear()
        {
            txtName.Clear();
            txtPassword.Clear();
            txtRetype.Clear();
            txtUser.Clear();
            cboRole.Text = "";
            txtUser.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if(txtPassword.Text != txtRetype.Text)
                {
                    MessageBox.Show("Password did not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new SqlCommand("insert into tbluser (username, password, role, name)values(@username, @password, @role, @name)", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                cm.Parameters.AddWithValue("@role", cboRole.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New account has saved!");
                Clear();

            }catch(Exception ex) 
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
