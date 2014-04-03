using System.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Agente u = null;
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
                    u = GetAgente(rdr);
                }
                conn.Close();
            }
            return u;
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
                c.FaxClie2 = rdr.GetString("MAICLIE1");
            if (!rdr.IsDBNull(rdr.GetOrdinal("MAICLIE2")))
                c.FaxClie2 = rdr.GetString("MAICLIE2");
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
                    maiclie2 AS MAICLIE2
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
                    maiclie2 AS MAICLIE2
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
                    maiclie2 AS MAICLIE2
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
                                        <div class='col-md-4'>
                                            <h4>{1}</h4>
                                        </div>
                                        <div class='col-md-1'>
                                            <h4>C:{0}</h4>
                                        </div>
                                        <div class='col-md-5'>

                                        </div>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div id='collapse{0}' class='panel-collapse collapse'>
                            <div class='panel-body'>
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
                                        <div class='col-sm-4'>
                                            <blockquote>
                                                Contacto (1): <em>{8}</em><br />
                                                Teléfono:
                                                <a href='tel:{9}'>{9}</a><br />
                                                Email:
                                                <a href='mailto:{10}'>{10}</a>
                                            </blockquote>
                                        </div>
                                        <div class='col-sm-4'>
                                            <blockquote>
                                                Contacto (2): <em>{11}</em><br />
                                                Teléfono:
                                                <a href='tel:{12}'>{12}</a><br />
                                                EMail:
                                                <a href='mailto:{13}'>{13}</a>
                                            </blockquote>
                                        </div>
                                        <div class='col-sm-4'>
                                            <a class='btn btn-primary btn-lg text-center' href='ClientesDetalle.aspx?CodClien={0}'>Ver detalles</a>
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

        public static string GetClienteHtml(Cliente cliente)
        {
            string html = "";
            if (cliente == null) return html;
            string plantilla = @"
                <div class='container'>
                    <div class='row'>
                        <div class='col-md-2'>
                            <strong>NIF</strong>
                            <p>{0}</p>
                        </div>
                        <div class='col-md-10'>
                            <strong>Nombre comercial</strong>
                            <p>{1}</p>
                        </div>
                    </div>
                    <div class='row'>
                        <div class='col-md-12'>
                            <strong>Domicilio</strong>
                            <p>{2}</p>
                        </div>
                    </div>
                    <div class='row'>
                        <div class='col-md-2'>
                            <strong>Cod. Postal</strong>
                            <p>{3}</p>
                        </div>
                        <div class='col-md-5'>
                            <strong>Población</strong>
                            <p>{4}</p>
                        </div>
                        <div class='col-md-5'>
                            <strong>Provincia</strong>
                            <p>{5}</p>
                        </div>
                    </div>
                    <div class='row'>
                        <div class='col-md-6'>
                            <div class='panel panel-default'>
                                <div class='panel-heading'>Datos de contacto (1)</div>
                                <div class='panel-body'>
                                    <strong>Persona contacto</strong>
                                    <p>{6}</p>
                                    <strong>Telefono:</strong>
                                    <a href='tel:{7}'>{7}</a>
                                    <strong>Correo:</strong>
                                    <a href='mailto:{8}'>{8}</a>
                                </div>
                            </div>
                        </div>
                        <div class='col-md-6'>
                            <div class='panel panel-default'>
                                <div class='panel-heading'>Datos de contacto (2)</div>
                                <div class='panel-body'>
                                    <strong>Persona contacto</strong>
                                    <p>{9}</p>
                                    <strong>Telefono:</strong>
                                    <a href='tel:{10}'>{10}</a>
                                    <strong>Correo:</strong>
                                    <a href='mailto:{11}'>{11}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ";
            html = String.Format(plantilla, cliente.NifClien, cliente.NomComer,
                cliente.DomClien, cliente.CodPobla, cliente.PobClien, cliente.ProClien,
                cliente.PerClie1, cliente.TelClie1, cliente.Maiclie1,
                cliente.PerClie2, cliente.TelClie2, cliente.Maiclie2);
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
                        <h4>Pedido {0}  de fecha {1:dd/MM/yyyy} por {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Cantidad</th>
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
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{3:##0.00}</td>
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
                        <h4>Oferta {0} de {1:dd/MM/yyyy} # {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Cantidad</th>
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
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{3:##0.00}</td>
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
                        <h4>Albaran {2}-{0}  de fecha {1:dd/MM/yyyy} por {3:#,###,##0.00 €}</h4>
                    </a>
                </div>
                <div id='collapse{0}' class='panel-collapse collapse'>
                    <div class='panel-body'>
                        <table class='table table-bordered'>
                            <tr>
                                <th>Linea</th>
                                <th>Artículo</th>
                                <th class='text-right'>Precio</th>
                                <th class='text-right'>Cantidad</th>
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
                <td class='text-right'>{2:###,##0.00}</td>
                <td class='text-right'>{3:##0.00}</td>
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
            html = String.Format(plantilla, a.NumAlbar, a.FechaAlb, a.CodTipom, a.TotalAlb, lineas);
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



    }
}  

