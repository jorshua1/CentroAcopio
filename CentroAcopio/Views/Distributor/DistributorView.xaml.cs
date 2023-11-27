using System.Data;
using System.Windows;
using System.Windows.Controls;
using CentroAcopio.Model;

namespace CentroAcopio.Views.Distributor
{
    public partial class DistributorView
    {
        private readonly ClaseConexion _crearConConexion = new ClaseConexion();

        public DistributorView()
        {
            InitializeComponent();
            ActualizarDataGrid();
        }

        private void ActualizarDataGrid()
        {
            var conexion = _crearConConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                "SELECT * FROM V_DISTRIBUIDORES";
            var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            DistribuidorDataGrid.ItemsSource = dt.DefaultView; // Esta línea vincula el DataTable con el DataGrid
            reader.Close();
        }

        private void CreateDistribuidor_OnClick(object sender, RoutedEventArgs e)
        {
            var createDistribuidor = new CreateDistributorView();
            // creo una funcion lambda (+=) que se suscribe al evento closed en la ventana createProvider y actualizo el datagrid
            createDistribuidor.Closed += (ss, ee) => { ActualizarDataGrid(); };
            createDistribuidor.Show();
        }

        private void UpdateDistribuidor_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DeleteDistribuidor_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}