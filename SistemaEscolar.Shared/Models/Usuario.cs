namespace SistemaEscolar.Shared.Models;

public class Usuario
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Estatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
