<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluacionTC.aspx.cs" Inherits="Web.Reports.EvaluacionTC" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" href="~/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server" style="width:100%; height:100%;">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>    
    
    
        <rsweb:ReportViewer ID="rptViewer" runat="server" Height="100%" Width="100%" SizeToReportContent="True">
        </rsweb:ReportViewer>
    

    </form>
</body>
</html>
