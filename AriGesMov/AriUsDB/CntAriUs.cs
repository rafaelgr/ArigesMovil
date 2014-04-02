using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AriUsDB
{
    public static class CntAriUs
    {
        public static MySqlConnection GetConnection()
        {
            // leer la cadena de conexion del config
            var connectionString = ConfigurationManager.ConnectionStrings["Usuarios"].ConnectionString;
            // crear la conexion y devolverla.
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        #region Usuario
        public static Usuario GetUsuario(MySqlDataReader rdr)
        {
            if (rdr.IsDBNull(rdr.GetOrdinal("CODUSU"))) return null;
            Usuario u = new Usuario();
            u.CodUsu = rdr.GetInt32("CODUSU");
            u.NomUsu = rdr.GetString("NOMUSU");
            u.Login = rdr.GetString("LOGIN");
            u.PasswordPropio = rdr.GetString("PASSWORD_PROPIO");
            return u;
        }

        public static Usuario GetUsuario(string login, string password)
        {
            Usuario u = null;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                string sql = @"SELECT 
                    codusu AS CODUSU, 
                    nomusu AS NOMUSU, 
                    login AS LOGIN, 
                    passwordpropio AS PASSWORD_PROPIO
                    FROM usuarios
                    WHERE login = '{0}'
                    AND passwordpropio = '{1}'";
                sql = String.Format(sql, login, password);
                cmd.CommandText = sql;
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    u = GetUsuario(rdr);
                }
                conn.Close();
            }
            return u;
        }
        #endregion
    }
}
