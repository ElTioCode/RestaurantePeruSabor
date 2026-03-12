using System.Windows;
using System.Windows.Input;
using RestaurantePeruSabor.Models;

namespace RestaurantePeruSabor.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            txtClave.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                    BtnIngresar_Click(s, e);
            };
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Password.Trim();

            var admin = new Usuario("admin", "1234", Usuario.Rol.ADMINISTRADOR);
            var cajero = new Usuario("cajero", "0000", Usuario.Rol.CAJERO);

            Usuario usuarioLogueado = null;

            if (admin.NombreUsuario == usuario && admin.Autenticar(clave))
                usuarioLogueado = admin;
            else if (cajero.NombreUsuario == usuario && cajero.Autenticar(clave))
                usuarioLogueado = cajero;

            if (usuarioLogueado != null)
            {
                var menu = new MenuPrincipalWindow(usuarioLogueado);
                menu.Show();
                this.Close();
            }
            else
            {
                lblMensaje.Text = "Usuario o contrasena incorrectos";
                txtClave.Clear();
                txtUsuario.Focus();
            }
        }
    }
}