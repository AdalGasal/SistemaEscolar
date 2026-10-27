using SistemaEscolar.Business.Interfaces;

namespace SistemaEscolar.Presentation.Views.Admin;

public partial class FrmAdminMain : Form
{
    private readonly ICarreraService _carreraService;

    public FrmAdminMain(ICarreraService carreraService)
    {
        _carreraService = carreraService;
        InitializeComponent();
    }

    private async void FrmAdminMain_Load(object sender, EventArgs e)
    {
        // Obtener total de carreras activas
        var totalCarreras = await _carreraService.GetCountActiveAsync();
        lblTotalCarreas.Text = totalCarreras.ToString();
    }
}
