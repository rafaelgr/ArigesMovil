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

public partial class ClientesOfertas : System.Web.UI.Page 
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
        string tabs = @"
        <ul class='nav nav-tabs'>
            <li>
                <a href='ClientesDetalle.aspx?CodClien={0}'><h4>Datos</h4></a>
            </li>
            <li class='active'>
                <a href='ClientesOfertas.aspx?CodClien={0}'><h4>Ofertas</h4></a>
            </li>
            <li>
                <a href='ClientesPedidos.aspx?CodClien={0}'><h4>Pedidos</h4></a>
            </li>
            <li>
                <a href='ClientesAlbaranes.aspx?CodClien={0}'><h4>Albaranes</h4></a>
            </li>
            <li>
                <a href='ClientesFacturas.aspx?CodClien={0}'><h4>Facturas</h4></a>
            </li>
            <li>
                <a href='ClientesPrecios.aspx?CodClien={0}'><h4>Precios</h4></a>
            </li>
            <li>
                <a href='ClientesMapas.aspx?CodClien={0}'><h4>Contacto</h4></a>
            </li>
        </ul>
        ";
        TabCliente.InnerHtml = String.Format(tabs, cliente.CodClien);
    }

    protected void CargarCuerpo(Cliente cliente)
    {
        IList<Oferta> ofertas = CntAriGes.GetOfertas(cliente.CodClien);
        BodyPedidos.InnerHtml = CntAriGes.GetOfertasHtml(ofertas);
    }
}
