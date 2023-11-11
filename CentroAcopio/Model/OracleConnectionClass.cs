using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Model
{
    public class Clase_Conexion
    {
        string UsuarioDB = "proyectointegradorjh";
        string ContraseñaDB = "proyectointegradorjh";
        string DataSource = "localhost";

        public OracleConnection ConexionDB_Oracle()
        {

            OracleConnection con = new OracleConnection("Data Source=" + DataSource + "; User Id=" + UsuarioDB + "; Password=" + ContraseñaDB + ";");
            //Obtengo la cadena de conexion del archivo App.config
            //String cadenaConexion = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;
            try
            {
                //Abro la conexion a la base de datos
                con.Open();
                //Si la conexion fue exitosa muestro un mensaje de conexion exitosa
                MessageBox.Show("Conexion exitosa");
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
