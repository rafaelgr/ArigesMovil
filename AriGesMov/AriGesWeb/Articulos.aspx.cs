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
using System.Web.Services;
using System.Web.Script.Services;

public partial class Articulos : System.Web.UI.Page 
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
        if (txtBuscar.Text == "" && txtProveedor.Text == "" && txtFamilia.Text == "" && txtCodigo.Text == "")
        {
            string cjs = "bootbox.alert('<h3>No se ha seleccionado ningún criterio de búsqueda</h3>');";
            txtBuscar.Text = "";
            divBusqueda.InnerHtml = "";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        string parNom = txtBuscar.Text;
        string codigo = txtCodigo.Text;
        string parProve = txtProveedor.Text;
        string parFam = txtFamilia.Text;
        bool obsoletos = chkObsoletos.Checked;
        Agente agente = null;
        IList<Articulo> articulos = CntAriGes.GetArticulosExt(parNom,parProve,parFam,codigo,obsoletos);
        if (articulos.Count == 0)
        {
            string cjs = "bootbox.alert('<h3>No hay artículos que coincidan con los criterios</h3>');";
            txtBuscar.Text = "";
            divBusqueda.InnerHtml = "";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        var vHtml = CntAriGes.GetArticulosHtmlExt(articulos);
        divBusqueda.InnerHtml = vHtml;
    }
    #region WebMethods
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IList<Familia> GetNombresFamilias(string pre, Aux aux)
    {
        IList<Familia> lf = new List<Familia>();
        lf = CntAriGes.GetFamilias(pre);
        return lf;
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static IList<Familia> GetNombresFamilias(string pre)
    //{
    //    IList<Familia> lf = new List<Familia>();
    //    lf = CntAriGes.GetFamilias(pre);
    //    return lf;
    //}


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IList<Proveedor> GetNombresProveedores(string pre)
    {
        IList<Proveedor> lp = new List<Proveedor>();
        lp = CntAriGes.GetProveedores(pre);
        return lp;
    }
    #endregion
    #region Clases auxiliares
    public class Aux
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int num;

        public int Num
        {
            get { return num; }
            set { num = value; }
        }
    }
    #endregion

}
