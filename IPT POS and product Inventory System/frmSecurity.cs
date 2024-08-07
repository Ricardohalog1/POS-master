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
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();

        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtUser.Clear();
            this.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _username="", _role="", _name = "";
            try 
            {
                bool found = false;
                cn.Open();
                cm = new SqlCommand("select * from tblUser where username = @username and password = @password", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        frmPOS frm = new frmPOS(this);
                        frm.lblUser.Text = _username;
                        frm.lblName.Text = _name + " | " + _role;
                        frm.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPass.Clear();
                        txtUser.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblname.Text = _name;
                        frm.lblRole.Text = _role;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }catch (Exception ex) 
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUser_Click(object sender, EventArgs e)
        {

        }
    }
}
