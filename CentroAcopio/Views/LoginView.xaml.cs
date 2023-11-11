using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            // string usuario = txtUsuario.Text;
            // string contrasena = txtContrasena.Password.ToString();

            string usuario = "admin";
            string contrasena = "admin";
            if (usuario == "admin" && contrasena == "admin")
            {
                DashboardView dashboard = new DashboardView();
                dashboard.Show();

                if (window != null)
                {
                    window.Close();
                }
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
