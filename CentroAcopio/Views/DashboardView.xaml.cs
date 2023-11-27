using System.Windows;
using System.Windows.Input;
using CentroAcopio.Views.Client;
using CentroAcopio.Views.Distributor;
using CentroAcopio.Views.Providers;

namespace CentroAcopio.Views
{
    public partial class DashboardView
    {
        public DashboardView()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            // Hago que la ventana se ponga en modo pantalla completa
            WindowState = WindowState.Maximized;
        }

        private void OnDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(this); // Obtener la ventana que contiene el UserControl

            if (window != null) window.WindowState = WindowState.Minimized; // Minimizar la ventana
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnProductoView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new ProductView();
            // throw new System.NotImplementedException();
        }

        private void BtnInventarioView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new PruebaDos();
        }

        private void BtnProveedorView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new ProviderView();
        }

        private void Logo_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            contenedor.Content = new DefaultDashboarView();
        }

        private void BtnClienteView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new ClientView();
        }

        private void BtnDistribuidoresView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new DistributorView();
        }
    }
}