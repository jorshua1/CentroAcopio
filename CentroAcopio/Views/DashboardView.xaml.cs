using System.Windows;

namespace CentroAcopio.Views
{
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            // Hago que la ventana se ponga en modo pantalla completa
            this.WindowState = WindowState.Maximized;
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this); // Obtener la ventana que contiene el UserControl

            if (window != null)
            {
                window.WindowState = WindowState.Minimized; // Minimizar la ventana
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnProductoView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new VistaPruebaContent();
            // throw new System.NotImplementedException();
        }

        private void BtnInventarioView_OnClick(object sender, RoutedEventArgs e)
        {
            contenedor.Content = new PruebaDos();
        }
}
}