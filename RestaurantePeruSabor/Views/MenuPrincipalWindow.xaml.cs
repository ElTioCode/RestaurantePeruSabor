using System.Windows;
using RestaurantePeruSabor.Models;
using RestaurantePeruSabor.Views;

namespace RestaurantePeruSabor.Views
{
    public partial class MenuPrincipalWindow : Window
    {
        private Usuario usuario;

        public MenuPrincipalWindow(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;

            lblBienvenida.Text = "Bienvenido, " + usuario.NombreUsuario.ToUpper()
                                 + "  |  Rol: " + usuario.RolUsuario.ToString();

            if (usuario.RolUsuario == Usuario.Rol.ADMINISTRADOR)
                btnReporte.Visibility = Visibility.Visible;
        }

        private void BtnAtender_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new RecepcionWindow(usuario);
            ventana.Show();
            this.Hide();
        }

        private void BtnReporte_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new ReporteWindow(usuario);
            ventana.Show();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}