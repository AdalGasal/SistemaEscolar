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
            label1.Location = new Point(181, 49);
            label1.Name = "label1";
            label1.Size = new Size(171, 30);
            label1.TabIndex = 0;
            label1.Text = "Inicio de sesión";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(24, 26);
            label2.Name = "label2";
            label2.Size = new Size(67, 21);
            label2.TabIndex = 1;
            label2.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(24, 99);
            label3.Name = "label3";
            label3.Size = new Size(92, 21);
            label3.TabIndex = 2;
            label3.Text = "Contraseña:";
            // 
            // grpBoxLogin
            // 
            grpBoxLogin.Controls.Add(txtPassword);
            grpBoxLogin.Controls.Add(txtUsername);
            grpBoxLogin.Controls.Add(label2);
            grpBoxLogin.Controls.Add(label3);
            grpBoxLogin.Location = new Point(25, 144);
            grpBoxLogin.Margin = new Padding(3, 2, 3, 2);
            grpBoxLogin.Name = "grpBoxLogin";
            grpBoxLogin.Padding = new Padding(3, 2, 3, 2);
            grpBoxLogin.Size = new Size(499, 186);
            grpBoxLogin.TabIndex = 3;
            grpBoxLogin.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(24, 122);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(454, 29);
            txtPassword.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(24, 50);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(454, 29);
            txtUsername.TabIndex = 3;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogin.Location = new Point(190, 359);
            btnLogin.Margin = new Padding(3, 2, 3, 2);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(151, 46);
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
            label4.Location = new Point(49, 79);
            label4.Name = "label4";
            label4.Size = new Size(453, 47);
            label4.TabIndex = 6;
            label4.Text = "Sistema de control escolar";
            // 
            // btnRegister
            // 
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            btnRegister.ForeColor = Color.Navy;
            btnRegister.Location = new Point(190, 410);
            btnRegister.Margin = new Padding(3, 2, 3, 2);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(151, 28);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Registrate aquí";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 470);
            Controls.Add(btnRegister);
            Controls.Add(label4);
            Controls.Add(btnLogin);
            Controls.Add(grpBoxLogin);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
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