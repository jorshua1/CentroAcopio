using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CentroAcopio.Model;
using Oracle.ManagedDataAccess.Client;

namespace CentroAcopio.Views.Providers
{
    public partial class UpdateProvider : Window
    {
        public readonly ClaseConexion CrearConexion = new ClaseConexion();

        public UpdateProvider(object cedula)
        {
            InitializeComponent();
            ObtenerCiudades();
            GetInfoFromCedula(cedula);
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

        private void GetInfoFromCedula(object cedula)
        {
            TxtCedula.Text = (string)cedula;
            var conexion = CrearConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();

            // Usamos parámetros para evitar problemas de seguridad y concatenamos el valor de cedula
            cmd.CommandText =
                "SELECT NOM_NOMBRE, NOM_APELLIDO, DIRECCION, TELEFONO, COD_CIUDAD FROM PROVEEDOR WHERE CEDULA = :cedula";
            cmd.Parameters.Add("cedula", OracleDbType.Varchar2).Value = cedula.ToString();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var nombre = reader["NOM_NOMBRE"].ToString();
                var apellido = reader["NOM_APELLIDO"].ToString();
                var direccion = reader["DIRECCION"].ToString();
                var telefono = reader["TELEFONO"].ToString();
                var codCiudad = reader["COD_CIUDAD"].ToString();


                // Aquí puedes usar las variables nombre, apellido y dirección como desees
                // Por ejemplo, asignarlos a los TextBox correspondientes en tu interfaz
                TxtNombre.Text = nombre;
                TxtApellido.Text = apellido;
                TxtDireccion.Text = direccion;
                TxtTelefono.Text = telefono;
                // ComboBoxCiudades.SelectedItem = codCiudad;
            }

            reader.Close();
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
            if (ComboBoxCiudades.SelectedItem == null) return;
            var codigoSeleccionado = ComboBoxCiudades.SelectedValue.ToString();
            Console.WriteLine(codigoSeleccionado);
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
                        "UPDATE PROVEEDOR " +
                        "SET NOM_NOMBRE = :nombre, NOM_APELLIDO = :apellido, DIRECCION = :direccion, TELEFONO = :telefono, COD_CIUDAD = :codigoSeleccionado " +
                        "WHERE CEDULA = :cedula";
                    cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = nombre;
                    cmd.Parameters.Add("apellido", OracleDbType.Varchar2).Value = apellido;
                    cmd.Parameters.Add("direccion", OracleDbType.Varchar2).Value = direccion;
                    cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = telefono;
                    cmd.Parameters.Add("codigoSeleccionado", OracleDbType.Varchar2).Value = codigoSeleccionado;
                    cmd.Parameters.Add("cedula", OracleDbType.Varchar2).Value = cedula;
                    cmd.ExecuteNonQuery();

                    // Confirmar los cambios mediante COMMIT
                    cmd.CommandText = "COMMIT";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Proveedor actualizado correctamente.");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar proveedor: {ex.Message}");
            }
        }
    }
}