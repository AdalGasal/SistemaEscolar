using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Business.Services;
using SistemaEscolar.Shared.Dtos.Auth;

namespace SistemaEscolar.Presentation.Views.Auth;

public partial class FrmRegister : Form
{
    private readonly IAuthService _authService;
    public FrmRegister(IAuthService authService)
    {
        _authService = authService;
        InitializeComponent();
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        var login = new LoginDto
        {
            UserName = txtUsername.Text,
            Password = txtPassword.Text
        };

        await _authService.RegisterAsync(login);

    }
}
