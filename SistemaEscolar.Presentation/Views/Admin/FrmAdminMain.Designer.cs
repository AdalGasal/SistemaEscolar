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
            groupBox4 = new GroupBox();
            grpBoxCarreras.SuspendLayout();
            SuspendLayout();
            // 
            // grpBoxCarreras
            // 
            grpBoxCarreras.Controls.Add(label2);
            grpBoxCarreras.Controls.Add(lblTotalCarreas);
            grpBoxCarreras.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grpBoxCarreras.ForeColor = Color.FromArgb(0, 0, 192);
            grpBoxCarreras.Location = new Point(12, 23);
            grpBoxCarreras.Name = "grpBoxCarreras";
            grpBoxCarreras.Size = new Size(273, 171);
            grpBoxCarreras.TabIndex = 0;
            grpBoxCarreras.TabStop = false;
            grpBoxCarreras.Text = "Carreras";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(20, 120);
            label2.Name = "label2";
            label2.Size = new Size(213, 28);
            label2.TabIndex = 1;
            label2.Text = "Total carreras activas";
            // 
            // lblTotalCarreas
            // 
            lblTotalCarreas.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalCarreas.ForeColor = Color.FromArgb(0, 0, 192);
            lblTotalCarreas.Location = new Point(6, 39);
            lblTotalCarreas.Name = "lblTotalCarreas";
            lblTotalCarreas.Size = new Size(261, 81);
            lblTotalCarreas.TabIndex = 0;
            lblTotalCarreas.Text = "0";
            lblTotalCarreas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            groupBox2.Font = new Font("Segoe UI", 12F);
            groupBox2.Location = new Point(313, 23);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(273, 171);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Materias";
            // 
            // groupBox3
            // 
            groupBox3.Font = new Font("Segoe UI", 12F);
            groupBox3.Location = new Point(621, 23);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(273, 171);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Docentes";
            // 
            // groupBox4
            // 
            groupBox4.Font = new Font("Segoe UI", 12F);
            groupBox4.Location = new Point(936, 23);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(273, 171);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "Ahí vemos";
            // 
            // FrmAdminMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 665);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(grpBoxCarreras);
            Name = "FrmAdminMain";
            Text = "FrmAdminMain";
            Load += FrmAdminMain_Load;
            grpBoxCarreras.ResumeLayout(false);
            grpBoxCarreras.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpBoxCarreras;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label2;
        private Label lblTotalCarreas;
    }
}