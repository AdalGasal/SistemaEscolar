namespace SistemaEscolar.Presentation.Views.Admin
{
    partial class FrmAdminMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpBoxCarreras = new GroupBox();
            label2 = new Label();
            lblTotalCarreas = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            grpBoxAlumnos = new GroupBox();
            label1 = new Label();
            lblTotalAlumnos = new Label();
            grpBoxCarreras.SuspendLayout();
            grpBoxAlumnos.SuspendLayout();
            SuspendLayout();
            // 
            // grpBoxCarreras
            // 
            grpBoxCarreras.Controls.Add(label2);
            grpBoxCarreras.Controls.Add(lblTotalCarreas);
            grpBoxCarreras.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpBoxCarreras.ForeColor = Color.FromArgb(0, 0, 192);
            grpBoxCarreras.Location = new Point(10, 17);
            grpBoxCarreras.Margin = new Padding(3, 2, 3, 2);
            grpBoxCarreras.Name = "grpBoxCarreras";
            grpBoxCarreras.Padding = new Padding(3, 2, 3, 2);
            grpBoxCarreras.Size = new Size(239, 128);
            grpBoxCarreras.TabIndex = 0;
            grpBoxCarreras.TabStop = false;
            grpBoxCarreras.Text = "Carreras";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe Fluent Icons", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(18, 90);
            label2.Name = "label2";
            label2.Size = new Size(148, 16);
            label2.TabIndex = 1;
            label2.Text = "Total carreras activas";
            // 
            // lblTotalCarreas
            // 
            lblTotalCarreas.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalCarreas.ForeColor = Color.FromArgb(0, 0, 192);
            lblTotalCarreas.Location = new Point(5, 29);
            lblTotalCarreas.Name = "lblTotalCarreas";
            lblTotalCarreas.Size = new Size(228, 61);
            lblTotalCarreas.TabIndex = 0;
            lblTotalCarreas.Text = "0";
            lblTotalCarreas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Segoe UI", 12F);
            groupBox2.Location = new Point(274, 17);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(239, 128);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Materias";
            // 
            // groupBox3
            // 
            groupBox3.Font = new Font("Segoe UI", 12F);
            groupBox3.Location = new Point(543, 17);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(239, 128);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Docentes";
            // 
            // grpBoxAlumnos
            // 
            grpBoxAlumnos.Controls.Add(label1);
            grpBoxAlumnos.Controls.Add(lblTotalAlumnos);
            grpBoxAlumnos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpBoxAlumnos.ForeColor = Color.FromArgb(0, 0, 192);
            grpBoxAlumnos.Location = new Point(819, 17);
            grpBoxAlumnos.Margin = new Padding(3, 2, 3, 2);
            grpBoxAlumnos.Name = "grpBoxAlumnos";
            grpBoxAlumnos.Padding = new Padding(3, 2, 3, 2);
            grpBoxAlumnos.Size = new Size(239, 128);
            grpBoxAlumnos.TabIndex = 1;
            grpBoxAlumnos.TabStop = false;
            grpBoxAlumnos.Text = "Alumnos";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Gray;
            label1.Location = new Point(16, 90);
            label1.Name = "label1";
            label1.Size = new Size(186, 21);
            label1.TabIndex = 3;
            label1.Text = "Total alumnos inscritos";
            // 
            // lblTotalAlumnos
            // 
            lblTotalAlumnos.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalAlumnos.ForeColor = Color.FromArgb(0, 0, 192);
            lblTotalAlumnos.Location = new Point(5, 23);
            lblTotalAlumnos.Name = "lblTotalAlumnos";
            lblTotalAlumnos.Size = new Size(228, 61);
            lblTotalAlumnos.TabIndex = 2;
            lblTotalAlumnos.Text = "0";
            lblTotalAlumnos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmAdminMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1068, 499);
            Controls.Add(grpBoxAlumnos);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(grpBoxCarreras);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmAdminMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmAdminMain";
            Load += FrmAdminMain_Load;
            grpBoxCarreras.ResumeLayout(false);
            grpBoxCarreras.PerformLayout();
            grpBoxAlumnos.ResumeLayout(false);
            grpBoxAlumnos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpBoxCarreras;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox grpBoxAlumnos;
        private Label label2;
        private Label lblTotalCarreas;
        private Label label1;
        private Label lblTotalAlumnos;
    }
}