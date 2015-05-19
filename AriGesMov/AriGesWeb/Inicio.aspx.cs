using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using AriUsDB;
using AriGesDB;

public partial class Inicio : System.Web.UI.Page 
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
        menuSuperior.InnerHtml = CntAriGes.GetTabGeneralHtml(u.NomUsu, u.NivelAriges);
    }
}
