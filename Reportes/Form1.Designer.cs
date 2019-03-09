//------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------
namespace Reportes
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SMECDataSet = new Reportes.SMECDataSet();
            this.prpt_capacitacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.prpt_capacitacionTableAdapter = new Reportes.SMECDataSetTableAdapters.prpt_capacitacionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.SMECDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prpt_capacitacionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.prpt_capacitacionBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reportes.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // SMECDataSet
            // 
            this.SMECDataSet.DataSetName = "SMECDataSet";
            this.SMECDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // prpt_capacitacionBindingSource
            // 
            this.prpt_capacitacionBindingSource.DataMember = "prpt_capacitacion";
            this.prpt_capacitacionBindingSource.DataSource = this.SMECDataSet;
            // 
            // prpt_capacitacionTableAdapter
            // 
            this.prpt_capacitacionTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SMECDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prpt_capacitacionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource prpt_capacitacionBindingSource;
        private SMECDataSet SMECDataSet;
        private SMECDataSetTableAdapters.prpt_capacitacionTableAdapter prpt_capacitacionTableAdapter;
    }
}

