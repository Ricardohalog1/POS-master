using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace IPT_POS_and_product_Inventory_System
{
    public partial class frmReportSold : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection dbcon = new DBconnection();
        frmSoldItem f;

        public frmReportSold(frmSoldItem frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Myconnection());
            f = frm;
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        public void LoadReport() 
        {
            try 
            {
                ReportDataSource rptDS;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\\Report3.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                if (f.cboCashier.Text == "All Cashier") { da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total " + "from tblCart as c " + "inner join tblProduct AS p ON c.pcode = p.pcode " + "where status like 'Sold' and sdate between '" + f.dt1.Value + "' and '" + f.dt2.Value + "'", cn); }
                else { da.SelectCommand = new SqlCommand("select c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total " + "from tblCart as c " + "inner join tblProduct AS p ON c.pcode = p.pcode " + "where status like 'Sold' and sdate between '" + f.dt1.Value + "' and '" + f.dt2.Value + "' and cashier like'" + f.cboCashier.Text + "'", cn); }
                da.Fill(ds.Tables["dtSoldReport"]);
                cn.Close();
                ReportParameter pDate = new ReportParameter("pDate", "Date From: " + f.dt1.Value.ToShortDateString() + " To " + f.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier: " + f.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "Sales Report");

                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);
                rptDS = new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDS);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                    reportViewer1.ZoomPercent = 100;
            }catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
