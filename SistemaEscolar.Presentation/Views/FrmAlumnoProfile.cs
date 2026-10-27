using SistemaEscolar.Business.Interfaces;
using SistemaEscolar.Shared.Models;

namespace SistemaEscolar.Presentation.Views;

public partial class FrmAlumnoProfile : Form
{
    #region Properties
    private readonly IAlumnoService _alumnoService;
    private const int _borderRadius = 10;

    private Alumno? _currentAlumno = null;
    public Alumno? CurrentAlumno
    {
        get => _currentAlumno;
        set => SetAlumno(value);
    }
    #endregion

    #region Form

    public FrmAlumnoProfile(IAlumnoService alumnoService)
    {
        _alumnoService = alumnoService;

        InitializeComponent();
        ViewStyles.BorderRadius(grpBoxPersonalData, _borderRadius);
        ViewStyles.BorderRadius(grpBoxDetail, _borderRadius);
        ViewStyles.BorderRadius(btnSave, _borderRadius);

        ViewStyles.BorderRadius(txtName, _borderRadius);
        ViewStyles.BorderRadius(txtLastName, _borderRadius);
        ViewStyles.BorderRadius(txtMaternalName, _borderRadius);
        ViewStyles.BorderRadius(numCode, _borderRadius);
        ViewStyles.BorderRadius(dtpBirthday, _borderRadius);
        ViewStyles.BorderRadius(dtpStartDate, _borderRadius);
        ViewStyles.BorderRadius(cboSex, _borderRadius);
    }

    private async void FrmAlumnoProfile_Load(object sender, EventArgs e)
    {
        GetSexs();

        // Información de prueba para mostrar en el formulario
        //_currentAlumno = new Alumno
        //{
        //    Nombre = "Juan",
        //    ApellidoPaterno = "Pérez",
        //    ApellidoMaterno = "García",
        //    Matricula = "123456",
        //    FechaNacimiento = new DateTime(2000, 1, 1),
        //    FechaIngreso = new DateTime(2018, 9, 1),
        //    Sexo = 'M',
        //};

        // Existe información de alumno cargada
        await GetAlumnoById(44);
    }
    #endregion

    private void GetSexs()
    {
        var listSexs = new[]
        {
            new { Value = 'M', Text = "Masculino" },
            new { Value = 'F', Text = "Femenino" },
            new { Value = 'I', Text = "Indefinido" }
        };

        cboSex.DataSource = listSexs;
        cboSex.DisplayMember = "Text";
        cboSex.ValueMember = "Value";
    }

    #region Alumnos
    private void SetAlumno(Alumno? alm)
    {
        // Opción de asignación por referencia (no recomendable si se desea mantener la integridad de los datos)
        _currentAlumno = alm;

        // Opción de asignación por copia (recomendable para mantener la integridad de los datos)
        //_currentAlumno = new Alumno
        //{
        //    Nombre = alm.Nombre,
        //    ApellidoPaterno = alm.ApellidoPaterno,
        //    ApellidoMaterno = alm.ApellidoMaterno,
        //    Matricula = alm.Matricula,
        //    FechaNacimiento = alm.FechaNacimiento,
        //    FechaIngreso = alm.FechaIngreso,
        //    Sexo = alm.Sexo,
        //};
        if (_currentAlumno != null)
        {
            txtName.Text = _currentAlumno.Nombre;
            txtLastName.Text = _currentAlumno.ApellidoPaterno;
            txtMaternalName.Text = _currentAlumno.ApellidoMaterno;
            numCode.Value = decimal.Parse(_currentAlumno.Matricula);
            dtpBirthday.Value = _currentAlumno.FechaNacimiento;
            dtpStartDate.Value = _currentAlumno.FechaIngreso;
            cboSex.SelectedValue = _currentAlumno.Sexo;
        }
    }

    private async Task GetAlumnoById(int id)
    {
        var alumno = await _alumnoService.GetByIdAsync(id);
        if (alumno == null)
        {
            MessageBox.Show("No se encontró el alumno con el ID proporcionado.");
            return;
        }
        CurrentAlumno = alumno;
    }

    private bool ValidateSave()
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("El nombre es obligatorio.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("El apellido paterno es obligatorio.");
            return false;
        }

        if (numCode.Value <= 0)
        {
            MessageBox.Show("La matrícula es obligatoria.");
            return false;
        }

        return true;
    }

    private async Task Save()
    {
        var alumno = new Alumno
        {
            Nombre = txtName.Text,
            ApellidoPaterno = txtLastName.Text,
            ApellidoMaterno = txtMaternalName.Text,
            Matricula = numCode.Value.ToString(),
            FechaNacimiento = dtpBirthday.Value,
            FechaIngreso = dtpStartDate.Value,
            Sexo = cboSex.SelectedValue?.ToString()?[0] ?? 'I',
            Estatus = true
        };

        await _alumnoService.AddAsync(alumno);


        MessageBox.Show("Perfil guardado correctamente.");
    }

    #endregion

    #region Events
    private async void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateSave())
        {
            await Save();
        }
    }



    #endregion
}
