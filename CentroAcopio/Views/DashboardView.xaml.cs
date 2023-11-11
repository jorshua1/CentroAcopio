using System;
using CentroAcopio.Model;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CentroAcopio.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public Clase_Conexion crearConexion = new Clase_Conexion();

        public DashboardView()
        {
            InitializeComponent();
            actualizarDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var conexion = crearConexion.ConexionDB_Oracle();
            OracleCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT * FROM categoria";
            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Acceder a los valores de las columnas por nombre o índice
                string categoriaID = reader.GetString(reader.GetOrdinal("codigo")); // Acceder por nombre
                string nombre = reader.GetString(reader.GetOrdinal("nombre")); // Acceder por nombre
                // Acceder por nombre

                // Realizar operaciones con los valores
                // MessageBox.Show($"ID: {categoriaID}, Nombre: {nombre}");
                Console.WriteLine(nombre);
            }
            

        }

        private void actualizarDataGrid()
        {
            DataGrid dataGrid = new DataGrid();
            var conexion = crearConexion.ConexionDB_Oracle();
            OracleCommand cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT t.DESCRIPCION PRODUCTO, C2.NOMBRE CATEGORIA FROM TIPO t JOIN PROYECTOINTEGRADORJH.CATEGORIA C2 on t.COD_CATEGORIA = C2.CODIGO";
            //Creo un objeto de tipo OracleDataReader para leer los datos de la consulta
            OracleDataReader reader = cmd.ExecuteReader();
            //Creo un objeto de tipo DataTable para almacenar los datos de la consulta
            DataTable dt = new DataTable();
            //Cargo los datos de la consulta al objeto DataTable
            dt.Load(reader);
            //Asigno el objeto DataTable como origen de datos del DataGrid
            myDataGrid.ItemsSource = dt.DefaultView;
            reader.Close();
        }
        
        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            //que hace el codigo superior?

            if(dr != null)
            {
                txtCodigoCat.Text = dr["codigo"].ToString();
                txtNombreCat.Text = dr["nombre"].ToString();
                txtValorCat.Text = dr["valor"].ToString();
                btnAgregarCat.IsEnabled = false;
                btnActualizarCat.IsEnabled = true;
                btnEliminarCat.IsEnabled = true;


            }
        }
        
        private void btnAgregarCat_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO categoria(codigo,nombre,valor) VALUES(:CODIGO,:NOMBRE,:VALOR)";
            this.AUD(sql, 0);
            btnAgregarCat.IsEnabled = false;
            btnActualizarCat.IsEnabled = true;
            btnEliminarCat.IsEnabled = true;
        }
    }
}
