using System.Data;
using CentroAcopio.Model;

namespace CentroAcopio.Views.Product
{
    public partial class CreateProduct
    {
        public readonly ClaseConexion CrearConexion = new ClaseConexion();

        public CreateProduct()
        {
            InitializeComponent();
            ActualizarDataGrid();
        }

        private void ActualizarDataGrid()
        {
            var conexion = CrearConexion.ConexionDB_Oracle();
            var cmd = conexion.CreateCommand();
            cmd.CommandText = "ALTER SESSION SET CURRENT_SCHEMA = proyectointegradorjh";
            cmd.ExecuteNonQuery();
            cmd.CommandText =
                "SELECT t.DESCRIPCION AS PRODUCTO, C2.NOMBRE AS CATEGORIA FROM TIPO t JOIN PROYECTOINTEGRADORJH.CATEGORIA C2 ON t.COD_CATEGORIA = C2.CODIGO";
            var reader = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            MyDataGrid.ItemsSource = dt.DefaultView; // Esta línea vincula el DataTable con el DataGrid
            reader.Close();
        }
    }
}