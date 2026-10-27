# Guía genérica: configurar y usar un DataGridView (Windows Forms)

Plantilla reutilizable con `T` como tipo de objeto cualquiera (Alumno, Docente, Producto…). Sustituye `T`, `dgv` y los nombres de propiedades por los de tu proyecto.

---

## 1. Flujo general

| Etapa | Qué se hace | Cuándo |
|---|---|---|
| 1. Configuración | Opciones del grid y columnas | Una sola vez (constructor / `Load`) |
| 2. Carga | Asignar `DataSource` con una lista | Cada vez que cambian los datos |
| 3. Eventos | Reaccionar a selección, clic, edición | Handlers suscritos una vez |
| 4. Toma de objetos | Recuperar `T` desde la fila | Dentro de los handlers |

Orden recomendado en el formulario:

```csharp
public FrmGenerico()
{
    InitializeComponent();
    ConfigurarGrid();
    SuscribirEventos();
    CargarDatos();
}
```

---

## 2. Configuración

### 2.1 Opciones generales

```csharp
private void ConfigurarGrid()
{
    dgv.AutoGenerateColumns = false;            // columnas definidas por código
    dgv.AllowUserToAddRows = false;             // sin fila vacía al final
    dgv.AllowUserToDeleteRows = false;          // el borrado lo controlas tú
    dgv.AllowUserToResizeRows = false;
    dgv.ReadOnly = true;                        // sin edición directa de celdas
    dgv.MultiSelect = false;                    // true si necesitas varias filas
    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    dgv.RowHeadersVisible = false;
    dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    dgv.EnableHeadersVisualStyles = false;      // permite personalizar encabezados

    dgv.Columns.Clear();
    AgregarColumnasDeDatos();
    AgregarColumnasDeAcciones();
}
```

### 2.2 Columnas de datos

Cada columna se enlaza a una propiedad de `T` con `DataPropertyName`. Usa `nameof` para evitar errores de texto.

```csharp
private void AgregarColumnasDeDatos()
{
    dgv.Columns.Add(new DataGridViewTextBoxColumn
    {
        Name = "colNombre",                       // identificador para código
        HeaderText = "Nombre",                    // título visible
        DataPropertyName = nameof(T.Nombre),      // propiedad enlazada
        FillWeight = 40                           // peso relativo en modo Fill
    });

    dgv.Columns.Add(new DataGridViewTextBoxColumn
    {
        Name = "colPrecio",
        HeaderText = "Precio",
        DataPropertyName = nameof(T.Precio),
        DefaultCellStyle = new DataGridViewCellStyle
        {
            Format = "C2",                                        // formato moneda
            Alignment = DataGridViewContentAlignment.MiddleRight
        }
    });

    dgv.Columns.Add(new DataGridViewCheckBoxColumn
    {
        Name = "colActivo",
        HeaderText = "Activo",
        DataPropertyName = nameof(T.Activo)
    });

    // Columna oculta con el Id (útil para búsquedas, no se muestra)
    dgv.Columns.Add(new DataGridViewTextBoxColumn
    {
        Name = "colId",
        DataPropertyName = nameof(T.Id),
        Visible = false
    });
}
```

Tipos de columna más usados:

| Tipo | Uso |
|---|---|
| `DataGridViewTextBoxColumn` | Texto, números, fechas |
| `DataGridViewCheckBoxColumn` | `bool` |
| `DataGridViewComboBoxColumn` | Lista de opciones / catálogos |
| `DataGridViewButtonColumn` | Botón de acción por fila |
| `DataGridViewImageColumn` | Iconos o imágenes |
| `DataGridViewLinkColumn` | Enlaces |

### 2.3 Columnas de acción (botones)

```csharp
private void AgregarColumnasDeAcciones()
{
    AgregarBoton("colVer", "Ver", 60);
    AgregarBoton("colEditar", "Editar", 70);
    AgregarBoton("colEliminar", "Eliminar", 80);
}

private void AgregarBoton(string name, string texto, int ancho)
{
    dgv.Columns.Add(new DataGridViewButtonColumn
    {
        Name = name,
        HeaderText = "",
        Text = texto,
        UseColumnTextForButtonValue = true,   // mismo texto en todas las filas
        Width = ancho,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
        Resizable = DataGridViewTriState.False
    });
}
```

---

## 3. Carga de datos

### 3.1 Carga básica desde una lista

Usa `BindingList<T>`: al agregar o quitar elementos el grid se actualiza solo (con `List<T>` no).

```csharp
private BindingList<T> _items = new BindingList<T>();

private void CargarDatos()
{
    List<T> lista = _servicio.ObtenerTodos();   // BD, API, archivo…
    _items = new BindingList<T>(lista);
    dgv.DataSource = _items;
}
```

### 3.2 Recargar / refrescar

```csharp
private void Recargar()
{
    dgv.DataSource = null;                        // desenlaza
    _items = new BindingList<T>(_servicio.ObtenerTodos());
    dgv.DataSource = _items;
}

// Si solo modificaste propiedades de un objeto ya cargado:
dgv.Refresh();                // repinta
dgv.InvalidateRow(indice);    // repinta una sola fila
```

### 3.3 Carga dinámica desde código

Para cuando no conoces las columnas de antemano o se arman según los datos.

**A) Columnas generadas a partir de las propiedades de `T` (reflexión)**

```csharp
private void CargarDinamico<TItem>(IEnumerable<TItem> datos)
{
    dgv.AutoGenerateColumns = false;
    dgv.DataSource = null;
    dgv.Columns.Clear();

    foreach (PropertyInfo prop in typeof(TItem).GetProperties())
    {
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "col" + prop.Name,
            HeaderText = prop.Name,
            DataPropertyName = prop.Name
        });
    }

    dgv.DataSource = new BindingList<TItem>(datos.ToList());
}
```

Alternativa rápida: `dgv.AutoGenerateColumns = true;` y asignar el `DataSource` (menos control sobre títulos y formato).

**B) Columnas definidas por configuración (lista de metadatos)**

```csharp
public record ColumnaConfig(string Propiedad, string Titulo, int Peso = 100, string Formato = null);

private void CargarConConfig<TItem>(IEnumerable<TItem> datos, IEnumerable<ColumnaConfig> columnas)
{
    dgv.AutoGenerateColumns = false;
    dgv.DataSource = null;
    dgv.Columns.Clear();

    foreach (var c in columnas)
    {
        var col = new DataGridViewTextBoxColumn
        {
            Name = "col" + c.Propiedad,
            HeaderText = c.Titulo,
            DataPropertyName = c.Propiedad,
            FillWeight = c.Peso
        };
        if (c.Formato != null)
            col.DefaultCellStyle.Format = c.Formato;
        dgv.Columns.Add(col);
    }

    AgregarColumnasDeAcciones();
    dgv.DataSource = new BindingList<TItem>(datos.ToList());
}

// Uso
CargarConConfig(productos, new[]
{
    new ColumnaConfig("Nombre", "Producto", 50),
    new ColumnaConfig("Precio", "Precio", 20, "C2"),
    new ColumnaConfig("Stock", "Existencia", 20)
});
```

**C) Filas agregadas manualmente (sin DataSource)**

Solo para casos simples: pierdes `DataBoundItem`; guarda el objeto en `Tag`.

```csharp
dgv.Rows.Clear();
foreach (T item in lista)
{
    int i = dgv.Rows.Add(item.Nombre, item.Precio);
    dgv.Rows[i].Tag = item;                    // guarda el objeto
}

// Recuperarlo
T obj = (T)dgv.Rows[indice].Tag;
```

**D) Cargar desde un `DataTable` (consulta SQL genérica)**

```csharp
DataTable tabla = _servicio.EjecutarConsulta("SELECT ...");
dgv.AutoGenerateColumns = true;
dgv.DataSource = tabla;

// Fila -> DataRow
DataRowView fila = dgv.CurrentRow?.DataBoundItem as DataRowView;
object valor = fila?["NombreColumna"];
```

---

## 4. Reacción a eventos del usuario

### 4.1 Suscripción

```csharp
private void SuscribirEventos()
{
    dgv.SelectionChanged += dgv_SelectionChanged;
    dgv.CellContentClick += dgv_CellContentClick;
    dgv.CellDoubleClick += dgv_CellDoubleClick;
    dgv.CellFormatting += dgv_CellFormatting;
    dgv.DataError += dgv_DataError;
    dgv.KeyDown += dgv_KeyDown;
}
```

> Suscribe cada evento **una sola vez** (o desde el diseñador, no ambos); si no, el handler se ejecuta duplicado.

### 4.2 Cambio de selección

```csharp
private void dgv_SelectionChanged(object sender, EventArgs e)
{
    T item = ObtenerSeleccionado();
    btnEditar.Enabled = item != null;
    btnEliminar.Enabled = item != null;
    // mostrar detalle en otros controles…
}
```

### 4.3 Clic en botones de fila

```csharp
private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0 || e.ColumnIndex < 0) return;     // encabezado

    T item = dgv.Rows[e.RowIndex].DataBoundItem as T;
    if (item == null) return;

    switch (dgv.Columns[e.ColumnIndex].Name)
    {
        case "colVer":      Ver(item);      break;
        case "colEditar":   Editar(item);   break;
        case "colEliminar": Eliminar(item); break;
    }
}
```

### 4.4 Doble clic en una fila

```csharp
private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return;
    if (dgv.Rows[e.RowIndex].DataBoundItem is T item)
        Editar(item);
}
```

### 4.5 Formato condicional de celdas

```csharp
private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
    if (e.RowIndex < 0) return;
    if (dgv.Rows[e.RowIndex].DataBoundItem is not T item) return;

    // Colorear la fila según una condición
    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
        item.Activo ? Color.White : Color.LightGray;

    // Transformar el valor mostrado
    if (dgv.Columns[e.ColumnIndex].Name == "colActivo")
    {
        // e.Value = item.Activo ? "Sí" : "No"; e.FormattingApplied = true;
    }
}
```

### 4.6 Teclado (Suprimir, Enter)

```csharp
private void dgv_KeyDown(object sender, KeyEventArgs e)
{
    var item = ObtenerSeleccionado();
    if (item == null) return;

    if (e.KeyCode == Keys.Delete) { Eliminar(item); e.Handled = true; }
    if (e.KeyCode == Keys.Enter)  { Editar(item);   e.Handled = true; }
}
```

### 4.7 Errores de datos

```csharp
private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
{
    // Evita el cuadro de excepción por defecto
    e.ThrowException = false;
    // Registrar o informar: e.Exception.Message
}
```

### 4.8 Edición en línea (si `ReadOnly = false`)

```csharp
dgv.CellValueChanged += (s, e) =>
{
    if (e.RowIndex < 0) return;
    if (dgv.Rows[e.RowIndex].DataBoundItem is T item)
        _servicio.Actualizar(item);       // el objeto ya trae el nuevo valor
};

// Para checkboxes: confirmar el cambio al instante
dgv.CurrentCellDirtyStateChanged += (s, e) =>
{
    if (dgv.IsCurrentCellDirty)
        dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
};
```

### 4.9 Ordenamiento por clic en encabezado

Con `BindingList<T>` el ordenamiento automático **no** viene incluido. Opciones: implementar un `SortableBindingList<T>` o ordenar manualmente:

```csharp
dgv.ColumnHeaderMouseClick += (s, e) =>
{
    string prop = dgv.Columns[e.ColumnIndex].DataPropertyName;
    if (string.IsNullOrEmpty(prop)) return;   // columnas de botones
    var ordenados = _items.OrderBy(x => typeof(T).GetProperty(prop).GetValue(x)).ToList();
    _items = new BindingList<T>(ordenados);
    dgv.DataSource = _items;
};
```

---

## 5. Toma de objetos cargados

El objeto original de cada fila vive en `DataBoundItem`. No reconstruyas el objeto leyendo `Cells[...]`.

### 5.1 Fila actual

```csharp
private T ObtenerSeleccionado()
{
    return dgv.CurrentRow?.DataBoundItem as T;
}
```

### 5.2 Fila por índice (clic en botón)

```csharp
T item = dgv.Rows[e.RowIndex].DataBoundItem as T;
```

### 5.3 Varias filas seleccionadas (`MultiSelect = true`)

```csharp
List<T> seleccionados = dgv.SelectedRows
    .Cast<DataGridViewRow>()
    .Select(r => r.DataBoundItem as T)
    .Where(x => x != null)
    .ToList();
```

### 5.4 Todos los objetos cargados

```csharp
List<T> todos = dgv.Rows
    .Cast<DataGridViewRow>()
    .Select(r => r.DataBoundItem as T)
    .Where(x => x != null)
    .ToList();

// Más directo: usar la lista origen
List<T> todos2 = _items.ToList();
```

### 5.5 Buscar una fila por Id y seleccionarla desde código

```csharp
private void SeleccionarPorId(int id)
{
    foreach (DataGridViewRow fila in dgv.Rows)
    {
        if (fila.DataBoundItem is T item && item.Id == id)
        {
            dgv.ClearSelection();
            fila.Selected = true;
            dgv.CurrentCell = fila.Cells[0];       // mueve el cursor
            dgv.FirstDisplayedScrollingRowIndex = fila.Index;   // hace scroll
            return;
        }
    }
}
```

### 5.6 Leer un valor de celda puntual (cuando solo necesitas un dato)

```csharp
object valor = dgv.CurrentRow?.Cells["colNombre"].Value;
```

---

## 6. Operaciones comunes sobre la lista

```csharp
// Agregar
_items.Add(nuevo);

// Eliminar (primero la BD / servicio, luego la lista)
_servicio.Eliminar(item.Id);
_items.Remove(item);

// Modificar propiedades y repintar
item.Nombre = "Nuevo";
dgv.Refresh();

// Filtrar sin perder el origen
string texto = txtBuscar.Text.Trim().ToLower();
var filtrados = _todos.Where(x => x.Nombre.ToLower().Contains(texto)).ToList();
dgv.DataSource = new BindingList<T>(filtrados);
```

---

## 7. Errores frecuentes

| Síntoma | Causa | Solución |
|---|---|---|
| Columnas duplicadas | `AutoGenerateColumns` en `true` con columnas manuales | Ponerlo en `false` antes de asignar el `DataSource` |
| Handler se ejecuta dos veces | Evento suscrito en diseñador y en código | Suscribir en un solo lugar |
| `NullReferenceException` al hacer clic | `DataBoundItem` nulo o clic en encabezado | Validar `RowIndex >= 0` y usar `as` + null check |
| El grid no refleja altas/bajas | Se usó `List<T>` | Usar `BindingList<T>` |
| Celda no se repinta tras cambiar propiedad | El objeto no notifica cambios | `dgv.Refresh()` o implementar `INotifyPropertyChanged` |
| Columna de datos vacía | `DataPropertyName` mal escrito o la propiedad no es pública con `get` | Usar `nameof(...)` y propiedades públicas |
| `InvalidOperationException` al eliminar en `foreach` | Se modifica la colección mientras se recorre | Recorrer `ToList()` o eliminar fuera del ciclo |
| Cambiar `DataSource` desde otro hilo falla | Acceso a UI fuera del hilo principal | Usar `Invoke` / `BeginInvoke` o `async/await` en el hilo de UI |

---

## 8. Checklist de implementación

- [ ] `AutoGenerateColumns = false` y opciones de selección definidas
- [ ] Columnas con `Name`, `HeaderText` y `DataPropertyName`
- [ ] Columnas de botones con `UseColumnTextForButtonValue = true`
- [ ] `DataSource = new BindingList<T>(lista)`
- [ ] Eventos suscritos una sola vez
- [ ] `e.RowIndex >= 0` validado en cada evento de celda
- [ ] Objeto obtenido con `DataBoundItem as T` y comprobado contra `null`
- [ ] Eliminar: confirmar, ejecutar en BD y luego quitar de la lista
