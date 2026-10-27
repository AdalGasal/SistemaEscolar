using SistemaEscolar.Business.Interfaces;

namespace SistemaEscolar.Presentation
{
    public partial class Form1 : Form
    {
        private readonly IAlumnoService _alumnoService;

        public Form1(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var alumnos = await _alumnoService.GetAllAsync();

            var alumnosStr = string.Join(Environment.NewLine, alumnos.Select(a => $"{a.Id}: {a.Nombre} {a.ApellidoPaterno}"));

            MessageBox.Show(alumnosStr, "Lista de Alumnos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
