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

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        lblVersion.Text = String.Format("VRS {0}", strVersion);
    }

    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        string login = txtLogin.Text;
        string password = txtPassword.Text;
        Usuario u = CntAriUs.GetUsuario(login, password);
        if (u == null)
        {
            string cjs = "bootbox.alert('<h3>Usuario o contraseña incorrectos</h3>');";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        Trabajador t = CntAriGes.GetTrabajador(u.Login);
        if (t == null)
        {
            string cjs = "bootbox.alert('<h3>No hay un trabajador que se corresponda con el usuario</h3>');";
            RadAjaxManager1.ResponseScripts.Add(cjs);
            return;
        }
        Session["Agente"] = t.Agente;
        Session["Usuario"] = u;
        Response.Redirect("Clientes.aspx");
    }
}
