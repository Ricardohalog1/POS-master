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
    public partial class frmCategoryList : Form //Database Connection in category
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection dbcon = new DBconnection();
        SqlDataReader dr;

        public frmCategoryList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
        }//END

        private void pictureBox1_Click(object sender, EventArgs e)//Close Button
        {
            this.Dispose();
        }//END

        public void LoadCategory()//Select into database in category
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblCategory order by category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            cn.Close();
        }//END

        private void pictureBox2_Click(object sender, EventArgs e)//Add New Category List
        {
            frmCategory frm = new frmCategory(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.ShowDialog();
        }//END

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//Edit Category
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmCategory frm = new frmCategory(this);
                frm.txtCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.lblID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();
            }//END
            else if (colName == "Delete")//DELETE
            {
                if (MessageBox.Show("Are you sure you want to delete this category?", "Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblcategory where id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                    LoadCategory();

                }//END
            }
        }
    }
}