using System.Data;
using System.Windows;
using CentroAcopio.Model;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Views.Client
{
    public partial class ClientView
    {
        private readonly ClaseConexion _crearConConexion = new ClaseConexion();

        public ClientView()
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
                "SELECT * FROM V_CLIENTES";
            var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            ClientDataGrid.ItemsSource = dt.DefaultView; // Esta línea vincula el DataTable con el DataGrid
            reader.Close();
        }

        private void CreateClient_OnClick(object sender, RoutedEventArgs e)
        {
            var createClient = new CreateClient();
            // creo una funcion lambda (+=) que se suscribe al evento closed en la ventana createProvider y actualizo el datagrid
            createClient.Closed += (ss, ee) => { ActualizarDataGrid(); };
            createClient.Show();
        }

        private void UpdateClient_OnClick(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)ClientDataGrid.SelectedItem;
                var cedula = filaSeleccionada["CEDULA"];

                UpdateClient updateClient = new UpdateClient(cedula);
                updateClient.Closed += (ss, ee) => { ActualizarDataGrid(); };
                updateClient.Show();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro para modificarlo.");
            }
        }

        private void DeleteClient_OnClick(object sender, RoutedEventArgs e)
        {
            if (ClientDataGrid.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)ClientDataGrid.SelectedItem;
                var cedula = (string)filaSeleccionada["CEDULA"];

                // Lógica para eliminar el registro en la base de datos usando la cedula seleccionada
                var conexion = _crearConConexion.ConexionDB_Oracle();
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DELETE FROM CLIENTE WHERE CEDULA = :cedula";
                cmd.Parameters.Add("cedula", OracleDbType.Varchar2).Value = cedula;

                cmd.ExecuteNonQuery();

                ActualizarDataGrid(); // Actualizar el DataGridView después de eliminar el registro
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro para eliminar.");
            }
        }
    }
}