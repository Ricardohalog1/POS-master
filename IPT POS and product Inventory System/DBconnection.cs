using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace IPT_POS_and_product_Inventory_System
{
    public class DBconnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public string Myconnection()
        {
            string con = @"Data Source=DESKTOP-VV76DMH\SQLEXPRESS;Initial Catalog=POS_DEMO_DB;Integrated Security=True";
            return con;
        }

        public double GetVal()
        {
            double vat=0;
            cn.ConnectionString = Myconnection();
            cn.Open();
            cm = new SqlCommand("select * from tblVat", cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                vat = Double.Parse(dr["vat"].ToString());
            }
            dr.Close();
            cn.Close();
            return vat;
        }
    }
}
