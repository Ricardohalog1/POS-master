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
    public partial class frmRecords : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();
        public frmRecords()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord()
        {
            int i = 0;
            cn.Open();
            cm = new SqlCommand("select pcode, pdesc, sum(qty) as qty from vwSoldItems where sdate between '" + dateTimePicker1.Value.ToString() + "' and '" + dateTimePicker2.Value.ToString() + "' and status like 'Sold' group by pcode, pdesc order by qty desc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString());
            }
            cn.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }
    }
}
