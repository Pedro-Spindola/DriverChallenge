namespace DriverChallenge
{
    partial class TelaInicioDoJogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaInicioDoJogo));
            buttonContinuar = new Button();
            buttonNovoJogo = new Button();
            buttonSobre = new Button();
            buttonConfiguracao = new Button();
            SuspendLayout();
            // 
            // buttonContinuar
            // 
            buttonContinuar.BackColor = SystemColors.ScrollBar;
            buttonContinuar.BackgroundImageLayout = ImageLayout.None;
            buttonContinuar.FlatAppearance.BorderSize = 0;
            buttonContinuar.FlatStyle = FlatStyle.Flat;
            buttonContinuar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            buttonContinuar.ForeColor = SystemColors.ControlText;
            buttonContinuar.Location = new Point(50, 50);
            buttonContinuar.Margin = new Padding(0);
            buttonContinuar.Name = "buttonContinuar";
            buttonContinuar.Size = new Size(380, 60);
            buttonContinuar.TabIndex = 0;
            buttonContinuar.Text = "CONTINUAR";
            buttonContinuar.UseVisualStyleBackColor = false;
            buttonContinuar.Click += ButtonContinuar_Click;
            // 
            // buttonNovoJogo
            // 
            buttonNovoJogo.BackColor = SystemColors.ScrollBar;
            buttonNovoJogo.BackgroundImageLayout = ImageLayout.None;
            buttonNovoJogo.FlatAppearance.BorderSize = 0;
            buttonNovoJogo.FlatStyle = FlatStyle.Flat;
            buttonNovoJogo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            buttonNovoJogo.ForeColor = SystemColors.ControlText;
            buttonNovoJogo.Location = new Point(50, 130);
            buttonNovoJogo.Margin = new Padding(0);
            buttonNovoJogo.Name = "buttonNovoJogo";
            buttonNovoJogo.Size = new Size(380, 60);
            buttonNovoJogo.TabIndex = 1;
            buttonNovoJogo.Text = "NOVO JOGO";
            buttonNovoJogo.UseVisualStyleBackColor = false;
            buttonNovoJogo.Click += ButtonNovoJogo_Click;
            // 
            // buttonSobre
            // 
            buttonSobre.BackColor = SystemColors.ScrollBar;
            buttonSobre.BackgroundImageLayout = ImageLayout.None;
            buttonSobre.FlatAppearance.BorderSize = 0;
            buttonSobre.FlatStyle = FlatStyle.Flat;
            buttonSobre.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            buttonSobre.Location = new Point(50, 210);
            buttonSobre.Margin = new Padding(0);
            buttonSobre.Name = "buttonSobre";
            buttonSobre.Size = new Size(380, 60);
            buttonSobre.TabIndex = 2;
            buttonSobre.Text = "SOBRE";
            buttonSobre.UseVisualStyleBackColor = false;
            // 
            // buttonConfiguracao
            // 
            buttonConfiguracao.BackColor = SystemColors.ScrollBar;
            buttonConfiguracao.BackgroundImageLayout = ImageLayout.None;
            buttonConfiguracao.FlatAppearance.BorderSize = 0;
            buttonConfiguracao.FlatStyle = FlatStyle.Flat;
            buttonConfiguracao.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            buttonConfiguracao.Location = new Point(50, 290);
            buttonConfiguracao.Margin = new Padding(0);
            buttonConfiguracao.Name = "buttonConfiguracao";
            buttonConfiguracao.Size = new Size(380, 60);
            buttonConfiguracao.TabIndex = 3;
            buttonConfiguracao.Text = "CONFIGURAÇÃO";
            buttonConfiguracao.UseVisualStyleBackColor = false;
            // 
            // TelaInicioDoJogo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(80, 80, 80);
            ClientSize = new Size(800, 450);
            Controls.Add(buttonConfiguracao);
            Controls.Add(buttonSobre);
            Controls.Add(buttonNovoJogo);
            Controls.Add(buttonContinuar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TelaInicioDoJogo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TelaInicioDoJogo";
            Load += TelaInicioDoJogo_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonContinuar;
        private Button buttonNovoJogo;
        private Button buttonSobre;
        private Button buttonConfiguracao;
    }
}