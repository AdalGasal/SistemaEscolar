namespace SistemaEscolar.Presentation.Views
{
    partial class FrmAlumnoProfile
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
            grpBoxPersonalData = new GroupBox();
            cboSex = new ComboBox();
            label6 = new Label();
            dtpStartDate = new DateTimePicker();
            label5 = new Label();
            label4 = new Label();
            dtpBirthday = new DateTimePicker();
            label3 = new Label();
            txtMaternalName = new TextBox();
            label2 = new Label();
            txtLastName = new TextBox();
            label1 = new Label();
            txtName = new TextBox();
            grpBoxDetail = new GroupBox();
            numCode = new NumericUpDown();
            label7 = new Label();
            btnSave = new Button();
            grpBoxPersonalData.SuspendLayout();
            grpBoxDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCode).BeginInit();
            SuspendLayout();
            // 
            // grpBoxPersonalData
            // 
            grpBoxPersonalData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpBoxPersonalData.BackColor = Color.WhiteSmoke;
            grpBoxPersonalData.Controls.Add(cboSex);
            grpBoxPersonalData.Controls.Add(label6);
            grpBoxPersonalData.Controls.Add(dtpStartDate);
            grpBoxPersonalData.Controls.Add(label5);
            grpBoxPersonalData.Controls.Add(label4);
            grpBoxPersonalData.Controls.Add(dtpBirthday);
            grpBoxPersonalData.Controls.Add(label3);
            grpBoxPersonalData.Controls.Add(txtMaternalName);
            grpBoxPersonalData.Controls.Add(label2);
            grpBoxPersonalData.Controls.Add(txtLastName);
            grpBoxPersonalData.Controls.Add(label1);
            grpBoxPersonalData.Controls.Add(txtName);
            grpBoxPersonalData.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpBoxPersonalData.Location = new Point(12, 12);
            grpBoxPersonalData.Name = "grpBoxPersonalData";
            grpBoxPersonalData.Size = new Size(896, 362);
            grpBoxPersonalData.TabIndex = 0;
            grpBoxPersonalData.TabStop = false;
            grpBoxPersonalData.Text = "Datos personales";
            // 
            // cboSex
            // 
            cboSex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboSex.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSex.FormattingEnabled = true;
            cboSex.Items.AddRange(new object[] { "Masculino", "Femenino", "Indefinido" });
            cboSex.Location = new Point(473, 306);
            cboSex.Name = "cboSex";
            cboSex.Size = new Size(402, 33);
            cboSex.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(240, 281);
            label6.Name = "label6";
            label6.Size = new Size(155, 25);
            label6.TabIndex = 11;
            label6.Text = "Fecha de ingreso";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(240, 306);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(202, 33);
            dtpStartDate.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 281);
            label5.Name = "label5";
            label5.Size = new Size(186, 25);
            label5.TabIndex = 9;
            label5.Text = "Fecha de nacimiento";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(473, 278);
            label4.Name = "label4";
            label4.Size = new Size(52, 25);
            label4.TabIndex = 7;
            label4.Text = "Sexo";
            // 
            // dtpBirthday
            // 
            dtpBirthday.Format = DateTimePickerFormat.Short;
            dtpBirthday.Location = new Point(6, 306);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(202, 33);
            dtpBirthday.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 192);
            label3.Name = "label3";
            label3.Size = new Size(159, 25);
            label3.TabIndex = 5;
            label3.Text = "Apellido Materno";
            // 
            // txtMaternalName
            // 
            txtMaternalName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaternalName.Location = new Point(6, 220);
            txtMaternalName.Name = "txtMaternalName";
            txtMaternalName.Size = new Size(869, 33);
            txtMaternalName.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 115);
            label2.Name = "label2";
            label2.Size = new Size(152, 25);
            label2.TabIndex = 3;
            label2.Text = "Apellido Paterno";
            // 
            // txtLastName
            // 
            txtLastName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLastName.Location = new Point(6, 143);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(869, 33);
            txtLastName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 37);
            label1.Name = "label1";
            label1.Size = new Size(81, 25);
            label1.TabIndex = 1;
            label1.Text = "Nombre";
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(6, 65);
            txtName.Name = "txtName";
            txtName.Size = new Size(869, 33);
            txtName.TabIndex = 0;
            // 
            // grpBoxDetail
            // 
            grpBoxDetail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpBoxDetail.BackColor = Color.WhiteSmoke;
            grpBoxDetail.Controls.Add(numCode);
            grpBoxDetail.Controls.Add(label7);
            grpBoxDetail.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpBoxDetail.Location = new Point(12, 397);
            grpBoxDetail.Name = "grpBoxDetail";
            grpBoxDetail.Size = new Size(896, 132);
            grpBoxDetail.TabIndex = 1;
            grpBoxDetail.TabStop = false;
            grpBoxDetail.Text = "Información escolar";
            // 
            // numCode
            // 
            numCode.Location = new Point(6, 64);
            numCode.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            numCode.Name = "numCode";
            numCode.Size = new Size(202, 33);
            numCode.TabIndex = 8;
            numCode.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 36);
            label7.Name = "label7";
            label7.Size = new Size(92, 25);
            label7.TabIndex = 7;
            label7.Text = "Matrícula";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom;
            btnSave.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(355, 564);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(197, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmAlumnoProfile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(920, 641);
            Controls.Add(btnSave);
            Controls.Add(grpBoxDetail);
            Controls.Add(grpBoxPersonalData);
            Name = "FrmAlumnoProfile";
            Text = "Perfíl de alumno";
            Load += FrmAlumnoProfile_Load;
            grpBoxPersonalData.ResumeLayout(false);
            grpBoxPersonalData.PerformLayout();
            grpBoxDetail.ResumeLayout(false);
            grpBoxDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCode).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpBoxPersonalData;
        private Label label3;
        private TextBox txtMaternalName;
        private Label label2;
        private TextBox txtLastName;
        private Label label1;
        private TextBox txtName;
        private Label label4;
        private DateTimePicker dtpBirthday;
        private Label label5;
        private Label label6;
        private DateTimePicker dtpStartDate;
        private GroupBox grpBoxDetail;
        private ComboBox cboSex;
        private Label label7;
        private Button btnSave;
        private NumericUpDown numCode;
    }
}