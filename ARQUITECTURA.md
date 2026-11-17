# Sistema Escolar — Documentación técnica de arquitectura

> Estado documentado: rama `main`, commit `7dc073c` (*Navigation Service Implementation*), noviembre 2026.

## 1. Visión general

Aplicación de escritorio **Windows Forms** sobre **.NET 10** para gestión escolar (alumnos, carreras, usuarios). Sigue una **arquitectura en N capas** con inyección de dependencias (`Microsoft.Extensions.DependencyInjection`), acceso a datos con **ADO.NET puro** (sin ORM) sobre **SQL Server**, y autenticación con contraseñas hasheadas con **BCrypt**.

| Proyecto | Tipo | Target | Rol |
|---|---|---|---|
| `SistemaEscolar.Presentation` | WinExe (WinForms) | `net10.0-windows` | UI, composición (DI), navegación, manejo global de errores |
| `SistemaEscolar.Business` | Class Library | `net10.0` | Reglas de negocio, validaciones, hashing, registro de servicios |
| `SistemaEscolar.Data` | Class Library | `net10.0` | Repositorios y acceso a SQL Server |
| `SistemaEscolar.Shared` | Class Library | `net10.0` | Modelos, DTOs, mappers, excepciones y extensiones comunes |

Solución: [`SistemaEscolar.slnx`](SistemaEscolar.slnx) (formato XML nuevo de Visual Studio).

## 2. Dependencias entre capas

```
            ┌──────────────────────────────┐
            │  SistemaEscolar.Presentation │  (WinForms, punto de entrada)
            └──────────────┬───────────────┘
                           │ referencia
                           ▼
            ┌──────────────────────────────┐
            │   SistemaEscolar.Business    │  (servicios + registro DI)
            └──────────────┬───────────────┘
                           │ referencia
                           ▼
            ┌──────────────────────────────┐
            │     SistemaEscolar.Data      │  (repositorios ADO.NET)
            └──────────────┬───────────────┘
                           │
                           ▼
            ┌──────────────────────────────┐
            │    SistemaEscolar.Shared     │  ◄── referenciado por TODAS las capas
            └──────────────────────────────┘
```

Referencias de proyecto reales (`.csproj`):

- **Presentation** → Business, Shared
- **Business** → Data, Shared
- **Data** → Shared
- **Shared** → (ninguna)

Presentation **no** referencia directamente a Data; lo alcanza de forma transitiva porque `Business` registra los repositorios en el contenedor DI.

### Paquetes NuGet por capa

| Capa | Paquetes |
|---|---|
| Presentation | `FontAwesome.Sharp` 6.6.0, `Microsoft.Extensions.Configuration(.Json)` 10.0.14, `Microsoft.Extensions.DependencyInjection` 10.0.14 |
| Business | `BCrypt.Net-Next` 4.2.0, `System.Security.Cryptography.Pkcs` 10.0.13 |
| Data | `Microsoft.Data.SqlClient` 7.1.1, `Microsoft.Extensions.Configuration` 10.0.13, `System.Security.Cryptography.Pkcs` 10.0.13 |
| Shared | — |

## 3. Flujo de una petición

```
Form (evento UI, async void)
   └─► IXxxService            (Business: valida / aplica reglas)
          └─► IXxxRepository  (Data: SQL parametrizado vía BaseRepository<T>)
                 └─► SQL Server
                 ◄── DbDataReader ──► XxxMapper.Map (Shared) ──► Modelo (Shared)
          ◄── Modelo / bool / int  (o lanza ValidationException)
   ◄── actualiza controles  |  error ─► Program.HandleError (MessageBox)
```

Ejemplo real — **Login**: `FrmLogin.btnLogin_Click` → `IAuthService.LoginAsync(LoginDto)` → `IUserRepository.GetByUserNameAsync` → `UserMapper.Map` → `PasswordHasher.Verify` → si es válido, `INavigationService.Replace<FrmAdminMain>()`. Si el usuario no existe, está inactivo o la contraseña no coincide se lanza `ValidationException`, que el manejador global muestra como advertencia.

---

## 4. Capa de Presentación — `SistemaEscolar.Presentation`

**Responsabilidad:** interfaz de usuario, *composition root* (arranque, configuración y DI), navegación entre formularios y captura global de excepciones. No contiene SQL ni reglas de negocio; consume interfaces de `Business`.

```
SistemaEscolar.Presentation/
├── Program.cs                     # Punto de entrada: config, DI, handlers globales, arranque
├── appsettings.json               # ConnectionStrings:DefaultConnection (copiado al output)
├── Form1.cs                       # Formulario de prueba (lista alumnos en MessageBox)
├── Helpers/
│   └── ViewStyles.cs              # Bordes redondeados genéricos para cualquier Control
├── Services/
│   └── INavigationService.cs      # INavigationService, IModalResult<T>, NavigationContext, NavigationService
├── Views/
│   ├── Auth/
│   │   ├── FrmLogin.cs            # Login → Replace<FrmAdminMain>
│   │   └── FrmRegister.cs         # Registro de usuario
│   ├── Admin/
│   │   └── FrmAdminMain.cs        # Panel principal (total de carreras activas)
│   └── FrmAlumnoProfile.cs        # Alta / consulta de perfil de alumno
└── datagridview_configuracion_generica.md   # Guía interna de uso de DataGridView
```

### 4.1 `Program.cs` (composition root)

1. `ApplicationConfiguration.Initialize()`.
2. Carga `appsettings.json` con `ConfigurationBuilder` y lo registra como `IConfiguration` (singleton).
3. `services.AddBusinessServices()` → registra repositorios y servicios (ver §5.3).
4. Registra servicios de presentación: `NavigationContext` y `INavigationService` como **singleton**.
5. Registra formularios como **transient**: `Form1`, `FrmAlumnoProfile`, `FrmLogin`, `FrmAdminMain`.
6. Construye el `ServiceProvider` (expuesto como `Program.ServiceProvider`).
7. Engancha `Application.ThreadException` y `AppDomain.CurrentDomain.UnhandledException` → `HandleError`.
8. `nav.Start<FrmLogin>()` y `Application.Run(navContext)`.

**Manejo de errores centralizado (`HandleError`):**
- `ValidationException` (Shared) → `MessageBox` de advertencia con el mensaje de negocio.
- Cualquier otra excepción → mensaje genérico "Error Crítico" (punto previsto para integrar un logger).

### 4.2 Servicio de navegación

Archivo `Services/INavigationService.cs` (namespace `SistemaEscolar.Presentation.Navigation`). Contiene contrato e implementación:

| Tipo | Descripción |
|---|---|
| `INavigationService` | API de navegación por **tipo** de formulario: `Start`, `Push`, `Pop`, `Replace`, `Clear`, `ShowModal<TForm>`, `ShowModal<TForm,TResult>`; propiedades `Current`, `StackCount`. |
| `IModalResult<TResult>` | Contrato para formularios modales que devuelven un valor tipado. |
| `NavigationContext` | `ApplicationContext` propio que permite cambiar `MainForm` en tiempo de ejecución. |
| `NavigationService` | Mantiene un `Stack<Form>`; crea formularios con `IServiceProvider.GetRequiredService<TForm>()` (DI resuelve sus dependencias), oculta/muestra y libera (`Dispose`) los formularios al cerrarse. |

Semántica: `Push` oculta el actual y muestra el nuevo; `Pop` cierra el tope y regresa al anterior; `Replace` cambia el formulario principal y limpia el stack (ej. login → menú); `Clear` vuelve al principal.

### 4.3 Formularios

| Formulario | Dependencias inyectadas | Función |
|---|---|---|
| `FrmLogin` | `IAuthService`, `INavigationService` | Autentica; en éxito navega con `Replace<FrmAdminMain>()`. Abre `FrmRegister` como diálogo. |
| `FrmRegister` | `IAuthService` | Registra un usuario (`RegisterAsync`). Se instancia con `new` desde `FrmLogin` (no está en el contenedor DI). |
| `FrmAdminMain` | `ICarreraService` | Muestra el total de carreras activas al cargar. |
| `FrmAlumnoProfile` | `IAlumnoService` | Carga un alumno por Id y permite guardar un alumno nuevo con validación de UI. |
| `Form1` | `IAlumnoService` | Formulario de prueba/legado. |

Convenciones observadas: nombres `Frm*`, prefijos húngaros en controles (`txt`, `btn`, `cbo`, `dtp`, `num`, `grpBox`, `lbl`), handlers `async void` que esperan servicios `async Task`, estilos aplicados con `ViewStyles.BorderRadius(control, radio)` después de `InitializeComponent()`.

### 4.4 `Helpers/ViewStyles.cs`

Clase estática (namespace global) con un único método público `BorderRadius(Control, int radio, Color? colorBorde, int anchoBorde)`. Para controles con borde nativo (TextBox, ComboBox, DateTimePicker, NumericUpDown, ListBox, ListView, TreeView) los envuelve en un contenedor que dibuja el borde; para el resto (Button, Panel, GroupBox, Label, PictureBox) pinta el borde sobre el propio control. Maneja anti‑aliasing, `Region` cuando hay imágenes de fondo y visibilidad del control envuelto.

---

## 5. Capa de Negocio — `SistemaEscolar.Business`

**Responsabilidad:** reglas y validaciones de negocio, orquestación de repositorios, seguridad de contraseñas y **registro de dependencias** de Business + Data en el contenedor DI. Expone únicamente interfaces a Presentación.

```
SistemaEscolar.Business/
├── ServiceProvider.cs          # Extensión AddBusinessServices(IServiceCollection)
├── Interfaces/
│   ├── IAlumnoService.cs
│   ├── IAuthService.cs
│   └── ICarreraService.cs
├── Services/
│   ├── AlumnoService.cs
│   ├── AuthService.cs
│   └── CarreraService.cs
└── Helpers/
    └── PasswordHasher.cs       # BCrypt Hash / Verify
```

### 5.1 Servicios

| Interfaz | Métodos | Reglas implementadas |
|---|---|---|
| `IAlumnoService` | `GetAllAsync()`, `GetByIdAsync(int)`, `AddAsync(Alumno)` | `GetById` lanza `KeyNotFoundException` si no existe; `Add` exige `Nombre` (`ArgumentException`). |
| `IAuthService` | `LoginAsync(LoginDto) : bool`, `RegisterAsync(LoginDto)` | Login: usuario existente, `Estatus` activo y contraseña verificada con BCrypt; cada falla lanza `ValidationException`. Registro: hashea la contraseña y crea el usuario activo. |
| `ICarreraService` | `GetCountActiveAsync() : int` | Delegación directa al repositorio. |

### 5.2 `PasswordHasher`

Métodos estáticos `Hash(string)` y `Verify(string, string)` sobre `BCrypt.Net.BCrypt`. Las contraseñas nunca se guardan en texto plano.

### 5.3 Registro DI (`ServiceProvider.AddBusinessServices`)

Todos con ciclo de vida **Scoped** (en WinForms, al no crearse scopes, se comportan como singletons del proveedor raíz):

| Contrato | Implementación | Capa |
|---|---|---|
| `IAlumnoRepository` | `AlumnoRepository` | Data |
| `IUserRepository` | `UserRepository` | Data |
| `ICarreraRepository` | `CarreraRepository` | Data |
| `IAlumnoService` | `AlumnoService` | Business |
| `IAuthService` | `AuthService` | Business |
| `ICarreraService` | `CarreraService` | Business |

> Nota: la clase se llama `ServiceProvider`, igual que `System.IServiceProvider`/`Program.ServiceProvider`; conviene tenerlo presente al leer `using`s.

---

## 6. Capa de Datos — `SistemaEscolar.Data`

**Responsabilidad:** acceso a SQL Server con ADO.NET, consultas parametrizadas, gestión del ciclo de vida de conexiones y scripts de base de datos. No contiene reglas de negocio.

```
SistemaEscolar.Data/
├── Interfaces/
│   ├── IAlumnoRepository.cs
│   ├── ICarreraRepository.cs
│   └── IUserRepository.cs
├── Repositories/
│   ├── BaseRepository.cs       # Base genérica BaseRepository<T>
│   ├── AlumnoRepository.cs
│   ├── CarreraRepository.cs
│   └── UserRepository.cs
└── Scripts/
    ├── init.sql                                    # Esquema
    ├── seed-20261002-alumnos.sql                   # Datos de alumnos
    └── seed-20261026-carreras-docentes-materias.sql
```

### 6.1 `BaseRepository<T>`

- Obtiene la cadena `ConnectionStrings:DefaultConnection` desde `IConfiguration` (falla con `InvalidOperationException` si no existe).
- Usa `DbProviderFactory` (`SqlClientFactory.Instance`) para crear conexiones, comandos y parámetros de forma **agnóstica al proveedor** (`DbConnection`, `DbCommand`, `DbParameter`).
- Métodos protegidos:

| Método | Uso |
|---|---|
| `CreateParameter(name, value)` | Crea un `DbParameter` (null → `DBNull.Value`). |
| `QueryAsync(query, map, params)` | Lista de `T` mapeada fila por fila con un `Func<DbDataReader, T>`. |
| `QuerySingleAsync(query, map, params)` | Primer registro o `null`. |
| `ExecuteAsync(query, params)` | `ExecuteNonQuery` → filas afectadas. |
| `ExecuteScalarAsync(query, params)` | `ExecuteScalar` convertido a `int` (conteos, `SCOPE_IDENTITY()`). |

- Cada operación abre y cierra su propia conexión con `await using` (aprovecha el *connection pooling*).

### 6.2 Repositorios

| Repositorio | Tabla | Operaciones |
|---|---|---|
| `AlumnoRepository` | `Alumnos` | `GetAllAsync`, `GetByIdAsync`, `AddAsync` (INSERT parametrizado) |
| `UserRepository` | `Usuarios` | `GetByUserNameAsync`, `RegisterAsync` (INSERT + `SCOPE_IDENTITY()`, asigna `Id`) |
| `CarreraRepository` | `Carreras` | `GetCountActiveAsync` (`COUNT` con `estatus = 1`) |

### 6.3 Base de datos (`Scripts/init.sql`)

SQL Server, base `SistemaEscolar`. Todas las tablas tienen `id int identity` como PK, `estatus bit default 1` y `createdAt datetime default sysdatetime()`.

| Tabla | Campos principales | Modelo en Shared |
|---|---|---|
| `Alumnos` | matricula, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, sexo | `Alumno` |
| `Usuarios` | username (unique), password (hash) | `Usuario` |
| `Carreras` | clave, nombre, duracion | `Carrera` |
| `Materias` | nombre, creditos, semestre, horas | `Materia` |
| `Docentes` | nombre, apellidos, fechaNacimiento, sexo, especialidad, fechaIngreso, sueldo, unidadAcademica | — (sin modelo aún) |
| `Periodos` | fechaInicio, fechaFin, nombre | — (sin modelo aún) |

No hay llaves foráneas ni tablas de relación definidas todavía.

---

## 7. Capa Compartida — `SistemaEscolar.Shared`

**Responsabilidad:** tipos transversales sin dependencias de otros proyectos: entidades de dominio, DTOs, mapeo desde `DbDataReader`, excepciones propias y extensiones utilitarias.

```
SistemaEscolar.Shared/
├── Models/
│   ├── Alumno.cs
│   ├── Carrera.cs
│   ├── Materia.cs
│   └── Usuario.cs
├── Dtos/
│   └── Auth/
│       └── LoginDto.cs           # UserName, Password
├── Mappers/
│   ├── AlumnoMapper.cs           # DbDataReader → Alumno
│   └── UserMapper.cs             # DbDataReader → Usuario
└── Helpers/
    ├── CustomExceptions.cs       # ValidationException
    └── DataReaderExtensions.cs   # GetSafeString/NullableString/DateTime/Char/Boolean
```

| Elemento | Detalle |
|---|---|
| **Models** | POCOs con propiedades auto-implementadas e inicialización `string.Empty`; nombres en español. |
| **Dtos** | Objetos de transporte entre UI y negocio (`LoginDto` se usa para login y registro). |
| **Mappers** | Métodos estáticos `Map(DbDataReader)` usados como delegado por `BaseRepository.QueryAsync`. Hay espacios reservados para mapeos modelo ↔ DTO. |
| **ValidationException** | Excepción de negocio "controlada": la UI la muestra como mensaje al usuario. |
| **DataReaderExtensions** | Lecturas seguras ante `DBNull` con valores por defecto. |

---

## 8. Convenciones generales

- **Async de punta a punta:** repositorios y servicios exponen `Task`/`Task<T>`; los eventos de UI son `async void`.
- **Programación contra interfaces:** cada servicio y repositorio tiene su `I*` y se resuelve por DI (constructor injection).
- **Namespaces:** `SistemaEscolar.<Capa>.<Carpeta>` con *file-scoped namespaces* (salvo algunos archivos de Presentation).
- **Idioma:** dominio y comentarios en español; nombres técnicos y algunos mensajes en inglés.
- **Nullable** e **ImplicitUsings** habilitados en todos los proyectos.
- **Configuración:** `appsettings.json` en Presentation, copiado al directorio de salida (`PreserveNewest`).

## 9. Observaciones y deuda técnica detectada

Puntos encontrados al analizar el código; no son cambios aplicados, solo referencias para próximas iteraciones:

1. **Credenciales en el repositorio:** `appsettings.json` contiene la cadena de conexión con usuario `sa` y contraseña en texto plano. Recomendable usar *User Secrets*/variables de entorno y rotar la contraseña.
2. **Acoplamiento Shared ↔ ADO.NET:** los mappers y `DataReaderExtensions` dependen de `System.Data.Common`; su lugar natural sería la capa Data.
3. **Registro de Data en Business:** `AddBusinessServices` registra los repositorios; podría separarse en un `AddDataServices` dentro de Data.
4. **Navegación inconsistente:** `FrmRegister` se crea con `new` y no está registrado en DI (no usa `INavigationService.ShowModal`).
5. **Ubicación/nombre del navegador:** `Services/INavigationService.cs` contiene la implementación completa y usa el namespace `...Navigation`, distinto de la carpeta.
6. **Excepciones mixtas:** `AuthService` usa `ValidationException`, pero `AlumnoService` lanza `KeyNotFoundException`/`ArgumentException`, que el manejador global trata como "Error Crítico".
7. **Valores de prueba en UI:** `FrmAlumnoProfile` carga el alumno con Id fijo `44`; `Form1` es un formulario de prueba registrado en DI.
8. **Modelos/tablas pendientes:** `Docentes` y `Periodos` sin modelo; `Carrera` y `Materia` sin mapper ni CRUD completo.
9. **`SELECT *`** en repositorios: acopla el mapeo al orden/nombres de columnas actuales.

## 10. Recursos adicionales

- `docs/` — infografías: *Servicio de Navegación*, *Genéricos en C#*, *Async/await*.
- `SistemaEscolar.Presentation/datagridview_configuracion_generica.md` — guía genérica para configurar `DataGridView`.
