using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaEscolar.Shared.Models;

public class Materia
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Creditos { get; set; }
    public int Semestre { get; set; }
    public int Horas { get; set; }
    public bool Estatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
