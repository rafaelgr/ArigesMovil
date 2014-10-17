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

public partial class ArticulosDetalle : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string codArtic;
        // controlamos que nadie entre sin hacer login
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        // mostramos el nombre de usuario en el menú.
        Usuario u = (Usuario)Session["Usuario"];
        lblUsuario.Text = u.NomUsu;
        // Tomamos el valor del cliente pasado
        if (Request["CodArtic"] == null)
        {
            // que hacer si falla el código pasado
        }
        codArtic = Request["CodArtic"].ToString();
        Articulo articulo = CntAriGes.GetArticuloExt(codArtic);
        if (articulo == null)
        {
            lblNomArtic.Text = string.Format("Artículo con código {0} desconocido", codArtic);
            return;
        }
        lblNomArtic.Text = articulo.NomArtic;
        CargarCuerpo(articulo);
    }


    protected void CargarCuerpo(Articulo articulo)
    {
        // Cargar sdetalle de stocks
        IList<LineaStock> stocks = CntAriGes.GetLineasStock(articulo);
        Stocks.InnerHtml = CntAriGes.GetStocksHtml(stocks);
        // Cargar componentes
        IList<LineaComponente> componentes = CntAriGes.GetLineasComponente(articulo);
        Componentes.InnerHtml = CntAriGes.GetComponentesHtml(componentes);
    }
}
