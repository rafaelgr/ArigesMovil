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

public partial class Clientes : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // controlamos que nadie entre sin hacer login
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        // mostramos el nombre de usuario en el menú.
        Usuario u = (Usuario)Session["Usuario"];
        lblUsuario.Text = u.NomUsu;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (txtBuscar.Text == "")
        {
            // buscar en vacio equivale a borrar.
            divBusqueda.InnerHtml = "";
            return;
        }
        string parNom = txtBuscar.Text;
        Agente agente = null;
        if (Session["Agente"] != null) agente = (Agente)Session["Agente"];
        IList<Cliente> clientes = CntAriGes.GetClientes(parNom, agente);
        if (clientes.Count == 0)
        {
            string cjs = "bootbox.alert('<h3>No hay clientes asignados que coincidan con los criterios</h3>');";
            txtBuscar.Text = "";
            divBusqueda.InnerHtml = "";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        var vHtml = CntAriGes.GetClientesHtml(clientes);
        divBusqueda.InnerHtml = vHtml;
    }
}
