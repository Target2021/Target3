﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Target2021
{
    public partial class Contatori : Form
    {
        public Contatori()
        {
            InitializeComponent();
        }

        private void configurazioneBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.configurazioneBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.target2021DataSet);

        }

        private void Contatori_Load(object sender, EventArgs e)
        {
            // TODO: questa riga di codice carica i dati nella tabella 'target2021DataSet.Configurazione'. È possibile spostarla o rimuoverla se necessario.
            this.configurazioneTableAdapter.Fill(this.target2021DataSet.Configurazione);

        }
    }
}
