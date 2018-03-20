using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Target2021
{
    public partial class CheckStampaggio : Form
    {
        public CheckStampaggio()
        {
            InitializeComponent();
        }

        private void CheckStampaggio_Load(object sender, EventArgs e)
        {
            String stringa = "Data Source=target2021.database.windows.net;Initial Catalog=Target2021;User ID=Amministratore;Password=Barilla23";
            string query = "SELECT * FROM Commesse WHERE TipoCommessa=2 AND Stato IN (SELECT Stato FROM Commesse WHERE Stato=1 OR Stato=0)";
            SqlConnection con = new SqlConnection(stringa);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            BindingSource source = new BindingSource();
            source.DataSource = dataTable;
            dataGridView1.DataSource = source;
            sda.Update(dataTable);
            con.Close();
        }
    }
}
