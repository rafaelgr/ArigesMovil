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
            if (!rdr.IsDBNull(rdr.GetOrdinal("CODAGENT"))) return null;
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
            if (!rdr.IsDBNull(rdr.GetOrdinal("CODTRABA"))) return null;
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
    }
}  

