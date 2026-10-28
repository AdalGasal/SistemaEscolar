
using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Presentation.Navigation;
using SistemaEscolar.Presentation.Views.Admin;
using SistemaEscolar.Shared.Dtos.Auth;

namespace SistemaEscolar.Presentation.Views.Auth
{
    public partial class FrmLogin : Form
    {
        private readonly IAuthService _authService;
        private readonly INavigationService _navService;

        public FrmLogin(IAuthService authService, INavigationService navService)
        {
            _authService = authService;
            _navService = navService;

            InitializeComponent();
            ViewStyles.BorderRadius(grpBoxLogin, 10);
            ViewStyles.BorderRadius(btnLogin, 10);
            ViewStyles.BorderRadius(txtUsername, 10);
            ViewStyles.BorderRadius(txtPassword, 10);
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Attempts to authenticate the user asynchronously using the provided credentials.
        /// </summary>
        /// <returns>True si el login se completa correctamnente. De otro modo, false.</returns>
        private async Task<bool> LoginAsync()
        {
            // Crear una instancia de login para backend
            var login = new LoginDto
            {
                UserName = txtUsername.Text,
                Password = txtPassword.Text
            };

            // Await para esperar que se haga el login y continuar
            var result = await _authService.LoginAsync(login);

            if (result)
                _navService.Replace<FrmAdminMain>();

            return result;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var result = await LoginAsync();
            DialogResult = result ? DialogResult.OK : DialogResult.No;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Hide();

            var frmRegister = new FrmRegister(_authService);
            frmRegister.ShowDialog();
            // Detenido hasta cerrar frmRegister

            Show();
        }
    }
}