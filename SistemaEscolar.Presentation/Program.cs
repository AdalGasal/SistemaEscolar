using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaEscolar.Business;
using SistemaEscolar.Presentation.Views;
using SistemaEscolar.Presentation.Views.Admin;
using SistemaEscolar.Presentation.Views.Auth;
using SistemaEscolar.Shared.Helpers;

namespace SistemaEscolar.Presentation
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            ApplicationConfiguration.Initialize();

            // Inicializar lectura de appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configurar contenedor de inyección de dependencias
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            // Register infrastructure and application services
            services.AddBusinessServices();

            // Generar inyección de formularios
            services.AddTransient<Form1>();
            services.AddTransient<FrmAlumnoProfile>();
            services.AddTransient<FrmLogin>();
            services.AddTransient<FrmAdminMain>();

            // Construir el proveedor de servicios
            ServiceProvider = services.BuildServiceProvider();

            // Configurar los manejadores de excepciones globales
            Application.ThreadException += UIThread_Exception;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var frmLogin = ServiceProvider.GetRequiredService<FrmLogin>();
            var resultLogin = frmLogin.ShowDialog();

            if (resultLogin != DialogResult.OK)
                return;

            var mainForm = ServiceProvider.GetRequiredService<FrmAdminMain>();

            Application.Run(mainForm);
        }

        // Captura errores ocurridos en los hilos de la interfaz de usuario (el 95% de los clicks)
        private static void UIThread_Exception(object sender, ThreadExceptionEventArgs e)
        {
            HandleError(e.Exception);
        }

        // Captura errores en hilos secundarios o tareas en segundo plano
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                HandleError(ex);
            }
        }

        // El cerebro del filtrado de errores
        private static void HandleError(Exception ex)
        {
            // Si el error es de negocio (nuestro), es un mensaje controlado para el usuario
            if (ex is ValidationException)
            {
                MessageBox.Show(ex.Message, "Validación Escolar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Si es un error técnico (SQL caído, null reference, etc.)
                // Aquí se debería usar un Logger (Serilog, NLog) para registrar el ex.ToString()
                MessageBox.Show("Ocurrió un error inesperado en el sistema. Por favor, contacte al administrador.",
                                "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}