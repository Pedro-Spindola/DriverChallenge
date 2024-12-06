namespace DriverChallenge
{
    partial class TelaDeRegistro
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
            buttonConfirmar = new Button();
            inputSenhaRegistro = new TextBox();
            inputNomeRegistro = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // buttonConfirmar
            // 
            buttonConfirmar.BackColor = SystemColors.ScrollBar;
            buttonConfirmar.BackgroundImageLayout = ImageLayout.None;
            buttonConfirmar.FlatAppearance.BorderSize = 0;
            buttonConfirmar.FlatStyle = FlatStyle.Flat;
            buttonConfirmar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            buttonConfirmar.ForeColor = SystemColors.ControlText;
            buttonConfirmar.Location = new Point(213, 350);
            buttonConfirmar.Margin = new Padding(0);
            buttonConfirmar.Name = "buttonConfirmar";
            buttonConfirmar.Size = new Size(380, 60);
            buttonConfirmar.TabIndex = 1;
            buttonConfirmar.Text = "CONFIRMAR";
            buttonConfirmar.UseVisualStyleBackColor = false;
            buttonConfirmar.Click += buttonConfirmar_Click;
            // 
            // inputSenhaRegistro
            // 
            inputSenhaRegistro.Font = new Font("Segoe UI", 14F);
            inputSenhaRegistro.ForeColor = SystemColors.WindowFrame;
            inputSenhaRegistro.Location = new Point(211, 250);
            inputSenhaRegistro.Margin = new Padding(0);
            inputSenhaRegistro.Name = "inputSenhaRegistro";
            inputSenhaRegistro.Size = new Size(380, 32);
            inputSenhaRegistro.TabIndex = 13;
            // 
            // inputNomeRegistro
            // 
            inputNomeRegistro.Font = new Font("Segoe UI", 14F);
            inputNomeRegistro.ForeColor = SystemColors.WindowFrame;
            inputNomeRegistro.Location = new Point(210, 100);
            inputNomeRegistro.Margin = new Padding(0);
            inputNomeRegistro.Name = "inputNomeRegistro";
            inputNomeRegistro.Size = new Size(380, 32);
            inputNomeRegistro.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(328, 200);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(146, 21);
            label2.TabIndex = 17;
            label2.Text = "INFORME A SENHA";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(328, 50);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(144, 21);
            label1.TabIndex = 18;
            label1.Text = "INFORME O NOME";
            // 
            // TelaDeRegistro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(80, 80, 80);
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(inputNomeRegistro);
            Controls.Add(inputSenhaRegistro);
            Controls.Add(buttonConfirmar);
            Name = "TelaDeRegistro";
            Text = "TelaDeRegistro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonConfirmar;
        private TextBox inputSenhaRegistro;
        private TextBox inputNomeRegistro;
        private Label label2;
        private Label label1;
    }
}