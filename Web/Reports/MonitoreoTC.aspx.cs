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
    public partial class MonitoreoTC : System.Web.UI.Page
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
            var resultadoid = 0;
            var actividadid = 0;
            var sectorid = 0;
            var ejeid = 0;
            
            var id = 0;

            if (Request.QueryString["Reporte"] != null)
            {
                Reporte = Request.QueryString["reporte"].ToString();
                string sPath = Server.MapPath("") + @"\MonitoreoTC\Fichas\";
                
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

                        Archivo = sPath + "EvaluacionTC.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_EVALUACION_TC",
                             new[] { new SqlParameter("@fechaini", fechainicio),
                                     new SqlParameter("@fechafin", fechafin) });
                        break;

                    case "4":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();

                        Archivo = sPath + "MonitoreoTC.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO_TC",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                     new SqlParameter("@fechafin", fechafin) });
                        break;

                    case "5":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();

                        Archivo = sPath + "Evaluacion.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_EVALUACION",
                             new[] { new SqlParameter("@fechaini", fechainicio),
                                     new SqlParameter("@fechafin", fechafin) });
                        break;

                    case "6":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();

                        Archivo = sPath + "Monitoreo.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                     new SqlParameter("@fechafin", fechafin) });
                        break;

                    case "101":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32( Request.QueryString["resultadoid"]);
                        actividadid = Convert.ToInt32( Request.QueryString["actividadid"]);
                        sectorid = Convert.ToInt32(Request.QueryString["sectorid"]);
                        ejeid = Convert.ToInt32(Request.QueryString["ejeid"]);
                        
                        Archivo = sPath + "MonitoreoTC_AvanceRegistroActividades.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO_TC_AVANCE_REGISTRO_ACTIVIDADES",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                    new SqlParameter("@fechafin", fechafin) ,
                                    new SqlParameter("@resultadoid", resultadoid), 
                                    new SqlParameter("@actividadid", actividadid),
                                    new SqlParameter("@sectorid", sectorid),
                                    new SqlParameter("@ejeid", ejeid)});
                        break;

                    case "102":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32(Request.QueryString["resultadoid"]);
                        actividadid = Convert.ToInt32(Request.QueryString["actividadid"]);
                        sectorid = Convert.ToInt32(Request.QueryString["sectorid"]);
                        ejeid = Convert.ToInt32(Request.QueryString["ejeid"]);;

                        Archivo = sPath + "MonitoreoTC_Contrapartida.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO_TC_CONTRAPARTIDA",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                    new SqlParameter("@fechafin", fechafin) ,
                                    new SqlParameter("@resultadoid", resultadoid), 
                                    new SqlParameter("@actividadid", actividadid),
                                    new SqlParameter("@sectorid", sectorid),
                                    new SqlParameter("@ejeid", ejeid)});
                        break;

                    
                    case "122":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32(Request.QueryString["resultadoid"]);
                        actividadid = Convert.ToInt32(Request.QueryString["actividadid"]);
                        sectorid = Convert.ToInt32(Request.QueryString["sectorid"]);
                        ejeid = Convert.ToInt32(Request.QueryString["ejeid"]);

                        Archivo = sPath + "MonitoreoTC_Ficha06.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO_TC_FICHA06",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                    new SqlParameter("@fechafin", fechafin) ,
                                    new SqlParameter("@resultadoid", resultadoid), 
                                    new SqlParameter("@actividadid", actividadid),
                                    new SqlParameter("@sectorid", sectorid),
                                    new SqlParameter("@ejeid", ejeid)});
                        break;

                    case "130":
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        resultadoid = Convert.ToInt32(Request.QueryString["resultadoid"]);
                        actividadid = Convert.ToInt32(Request.QueryString["actividadid"]);
                        sectorid = Convert.ToInt32(Request.QueryString["sectorid"]);
                        ejeid = Convert.ToInt32(Request.QueryString["ejeid"]);

                        Archivo = sPath + "MonitoreoTC_Ficha0.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "RP_MONITOREO_TC_FICHA0",
                            new[] { new SqlParameter("@fechaini", fechainicio),
                                    new SqlParameter("@fechafin", fechafin) ,
                                    new SqlParameter("@resultadoid", resultadoid), 
                                    new SqlParameter("@actividadid", actividadid),
                                    new SqlParameter("@sectorid", sectorid),
                                    new SqlParameter("@ejeid", ejeid)});
                        break;


                   case "201":
                        id = Convert.ToInt32(Request.QueryString["id"]);
                        
                        Archivo = sPath + "MonitoreoTC_Ficha06_participantes.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_participantes",
                            new[] { new SqlParameter("@id", id)});
                        break;

                        
                }


                ViewReport(Archivo, dt);                
            }
        }

        private void ViewReport(string File, DataTable dt)
        {
            rptViewer.LocalReport.ReportPath = File;

            ReportParameter par = new ReportParameter("Usuario", HttpContext.Current.User.Identity.Name);
            rptViewer.LocalReport.SetParameters(par);
            rptViewer.LocalReport.DisplayName = Util.GetReportName(File);

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.ProcessingMode = ProcessingMode.Local;

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rptViewer.PageCountMode = PageCountMode.Actual;
            rptViewer.LocalReport.Refresh();
            //rptViewer.DocumentMapCollapsed = true;

        }
    }
}