namespace SistemaEscolar.Shared.Models;

public class Alumno
{
    public int Id { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public DateTime FechaIngreso { get; set; }
    public char Sexo { get; set; }
    public bool Estatus { get; set; }
}
