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

public partial class Proveedores : System.Web.UI.Page 
{
    Usuario u = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        // controlamos que nadie entre sin hacer login
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        // mostramos el nombre de usuario en el menú.
        u = (Usuario)Session["Usuario"];
        menuSuperior.InnerHtml = CntAriGes.GetTabGeneralHtml(u.NomUsu, u.NivelAriges);
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
        IList<Proveedor> proveedores = CntAriGes.GetProveedores(parNom);
        if (proveedores.Count == 0)
        {
            string cjs = "bootbox.alert('<h3>No hay proveedores que coincidan con los criterios</h3>');";
            txtBuscar.Text = "";
            divBusqueda.InnerHtml = "";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        var vHtml = CntAriGes.GetProveedoresHtml(proveedores);
        divBusqueda.InnerHtml = vHtml;
    }
}
