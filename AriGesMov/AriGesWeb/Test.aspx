<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta charset="utf-8"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <title>AriGesMov TEST</title>
        <!-- Bootstrap -->
        <link href="css/bootstrap.min.css" rel="stylesheet"/>

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
        <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
    </head>
    <body>
        <form id="form1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                </Scripts>
            </telerik:RadScriptManager>
            <script type="text/javascript">
                //Put your JavaScript code here.
            </script>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            </telerik:RadAjaxManager>
            <div class="container">
                <div class="page-header">
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <img src="img/company_logo.png"/>
                        </div>
                        <div class="col-md-6 text-right text-primary">
                            <h1>AriGes Móvil</h1>
                        </div>
                    </div>
                </div>
                <div class="navbar navbar-inverse">
                    <div class="container">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                        </div>
                        <div class="collapse navbar-collapse">
                            <ul class="nav navbar-nav">
                                <li class="active">
                                    <a href="#">Inicio</a>
                                </li>
                                <li>
                                    <a href="#about">Clientes</a>
                                </li>
                                <li>
                                    <a href="Default.aspx">Salir</a>
                                </li>
                            </ul>
                            <ul class="nav navbar-nav pull-right">
                                <li>
                                    <a href="#">
                                        <asp:Label runat="server" ID="lblUsuario" Text="USUARIO"></asp:Label>
                                    </a>
                                </li>
                            </ul>
                        </div><!--/.nav-collapse -->
                    </div>
                </div>

                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h4>Pedro del Pino y Carretera</h4>
                                        </div>
                                        <div class="col-md-1">
                                            <h4>C:1254666</h4>
                                        </div>
                                        <div class="col-md-5">

                                        </div>

                                    </div>
                                </div>
                            </a>
                        </div>
                        <div id="collapseOne" class="panel-collapse collapse">
                            <div class="panel-body">
                                <address>
                                    <strong>NIF:A4555555 EL palomocojo</strong><br />
                                    Calle del pato N.33<br />
                                    45555 Valencia Valencia
                                </address>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <h4>Pedro del Pino y Carretera</h4>
                                        </div>
                                        <div class="col-md-1">
                                            <h4>C:1254666</h4>
                                        </div>
                                        <div class="col-md-5">

                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse">
                            <div class="panel-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <blockquote>
                                                <strong>A4555555 German S.A.</strong><br />
                                                Calle del pato N.33<br />
                                                45555 Valencia Valencia
                                            </blockquote>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <blockquote>
                                                Contacto (1): <em>Juan palomo</em><br />
                                                Teléfono:
                                                <a href="tel:98899">988888</a><br />
                                                Email:
                                                <a href="mailto:pepe@gmail.com">pepe@gmail.com</a>
                                            </blockquote>
                                        </div>
                                        <div class="col-sm-4">
                                            <blockquote>
                                                Contacto (2): <em>Pedro Marmol</em><br />
                                                Teléfono:
                                                <a href="tel:98899">988888</a><br />
                                                EMail:
                                                <a href="mailto:pepe@gmail.com">pepe@gmail.com</a>
                                            </blockquote>
                                        </div>
                                        <div class="col-sm-4">
                                            <a class='btn btn-primary btn-lg text-center' href='ClientesDetalle.aspx?CodClien={0}'>Ver detalles</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">

                        </div>
                    </div>
                </div>
            </div>
        </form>
        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
        <!-- Include all compiled plugins (below), or include individual files as needed -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- Bootbox.js para mostrar mensajes -->
        <script type="text/javascript" src="js/bootbox.js"></script>
    </body>
</html>
