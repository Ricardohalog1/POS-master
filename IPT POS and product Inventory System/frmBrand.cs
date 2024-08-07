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
    public partial class frmBrand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        frmBrandList frmlist;

        public frmBrand(frmBrandList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            frmlist = flist;
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtBrand.Clear();
            txtBrand.Focus();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure you want to save this brand?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTo tblBrand(Brand)VALUEs(@brand)", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved,");
                    Clear();
                    frmlist.LoadRecords();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand","update Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblbrand set brand = @brand where id like '" + lblID.Text + "'",cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                        cn.Close();
                    MessageBox.Show("Brand has been successfully updated.");
                    Clear();
                    frmlist.LoadRecords();
                    this.Dispose();
                    
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
