//------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'SMECDataSet.prpt_capacitacion' table. You can move, or remove it, as needed.
            this.prpt_capacitacionTableAdapter.Fill(this.SMECDataSet.prpt_capacitacion);
            this.reportViewer1.RefreshReport();
        }
    }
}