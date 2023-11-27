using System.Windows;
using System.Windows.Controls;
using CentroAcopio.Model;

namespace CentroAcopio.Views
{
    public partial class DefaultDashboarView : UserControl
    {
        public readonly ClaseConexion crearConexion = new ClaseConexion();

        public DefaultDashboarView()
        {
            InitializeComponent();
            ViewCantidadProductos();
        }

        private void BtnCreateClient_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ViewCantidadProductos()
        {
            var conexion = crearConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM PROYECTOINTEGRADORJH.V_CANTIDAD_PRODUCTOS";
            var resultado = cmd.ExecuteScalar();
            if (resultado != null)
            {
                // Convertir el resultado a string (asumiendo que es un string)
                string cantidadProductos = resultado.ToString();

                // Asignar el texto al TextBlock
                CantidadProductos.Text = cantidadProductos;
            }
        }
    }
}