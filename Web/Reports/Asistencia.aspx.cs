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
    public partial class Asistencia : System.Web.UI.Page
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
            string configuracionid = string.Empty;

            if (Request.QueryString["Reporte"] != null)
            {
                Reporte = Request.QueryString["reporte"].ToString();
                string sPath = Server.MapPath("") + @"\Asistencia\";
                string ejeid = string.Empty;
                string telecentroid = string.Empty;
                string actividadid = string.Empty;
                string fechainicio = string.Empty;
                string fechafin = string.Empty;
                string id = string.Empty;
                string programaid = string.Empty;
                string moduloid = string.Empty;
                string nivelid = string.Empty;
                string periodoid = string.Empty;
                string tipo = string.Empty;
                string excel = string.Empty;
                string formato = string.Empty;
                string procedurename = string.Empty;
                
                switch (Reporte)
                {

                    case "1":
                        configuracionid = Request.QueryString["configuracionid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        moduloid = Request.QueryString["moduloid"].ToString();
                        procedurename = "prpt_capacitacion";
                        
                        if (programaid=="01")
                            Archivo = sPath + "Capacitacion" + programaid + ".rdlc";

                        //if (programaid == "02" || programaid == "03")
                        //    Archivo = sPath + "Capacitacion" + programaid + moduloid + ".rdlc";

                        //if (programaid == "03")
                        //    procedurename += programaid + moduloid;

                        if (programaid == "02")
                            Archivo = sPath + "Capacitacion04" + ".rdlc";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename,
                                new[] { new SqlParameter("@configuracionid", configuracionid)});
                        break;


                    case "2":
                        configuracionid = Request.QueryString["configuracionid"].ToString();
                        Archivo = sPath + "Telecentro.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_telecentro",
                                new[] { new SqlParameter("@configuracionid", configuracionid) });
                        break;

                    case "3":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumen" + tipo + ".rdlc";

                        procedurename = (tipo == "0") ? "prpt_asistencia_listado" : "prpt_asistencia_resumen";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "502":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumenIFM.rdlc";

                        procedurename = "prpt_asistencia_resumen_ifm";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});

                        break;


                    case "503":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionListadoIFM.rdlc";

                        procedurename = "prpt_asistencia_listado_ifm";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});

                        break;

                    case "901":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumen" + tipo + ".rdlc";
                        excel = "excel";

                        procedurename = (tipo == "0") ? "prpt_asistencia_listado" : "prpt_asistencia_resumen";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "4":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "TelecentroResumen" + tipo + ".rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_telecentro_resumen" + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "6":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        Archivo = sPath + "AsistenteParticipante.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_all_capacitados",
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "7":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        Archivo = sPath + "InscritoSinCapacitacion.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_participantes_sin_capacitacion",
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "905":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "TelecentroResumen" + tipo + ".rdlc";
                        excel = "excel";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_telecentro_resumen" + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "5":
                        id = Request.QueryString["id"].ToString();
                        tipo = Request.QueryString["tipo"].ToString();
                        Archivo = sPath + ((tipo == "0") ? "Persona.rdlc" : "Persona01.rdlc");
                        procedurename = ((tipo == "0") ? "prpt_persona" : "prpt_persona01");

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename,
                                new[] { new SqlParameter("@id", id)});
                        break;

                    case "10":
                        excel = Request.QueryString["excel"].ToString();
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        Archivo = sPath + "CapacitacionResumen_Excel.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_asistencia_excel",
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "11":
                        excel = Request.QueryString["excel"].ToString();
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        Archivo = sPath + "TelecentroResumen_Excel.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_telecentro_resumen_excel",
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "12":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumenPer.rdlc";

                        procedurename = (tipo == "0") ? "prpt_asistencia_resumen_capacitados" : "prpt_asistencia_resumen_capacitados";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename,
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "13":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        actividadid = Request.QueryString["actividadid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        Archivo = sPath + "ResumenActividades.rdlc";
                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), "prpt_resumen_actividad",
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@actividadid", actividadid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "300":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumen_EF" + tipo + ".rdlc";

                        procedurename = (tipo == "0") ? "prpt_asistencia_ef_listado" : "prpt_asistencia_ef_resumen";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "301":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionesResumen.rdlc";

                        procedurename = "prpt_asistencia_resumen_capacitaciones";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});

                        break;

                    case "902":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "CapacitacionResumen_EF" + tipo + ".rdlc";
                        excel = "excel";

                        procedurename = (tipo == "0") ? "prpt_asistencia_ef_listado" : "prpt_asistencia_ef_resumen";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@periodo", periodoid),
                                        new SqlParameter("@programaid", programaid),
                                        new SqlParameter("@nivelid", nivelid),
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "400":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteResumen.rdlc";

                        procedurename = "prpt_asistencia_resumen_persona";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "440":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteResumenG.rdlc";

                        procedurename = "prpt_asistencia_resumen_persona";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "441":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteResumen2.rdlc";

                        procedurename = "prpt_asistencia_resumen_persona2";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "442":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteResumenG2.rdlc";

                        procedurename = "prpt_asistencia_resumen_persona2";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;


                    case "903":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteResumen.rdlc";
                        excel = "excel";

                        procedurename = "prpt_asistencia_resumen_persona";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "401":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteListado.rdlc";

                        procedurename = "prpt_asistencia_listado_persona";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;

                    case "904":
                        ejeid = Request.QueryString["ejeid"].ToString();
                        telecentroid = Request.QueryString["telecentroid"].ToString();
                        programaid = Request.QueryString["programaid"].ToString();
                        nivelid = Request.QueryString["nivelid"].ToString();
                        periodoid = Request.QueryString["periodoid"].ToString();
                        fechainicio = Request.QueryString["fechainicio"].ToString();
                        fechafin = Request.QueryString["fechafin"].ToString();
                        tipo = Request.QueryString["tipoid"].ToString();
                        formato = Request.QueryString["formatoid"].ToString();
                        Archivo = sPath + "AsistenteParticipanteListado.rdlc";
                        excel = "excel";

                        procedurename = "prpt_asistencia_listado_persona";

                        dt = new Repositorio.General().ExecuteStoredProcedure(new SMECEntities(), procedurename + ((formato == "1") ? "_contable" : ""),
                                new[] { new SqlParameter("@fechaini", fechainicio) ,
                                        new SqlParameter("@fechafin", fechafin) ,
                                        new SqlParameter("@ejeid", ejeid),
                                        new SqlParameter("@telecentroid", telecentroid)});
                        break;
                }

                //if (Reporte == "5")
                //{
                //    ViewReportExport_v2(Archivo,"Word","doc", dt);
                //}
                //else
                //{
                    if(excel!="")
                        ViewReportExport(Archivo, dt);
                    else
                        ViewReport(Archivo, dt);    
                //}
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
            //rptViewer.LocalReport.EnableExternalImages = true;
            rptViewer.PageCountMode = PageCountMode.Actual;
            rptViewer.LocalReport.Refresh();
        }

        private void ViewReportExport(string File, DataTable dt)
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = File;
            ReportParameter par = new ReportParameter("Usuario", HttpContext.Current.User.Identity.Name);
            localReport.SetParameters(par);
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
        }

        private void ViewReportExport_v2(string File, string TipoArchivo, string Extension, DataTable dt)
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = File;
            ReportParameter par = new ReportParameter("Usuario", HttpContext.Current.User.Identity.Name);
            localReport.SetParameters(par);
            localReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            byte[] bytes = localReport.Render(
               TipoArchivo, null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            Random r = new Random();
            int aleat = r.Next(1, 9999);

            filename = "reporte" + aleat.ToString() + "." + Extension;
            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}