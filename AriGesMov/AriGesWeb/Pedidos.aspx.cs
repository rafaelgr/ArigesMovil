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

public partial class Pedidos : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int codClien;
        // controlamos que nadie entre sin hacer login
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        // mostramos el nombre de usuario en el menú.
        Usuario u = (Usuario)Session["Usuario"];
        lblUsuario.Text = u.NomUsu;
        // agente si lo hay
        Agente agente = null;
        if (Session["Agente"] != null) agente = (Agente)Session["Agente"];

        CargarCuerpo(agente);
    }


    protected void CargarCuerpo(Agente agente)
    {
        IList<Pedido> pedidos = CntAriGes.GetPedidos(agente);
        BodyPedidos.InnerHtml = CntAriGes.GetPedidosHtmlAgente(pedidos);
    }
}
