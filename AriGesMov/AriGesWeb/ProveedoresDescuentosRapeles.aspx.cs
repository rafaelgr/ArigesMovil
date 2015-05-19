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

public partial class ProveedoresDescuentosRapeles : System.Web.UI.Page 
{
    Usuario u = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        int codProve;
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
        codProve = int.Parse(Request["CodProve"].ToString());
        Proveedor proveedor = CntAriGes.GetProveedor(codProve);
        if (proveedor == null)
        {
            lblNomProve.Text = string.Format("Cliente con código {0} desconocido", codProve);
            return;
        }
        lblNomProve.Text = proveedor.NomProve;
        CargarTabs(proveedor);
        CargarCuerpo(proveedor);
    }

    protected void CargarTabs(Proveedor proveedor)
    {
        TabProveedor.InnerHtml = CntAriGes.GetTabProveedoresHtml(proveedor);
    }

    protected void CargarCuerpo(Proveedor proveedor)
    {
        IList<DescuentoRapel> ldr = CntAriGes.GetDescuentosRapeles(proveedor.CodProve);
        BodyDescuentosRapeles.InnerHtml = CntAriGes.GetDescuentosRapelesHtml(ldr);
    }
}
