using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

public static class ViewStyles
{
    private static readonly Color ColorBordePorDefecto = Color.FromArgb(180, 180, 180);

    // ====================================================================
    // MÉTODO ÚNICO DE BORDES REDONDEADOS PARA CUALQUIER CONTROL WINFORMS
    // ====================================================================
    /// <summary>
    /// Aplica bordes redondeados a cualquier control. Llamar después de InitializeComponent().
    /// <list type="bullet">
    /// <item>Controles con borde nativo y pintura propia (TextBox, NumericUpDown, ComboBox,
    /// DateTimePicker, ListBox...): se insertan dentro de un contenedor que dibuja el borde.</item>
    /// <item>Resto (Button, Panel, GroupBox, Label, PictureBox...): se dibuja el borde sobre el propio control.</item>
    /// </list>
    /// Esquinas: si el fondo del padre es un color sólido se pintan con ese color (anti-aliasing).
    /// Si algún ancestro tiene BackgroundImage se usa un Region (borde exterior sin anti-aliasing),
    /// que además recorta a los hijos y a los clics en las esquinas. No se detectan padres con
    /// fondo dibujado a mano en su propio OnPaint.
    /// </summary>
    public static void BorderRadius(Control control, int radio, Color? colorBorde = null, int anchoBorde = 1)
    {
        if (control == null) return;

        Color color = colorBorde ?? ColorBordePorDefecto;
        radio = Math.Max(radio, 0);
        anchoBorde = Math.Max(anchoBorde, 1);

        if (RequiereContenedor(control))
        {
            if (control.Parent == null)
            {
                // Aún no está en el formulario: se aplica en cuanto tenga padre
                void alAsignarPadre(object? s, EventArgs e)
                {
                    if (control.Parent == null) return;
                    control.ParentChanged -= alAsignarPadre;
                    EnvolverEnContenedor(control, radio, color, anchoBorde);
                }
                control.ParentChanged += alAsignarPadre;
                return;
            }

            EnvolverEnContenedor(control, radio, color, anchoBorde);
            return;
        }

        PintarSobreControl(control, radio, color, anchoBorde);
    }

    // ====================================================================
    // CONTROLES CON BORDE NATIVO: SE ENVUELVEN EN UN CONTENEDOR
    // ====================================================================
    private sealed class ContenedorBorde : Panel
    {
        // true cuando el control envuelto tiene Visible = false (el contenedor se vacía con un Region)
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Oculto { get; set; }
    }

    private const int EstadoVisible = 0x00000002;

    private static readonly MethodInfo? MetodoGetState = typeof(Control).GetMethod(
        "GetState", BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(int) }, null);

    // Control.Visible devuelve false mientras el formulario no se muestra, aunque nadie lo haya ocultado;
    // el flag interno guarda lo que realmente se asignó
    private static bool VisibleSolicitado(Control c) =>
        MetodoGetState != null ? (bool)MetodoGetState.Invoke(c, new object[] { EstadoVisible })! : c.Visible;

    private static bool RequiereContenedor(Control c) =>
        c is TextBoxBase or UpDownBase or ComboBox or DateTimePicker or ListBox or ListView or TreeView;

    // Estos controles dibujan un borde propio que no se puede quitar: se recorta con el contenedor
    private static bool BordeNativoNoRemovible(Control c) =>
        c is ComboBox or DateTimePicker or ListView or TreeView;

    // Estos controles tienen altura fija según su fuente: se centran en vez de estirarse
    private static bool AlturaFija(Control c) =>
        c is ComboBox or DateTimePicker or UpDownBase or TextBox { Multiline: false };

    private static void EnvolverEnContenedor(Control control, int radio, Color color, int ancho)
    {
        Control parent = control.Parent!;
        if (parent is ContenedorBorde) return; // ya estilizado

        // Se captura la geometría antes de quitar el borde nativo (que cambia la altura)
        Rectangle bounds = control.Bounds;
        int indiceZ = parent.Controls.GetChildIndex(control);
        bool visible = VisibleSolicitado(control);

        switch (control)
        {
            case TextBoxBase t: t.BorderStyle = BorderStyle.None; break;
            case UpDownBase u: u.BorderStyle = BorderStyle.None; break;
            case ListBox l: l.BorderStyle = BorderStyle.None; break;
            case ListView lv: lv.BorderStyle = BorderStyle.None; break;
            case TreeView tv: tv.BorderStyle = BorderStyle.None; break;
        }

        var host = new ContenedorBorde
        {
            BackColor = control.BackColor,
            Bounds = bounds,
            Anchor = control.Anchor,
            Dock = control.Dock,
            Margin = control.Margin,
            TabIndex = control.TabIndex,
            TabStop = false
        };
        host.Name = $"{control.Name}_Borde";
        ActivarDobleBuffer(host);

        parent.SuspendLayout();
        parent.Controls.Remove(control);
        parent.Controls.Add(host);
        parent.Controls.SetChildIndex(host, indiceZ);
        // host (dibuja el borde) -> recorte (zona útil) -> control
        var recorte = new Panel { BackColor = host.BackColor };
        control.Dock = DockStyle.None;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        recorte.Controls.Add(control);
        host.Controls.Add(recorte);
        parent.ResumeLayout(true);

        bool acomodando = false;
        void acomodar()
        {
            if (acomodando) return;
            acomodando = true;
            AcomodarInterno(host, recorte, control, radio, ancho);
            acomodando = false;
        }
        host.SizeChanged += (_, _) => acomodar();
        control.SizeChanged += (_, _) => acomodar();
        host.Click += (_, _) => control.Focus();
        recorte.Click += (_, _) => control.Focus();
        acomodar();

        // Si el control se oculta o se muestra, el contenedor (borde incluido) debe seguirlo
        host.Oculto = !visible;
        control.VisibleChanged += (_, _) =>
        {
            bool oculto = !VisibleSolicitado(control);
            if (host.Oculto == oculto) return; // el cambio vino de un ancestro
            host.Oculto = oculto;
            ActualizarRegion(host, radio);
            host.Invalidate();
        };

        PintarSobreControl(host, radio, color, ancho);
    }

    /// <summary>
    /// Calcula la zona útil dentro del contenedor dejando que las esquinas cuadradas del control
    /// queden dentro del arco (si no, tapan el borde redondeado y lo "cortan"). Si el control tiene
    /// un borde nativo que no se puede quitar, se desplaza para que el panel de recorte lo oculte.
    /// </summary>
    private static void AcomodarInterno(Control host, Control recorte, Control inner, int radio, int ancho)
    {
        int ancha = host.Width, alta = host.Height;
        if (ancha <= 0 || alta <= 0) return;

        radio = Math.Min(radio, Math.Min(ancha, alta) / 2);
        int cortar = BordeNativoNoRemovible(inner) ? 2 : 0;

        if (!AlturaFija(inner))
            inner.Height = Math.Max(alta - 2 * (ancho + 1) + 2 * cortar, 1);

        // Altura visible del control y margen vertical que sobra al centrarlo
        int altoUtil = Math.Max(inner.Height - 2 * cortar, 1);

        // Un control de altura fija más alto que el contenedor taparía el borde: el contenedor crece
        int altoMinimo = altoUtil + 2 * (ancho + 1);
        if (alta < altoMinimo) host.Height = alta = altoMinimo;

        int v = Math.Max((alta - altoUtil) / 2, 0);

        // Margen horizontal mínimo para que la esquina (h, v) quede dentro del arco interior
        double rInterior = radio - ancho - 1;
        double dy = radio - v;
        int h = ancho + 3;
        if (radio > 0 && dy > 0)
        {
            double dx = rInterior * rInterior - dy * dy;
            h = Math.Max(h, (int)Math.Ceiling(radio - (dx > 0 ? Math.Sqrt(dx) : 0)));
        }

        var zona = new Rectangle(h, v, Math.Max(ancha - 2 * h, 1), altoUtil);
        if (recorte.Bounds != zona) recorte.Bounds = zona;

        var rect = new Rectangle(-cortar, -cortar, zona.Width + 2 * cortar, inner.Height);
        if (inner.Bounds != rect) inner.Bounds = rect;
    }

    // ====================================================================
    // DIBUJO DEL BORDE SOBRE EL PROPIO CONTROL
    // ====================================================================
    private static void PintarSobreControl(Control control, int radio, Color color, int ancho)
    {
        switch (control)
        {
            case ButtonBase b:
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                break;
            case Panel p: p.BorderStyle = BorderStyle.None; break;
            case Label l: l.BorderStyle = BorderStyle.None; break;
            case PictureBox pb: pb.BorderStyle = BorderStyle.None; break;
        }

        ActivarDobleBuffer(control);
        control.Paint += (_, e) =>
        {
            bool oculto = control is ContenedorBorde { Oculto: true };
            bool solido = FondoSolido(control);

            // El fondo del padre cambió (o aún no se había evaluado): se ajusta el Region
            if ((oculto || !solido) != (control.Region != null)) ActualizarRegion(control, radio);
            if (oculto) return;

            DibujarBorde(e.Graphics, control, radio, color, ancho, solido);
        };
        control.SizeChanged += (_, _) => { ActualizarRegion(control, radio); control.Invalidate(); };
        control.ParentChanged += (_, _) => { ActualizarRegion(control, radio); control.Invalidate(); };
        ActualizarRegion(control, radio);
        control.Invalidate();
    }

    // Fondo sólido: ningún ancestro (hasta el primero opaco) tiene imagen de fondo
    private static bool FondoSolido(Control c)
    {
        for (Control? p = c.Parent; p != null; p = p.Parent)
        {
            if (p.BackgroundImage != null) return false;
            if (p.BackColor.A == 255) return true;
        }
        return true;
    }

    // Region redondeado solo cuando no se pueden pintar las esquinas con el color del padre
    private static void ActualizarRegion(Control c, int radio)
    {
        if (c is ContenedorBorde { Oculto: true })
        {
            c.Region = new Region(Rectangle.Empty);
            return;
        }

        if (FondoSolido(c) || c.Width < 2 || c.Height < 2)
        {
            if (c.Region != null) c.Region = null;
            return;
        }

        using GraphicsPath ruta = CrearRutaRedondeada(new RectangleF(0, 0, c.Width, c.Height), radio);
        c.Region = new Region(ruta);
    }

    private static void DibujarBorde(Graphics g, Control c, int radio, Color color, int ancho, bool fondoSolido)
    {
        int w = c.ClientSize.Width, h = c.ClientSize.Height;
        if (w < 2 || h < 2) return;

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.PixelOffsetMode = PixelOffsetMode.Half;

        var area = new RectangleF(0, 0, w, h);
        Color fondo = ColorSolido(c);

        // El GroupBox dibuja su propio marco y título: se tapa y se redibuja el título dentro del borde
        if (c is GroupBox)
        {
            using (var brush = new SolidBrush(fondo)) g.FillRectangle(brush, area);

            int x = Math.Max(8, radio / 2), y = ancho + 4;
            TextRenderer.DrawText(g, c.Text, c.Font, new Rectangle(x, y, Math.Max(w - 2 * x, 1), c.Font.Height + 2),
                c.ForeColor, TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.NoPrefix | TextFormatFlags.EndEllipsis);
        }

        // Esquinas con fondo sólido: el área fuera del arco se pinta con el color del padre (con anti-aliasing).
        // Sin fondo sólido las recorta el Region, y el trazo se aleja medio píxel de ese corte (sin
        // anti-aliasing) para que el escalón no lo parta.
        float holgura = 0;
        if (fondoSolido)
        {
            using (GraphicsPath externa = CrearRutaRedondeada(area, radio))
            using (GraphicsPath esquinas = new GraphicsPath(FillMode.Alternate))
            using (var brush = new SolidBrush(ColorSolido(c.Parent ?? c)))
            {
                esquinas.AddRectangle(new RectangleF(-1, -1, w + 2, h + 2));
                esquinas.AddPath(externa, false);
                g.FillPath(brush, esquinas);
            }
        }
        else holgura = 0.5f;

        // Borde: el trazo (centrado) queda por completo dentro del control
        float mitad = ancho / 2f + holgura;
        var trazo = new RectangleF(mitad, mitad, w - 2 * mitad, h - 2 * mitad);
        using (GraphicsPath ruta = CrearRutaRedondeada(trazo, Math.Max(radio - mitad, 0)))
        using (var pen = new Pen(color, ancho) { LineJoin = LineJoin.Round })
            g.DrawPath(pen, ruta);
    }

    private static GraphicsPath CrearRutaRedondeada(RectangleF r, float radio)
    {
        var path = new GraphicsPath();
        float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));

        if (d <= 0)
        {
            path.AddRectangle(r);
            return path;
        }

        path.AddArc(r.X, r.Y, d, d, 180, 90);
        path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    // Color de fondo efectivo: sube por los padres si el control es transparente
    private static Color ColorSolido(Control c)
    {
        for (Control? actual = c; actual != null; actual = actual.Parent)
            if (actual.BackColor.A == 255) return actual.BackColor;
        return SystemColors.Control;
    }

    private static void ActivarDobleBuffer(Control c) =>
        typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
            ?.SetValue(c, true);
}
