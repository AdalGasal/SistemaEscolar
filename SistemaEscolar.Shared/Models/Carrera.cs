using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaEscolar.Shared.Models;

public class Carrera
{
    public int Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int Duracion { get; set; }
    public bool Estatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
