using System.Windows;
using System.Windows.Controls;

namespace CentroAcopio.Views
{
    /// <summary>
    ///     Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this); // Obtener la ventana que contiene el UserControl

            if (window != null) window.WindowState = WindowState.Minimized; // Minimizar la ventana
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            // string usuario = txtUsuario.Text;
            // string contrasena = txtContrasena.Password.ToString();

            var usuario = "admin";
            var contrasena = "admin";
            if (usuario == "admin" && contrasena == "admin")
            {
                var dashboardView = new DashboardView();
                dashboardView.Show();

                if (window != null) window.Close();
            }
            else
            {
                MessageBox.Show("Los datos ingresados son incorrectos, intentelo de nuevo");
                txtContrasena.Clear();
                txtUsuario.Text = "";
            }
        }
    }
}