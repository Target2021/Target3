﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Target2021
{
    public partial class Dettaglio_Articoli : Form
    {
        public Dettaglio_Articoli()
        {
            InitializeComponent();
        }

        private void dettaglio_ordini_multirigaBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.dettArticoliBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.target2021DataSet);

        }

        private void Dettaglio_Load(object sender, EventArgs e)
        {
            // TODO: questa riga di codice carica i dati nella tabella 'target2021DataSet.DettArticoli'. È possibile spostarla o rimuoverla se necessario.
            this.dettArticoliTableAdapter.Fill(this.target2021DataSet.DettArticoli);
            // TODO: questa riga di codice carica i dati nella tabella 'target2021DataSet.dettaglio_ordini_multiriga'. È possibile spostarla o rimuoverla se necessario.
            this.dettArticoliTableAdapter.Fill(this.target2021DataSet.DettArticoli);

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (Filter.Text == "IDArticolo")
            {
                Search_Filter("SELECT * FROM DettArticoli WHERE IDDettaglioArticolo LIKE '" + textBox1.Text + "'");
            }
            if (Filter.Text == "Progressivo")
            {
                backgroundWorker1.RunWorkerAsync( ControlLetters(textBox1.Text));
                Search_Filter("SELECT * FROM DettArticoli WHERE progressivo LIKE '%" + textBox1.Text + "%'");
            }
            if (Filter.Text == "Codice_articolo")
            {
                Search_Filter("SELECT * FROM DettArticoli WHERE codice_articolo LIKE '%" + textBox1.Text + "%'");
            }
        }
        public int ControlLetters(string frase)
        {
            int parsedValue;
            if (!int.TryParse(frase, out parsedValue)) //se restituisce falso vuol dire che nella stringa ci sono lettere
            {
                MessageBox.Show("non sono ammesse lettere");
                textBox1.Text = "";
                return 0;
            }
            return 0;
        }
        public void Search_Filter(string query) //metodo di connessione al db
        {
            String stringa = "Data Source=target2021.database.windows.net;Initial Catalog=Target2021;User ID=Amministratore;Password=Barilla23";
            SqlConnection con = new SqlConnection(stringa);
            string variabile = textBox1.Text;
            SqlCommand cmd = new SqlCommand(query, con);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                sda.Fill(dataTable);
                BindingSource source = new BindingSource();
                source.DataSource = dataTable;
                dettArticoliDataGridView.DataSource = source;
                sda.Update(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Button2_Click(sender, e);
            }

        }
        private void dettaglio_ordini_multirigaDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow row in dettArticoliDataGridView.Rows)
            {
                if (row.Index== dettArticoliDataGridView.CurrentRow.Index )
                {
                    String id;
                    id = dettArticoliDataGridView.CurrentCell.Value.ToString(); //prendo l'ID contenuto nella prima cella della gridview
                    RigaCompleta rigaCompleta = new RigaCompleta();
                    rigaCompleta.MdiParent = this.MdiParent;
                    rigaCompleta.Show();
                    rigaCompleta.LoadRow(id,this.Text); //passo l'id in modo da caricare nell'altro form solo la riga interessata
                    //la condizione è che  la proprietà Text che gli passiamo deve essere uguale a quello di questo form così da identificare i vari form
                }
            }
        }
    }
}
