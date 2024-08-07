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
    public partial class Form1 : Form //Database in form1 to function in list
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();

        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            //cn.Open();

        }//END

        private void pictureBox2_Click(object sender, EventArgs e)//Close button
        {
            this.Dispose();
        }//END

        private void btnBrand_Click(object sender, EventArgs e)//Brand hyperlink
        {
            frmBrandList frm = new frmBrandList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }//END

        private void btnCategory_Click(object sender, EventArgs e)//Category hyperlink
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategory();
            frm.Show();
        }//END

        private void btnProduct_Click(object sender, EventArgs e)//Product hyperlink
        {
            frmProductList frm = new frmProductList();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }//END

        private void btnStockIn_Click(object sender, EventArgs e)//Stockin hyperlink
        {
            frmStockIn frm = new frmStockIn();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
        }

        private void btnSalesHostory_Click(object sender, EventArgs e)
        {
            frmSoldItem frm = new frmSoldItem();
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
    }
}
