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
                    faxclie2 AS FAXCLIE2
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
                    faxclie2 AS FAXCLIE2
                    FROM sclien
                    WHERE nomclien LIKE '%{0}%'";
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
                    faxclie2 AS FAXCLIE2
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
                            <h4 class='panel-title'>
                                <a data-toggle='collapse' data-parent='#accordion' href='#collapse{0}'>
                                    {1}
                                </a>
                            </h4>
                        </div>
                        <div id='collapse{0}' class='panel-collapse collapse'>
                            <div class='panel-body'>
                                <div class='container'>
                                    <div class='col-md-3'>
                                        <strong>NIF:</strong>
                                        <p>{2}</p>
                                    </div>
                                    <div class='col-md-9'>
                                        <strong>Nombre comercial:</strong>
                                        <p>{3}</p>
                                    </div>
                                </div>
                                <div class='container'>
                                    <div class='col-md-3'>
                                        <strong>Persona contacto:</strong>
                                        <p>{4}</p>
                                    </div>
                                    <div class='col-md-3'>
                                        <strong>Teléfono (1):</strong>
                                        <p>{5}</p>
                                    </div>
                                    <div class='col-md-3'>
                                        <strong>Teléfono (2):</strong>
                                        <p>{6}</p>
                                    </div>
                                    <div class='col-md-3'>
                                        <a class='btn btn-primary btn-lg text-center' href='ClienteDetalle.aspx?CodClien={0}'>Ver detalles</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ";
                subpanel += String.Format(panel, c.CodClien, c.NomClien, c.NifClien, c.NomComer, c.PerClie1, c.TelClie1, c.TelClie2);
            }
            html = String.Format(panelGroup, subpanel);
            return html;
        }
        #endregion

    }
}  

