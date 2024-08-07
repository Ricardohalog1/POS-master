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
    public partial class frmProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        frmProductList flist;

        public frmProduct(frmProductList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            flist = frm;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select category from tblcategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            cn.Open();
            cm = new SqlCommand("select brand from tblbrand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = ""; string cid = "";
                    cn.Open();
                    cm = new SqlCommand("Select id from tblBrand where brand like '" + cboBrand.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { bid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { cid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblProduct (pcode, barcode, pdesc, bid, cid, price, qty, exp_date)VALUES(@pcode, @barcode, @pdesc, @bid, @cid, @price, @qty, @exp_date)", cn);
                    cm.Parameters.AddWithValue("@pcode", txtpcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtpdesc.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@qty", txtqty.Text);
                    cm.Parameters.AddWithValue("@exp_date", dTP.Value);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                    flist.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            dTP.Value = DateTime.Now;
            txtqty.Clear();
            txtPrice.Clear();
            txtpdesc.Clear();
            txtpcode.Clear();
            txtBarcode.Clear();
            cboBrand.Text = "";
            cboCategory.Text = "";
            txtpcode.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = ""; string cid = "";
                    cn.Open();
                    cm = new SqlCommand("Select id from tblBrand where brand like '" + cboBrand.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { bid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("Select id from tblCategory where category like '" + cboCategory.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { cid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblProduct SET barcode = @barcode,  pdesc=@pdesc, bid=@bid, cid=@cid, price=@price, qty=@qty, exp_date=@exp_date where pcode like @pcode", cn);
                    cm.Parameters.AddWithValue("@pcode", txtpcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtpdesc.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@qty", txtqty.Text);
                    cm.Parameters.AddWithValue("@exp_date", dTP.Value);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully updated.");
                    Clear();
                    flist.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {

            }else if (e.KeyChar == 8){

            }else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
