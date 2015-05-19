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

public partial class ClientesPrecios : System.Web.UI.Page 
{
    Cliente cliente;
    Usuario u = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        int codClien;
        Loader.Visible = false;
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
        cliente = CntAriGes.GetCliente(codClien);
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
        
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string jsCmd = "$('#Loader').show();";
        RadAjaxManager1.ResponseScripts.Add(jsCmd);
        IList<Articulo> articulos = CntAriGes.GetArticulos(txtBuscar.Text, cliente);
        BodyPrecios.InnerHtml = CntAriGes.GetArticulosHtml(articulos);
        jsCmd = "$('#Loader').hide();";
        RadAjaxManager1.ResponseScripts.Add(jsCmd);
    }
}
