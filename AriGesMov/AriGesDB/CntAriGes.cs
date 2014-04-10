using System.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AriGesDB
{
    public static class CntAriGes
    {
        public static MySqlConnection GetConnection()
        {
            // leer la cadena de conexion del config
            var connectionString = ConfigurationManager.ConnectionStrings["Ariges"].ConnectionString;
            // crear la conexion y devolverla.
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        public static string GetCadenaConexionConta()
        {
            string cadena = "Server={0};Database={1};Uid={2};Pwd={3};";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    serconta AS SERCONTA,
                    usuconta AS USUCONTA,
                    pasconta AS PASCONTA,
                    numconta AS NUMCONTA
                    FROM spara1;
                ";
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    string server = rdr.GetString("SERCONTA");
                    string database = "CONTA" + rdr.GetInt32("NUMCONTA").ToString();
                    string user = rdr.GetString("USUCONTA");
                    string password = rdr.GetString("PASCONTA");
                    cadena = String.Format(cadena, server, database, user, password);
                }
                conn.Close();
            }
            return cadena;
        }

        public static MySqlConnection GetConnectionConta()
        {
            // leer la cadena de conexion del config
            var connectionString = GetCadenaConexionConta();
            // crear la conexion y devolverla.
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        #region Agente
        public static Agente GetAgente(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("CODAGENT"))) return null;
            Agente a = new Agente();
            a.CodAgent = rdr.GetInt32("CODAGENT");
            a.NomAgent = rdr.GetString("NOMAGENT");
            return a;
        }

        public static Agente GetAgente(int codAgent)
        {
            Agente a = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    codagent AS CODAGENT, 
                    nomagent AS NOMAGENT, 
                    FROM sagent
                    WHERE codagent = '{0}'";
                sql = String.Format(sql, codAgent);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    a = GetAgente(rdr);
                }
                conn.Close();
            }
            return a;
        }
        #endregion 

        #region Trabajador
        public static Trabajador GetTrabajador(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("CODTRABA"))) return null;
            Trabajador t = new Trabajador();
            t.CodTraba = rdr.GetInt32("CODTRABA");
            t.NomTraba = rdr.GetString("NOMTRABA");
            t.Login = rdr.GetString("LOGIN");
            return t;
        }

        public static Trabajador GetTrabajador(int codTraba)
        {
            Trabajador t = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT
                    t.codtraba AS CODTRABA,
                    t.nomtraba AS NOMTRABA,
                    t.login AS LOGIN,
                    a.codagent AS CODAGENT,
                    a.nomagent AS NOMAGENT
                    FROM straba AS t 
                    LEFT JOIN sagent AS a ON a.codagent = t.codagent
                    WHERE codtraba = '{0}'";
                sql = String.Format(sql, codTraba);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    t = GetTrabajador(rdr);
                    t.Agente = GetAgente(rdr);
                }
                conn.Close();
            }
            return t;
        }

        public static Trabajador GetTrabajador(string login)
        {
            Trabajador t = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT
                    t.codtraba AS CODTRABA,
                    t.nomtraba AS NOMTRABA,
                    t.login AS LOGIN,
                    a.codagent AS CODAGENT,
                    a.nomagent AS NOMAGENT
                    FROM straba AS t 
                    LEFT JOIN sagent AS a ON a.codagent = t.codagent
                    WHERE t.login = '{0}'";
                sql = String.Format(sql, login);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    t = GetTrabajador(rdr);
                    t.Agente = GetAgente(rdr);
                }
                conn.Close();
            }
            return t;
        }
        #endregion

        #region Cliente
        public static Cliente GetCliente(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("CODCLIEN"))) return null;
            Cliente c = new Cliente();
            c.CodClien = rdr.GetInt32("CODCLIEN");
            c.NomClien = rdr.GetString("NOMCLIEN");
            c.NomComer = rdr.GetString("NOMCOMER");
            c.DomClien = rdr.GetString("DOMCLIEN");
            c.CodPobla = rdr.GetString("CODPOBLA");
            c.PobClien = rdr.GetString("POBCLIEN");
            c.ProClien = rdr.GetString("PROCLIEN");
            c.NifClien = rdr.GetString("NIFCLIEN");
            if (!rdr.IsDBNull(rdr.GetOrdinal("PERCLIE1")))
                c.PerClie1 = rdr.GetString("PERCLIE1");
            if (!rdr.IsDBNull(rdr.GetOrdinal("TELCLIE1")))
                c.TelClie1 = rdr.GetString("TELCLIE1");
            if (!rdr.IsDBNull(rdr.GetOrdinal("FAXCLIE1")))
                c.FaxClie1 = rdr.GetString("FAXCLIE1");
            if (!rdr.IsDBNull(rdr.GetOrdinal("PERCLIE2")))
                c.PerClie2 = rdr.GetString("PERCLIE2");
            if (!rdr.IsDBNull(rdr.GetOrdinal("TELCLIE2")))
                c.TelClie2 = rdr.GetString("TELCLIE2");
            if (!rdr.IsDBNull(rdr.GetOrdinal("FAXCLIE2")))
                c.FaxClie2 = rdr.GetString("FAXCLIE2");
            if (!rdr.IsDBNull(rdr.GetOrdinal("MAICLIE1")))
                c.Maiclie1 = rdr.GetString("MAICLIE1");
            if (!rdr.IsDBNull(rdr.GetOrdinal("MAICLIE2")))
                c.Maiclie2 = rdr.GetString("MAICLIE2");
            if (!rdr.IsDBNull(rdr.GetOrdinal("CODMACTA")))
                c.Codmacta = rdr.GetString("CODMACTA");
            c.CodActiv = rdr.GetInt32("CODACTIV");
            c.CodTarif = rdr.GetInt32("CODTARIF");
            c.Promocio = rdr.GetInt32("PROMOCIO");
            return c;
        }

        public static Cliente GetCliente(int codClien)
        {
            Cliente c = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT
                    codclien AS CODCLIEN,
                    nomclien AS NOMCLIEN,
                    nomcomer AS NOMCOMER,
                    domclien AS DOMCLIEN,
                    codpobla AS CODPOBLA,
                    pobclien AS POBCLIEN,
                    proclien AS PROCLIEN,
                    nifclien AS NIFCLIEN,
                    perclie1 AS PERCLIE1,
                    telclie1 AS TELCLIE1,
                    faxclie1 AS FAXCLIE1,
                    perclie2 AS PERCLIE2,
                    telclie2 AS TELCLIE2,
                    faxclie2 AS FAXCLIE2,
                    maiclie1 AS MAICLIE1,
                    maiclie2 AS MAICLIE2,
                    codmacta AS CODMACTA,
                    codactiv AS CODACTIV,
                    codtarif AS CODTARIF,
                    promocio AS PROMOCIO
                    FROM sclien
                    WHERE codclien = '{0}'";
                sql = String.Format(sql, codClien);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    c = GetCliente(rdr);
                }
                conn.Close();
            }
            return c;
        }

        public static IList<Cliente> GetClientes(string parNom)
        {
            IList<Cliente> lc = new List<Cliente>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT
                    codclien AS CODCLIEN,
                    nomclien AS NOMCLIEN,
                    nomcomer AS NOMCOMER,
                    domclien AS DOMCLIEN,
                    codpobla AS CODPOBLA,
                    pobclien AS POBCLIEN,
                    proclien AS PROCLIEN,
                    nifclien AS NIFCLIEN,
                    perclie1 AS PERCLIE1,
                    telclie1 AS TELCLIE1,
                    faxclie1 AS FAXCLIE1,
                    perclie2 AS PERCLIE2,
                    telclie2 AS TELCLIE2,
                    faxclie2 AS FAXCLIE2,
                    maiclie1 AS MAICLIE1,
                    maiclie2 AS MAICLIE2,
                    codmacta AS CODMACTA,
                    codactiv AS CODACTIV,
                    codtarif AS CODTARIF,
                    promocio AS PROMOCIO
                    FROM sclien
                    WHERE nomclien LIKE '%{0}%'
                    ORDER BY nomclien";
                sql = String.Format(sql, parNom);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Cliente c = new Cliente();
                        c = GetCliente(rdr);
                        if (c != null) lc.Add(c);
                    }
                }
                conn.Close();
            }
            return lc;
        }

        public static IList<Cliente> GetClientes(string parNom, Agente agente)
        {
            IList<Cliente> lc = new List<Cliente>();
            if (agente == null)
            {
                lc = GetClientes(parNom);
                return lc;
            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT
                    codclien AS CODCLIEN,
                    nomclien AS NOMCLIEN,
                    nomcomer AS NOMCOMER,
                    domclien AS DOMCLIEN,
                    codpobla AS CODPOBLA,
                    pobclien AS POBCLIEN,
                    proclien AS PROCLIEN,
                    nifclien AS NIFCLIEN,
                    perclie1 AS PERCLIE1,
                    telclie1 AS TELCLIE1,
                    faxclie1 AS FAXCLIE1,
                    perclie2 AS PERCLIE2,
                    telclie2 AS TELCLIE2,
                    faxclie2 AS FAXCLIE2,
                    maiclie1 AS MAICLIE1,
                    maiclie2 AS MAICLIE2,
                    codmacta AS CODMACTA,
                    codactiv AS CODACTIV,
                    codtarif AS CODTARIF,
                    promocio AS PROMOCIO
                    FROM sclien
                    WHERE codagent = {0} 
                    AND nomclien LIKE '%{1}%'";
                sql = String.Format(sql, agente.CodAgent, parNom);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Cliente c = new Cliente();
                        c = GetCliente(rdr);
                        if (c != null) lc.Add(c);
                    }
                }
                conn.Close();
            }
            return lc;
        }

        public static string GetClientesHtml(IList<Cliente> clientes)
        {
            string html = "";
            string panelGroup = @"
                <div class='panel-group' id='accordion'>
                    {0}
                </div>
            ";
            string subpanel = "";
            foreach (Cliente c in clientes)
            {
                string panel = @"
                    <div class='panel panel-default'>
                        <div class='panel-heading'>
                            <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}'>
                                <div class='container'>
                                    <div class='row'>
                                        <div class='col-md-8'>
                                            <h4>{1}</h4>
                                        </div>
                                        <div class='col-md-1'>
                                            <h4>C:{0}</h4>
                                        </div>
                                        <div class='col-md-3'>

                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div id='collapse{0}' class='panel-collapse collapse'>
                            <div class='panel-body'>
                                <div class='container'>
                                    <div class='row'>
                                        <div class='col-sm-8'>
                                            <blockquote>
                                                <strong>{2} {3}</strong><br />
                                                {4}<br />
                                                {5} {6} {7}
                                            </blockquote>
                                        </div>
                                        <div class='col-sm-4'>
                                            <a class='btn btn-primary btn-lg text-center' href='ClientesDetalle.aspx?CodClien={0}'>Ver detalles</a>
                                        </div>
                                    </div>
                                    <div class='row'>
                                        <div class='col-sm-6'>
                                            <blockquote>
                                                <strong>Administración</strong><br/>
                                                <em>{8}</em><br />
                                                Teléfono:
                                                <a href='tel:{9}'>{9}</a><br />
                                                Email:
                                                <a href='mailto:{10}'>{10}</a>
                                            </blockquote>
                                        </div>
                                        <div class='col-sm-6'>
                                            <blockquote>
                                                <strong>Comercial</strong><br/>
                                                <em>{11}</em><br />
                                                Teléfono:
                                                <a href='tel:{12}'>{12}</a><br />
                                                EMail:
                                                <a href='mailto:{13}'>{13}</a>
                                            </blockquote>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ";
                subpanel += String.Format(panel, c.CodClien, c.NomClien, 
                    c.NifClien, c.NomComer, 
                    c.DomClien, c.CodPobla, c.PobClien, c.ProClien,
                    c.PerClie1, c.TelClie1, c.Maiclie1,
                    c.PerClie2, c.TelClie2, c.Maiclie2);
            }
            html = String.Format(panelGroup, subpanel);
            return html;
        }

        public static string GetClienteHtml(Cliente c)
        {
            string html = "";
            if (c == null) return html;
            string plantilla = @"
                    <div class='container'>
                        <div class='row'>
                            <div class='col-sm-12'>
                                <blockquote>
                                    <strong>{2} {3}</strong><br />
                                    {4}<br />
                                    {5} {6} {7}
                                </blockquote>
                            </div>
                        </div>
                        <div class='row'>
                            <div class='col-sm-6'>
                                <blockquote>
                                    <strong>Administración</strong><br/>
                                    <em>{8}</em><br />
                                    Teléfono:
                                    <a href='tel:{9}'>{9}</a><br />
                                    Email:
                                    <a href='mailto:{10}'>{10}</a>
                                </blockquote>
                            </div>
                            <div class='col-sm-6'>
                                <blockquote>
                                    <strong>Comercial</strong><br/>
                                    <em>{11}</em><br />
                                    Teléfono:
                                    <a href='tel:{12}'>{12}</a><br />
                                    EMail:
                                    <a href='mailto:{13}'>{13}</a>
                                </blockquote>
                            </div>
                        </div>
                    </div>
            ";
            html = String.Format(plantilla, c.CodClien, c.NomClien,
                    c.NifClien, c.NomComer,
                    c.DomClien, c.CodPobla, c.PobClien, c.ProClien,
                    c.PerClie1, c.TelClie1, c.Maiclie1,
                    c.PerClie2, c.TelClie2, c.Maiclie2);
            return html;
        }
        #endregion

        #region Pedido
        public static Pedido GetPedido(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMPEDCL"))) return null;
            Pedido p = new Pedido();
            p.NumPedcl = rdr.GetInt32("NUMPEDCL");
            p.FecPedcl = rdr.GetDateTime("FECPEDCL");
            p.FecEntre = rdr.GetDateTime("FECENTRE");
            p.TotalPed = rdr.GetDecimal("TOTALPED");
            return p;
        }

        public static string GetPedidoHtml(Pedido p)
        {
            string html = "";
            string plantilla = @"
            <div class='panel panel-default'>
                <div class='panel-heading'>
                    <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}'>
                        <h4>Pedido {0:0000000}  de fecha {1:dd/MM/yyyy} # {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr class='info'>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Cantidad</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Dto1 (%)</th>
                                <th class='text-right'>Dto2 (%)</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            {4}
                        </table>
                    </div>
                </div>
            </div>             
            ";
            string plantillaLinea = @"
            <tr>
                <td>{0}</td>
                <td>{1}</td>
                <td class='text-right'>{3:##0.00}</td>
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{4:0.00}</td>
                <td class='text-right'>{5:0.00}</td>
                <td class='text-right'>{6:##,###,##0.00}</td>
            </tr>
            ";
            // Cargar las líneas
            string lineas = "";
            foreach (LinPedido lp in p.LineasPedido)
            {
                lineas += String.Format(plantillaLinea, lp.NumLinea, lp.NomArtic, lp.PrecioAr, lp.Cantidad, lp.DtoLine1, lp.DtoLine2, lp.Importel);
            }
            html = String.Format(plantilla, p.NumPedcl, p.FecPedcl, p.FecEntre, p.TotalPed, lineas);
            return html;
        }

        public static string GetPedidosHtml(IList<Pedido> pedidos)
        {
            string html = "";
            if (pedidos.Count == 0)
            {
                html = "<h3>No hay pedidos pendientes de este cliente</h3>";
                return html;
            }
            string plantilla = @"
            <div class='panel-group' id='accordion'>
                {0}
            </div>
            ";
            string detPedidos = "";
            foreach (Pedido p in pedidos)
            {
                detPedidos += GetPedidoHtml(p);
            }
            html = String.Format(plantilla, detPedidos);
            return html;
        }

        public static LinPedido GetLinPedido(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMLINEA"))) return null;
            LinPedido lp = new LinPedido();
            lp.NumLinea = rdr.GetInt32("NUMLINEA");
            lp.CodArtic = rdr.GetString("CODARTIC");
            lp.NomArtic = rdr.GetString("NOMARTIC");
            lp.PrecioAr = rdr.GetDecimal("PRECIOAR");
            lp.Cantidad = rdr.GetDecimal("CANTIDAD");
            lp.DtoLine1 = rdr.GetDecimal("DTOLINE1");
            lp.DtoLine2 = rdr.GetDecimal("DTOLINE2");
            lp.Importel = rdr.GetDecimal("IMPORTEL");
            return lp;

        }
        public static IList<Pedido> GetPedidos(int codClien)
        {
            IList<Pedido> lp = new List<Pedido>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    sc.numpedcl AS NUMPEDCL,
                    sc.codclien AS CODCLIEN,
                    sc.fecpedcl AS FECPEDCL,
                    sc.fecentre AS FECENTRE,
                    sl2.total AS TOTALPED,
                    sl.numlinea AS NUMLINEA,
                    sl.codartic AS CODARTIC,
                    sl.nomartic AS NOMARTIC,
                    sl.precioar AS PRECIOAR,
                    sl.cantidad AS CANTIDAD,
                    sl.servidas AS SERVIDAS,
                    sl.dtoline1 AS DTOLINE1,
                    sl.dtoline2 AS DTOLINE2,
                    sl.importel AS IMPORTEL
                    FROM scaped AS sc
                    LEFT JOIN sliped AS sl ON sl.numpedcl = sc.numpedcl
                    LEFT JOIN (SELECT numpedcl, SUM(importel) AS total
                    FROM sliped
                    GROUP BY numpedcl) AS sl2 ON sl2.numpedcl = sc.numpedcl
                    WHERE sc.codclien = {0}
                    ORDER BY sc.fecpedcl DESC,sc.numpedcl,sl.numlinea;
                ";
                sql = String.Format(sql, codClien);
                cmd.CommandText=sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    int numPedCl = 0;
                    Pedido p = null;
                    while (rdr.Read())
                    {
                        // eliminamos pedidos sin líneas
                        if (rdr.IsDBNull(rdr.GetOrdinal("TOTALPED"))) continue;
                        if (rdr.GetInt32("NUMPEDCL") != numPedCl)
                        {
                            p = GetPedido(rdr);
                            numPedCl = p.NumPedcl;
                            p.LineasPedido.Add(GetLinPedido(rdr));
                            lp.Add(p);
                        }
                        else
                        {
                            p.LineasPedido.Add(GetLinPedido(rdr));
                        }
                       
                    }
                }
            }
            return lp;
        }
        #endregion

        #region Oferta
        public static Oferta GetOferta(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMOFERT"))) return null;
            Oferta o = new Oferta();
            o.NumOfert = rdr.GetInt32("NUMOFERT");
            o.FecOfert = rdr.GetDateTime("FECOFERT");
            o.FecEntre = rdr.GetDateTime("FECENTRE");
            o.TotalOfe = rdr.GetDecimal("TOTALOFE");
            o.Aceptado = rdr.GetBoolean("ACEPTADO");
            return o;
        }

        public static string GetOfertaHtml(Oferta o)
        {
            string html = "";
            string plantilla = @"
            <div class='panel {5}'>
                <div class='panel-heading'>
                    <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}'>
                        <h4>Oferta {0:0000000} de {1:dd/MM/yyyy} # {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr class='info'>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Cantidad</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Dto1 (%)</th>
                                <th class='text-right'>Dto2 (%)</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            {4}
                        </table>
                    </div>
                </div>
            </div>             
            ";
            string plantillaLinea = @"
            <tr>
                <td>{0}</td>
                <td>{1}</td>
                <td class='text-right'>{3:##0.00}</td>
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{4:0.00}</td>
                <td class='text-right'>{5:0.00}</td>
                <td class='text-right'>{6:##,###,##0.00}</td>
            </tr>
            ";
            // Cargar las líneas
            string lineas = "";
            foreach (LinOferta lp in o.LineasOferta)
            {
                lineas += String.Format(plantillaLinea, lp.NumLinea, lp.NomArtic, lp.PrecioAr, lp.Cantidad, lp.DtoLine1, lp.DtoLine2, lp.Importel);
            }
            string clase = "panel-default";
            if (o.Aceptado) clase = "panel-success";
            html = String.Format(plantilla, o.NumOfert, o.FecOfert, o.FecEntre, o.TotalOfe, lineas, clase);
            return html;
        }

        public static string GetOfertasHtml(IList<Oferta> Ofertas)
        {
            string html = "";
            if (Ofertas.Count == 0)
            {
                html = "<h3>No hay ofertas de este cliente</h3>";
                return html;
            }
            string plantilla = @"
            <div class='panel-group' id='accordion'>
                {0}
            </div>
            ";
            string detOfertas = "";
            foreach (Oferta p in Ofertas)
            {
                detOfertas += GetOfertaHtml(p);
            }
            html = String.Format(plantilla, detOfertas);
            return html;
        }

        public static LinOferta GetLinOferta(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMLINEA"))) return null;
            LinOferta lp = new LinOferta();
            lp.NumLinea = rdr.GetInt32("NUMLINEA");
            lp.CodArtic = rdr.GetString("CODARTIC");
            lp.NomArtic = rdr.GetString("NOMARTIC");
            lp.PrecioAr = rdr.GetDecimal("PRECIOAR");
            lp.Cantidad = rdr.GetDecimal("CANTIDAD");
            lp.DtoLine1 = rdr.GetDecimal("DTOLINE1");
            lp.DtoLine2 = rdr.GetDecimal("DTOLINE2");
            lp.Importel = rdr.GetDecimal("IMPORTEL");
            return lp;

        }
        public static IList<Oferta> GetOfertas(int codClien)
        {
            IList<Oferta> lp = new List<Oferta>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    sc.numofert AS NUMOFERT,
                    sc.codclien AS CODCLIEN,
                    sc.fecofert AS FECOFERT,
                    sc.fecentre AS FECENTRE,
                    sc.aceptado AS ACEPTADO,
                    sl2.total AS TOTALOFE,
                    sl.numlinea AS NUMLINEA,
                    sl.codartic AS CODARTIC,
                    sl.nomartic AS NOMARTIC,
                    sl.precioar AS PRECIOAR,
                    sl.cantidad AS CANTIDAD,
                    sl.dtoline1 AS DTOLINE1,
                    sl.dtoline2 AS DTOLINE2,
                    sl.importel AS IMPORTEL
                    FROM scapre AS sc
                    LEFT JOIN slipre AS sl ON sl.numofert = sc.numofert
                    LEFT JOIN (SELECT numofert, SUM(importel) AS total
                    FROM slipre
                    GROUP BY numofert) AS sl2 ON sl2.numofert = sc.numofert
                    WHERE sc.codclien = {0}
                    ORDER BY sc.fecofert DESC,sc.numofert,sl.numlinea;
                ";
                sql = String.Format(sql, codClien);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    int numOfert = 0;
                    Oferta o = null;
                    while (rdr.Read())
                    {
                        // eliminamos Ofertas sin líneas
                        if (rdr.IsDBNull(rdr.GetOrdinal("TOTALOFE"))) continue;
                        if (rdr.GetInt32("NUMOFERT") != numOfert)
                        {
                            o = GetOferta(rdr);
                            numOfert = o.NumOfert;
                            o.LineasOferta.Add(GetLinOferta(rdr));
                            lp.Add(o);
                        }
                        else
                        {
                            o.LineasOferta.Add(GetLinOferta(rdr));
                        }

                    }
                }
            }
            return lp;
        }
        #endregion

        #region Albaran
        public static Albaran GetAlbaran(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMALBAR"))) return null;
            Albaran a = new Albaran();
            a.CodTipom = rdr.GetString("CODTIPOM");
            a.NumAlbar = rdr.GetInt32("NUMALBAR");
            a.FechaAlb = rdr.GetDateTime("FECHAALB");
            a.TotalAlb = rdr.GetDecimal("TOTALALB");
            return a;
        }

        public static string GetAlbaranHtml(Albaran a)
        {
            string html = "";
            string plantilla = @"
            <div class='panel panel-default'>
                <div class='panel-heading'>
                    <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}'>
                        <h4>Albaran {5} de {1:dd/MM/yyyy} # {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr class='info'>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Cantidad</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Dto1 (%)</th>
                                <th class='text-right'>Dto2 (%)</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            {4}
                        </table>
                    </div>
                </div>
            </div>             
            ";
            string plantillaLinea = @"
            <tr>
                <td>{0}</td>
                <td>{1}</td>
                <td class='text-right'>{3:##0.00}</td>
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{4:0.00}</td>
                <td class='text-right'>{5:0.00}</td>
                <td class='text-right'>{6:##,###,##0.00}</td>
            </tr>
            ";
            // Cargar las líneas
            string lineas = "";
            foreach (LinAlbaran lp in a.LineasAlbaran)
            {
                lineas += String.Format(plantillaLinea, lp.NumLinea, lp.NomArtic, lp.PrecioAr, lp.Cantidad, lp.DtoLine1, lp.DtoLine2, lp.Importel);
            }
            string codAlbar = String.Format("{0}-{1:0000000}", a.CodTipom, a.NumAlbar);
            if (codAlbar == "-0000000") codAlbar = "";
            html = String.Format(plantilla, a.NumAlbar, a.FechaAlb, a.CodTipom, a.TotalAlb, lineas,codAlbar);
            return html;
        }

        public static string GetAlbaransHtml(IList<Albaran> Albarans)
        {
            string html = "";
            if (Albarans.Count == 0)
            {
                html = "<h3>No hay albaranes de este cliente</h3>";
                return html;
            }
            string plantilla = @"
            <div class='panel-group' id='accordion'>
                {0}
            </div>
            ";
            string detAlbarans = "";
            foreach (Albaran p in Albarans)
            {
                detAlbarans += GetAlbaranHtml(p);
            }
            html = String.Format(plantilla, detAlbarans);
            return html;
        }

        public static LinAlbaran GetLinAlbaran(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMLINEA"))) return null;
            LinAlbaran lp = new LinAlbaran();
            lp.NumLinea = rdr.GetInt32("NUMLINEA");
            lp.CodArtic = rdr.GetString("CODARTIC");
            lp.NomArtic = rdr.GetString("NOMARTIC");
            lp.PrecioAr = rdr.GetDecimal("PRECIOAR");
            lp.Cantidad = rdr.GetDecimal("CANTIDAD");
            lp.DtoLine1 = rdr.GetDecimal("DTOLINE1");
            lp.DtoLine2 = rdr.GetDecimal("DTOLINE2");
            lp.Importel = rdr.GetDecimal("IMPORTEL");
            return lp;

        }
        public static IList<Albaran> GetAlbarans(int codClien)
        {
            IList<Albaran> lp = new List<Albaran>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    sc.codtipom AS CODTIPOM,
                    sc.numalbar AS NUMALBAR,
                    sc.codclien AS CODCLIEN,
                    sc.fechaalb AS FECHAALB,
                    sl2.totalalb AS TOTALALB,
                    sl.numlinea AS NUMLINEA,
                    sl.codartic AS CODARTIC,
                    sl.nomartic AS NOMARTIC,
                    sl.precioar AS PRECIOAR,
                    sl.cantidad AS CANTIDAD,
                    sl.dtoline1 AS DTOLINE1,
                    sl.dtoline2 AS DTOLINE2,
                    sl.importel AS IMPORTEL
                    FROM scaalb AS sc
                    LEFT JOIN slialb AS sl ON (sl.codtipom = sc.codtipom AND sl.numalbar = sc.numalbar)
                    LEFT JOIN (SELECT codtipom, numalbar, SUM(importel) AS totalalb
                    FROM slialb
                    GROUP BY codtipom, numalbar) AS sl2 ON (sl2.codtipom = sc.codtipom AND sl2.numalbar = sc.numalbar)
                    WHERE sc.codclien = {0}
                    ORDER BY sc.fechaalb DESC,sc.codtipom,sc.numalbar,sl.numlinea;
                ";
                sql = String.Format(sql, codClien);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    string codTipom = "";
                    int numAlbar = 0;
                    Albaran a = null;
                    while (rdr.Read())
                    {
                        // eliminamos Albarans sin líneas
                        if (rdr.IsDBNull(rdr.GetOrdinal("TOTALALB"))) continue;
                        if (rdr.GetString("CODTIPOM") != codTipom && rdr.GetInt32("NUMALBAR") != numAlbar)
                        {
                            a = GetAlbaran(rdr);
                            codTipom = a.CodTipom;
                            numAlbar = a.NumAlbar;
                            a.LineasAlbaran.Add(GetLinAlbaran(rdr));
                            lp.Add(a);
                        }
                        else
                        {
                            a.LineasAlbaran.Add(GetLinAlbaran(rdr));
                        }

                    }
                }
            }
            return lp;
        }
        #endregion

        #region Factura
        public static Factura GetFactura(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMFACTU"))) return null;
            Factura f = new Factura();
            f.CodTipom = rdr.GetString("CODTIPOM");
            f.NumFactu = rdr.GetInt32("NUMFACTU");
            f.FecFactu = rdr.GetDateTime("FECFACTU");
            f.Bases = rdr.GetDecimal("BASES");
            f.Cuotas = rdr.GetDecimal("CUOTAS");
            f.TotalFac = rdr.GetDecimal("TOTALFAC");
            return f;
        }

        public static string GetFacturaHtml(Factura f)
        {
            string html = "";
            string plantilla = @"
            <div class='panel panel-default'>
                <div class='panel-heading'>
                    <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}{1}'>
                        <h4>Factura {0}-{1:0000000} de {2:dd/MM/yyyy} # {3:#,###,##0.00} + {4:#,###,##0.00} = {5:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}{1}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr class='info'>
                                <th>Albaran</th>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Cantidad</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Dto1 (%)</th>
                                <th class='text-right'>Dto2 (%)</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            {6}
                        </table>
                    </div>
                </div>
            </div>             
            ";
            string plantillaLinea = @"
            <tr>
                <td>{10}</td>
                <td>{2}</td>
                <td>{3}</td>
                <td class='text-right'>{5:##0.00}</td>
                <td class='text-right'>{4:###,##0.00}</td>
                <td class='text-right'>{6:0.00}</td>
                <td class='text-right'>{7:0.00}</td>
                <td class='text-right'>{8:##,###,##0.00}</td>
            </tr>
            ";
            string plantillaAlb = @"
            <tr>
                <td colspan='7'>
                    <strong>Albaran: </strong>{0}
                </td>
            </tr>
            ";
            // Cargar las líneas
            string lineas = "";
            string codAlbar = "";
            string codAlbarOld = "";
            string showAlbar;
            foreach (LinFactura lf in f.LineasFactura)
            {
                codAlbar = String.Format("{0}-{1:0000000}", lf.CodTipoa, lf.NumAlbar);
                if (codAlbar != codAlbarOld)
                {
                    showAlbar = codAlbar;
                    if (codAlbar == "-0000000") showAlbar = "";
                    codAlbarOld = codAlbar;
                }
                else
                {
                    showAlbar = "";
                }
                lineas += String.Format(plantillaLinea, lf.CodTipoa, lf.NumAlbar,
                    lf.NumLinea, lf.NomArtic, lf.PrecioAr,lf.Cantidad, lf.DtoLine1, lf.DtoLine2,lf.Importel, codAlbar, showAlbar);
            }
            html = String.Format(plantilla, f.CodTipom, f.NumFactu,
                f.FecFactu,f.Bases,f.Cuotas,f.TotalFac, lineas);
            return html;
        }

        public static string GetFacturasHtml(IList<Factura> facturas)
        {
            string html = "";
            if (facturas.Count == 0)
            {
                html = "<h3>No hay facturas de este cliente</h3>";
                return html;
            }
            string plantilla = @"
            <div class='panel-group' id='accordion'>
                {0}
            </div>
            ";
            string detFacturas = "";
            foreach (Factura f in facturas)
            {
                detFacturas += GetFacturaHtml(f);
            }
            html = String.Format(plantilla, detFacturas);
            return html;
        }

        public static LinFactura GetLinFactura(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("NUMLINEA"))) return null;
            LinFactura lf = new LinFactura();
            lf.CodTipoa = rdr.GetString("CODTIPOA");
            lf.NumAlbar = rdr.GetInt32("NUMALBAR");
            lf.NumLinea = rdr.GetInt32("NUMLINEA");
            lf.CodArtic = rdr.GetString("CODARTIC");
            lf.NomArtic = rdr.GetString("NOMARTIC");
            lf.PrecioAr = rdr.GetDecimal("PRECIOAR");
            lf.Cantidad = rdr.GetDecimal("CANTIDAD");
            lf.DtoLine1 = rdr.GetDecimal("DTOLINE1");
            lf.DtoLine2 = rdr.GetDecimal("DTOLINE2");
            lf.Importel = rdr.GetDecimal("IMPORTEL");
            return lf;

        }

        public static IList<Factura> GetFacturas(int codClien)
        {
            IList<Factura> lf = new List<Factura>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                        SELECT
                        f.codtipom AS CODTIPOM,
                        f.numfactu AS NUMFACTU,
                        f.fecfactu AS FECFACTU,
                        (COALESCE(f.baseimp1,0) + COALESCE(f.baseimp2,0) + COALESCE(f.baseimp3,0)) AS BASES,
                        (COALESCE(f.imporiv1,0) + COALESCE(f.imporiv2,0) + COALESCE(f.imporiv3,0)
                         + COALESCE(f.imporiv1re,0) + COALESCE(f.imporiv2re,0) + COALESCE(f.imporiv3re,0)) AS CUOTAS,
                        f.totalfac AS TOTALFAC,
                        lf.codtipoa AS CODTIPOA,
                        lf.numalbar AS NUMALBAR,
                        lf.numlinea AS NUMLINEA,
                        lf.codartic AS CODARTIC,
                        lf.nomartic AS NOMARTIC,
                        lf.precioar AS PRECIOAR,
                        lf.cantidad AS CANTIDAD,
                        lf.dtoline1 AS DTOLINE1,
                        lf.dtoline2 AS DTOLINE2,
                        lf.importel AS IMPORTEL
                        FROM scafac AS f
                        LEFT JOIN slifac AS lf ON (lf.codtipom = f.codtipom AND lf.numfactu = f.numfactu AND lf.fecfactu = f.fecfactu)
                        WHERE f.codclien = {0}
                        ORDER BY f.fecfactu DESC, f.codtipom, f.numfactu, lf.codtipoa, lf.numalbar, lf.numlinea;
                ";
                sql = String.Format(sql, codClien);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    string codTipom = "";
                    int numFactu = 0;
                    Factura f = null;
                    while (rdr.Read())
                    {
                        // eliminamos Albarans sin líneas
                        if (rdr.IsDBNull(rdr.GetOrdinal("TOTALFAC"))) continue;
                        if (rdr.GetString("CODTIPOM") != codTipom || rdr.GetInt32("NUMFACTU") != numFactu)
                        {
                            f = GetFactura(rdr);
                            codTipom = f.CodTipom;
                            numFactu = f.NumFactu;
                            f.LineasFactura.Add(GetLinFactura(rdr));
                            lf.Add(f);
                        }
                        else
                        {
                            f.LineasFactura.Add(GetLinFactura(rdr));
                        }

                    }
                }
            }
            return lf;
        }
        #endregion

        #region Cobro

        public static Cobro GetCobro(MySqlDataReader rdr)
        {
            Cobro c = new Cobro();
            c.FechaVenci = rdr.GetDateTime("FECHAVENCI");
            c.FechaFact = rdr.GetDateTime("FECHAFACT");
            c.NumFact = rdr.GetString("NUMFACT");
            c.NomForpa = rdr.GetString("NOMFORPA");
            c.Total = rdr.GetDecimal("TOTAL");
            return c;
        }

        public static IList<Cobro> GetCobros(Cliente cliente)
        {
            IList<Cobro> lc = new List<Cobro>();
            using (MySqlConnection conn = GetConnectionConta())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT 
                    fecvenci AS FECHAVENCI,
                    CONCAT(numserie,RIGHT(CONCAT('00000000',codfaccl),7)) AS NUMFACT,
                    fecfaccl AS FECHAFACT,
                    nomforpa AS NOMFORPA,
                    impvenci+IF(gastos IS NULL,0,gastos)-IF(impcobro IS NULL,0,impcobro) AS TOTAL 
                    FROM  scobro 
                    INNER JOIN sforpa ON scobro.codforpa=sforpa.codforpa  
                    WHERE scobro.codmacta = '{0}'
                    AND impvenci+IF(gastos IS NULL,0,gastos)-IF(impcobro IS NULL,0,impcobro) <> 0
                ";
                sql = String.Format(sql, cliente.Codmacta);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        lc.Add(GetCobro(rdr));
                    }
                }
                conn.Close();
            }
            return lc;
        }

        public static string GetCobrosHtml(IList<Cobro> cobros)
        {
            string html = "";
            string plantilla = @"
                <div class='panel panel-default'>
                    <div class='panel-heading'>Cobros pendientes</div>
                    <div class='panel-body'>
                        {0}
                    </div>
                </div>
            ";
            if (cobros.Count == 0)
            {
                html = String.Format(plantilla, "<h4>No hay cobros pendientes para este cliente</h4>");
            }
            else
            {
                string plantillaTabla = @"
                    <div class='table-responsive'>
                        <table class='table table-bordered'>
                            <tr>
                                <th>Fecha ven.</th>
                                <th>Tipo cobro</th>
                                <th>Factura</th>
                                <th>Fecha fac.</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            {0}
                        </table>
                    </div>
                ";
                string plantillaCobro = @"
                    <tr {0}>
                        <td>{1:dd/MM/yyyy}</td>
                        <td>{2}</td>
                        <td>{3}</td>
                        <td>{4:dd/MM/yyyy}</td>
                        <td class='text-right'>{5:###,###,##0.00 €}</td>
                    </tr>
                ";
                string detCobro = "";
                foreach (Cobro c in cobros)
                {
                    string vencido = "";
                    if (c.FechaVenci < DateTime.Now) vencido = "class='danger'";
                    detCobro += String.Format(plantillaCobro, vencido, c.FechaVenci, c.NomForpa, c.NumFact, c.FechaFact, c.Total);
                }
                string tabla = String.Format(plantillaTabla, detCobro);
                html = String.Format(plantilla, tabla);
            }
            return html;
        }
        #endregion

        #region Articulo

        public static decimal GetStock(string codArtic)
        {
            decimal stock = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    SUM(canstock) AS STOCK
                    FROM salmac
                    WHERE codartic = '{0}';
                ";
                sql = String.Format(sql, codArtic);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    stock = rdr.GetDecimal("STOCK");
                }
                conn.Close();
            }
            return stock;
        }


        public static Articulo GetArticulo(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("CODARTIC"))) return null;
            Articulo a = new Articulo();
            a.CodArtic = rdr.GetString("CODARTIC");
            a.NomArtic = rdr.GetString("NOMARTIC");
            a.Preciove = rdr.GetDecimal("PRECIOVE");
            a.CodFamia = rdr.GetInt32("CODFAMIA");
            a.CodMarca = rdr.GetInt32("CODMARCA");
            return a;
        }

        public static Articulo GetArticulo(string codArtic)
        {
            Articulo a = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    codartic AS CODARTIC, 
                    nomartic AS NOMARTIC, 
                    preciove AS PRECIOVE,
                    codfamia AS CODFAMIA,
                    codmarca AS CODMARCA
                    FROM sartic
                    WHERE codartic = '{0}'";
                sql = String.Format(sql, codArtic);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    a = GetArticulo(rdr);
                }
                conn.Close();
            }
            a.Stock = GetStock(a.CodArtic);
            return a;
        }

        public static IList<Articulo> GetArticulos(string parNom, Cliente cliente)
        {
            IList<Articulo> la = new List<Articulo>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    codartic AS CODARTIC, 
                    nomartic AS NOMARTIC, 
                    preciove AS PRECIOVE,
                    codfamia AS CODFAMIA,
                    codmarca AS CODMARCA
                    FROM sartic
                    WHERE nomartic LIKE '%{0}%'
                    ORDER BY nomartic";
                sql = String.Format(sql, parNom);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Articulo a = new Articulo();
                        a = GetArticulo(rdr);
                        if (a != null)
                        {
                            a.Precio = GetPrecio(a, cliente);
                            a.Stock = GetStock(a.CodArtic);
                            la.Add(a);
                        }
                    }
                }
                conn.Close();
            }
            return la;
        }

        public static string GetArticuloHtml(Articulo a)
        {
            string html = "";
            string plantilla = @"
            <div class='panel panel-default'>
                <div class='panel-heading'>
                    <a data-toggle='collapse' data-parent='#accordion' href='#collapse{8}'>
                        <h4>{1} C:{0} # {5:#,###,##0.00 €} # Stock: {7:###,##0.00}</h4>
                    </a>
                </div>
                <div id='collapse{8}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr class='info'>
                                <th>Origen</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Dto1 (%)</th>
                                <th class='text-right'>Dto2 (%)</th>
                                <th class='text-right'>Importe</th>
                            </tr>
                            <tr>
                                <td>{6}</td>
                                <td class='text-right'>{2:#,###,##0.00}</td>
                                <td class='text-right'>{3:0.00}</td>
                                <td class='text-right'>{4:0.00}</td>
                                <td class='text-right'>{5:#,###,##0.00}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>             
            ";
            string pattern = "[^0-9a-zA-z_\\-:]";
            Regex rgx = new Regex(pattern);
            string cod = rgx.Replace(a.CodArtic, "");
            html = String.Format(plantilla, a.CodArtic, a.NomArtic, a.Precio.Pvp, a.Precio.Dto1, a.Precio.Dto2, a.Precio.Importe, a.Precio.Origen, a.Stock, cod);
            return html;
        }

        public static string GetArticulosHtml(IList<Articulo> articulos)
        {
            string html = "";
            if (articulos.Count == 0)
            {
                html = "<h4>No se han encontrado artículos</h4>";
                return html;
            }
            string plantilla = @"
            <div class='panel-group' id='accordion'>
                {0}
            </div>
            ";
            string detArticulos = "";
            foreach (Articulo a in articulos)
            {
                detArticulos += GetArticuloHtml(a);
            }
            html = String.Format(plantilla, detArticulos);
            return html;
        }

        #endregion 

        #region Estadísticas
        public static string GetJSONFacturacionAnual(int codClien)
        {
            string plantilla = "y: '{0}', a: {1}, b:{2}";
            string JSON = "";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    (SUM(COALESCE(f.baseimp1,0) + COALESCE(f.baseimp2,0) + COALESCE(f.baseimp3,0)) /
                    COUNT(DISTINCT(f.codclien))) AS VENTA,
                    YEAR(f.fecfactu) AS ANO
                    FROM scafac AS f
                    GROUP BY ANO;        
                ";
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        MySqlConnection conn2 = GetConnection();
                        conn2.Open();
                        MySqlCommand cmd2 = conn2.CreateCommand();
                        string sql2 = @"
                            SELECT
                            COALESCE(SUM(COALESCE(f.baseimp1,0) + COALESCE(f.baseimp2,0) + COALESCE(f.baseimp3,0)),0) AS TOTAL
                            FROM scafac AS f
                            WHERE f.codclien = {0} AND YEAR(f.fecfactu) = '{1}';
                        ";
                        sql2 = String.Format(sql2, codClien, rdr.GetInt32("ANO"));
                        cmd2.CommandText = sql2;
                        MySqlDataReader rdr2 = cmd2.ExecuteReader();
                        if (rdr2.HasRows)
                        {
                            rdr2.Read();
                            string total = Math.Round(rdr2.GetDecimal("TOTAL"),2).ToString().Replace(",",".");
                            string venta = Math.Round(rdr.GetDecimal("VENTA"),2).ToString().Replace(",",".");
                            JSON += "{" + String.Format(plantilla, rdr.GetInt32("ANO"), total, venta) + "},";
                        }
                        conn2.Close();
                    }
                }
                conn.Close();
            }
            return JSON;
        }

        public static string GetIndicadoresHtml(Cliente cliente)
        {
            string html = "";
            string plantilla = @"
                <div class='panel panel-default'>
                    <div class='panel-heading'>INDICADORES</div>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr>
                                <th class='text-center info'>Ofertas</th>
                                <th class='text-center info'>Pedidos</th>
                                <th class='text-center info'>Albaranes</th>
                                <th class='text-right success'>Saldo pendiente</th>
                                <th class='text-right danger'>Saldo vencido</th>
                            </tr>
                            <tr>
                                <td class='text-center'>{0}</td>
                                <td class='text-center'>{1}</td>
                                <td class='text-center'>{2}</td>
                                <td class='text-right'>{3:###,###,##0.00 €}</td>
                                <td class='text-right'>{4:###,###,##0.00 €}</td>
                            </tr>
                        </table>
                    </div>
                </div>
            ";
            int numOfertas = 0;
            IList<Oferta> ofertas = GetOfertas(cliente.CodClien);
            numOfertas = ofertas.Count;
            int numPedidos = 0;
            IList<Pedido> pedidos = GetPedidos(cliente.CodClien);
            numPedidos = pedidos.Count;
            int numAlbaranes = 0;
            IList<Albaran> albaranes = GetAlbarans(cliente.CodClien);
            numAlbaranes = albaranes.Count;
            decimal saldoPendiente = 0;
            decimal saldoVencido = 0;
            IList<Cobro> cobros = GetCobros(cliente);
            foreach (Cobro c in cobros)
            {
                saldoPendiente += c.Total;
                if (c.FechaVenci < DateTime.Now) saldoVencido += c.Total;
            }
            html = String.Format(plantilla, numOfertas, numPedidos, numAlbaranes, saldoPendiente, saldoVencido);
            return html;
        }
        #endregion

        #region Cálculo de precios

        public static Precio GetPrecio(Articulo a, Cliente c)
        {
            Precio precio = new Precio();
            bool precioMinimo = GetPrecioMinimo();
            if (!precioMinimo)
            {
                precio = GetPrecioPromocion(a, c);
                if (precio.Pvp != 0) return precio;
                precio = GetPrecioEspeciales(a, c);
                if (precio.Pvp != 0) return precio;
                precio = GetPrecioTarifas(a, c);
                if (precio.Pvp != 0) return precio;
                precio = GetPrecioArticulo(a, c);
            }
            else
            {
                Precio pAux = new Precio();
                pAux.Pvp = 9999999;
                pAux.Origen = "ERROR";
                precio = GetPrecioPromocion(a, c);
                if (precio.Pvp != 0 && precio.Pvp < pAux.Pvp) pAux = precio;
                precio = GetPrecioEspeciales(a, c);
                if (precio.Pvp != 0 && precio.Pvp < pAux.Pvp) pAux = precio;
                precio = GetPrecioTarifas(a, c);
                if (precio.Pvp != 0 && precio.Pvp < pAux.Pvp) pAux = precio;
                precio = GetPrecioArticulo(a, c);
                if (precio.Pvp != 0 && precio.Pvp < pAux.Pvp) pAux = precio;
                precio = pAux;
            }
            return precio;
        }

        public static bool GetPrecioMinimo()
        {
            bool precioMinimo = false;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    preciominimo AS PRECIOMINIMO
                    FROM spara1";
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precioMinimo = rdr.GetBoolean("PRECIOMINIMO");
                }
                conn.Close();
            }
            return precioMinimo;
        }

        public static bool GetSobreResto()
        {
            bool sobreResto = false;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    tipodtos AS TIPODTOS
                    FROM spara1";
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    sobreResto = rdr.GetBoolean("TIPODTOS");
                }
                conn.Close();
            }
            return sobreResto;
        }

        public static Precio GetPrecioPromocion(Articulo a, Cliente c)
        {
            Precio precio = new Precio();
            bool dtoPermi = false;
            precio.Origen = "PROMOCION";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precioac AS PRECIOAC
                    FROM spromo
                    WHERE codartic = '{0}'
                    AND codlista = {1}
                    AND (fechaini <= '{2:yyyy-MM-dd}' AND fechafin >= '{2:yyyy-MM-dd}');
                ";
                sql = String.Format(sql, a.CodArtic, c.CodTarif, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIOAC");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                }
                conn.Close();
            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precion1 AS PRECIONU
                    FROM spromo
                    WHERE codartic = '{0}'
                    AND codlista = {1}
                    AND (fechain1 >= '{2:yyyy-MM-dd}' AND fechafi1 <= '{2:yyyy-MM-dd}');
                ";
                sql = String.Format(sql, a.CodArtic, c.CodTarif, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIONU");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                }
                conn.Close();
            }
            if (precio.Pvp != 0 && dtoPermi)
            {
                // calcular los descuentos
                precio = GetDescuento(a, c, precio);
            }
            precio = CalcularDescuento(precio);
            return precio;
        }

        public static Precio GetPrecioEspeciales(Articulo a, Cliente c)
        {
            Precio precio = new Precio();
            bool dtoPermi = false;
            precio.Origen = "ESPECIAL";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precioa1 AS PRECIOAC,
                    dtoespec AS DTOESPEC
                    FROM sprees
                    WHERE codclien = {0}
                    AND codartic = '{1}';
                ";
                sql = String.Format(sql, c.CodClien, a.CodArtic);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIOAC");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DTOESPEC")))
                        precio.Dto1 = rdr.GetDecimal("DTOESPEC");
                }
                conn.Close();
            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precionu AS PRECIONU,
                    dtoespe1 AS DTOESPE1
                    FROM sprees
                    WHERE codclien = {0}
                    AND codartic = '{1}'
                    AND (fechanue <= '{2:yyyy-MM-dd}');
                ";
                sql = String.Format(sql, c.CodClien, a.CodArtic, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIONU");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DTOESPE1")))
                        precio.Dto1 = rdr.GetDecimal("DTOESPE1");
                }
                conn.Close();
            }
            if (precio.Pvp != 0 && dtoPermi && precio.Dto1 == 0)
            {
                // calcular los descuentos
                precio = GetDescuento(a, c, precio);
            }
            precio = CalcularDescuento(precio);
            return precio;
        }

        public static Precio GetPrecioTarifas(Articulo a, Cliente c)
        {
            Precio precio = new Precio();
            bool dtoPermi = false;
            precio.Origen = "TARIFAS";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precioac AS PRECIOAC
                    FROM slista
                    WHERE codlista = {0}
                    AND codartic = '{1}';
                ";
                sql = String.Format(sql, c.CodTarif, a.CodArtic);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIOAC");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                }
                conn.Close();
            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    dtopermi AS DTOPERMI,
                    precionu AS PRECIONU
                    FROM slista
                    WHERE codlista = {0}
                    AND codartic = '{1}'
                    AND (fechanue <= '{2:yyyy-MM-dd}');
                ";
                sql = String.Format(sql, c.CodTarif, a.CodArtic, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    precio.Pvp = rdr.GetDecimal("PRECIONU");
                    dtoPermi = rdr.GetBoolean("DTOPERMI");
                }
                conn.Close();
            }
            if (precio.Pvp != 0 && dtoPermi)
            {
                // calcular los descuentos
                precio = GetDescuento(a, c, precio);
            }
            precio = CalcularDescuento(precio);
            return precio;
        }

        public static Precio GetPrecioArticulo(Articulo a, Cliente c)
        {
            Precio precio = new Precio();
            precio.Origen = "ARTICULO";
            precio.Pvp = a.Preciove;
            //precio = GetDescuento(a, c, precio);
            precio = CalcularDescuento(precio);
            return precio;
        }


        public static Precio CalcularDescuento(Precio p)
        {
            bool sobreResto = GetSobreResto();
            if (sobreResto)
            {
                p.Importe = p.Pvp - ((p.Pvp * p.Dto1) / 100M);
                p.Importe = p.Importe - ((p.Importe * p.Dto2) / 100M);
            }
            else
            {
                p.Importe = p.Pvp - ((p.Pvp * (p.Dto1+ p.Dto2)) / 100M);
            }
            return p;
        }

        public static Precio GetDescuento(Articulo a, Cliente c, Precio p)
        {
            // segun cliente
            p = GetDescuentoCFM(a, c, p);
            if (p.Dto1 > 0) return p;
            p = GetDescuentoCF(a, c, p);
            if (p.Dto1 > 0) return p;
            p = GetDescuentoCM(a, c, p);
            if (p.Dto1 > 0) return p;
            // segun actividad
            p = GetDescuentoAFM(a, c, p);
            if (p.Dto1 > 0) return p;
            p = GetDescuentoAF(a, c, p);
            if (p.Dto1 > 0) return p;
            p = GetDescuentoAM(a, c, p);
            if (p.Dto1 > 0) return p;
            return p;
        }

        public static Precio GetDescuentoCFM(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codclien = {0}
                    AND codfamia = {1}
                    AND codmarca = {2}
                    AND fechadto <= '{3:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodClien, a.CodFamia, a.CodMarca, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }

        public static Precio GetDescuentoCF(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codclien = {0}
                    AND codfamia = {1}
                    AND fechadto <= '{2:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodClien, a.CodFamia, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }

        public static Precio GetDescuentoCM(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codclien = {0}
                    AND codmarca = {1}
                    AND fechadto <= '{2:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodClien, a.CodMarca, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }

        public static Precio GetDescuentoAFM(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codactiv = {0}
                    AND codfamia = {1}
                    AND codmarca = {2}
                    AND fechadto <= '{3:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodActiv, a.CodFamia, a.CodMarca, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }
        
        public static Precio GetDescuentoAF(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codactiv = {0}
                    AND codfamia = {1}
                    AND fechadto <= '{2:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodActiv, a.CodFamia, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }

        public static Precio GetDescuentoAM(Articulo a, Cliente c, Precio p)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"
                    SELECT
                    dtoline1 AS DTOLINE1,
                    dtoline2 AS DTOLINE2
                    FROM sdtofm
                    WHERE codactiv = {0}
                    AND codmarca = {1}
                    AND fechadto <= '{2:yyyy-MM-dd}';
                ";
                sql = String.Format(sql, c.CodActiv, a.CodMarca, DateTime.Now);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    p.Dto1 = rdr.GetDecimal("DTOLINE1");
                    p.Dto2 = rdr.GetDecimal("DTOLINE2");
                }
                conn.Close();
            }
            return p;
        }

        #endregion 

    }
}  

