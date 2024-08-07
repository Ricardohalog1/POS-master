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
    public partial class frmQty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        private String pcode;
        private double price;
        private String transno;
        string stitle = "Simple POS System";
        frmPOS fpos;

        public frmQty(frmPOS frmpos)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            fpos = frmpos;
        }

        public void ProductDatails(String pcode, double price, String transno)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQty.Text != String.Empty))
            {
                String id="";
                bool found = false;
                cn.Open();
                cm = new SqlCommand("Select * from tblcart where transno = @transno and pcode = @pcode", cn);
                cm.Parameters.AddWithValue("@transno", fpos.lblTranNo.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET qty = (qty + " + int.Parse(txtQty.Text) + ") where id = '" + id + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }
                else
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier) VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                    cm.Parameters.AddWithValue("@transno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@price", price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", fpos.lblUser.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                    this.Dispose();
                }
            }
        }

        private void txtQty_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
