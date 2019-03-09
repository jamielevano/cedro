using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using DatabaseContext;
using System.Data.SqlClient;

namespace Web.Reports
{
    public partial class ReportPage : System.Web.UI.Page
    {

        string Reporte = string.Empty;
        string Archivo = string.Empty;

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
            var fechainicio = "";
            var fechafin = "";
            var codigo = "";
            int propositoid = 0;
            int resultadoid = 0;
            int actividadid = 0;
            int tareaid = 0;
            int telecentroid = 0;
            int tipoid = 0;
            int ejeid = 0;


            if (Request.QueryString["Reporte"] != null)
            {
                Reporte = Request.QueryString["reporte"].ToString();
                string sPath = Server.MapPath("") + @"\";
                
                switch (Reporte)
                {

                    case "1":
                        Archivo = sPath + "MatrizMarcoLogicoTC.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MATRIZ_MARCOLOGICO_TC");
                        break;

                    case "2":
                        Archivo = sPath + "MatrizPlanOperativoTC.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MATRIZ_PLANOPERATIVO_TC");
                        break;

                    case "3":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32(Request.QueryString["idResultado"]);
                        propositoid = Convert.ToInt32(Request.QueryString["idProposito"]);

                        Archivo = sPath + "EvaluacionTC.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_EVALUACION_TC",
                             new[] { new SqlParameter("@fechaini", fechainicio),
                                     new SqlParameter("@fechafin", fechafin) ,
                                     new SqlParameter("@propositoid", propositoid),
                                     new SqlParameter("@resultadoid", resultadoid)});
                        break;

                    case "4":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32(Request.QueryString["idResultado"]);
                        actividadid = Convert.ToInt32(Request.QueryString["idActividad"]);
                        tareaid = Convert.ToInt32(Request.QueryString["idTarea"]);
                        telecentroid = Convert.ToInt32(Request.QueryString["idTelecentro"]);
                        tipoid = Convert.ToInt32(Request.QueryString["idTipo"]);
                        ejeid = Convert.ToInt32(Request.QueryString["idEjeIntervencion"]);

                        Archivo = sPath + ((tipoid == 2) ? "MonitoreoTC_Consolidado.rdlc" : "MonitoreoTC.rdlc");

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), ((tipoid == 2) ? "RP_MONITOREO_TC_CONSOLIDADO" : "RP_MONITOREO_TC"),
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                    new SqlParameter("@fechafin", fechafin),
                                    new SqlParameter("@resultadoid", resultadoid),
                                    new SqlParameter("@actividadid", actividadid),
                                    new SqlParameter("@tareaid", tareaid),
                                    new SqlParameter("@telecentroid", telecentroid),
                                    new SqlParameter("@ejeintervencionid", ejeid)
                            });
                        break;

                    

                    case "5":
                        //fechainicio = Request.QueryString["fechainicio"].ToString();
                        //fechafin = Request.QueryString["fechafin"].ToString();
                        codigo = Request.QueryString["codigo"].ToString();

                        Archivo = sPath + "Evaluacion.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_EVALUACION",
                             new[] { //new SqlParameter("@fechaini", fechainicio),
                             //        new SqlParameter("@fechafin", fechafin) ,
                                     new SqlParameter("@codigo", codigo)});
                        break;

                    case "6":
                        //fechainicio = Request.QueryString["fechainicio"].ToString();
                        //fechafin = Request.QueryString["fechafin"].ToString();
                        codigo = Request.QueryString["codigo"].ToString();

                        Archivo = sPath + "Monitoreo.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO",
                            new[] {  new SqlParameter("@codigo", codigo)});
                        break;

                    case "10":
                        codigo = Request.QueryString["codigo"].ToString();
                        Archivo = sPath + "MatrizMarcoLogico.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MATRIZ_MARCOLOGICO",
                            new[] { new SqlParameter("@codigo", codigo) });
                        break;

                    case "11":
                        codigo = Request.QueryString["codigo"].ToString();
                        Archivo = sPath + "MatrizPlanOperativo.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MATRIZ_PLANOPERATIVO",
                            new[] { new SqlParameter("@codigo", codigo) });
                        break;

                    
                }

                if (tipoid==2)
                    ViewReportExport(Archivo, dt);
                else
                    ViewReport(Archivo, dt);
                
            }
        }

        private void ViewReport(string File, DataTable dt )
        {
            rptViewer.LocalReport.ReportPath = File;

            ReportParameter par = new ReportParameter("Usuario", HttpContext.Current.User.Identity.Name);
            rptViewer.LocalReport.SetParameters(par);

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rptViewer.PageCountMode = PageCountMode.Actual;
            rptViewer.LocalReport.Refresh();
            //rptViewer.DocumentMapCollapsed = true;

        }

        private void ViewReportExport(string File, DataTable dt)
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = File;
            localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            byte[] bytes = localReport.Render(
               "Excel", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            Random r = new Random();
            int aleat = r.Next(1, 9999);

            filename = "reporte" + aleat.ToString() + ".xls";
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            //LocalReport localReport = new LocalReport();
            
            //localReport.ReportPath = File;
            //localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            //string reportType = "Excel";
            //string mimeType;
            //string encoding;
            //string fileNameExtension;
            //string deviceInfo = string.Empty;

            //deviceInfo = "<DeviceInfo>" +
            //                    "  <OutputFormat>" + reportType + "</OutputFormat>" +
            //                    "  <PageWidth>11.69in</PageWidth>" +
            //                    "  <PageHeight>8.27in</PageHeight>" +
            //                    "  <MarginTop>0.5in</MarginTop>" +
            //                    "  <MarginLeft>0.19in</MarginLeft>" +
            //                    "  <MarginRight>0.19in</MarginRight>" +
            //                    "  <MarginBottom>0.5in</MarginBottom>" +
            //                    "</DeviceInfo>";

            //Warning[] warnings;
            //string[] streams;
            //byte[] renderedBytes;

            //renderedBytes = localReport.Render(
            //    reportType,
            //    deviceInfo,
            //    out mimeType,
            //    out encoding,
            //    out fileNameExtension,
            //    out streams,
            //    out warnings);


            //Random r = new Random();
            //int aleat = r.Next(1, 9999);

            //Response.Clear();
            //Response.ClearHeaders();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.ContentType = mimeType;
            //Response.AddHeader("content-disposition", "attachment; filename=reporte" + aleat.ToString() + "." + fileNameExtension);
            //Response.BinaryWrite(renderedBytes);
            //Response.End();
        }
    }
}