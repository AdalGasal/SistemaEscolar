namespace SistemaEscolar.Presentation.Views.Auth
{
    partial class FrmLogin
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            grpBoxLogin = new GroupBox();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            btnLogin = new Button();
            label4 = new Label();
            btnRegister = new Button();
            grpBoxLogin.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(189, 65);
            label1.Name = "label1";
            label1.Size = new Size(218, 38);
            label1.TabIndex = 0;
            label1.Text = "Inicio de sesión";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(27, 35);
            label2.Name = "label2";
            label2.Size = new Size(83, 28);
            label2.TabIndex = 1;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(27, 132);
            label3.Name = "label3";
            label3.Size = new Size(114, 28);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            // 
            // grpBoxLogin
            // 
            grpBoxLogin.Controls.Add(txtPassword);
            grpBoxLogin.Controls.Add(txtUsername);
            grpBoxLogin.Controls.Add(label2);
            grpBoxLogin.Controls.Add(label3);
            grpBoxLogin.Location = new Point(29, 192);
            grpBoxLogin.Name = "grpBoxLogin";
            grpBoxLogin.Size = new Size(570, 248);
            grpBoxLogin.TabIndex = 3;
            grpBoxLogin.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(27, 163);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(518, 34);
            txtPassword.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(27, 67);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(518, 34);
            txtUsername.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(217, 479);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(173, 61);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Iniciar";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(29, 103);
            label4.Name = "label4";
            label4.Size = new Size(570, 60);
            label4.TabIndex = 6;
            label4.Text = "Sistema de control escolar";
            // 
            // btnRegister
            // 
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            btnRegister.ForeColor = Color.Navy;
            btnRegister.Location = new Point(217, 546);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(173, 38);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Registrate aquí";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 627);
            Controls.Add(btnRegister);
            Controls.Add(label4);
            Controls.Add(btnLogin);
            Controls.Add(grpBoxLogin);
            Controls.Add(label1);
            Name = "FrmLogin";
            Text = "FrmLogin";
            Load += FrmLogin_Load;
            grpBoxLogin.ResumeLayout(false);
            grpBoxLogin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private GroupBox grpBoxLogin;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnLogin;
        private Label label4;
        private Button btnRegister;
    }
}