using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CentroAcopio.Model;
using CentroAcopio.Views.Providers;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Views.Client
{
    public partial class CreateClient : Window
    {
        public readonly ClaseConexion CrearConexion = new ClaseConexion();

        public CreateClient()
        {
            InitializeComponent();
            ObtenerCiudades();
        }

        private void ObtenerCiudades()
        {
            try
            {
                var conexion = CrearConexion.ConexionDB_Oracle();
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "select codigo, nombre from PROYECTOINTEGRADORJH.CIUDAD";
                var reader = cmd.ExecuteReader();
                List<Ciudad> ciudades = new List<Ciudad>();
                while (reader.Read())
                {
                    ciudades.Add(new Ciudad
                    {
                        Codigo = reader["CODIGO"].ToString(),
                        Nombre = reader["NOMBRE"].ToString()
                    });
                }

                // Asignar las ciudades como origen de datos del ComboBox
                ComboBoxCiudades.ItemsSource = ciudades;
                ComboBoxCiudades.DisplayMemberPath = "Nombre";
                ComboBoxCiudades.SelectedValuePath = "Codigo";
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar las ciudades: " + e.Message);
                throw;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            var cedula = TxtCedula.Text;
            var nombre = TxtNombre.Text;
            var apellido = TxtApellido.Text;
            var direccion = TxtDireccion.Text;
            var telefono = TxtTelefono.Text;
            var nombreComercial = TxtNombreComercial.Text;

            if (ComboBoxCiudades.SelectedItem == null) return;
            var codigoSeleccionado = ComboBoxCiudades.SelectedValue.ToString();

            Console.WriteLine(
                $@"{nombre} {apellido} {cedula} {direccion} {telefono} {nombreComercial} {codigoSeleccionado} ");

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
                        "INSERT INTO CLIENTE (CEDULA, NOM_NOMBRE, NOM_APELLIDO, DIRECCION, TELEFONO, NOMBRECOMERCIAL, COD_CIUDAD) " +
                        "VALUES (:cedula, :nombre, :apellido, :direccion, :telefono, :nombreComercial, :codigoSeleccionado)";

                    cmd.Parameters.Add("cedula", OracleDbType.Varchar2).Value = cedula;
                    cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = nombre;
                    cmd.Parameters.Add("apellido", OracleDbType.Varchar2).Value = apellido;
                    cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = direccion;
                    cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = telefono;
                    cmd.Parameters.Add("nombreComercial", OracleDbType.Varchar2).Value = nombreComercial;
                    cmd.Parameters.Add("codigoSeleccionado", OracleDbType.Varchar2).Value = codigoSeleccionado;

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