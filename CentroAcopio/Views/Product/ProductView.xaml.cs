using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using CentroAcopio.Model;
using CentroAcopio.Views.Product;

namespace CentroAcopio.Views
{
    public partial class ProductView : UserControl
    {
        public ClaseConexion crearConexion = new ClaseConexion();

        public ProductView()
        {
            InitializeComponent();
            ActualizarDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var conexion = crearConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM categoria";
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Acceder a los valores de las columnas por nombre o índice
                var categoriaID = reader.GetString(reader.GetOrdinal("codigo")); // Acceder por nombre
                var nombre = reader.GetString(reader.GetOrdinal("nombre")); // Acceder por nombre
                // Acceder por nombre

                // Realizar operaciones con los valores
                // MessageBox.Show($"ID: {categoriaID}, Nombre: {nombre}");
                Console.WriteLine(nombre);
            }
        }

        private void ActualizarDataGrid()
        {
            var dataGrid = new DataGrid();
            var conexion = crearConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                "SELECT * FROM V_PRODUCTOS";
            //Creo un objeto de tipo OracleDataReader para leer los datos de la consulta
            var reader = cmd.ExecuteReader();
            //Creo un objeto de tipo DataTable para almacenar los datos de la consulta
            var dt = new DataTable();
            //Cargo los datos de la consulta al objeto DataTable
            dt.Load(reader);
            //Asigno el objeto DataTable como origen de datos del DataGrid
            myDataGrid.ItemsSource = dt.DefaultView;
            reader.Close();
        }

        //
        // private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        //
        //     DataGrid dg = sender as DataGrid;
        //     DataRowView dr = dg.SelectedItem as DataRowView;
        //     //que hace el codigo superior?
        //
        //     if(dr != null)
        //     {
        //         txtCodigoCat.Text = dr["codigo"].ToString();
        //         txtNombreCat.Text = dr["nombre"].ToString();
        //         txtValorCat.Text = dr["valor"].ToString();
        //         btnAgregarCat.IsEnabled = false;
        //         btnActualizarCat.IsEnabled = true;
        //         btnEliminarCat.IsEnabled = true;
        //
        //
        //     }
        // }
        //
        // private void btnAgregarCat_Click(object sender, RoutedEventArgs e)
        // {
        //     string sql = "INSERT INTO categoria(codigo,nombre,valor) VALUES(:CODIGO,:NOMBRE,:VALOR)";
        //     this.AUD(sql, 0);
        //     btnAgregarCat.IsEnabled = false;
        // }
        private void SeeProducts_OnClick(object sender, RoutedEventArgs e)
        {
            var createProduct = new CreateProduct();
            createProduct.Show();
        }
    }
}