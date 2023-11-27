using System;
using System.Data;
using System.Windows;
using CentroAcopio.Model;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Views.Providers
{
    public partial class ProviderView
    {
        private readonly ClaseConexion _crearConConexion = new ClaseConexion();

        public ProviderView()
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
                "SELECT * FROM V_PROVEEDORES";
            var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            ProviderDataGrid.ItemsSource = dt.DefaultView; // Esta línea vincula el DataTable con el DataGrid
            reader.Close();
        }

        private void CreateProvider_OnClick(object sender, RoutedEventArgs e)
        {
            var createProvider = new CreateProviderView();
            // creo una funcion lambda (+=) que se suscribe al evento closed en la ventana createProvider y actualizo el datagrid
            createProvider.Closed += (ss, ee) => { ActualizarDataGrid(); };
            createProvider.Show();
        }

        private void UpdateProvider_OnClick(object sender, RoutedEventArgs e)
        {
            if (ProviderDataGrid.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)ProviderDataGrid.SelectedItem;
                var cedula = filaSeleccionada["CEDULA"];

                UpdateProvider updateProvider = new UpdateProvider(cedula);
                updateProvider.Closed += (ss, ee) => { ActualizarDataGrid(); };
                updateProvider.Show();
            }else
            {
                MessageBox.Show("Por favor, seleccione un registro para modificarlo.");
            }
        }

        private void DeleteProvider_OnClick(object sender, EventArgs e)
        {
            if (ProviderDataGrid.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)ProviderDataGrid.SelectedItem;
                var cedula = (string)filaSeleccionada["CEDULA"];

                // Lógica para eliminar el registro en la base de datos usando la cedula seleccionada
                var conexion = _crearConConexion.ConexionDB_Oracle();
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"DELETE FROM PROVEEDOR WHERE CEDULA = :cedula";
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