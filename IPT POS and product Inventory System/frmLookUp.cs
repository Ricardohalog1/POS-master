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
    public partial class frmLookUp : Form
    {
        frmPOS f;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;
        String stitle = "Simple POS System";

        public frmLookUp(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
            this.KeyPreview = true;
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty, p.exp_date FROM tblProduct AS p INNER JOIN tblbrand AS b ON b.id = p.bid INNER JOIN tblcategory AS c ON c.id = p.cid WHERE p.pdesc LIKE '" + txtSearch.Text + "%'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                frmQty frm = new frmQty(f);
                frm.ProductDatails(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()), f.lblTranNo.Text);
                frm.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
