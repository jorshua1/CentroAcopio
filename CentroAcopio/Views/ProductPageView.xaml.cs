using System.Windows;

namespace CentroAcopio.Views
{
    public partial class ProductPageView : Window
    {
        public ProductPageView()
        {
            InitializeComponent();
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
    }
}