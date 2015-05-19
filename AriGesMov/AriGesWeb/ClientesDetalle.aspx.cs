using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using AriGesDB;
using AriUsDB;

public partial class ClientesDetalle : System.Web.UI.Page 
{
    Usuario u = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        int codClien;
        // controlamos que nadie entre sin hacer login
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        // mostramos el nombre de usuario en el menú.
        u = (Usuario)Session["Usuario"];
        menuSuperior.InnerHtml = CntAriGes.GetTabGeneralHtml(u.NomUsu, u.NivelAriges);
        // Tomamos el valor del cliente pasado
        if (Request["CodClien"] == null)
        {
            // que hacer si falla el código pasado
        }
        codClien = int.Parse(Request["CodClien"].ToString());
        Cliente cliente = CntAriGes.GetCliente(codClien);
        if (cliente == null)
        {
            lblNomClien.Text = string.Format("Cliente con código {0} desconocido", codClien);
            return;
        }
        lblNomClien.Text = cliente.NomClien;
        CargarTabs(cliente);
        CargarCuerpo(cliente);
    }

    protected void CargarTabs(Cliente cliente)
    {
        TabCliente.InnerHtml = CntAriGes.GetTabClientesHtml(cliente, u.NivelAriges);
    }

    protected void CargarCuerpo(Cliente cliente)
    {
        // Estadísticas de facturación anual
        string jsCmd = @"
              element: 'grafico-facturas',
              data: [
                {0}
              ],
              xkey: 'y',
              ykeys: ['a', 'b'],
              labels: ['Este cliente', 'Media clientes']
        ";
        jsCmd = "{" + String.Format(jsCmd, CntAriGes.GetJSONFacturacionAnual(cliente.CodClien)) + "}";
        RadAjaxManager1.ResponseScripts.Add(String.Format("Morris.Line({0})", jsCmd));
        
        // Cargar cobros pendientes
        IList<Cobro> cobros = CntAriGes.GetCobros(cliente);
        CobrosPendientes.InnerHtml = CntAriGes.GetCobrosHtml(cobros);
        // Indicadores
        Indicadores.InnerHtml = CntAriGes.GetIndicadoresHtml(cliente);
    }
}
