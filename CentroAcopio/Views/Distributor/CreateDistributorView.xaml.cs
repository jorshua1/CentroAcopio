using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CentroAcopio.Model;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Views.Distributor
{
    public partial class CreateDistributorView : Window
    {
        public readonly ClaseConexion CrearConexion = new ClaseConexion();

        public CreateDistributorView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            var id = TxtId.Text;
            var nombre = TxtNombre.Text;
            var placa = TxtPlaca.Text;

            try
            {
                using (var conexion = CrearConexion.ConexionDB_Oracle())
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();

                    var cmd = conexion.CreateCommand();
                    cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "INSERT INTO DISTRIBUIDOR (ID, NOMBRE, PLACA) " +
                        "VALUES (:id, :nombre, :placa)";

                    cmd.Parameters.Add("id", OracleDbType.Varchar2).Value = id;
                    cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = nombre;
                    cmd.Parameters.Add("placa", OracleDbType.Varchar2).Value = placa;
                   
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Datos insertados correctamente.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al insertar datos: {ex.Message}");
            }
        }
    }
}