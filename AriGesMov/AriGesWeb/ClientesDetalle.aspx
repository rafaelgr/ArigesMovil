﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientesDetalle.aspx.cs" Inherits="ClientesDetalle" %>

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
        <link rel="stylesheet" href="http://cdn.oesmith.co.uk/morris-0.4.3.min.css"/>
        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
        <telerik:RadStyleSheetManager id="RadStyleSheetManager1" runat="server" />
        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
        <!-- Include all compiled plugins (below), or include individual files as needed -->
        <script type="text/javascript" src="js/bootstrap.min.js"></script>
        <!-- Bootbox.js para mostrar mensajes -->
        <script type="text/javascript" src="js/bootbox.js"></script>
        <!-- Para gráficas -->
        <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
        <script type="text/javascript" src="js/morris.js"></script>

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
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                        </div>
                        <div id="menuSuperior" runat="server" class="collapse navbar-collapse">
                        </div><!--/.nav-collapse -->
                    </div>
                </div>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12">
                            <h2 class="text-primary">
                                <asp:Label ID="lblNomClien" runat="server"></asp:Label>
                            </h2>
                        </div>
                    </div>
                </div>
                <div id="TabCliente" runat="server">

                </div>
                <br />
                <div id="Indicadores" runat="server">

                </div>
                <div id="CobrosPendientes" runat="server">

                </div>
                <div class="panel panel-default">
                    <div class="panel-heading">Facturación anual</div>
                    <div class="panel-body">
                        <div id="grafico-facturas">

                        </div>
                    </div>
                </div>

            </div>
        </form>
        <script type="text/javascript">
            (function ($) {
                $(document).ready(function () {
                    $('#Datos').addClass('active');
                    $('#Clientes').addClass('active');
                });
            })(jQuery);
        </script>
    </body>
</html>
