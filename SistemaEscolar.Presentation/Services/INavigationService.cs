using Microsoft.Extensions.DependencyInjection;

namespace SistemaEscolar.Presentation.Navigation;

/// <summary>
/// Contrato que implementan los formularios modales que devuelven un valor.
/// </summary>
public interface IModalResult<out TResult>
{
    TResult? Result { get; }
}

/// <summary>
/// Servicio de navegación. Los formularios solo indican el TIPO de destino;
/// la creación, el stack y la visibilidad quedan encapsulados en la implementación.
/// </summary>
public interface INavigationService
{
    /// <summary>Formulario visible actualmente (tope del stack).</summary>
    Form? Current { get; }

    /// <summary>Cantidad de formularios en el stack (incluye el principal).</summary>
    int StackCount { get; }

    /// <summary>Crea y muestra el formulario principal. Se llama una vez al iniciar.</summary>
    void Start<TForm>() where TForm : Form;

    /// <summary>Muestra un nuevo formulario y oculta el actual.</summary>
    void Push<TForm>() where TForm : Form;

    /// <summary>Cierra el formulario actual y regresa al anterior.</summary>
    void Pop();

    /// <summary>Reemplaza el formulario principal (ej. login → menú) y limpia el stack.</summary>
    void Replace<TForm>() where TForm : Form;

    /// <summary>Cierra todo lo que esté sobre el principal y vuelve a él.</summary>
    void Clear();

    /// <summary>Muestra un formulario con ShowDialog y devuelve el DialogResult.</summary>
    DialogResult ShowModal<TForm>() where TForm : Form;

    /// <summary>Muestra un formulario modal y devuelve su resultado tipado (null/default si se cancela).</summary>
    TResult? ShowModal<TForm, TResult>() where TForm : Form, IModalResult<TResult>;
}

/// <summary>
/// ApplicationContext propio: permite cambiar el MainForm en tiempo de ejecución
/// (Application.MainForm es de solo lectura). Usar: Application.Run(context);
/// </summary>
public class NavigationContext : ApplicationContext { }

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _provider;
    private readonly NavigationContext _context;
    private readonly Stack<Form> _stack = new();
    private bool _isClearing;

    public NavigationService(IServiceProvider provider, NavigationContext context)
    {
        _provider = provider;
        _context = context;
    }

    // Propiedades públicas de solo electura (encapsulamiento)
    public Form? Current => _stack.Count > 0 ? _stack.Peek() : null;

    public int StackCount => _stack.Count;

    public void Clear()
    {
        if (_stack.Count <= 1) return;

        _isClearing = true;
        try
        {
            while (_stack.Count > 1)
                _stack.Peek().Close();
        }
        finally
        {
            _isClearing = false;
        }

        ShowCurrent();
    }

    public void Pop()
    {
        if (_stack.Count <= 1) return;

        Current!.Close();
    }

    public void Push<TForm>() where TForm : Form
    {
        var previous = Current;
        var form = Create<TForm>();

        form.FormClosed += OnPushedFormClosed;

        _stack.Push(form);
        previous?.Hide();

        form.Show();
    }

    public void Replace<TForm>() where TForm : Form
    {
        var oldForms = _stack.ToArray();
        _stack.Clear();

        var main = Create<TForm>();
        _stack.Push(main);

        _context.MainForm = main;
        main.Show();

        _isClearing = true;
        try
        {
            foreach (var old in oldForms)
            {
                old.FormClosed -= OnPushedFormClosed;
                old.Close();
                old.Dispose();
            }
        }
        finally
        {
            _isClearing = false;
        }
    }

    public DialogResult ShowModal<TForm>() where TForm : Form
    {
        using var form = Create<TForm>();
        return form.ShowDialog(Current);
    }

    public TResult? ShowModal<TForm, TResult>() where TForm : Form, IModalResult<TResult>
    {
        using var form = Create<TForm>();
        return form.ShowDialog(Current) == DialogResult.OK ? form.Result : default;
    }

    public void Start<TForm>() where TForm : Form
    {
        if (_stack.Count > 0)
            throw new InvalidOperationException("La navefaciójn fue iniciada. Usa Replace<TForm>() en su lugar.");

        var main = Create<TForm>();
        _stack.Push(main);
        _context.MainForm = main;
        main.Show();
    }

    #region Private Methods

    private void ShowCurrent()
    {
        var current = Current;
        if (current is null) return;
        current.Show();
        current.Activate();
    }

    private TForm Create<TForm>() where TForm : Form
        => _provider.GetRequiredService<TForm>();   // DI resuelve las dependencias del form

    private void OnPushedFormClosed(object? sender, FormClosedEventArgs e)
    {
        var form = (Form)sender!;
        form.FormClosed -= OnPushedFormClosed;

        if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), form))
            _stack.Pop();

        form.Dispose();

        if (!_isClearing)
            ShowCurrent();
    }
    #endregion
}