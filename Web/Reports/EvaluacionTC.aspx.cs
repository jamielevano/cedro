using DatabaseContext;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Web.Reports
{
    public partial class EvaluacionTC : System.Web.UI.Page
    {
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

            if (!IsPostBack)
                LoadDatos();

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadDatos()
        {
            DataTable dt = null;

            if (Request.QueryString["Reporte"] != null)
            {
                var reporte = int.Parse(Request.QueryString["reporte"]);
                var busquedaini = Request.QueryString["busquedaini"];
                var busquedafin = Request.QueryString["busquedafin"];
                var fechaini = Request.QueryString["fechaini"];
                var fechafin = Request.QueryString["fechafin"];
                var archivo = Server.MapPath("") + @"\EvaluacionTC\" + (reporte / 100) + @"\" + reporte + ".rdlc";

                switch (reporte / 100)
                {
                    case 2:
                        {
                            var tipo = int.Parse(Request.QueryString["tipo"]);
                            var localid = int.Parse(Request.QueryString["localid"]);
                            var localidad = Server.UrlDecode(Request.QueryString["localidad"]);

                            dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "rp_ev" + reporte,
                                new[] { new SqlParameter("@fechaini", fechaini),
                                    new SqlParameter("@fechafin", fechafin),
                                    new SqlParameter("@tipo", tipo != 0 ? (object)tipo : DBNull.Value),
                                    new SqlParameter("@localidad", localid)});
                            ViewReport(archivo, dt, busquedaini, busquedafin, localidad);
                            break;
                        }
                    case 1:
                    case 3:
                    case 4:
                        {
                            var localid = int.Parse(Request.QueryString["localid"]);
                            var localidad = Server.UrlDecode(Request.QueryString["localidad"]);

                            dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "rp_ev" + reporte,
                                new[] { new SqlParameter("@fechaini", fechaini),
                                    new SqlParameter("@fechafin", fechafin),
                                    new SqlParameter("@localidad", localid)});
                            ViewReport(archivo, dt, busquedaini, busquedafin, localidad);
                            break;
                        }
                    case 5:
                    case 6:
                        {
                            var eje = int.Parse(Request.QueryString["eje"]);
                            var nivel = Request.QueryString["nivel"];
                            var region = Request.QueryString["region"];
                            var provincia = Request.QueryString["provincia"];
                            var distrito = Request.QueryString["distrito"];

                            dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "rp_ev" + reporte,
                                new[] { new SqlParameter("@fechaini", fechaini),
                                    new SqlParameter("@fechafin", fechafin),
                                    new SqlParameter("@eje", eje != 0 ? (object)eje : DBNull.Value),
                                    new SqlParameter("@region", (object)region ?? DBNull.Value) ,
                                    new SqlParameter("@provincia", (object)provincia ?? DBNull.Value) ,
                                    new SqlParameter("@distrito", (object)distrito ?? DBNull.Value)});
                            ViewReport(archivo, dt, busquedaini, busquedafin, nivel);
                            break;
                        }
                    case 7:
                        {
                            var eje = int.Parse(Request.QueryString["eje"]);
                            var localid = int.Parse(Request.QueryString["localid"]);
                            dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "rp_ev" + reporte,
                                new[] { new SqlParameter("@fechaini", fechaini),
                                    new SqlParameter("@fechafin", fechafin),
                                    new SqlParameter("@localidad", localid)});
                                    //new SqlParameter("@eje", eje != 0 ? (object)eje : DBNull.Value)});
                            ViewReport(archivo, dt, busquedaini, busquedafin);
                            break;
                        }
                }
            }
        }

        private void ViewReport(string File, DataTable dt, string busquedaini, string busquedafin, string lugar = null)
        {
            rptViewer.LocalReport.ReportPath = File;

            rptViewer.LocalReport.SetParameters(new ReportParameter("Usuario", HttpContext.Current.User.Identity.Name));
            rptViewer.LocalReport.SetParameters(new ReportParameter("busquedaini", busquedaini));
            rptViewer.LocalReport.SetParameters(new ReportParameter("busquedafin", busquedafin));

            rptViewer.LocalReport.DisplayName = Util.GetReportName(File);

            if (lugar != null) rptViewer.LocalReport.SetParameters(new ReportParameter("Localidad", lugar));

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rptViewer.PageCountMode = PageCountMode.Actual;
            rptViewer.LocalReport.Refresh();
        }
    }
}