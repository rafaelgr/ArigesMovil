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
using AriGesDB;
using AriUsDB;

public partial class ClientesMapas : System.Web.UI.Page 
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
        //
        Contacto.InnerHtml = CntAriGes.GetClienteHtml(cliente);
        //
        string address = String.Format("{0} {1} {2} {3}", cliente.DomClien, cliente.CodPobla, cliente.PobClien, cliente.ProClien);
        HtmlControl frame = (HtmlControl)this.FindControl("FrmArea2");
        //string url = String.Format("Mapas1.html?direccion={0}", address).Replace(" ", "%20");
        frame.Attributes["src"] = String.Format("Mapas1.html?direccion={0}", address);
    }

    protected void CargarTabs(Cliente cliente)
    {
        TabCliente.InnerHtml = CntAriGes.GetTabClientesHtml(cliente, u.NivelAriges);
    }

}
