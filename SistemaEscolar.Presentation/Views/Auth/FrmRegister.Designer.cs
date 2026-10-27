namespace SistemaEscolar.Presentation.Views.Auth
{
    partial class FrmRegister
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
            label4 = new Label();
            btnRegister = new Button();
            grpBoxLogin = new GroupBox();
            txtPassword2 = new TextBox();
            label5 = new Label();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            grpBoxLogin.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(22, 80);
            label4.Name = "label4";
            label4.Size = new Size(570, 60);
            label4.TabIndex = 10;
            label4.Text = "Sistema de control escolar";
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRegister.Location = new Point(208, 482);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(173, 61);
            btnRegister.TabIndex = 9;
            btnRegister.Text = "Registrar";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // grpBoxLogin
            // 
            grpBoxLogin.Controls.Add(txtPassword2);
            grpBoxLogin.Controls.Add(label5);
            grpBoxLogin.Controls.Add(txtPassword);
            grpBoxLogin.Controls.Add(txtUsername);
            grpBoxLogin.Controls.Add(label2);
            grpBoxLogin.Controls.Add(label3);
            grpBoxLogin.Location = new Point(22, 143);
            grpBoxLogin.Name = "grpBoxLogin";
            grpBoxLogin.Size = new Size(570, 325);
            grpBoxLogin.TabIndex = 8;
            grpBoxLogin.TabStop = false;
            // 
            // txtPassword2
            // 
            txtPassword2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword2.Location = new Point(27, 255);
            txtPassword2.Name = "txtPassword2";
            txtPassword2.PasswordChar = '•';
            txtPassword2.Size = new Size(522, 34);
            txtPassword2.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(27, 224);
            label5.Name = "label5";
            label5.Size = new Size(171, 28);
            label5.TabIndex = 5;
            label5.Text = "Repita contraseña:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassword.Location = new Point(27, 163);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '•';
            txtPassword.Size = new Size(522, 34);
            txtPassword.TabIndex = 4;
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUsername.Location = new Point(27, 67);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(522, 34);
            txtUsername.TabIndex = 3;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(166, 42);
            label1.Name = "label1";
            label1.Size = new Size(270, 38);
            label1.TabIndex = 7;
            label1.Text = "Registro de usuario";
            // 
            // FrmRegister
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 559);
            Controls.Add(label4);
            Controls.Add(btnRegister);
            Controls.Add(grpBoxLogin);
            Controls.Add(label1);
            Name = "FrmRegister";
            Text = "FrmRegister";
            grpBoxLogin.ResumeLayout(false);
            grpBoxLogin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Button btnRegister;
        private GroupBox grpBoxLogin;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Label label2;
        private Label label3;
        private Label label1;
        private TextBox txtPassword2;
        private Label label5;
    }
}