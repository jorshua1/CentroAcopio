using System;
using System.Windows;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Model
{
    public class ClaseConexion
    {
        private const string ContraseñaDb = "proyectointegradorjh";
        private const string DataSource = "localhost";
        private const string UsuarioDb = "proyectointegradorjh";

        public OracleConnection ConexionDB_Oracle()
        {
            var con = new OracleConnection("Data Source=" + DataSource + "; User Id=" + UsuarioDb +
                                           "; Password=" + ContraseñaDb + ";");
            //Obtengo la cadena de conexion del archivo App.config
            //String cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;
            try
            {
                //Abro la conexion a la base de datos
                con.Open();
                //Si la conexion fue exitosa muestro un mensaje de conexion exitosa
                Console.WriteLine(@"Conexion exitosa");
            }
            catch (Exception ex)
            {
                //Si la conexion no fue exitosa muestro un mensaje de error
                MessageBox.Show("Error al conectar" + ex.Message);
            }

            return con;
        }
    }
}