namespace DriverChallenge
{
    partial class TelaIniciarNovoJogo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaIniciarNovoJogo));
            listEscolheNacionalidade = new ComboBox();
            label1 = new Label();
            inputNomePiloto = new TextBox();
            buttonContinuar = new Button();
            label5 = new Label();
            inputSobrenomePiloto = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // listEscolheNacionalidade
            // 
            listEscolheNacionalidade.DropDownStyle = ComboBoxStyle.DropDownList;
            listEscolheNacionalidade.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            listEscolheNacionalidade.FormattingEnabled = true;
            listEscolheNacionalidade.Location = new Point(360, 215);
            listEscolheNacionalidade.Margin = new Padding(0);
            listEscolheNacionalidade.Name = "listEscolheNacionalidade";
            listEscolheNacionalidade.Size = new Size(150, 25);
            listEscolheNacionalidade.TabIndex = 10;
            listEscolheNacionalidade.SelectedIndexChanged += ListEscolheNacionalidade_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(312, 28);
            label1.Name = "label1";
            label1.Size = new Size(176, 21);
            label1.TabIndex = 11;
            label1.Text = "INFORME O SEU NOME";
            // 
            // inputNomePiloto
            // 
            inputNomePiloto.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            inputNomePiloto.ForeColor = SystemColors.WindowFrame;
            inputNomePiloto.Location = new Point(210, 64);
            inputNomePiloto.Margin = new Padding(0);
            inputNomePiloto.Name = "inputNomePiloto";
            inputNomePiloto.Size = new Size(380, 32);
            inputNomePiloto.TabIndex = 12;
            inputNomePiloto.TextChanged += InputNomePiloto_TextChanged;
            // 
            // buttonContinuar
            // 
            buttonContinuar.BackColor = SystemColors.ScrollBar;
            buttonContinuar.BackgroundImageLayout = ImageLayout.None;
            buttonContinuar.FlatAppearance.BorderSize = 0;
            buttonContinuar.FlatStyle = FlatStyle.Flat;
            buttonContinuar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonContinuar.ForeColor = SystemColors.ControlText;
            buttonContinuar.Location = new Point(210, 310);
            buttonContinuar.Margin = new Padding(0);
            buttonContinuar.Name = "buttonContinuar";
            buttonContinuar.Size = new Size(380, 60);
            buttonContinuar.TabIndex = 13;
            buttonContinuar.Text = "CONTINUAR";
            buttonContinuar.UseVisualStyleBackColor = false;
            buttonContinuar.Click += ButtonContinuar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = Color.White;
            label5.Location = new Point(210, 217);
            label5.Name = "label5";
            label5.Size = new Size(139, 21);
            label5.TabIndex = 14;
            label5.Text = "NACIONALIDADE: ";
            // 
            // inputSobrenomePiloto
            // 
            inputSobrenomePiloto.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            inputSobrenomePiloto.ForeColor = SystemColors.WindowFrame;
            inputSobrenomePiloto.Location = new Point(208, 152);
            inputSobrenomePiloto.Margin = new Padding(0);
            inputSobrenomePiloto.Name = "inputSobrenomePiloto";
            inputSobrenomePiloto.Size = new Size(380, 32);
            inputSobrenomePiloto.TabIndex = 15;
            inputSobrenomePiloto.TextChanged += InputSobrenomePiloto_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(286, 120);
            label2.Name = "label2";
            label2.Size = new Size(224, 21);
            label2.TabIndex = 16;
            label2.Text = "INFORME O SEU SOBRENOME";
            // 
            // TelaNewGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(80, 80, 80);
            ClientSize = new Size(800, 420);
            Controls.Add(label2);
            Controls.Add(inputSobrenomePiloto);
            Controls.Add(label5);
            Controls.Add(buttonContinuar);
            Controls.Add(inputNomePiloto);
            Controls.Add(label1);
            Controls.Add(listEscolheNacionalidade);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TelaNewGame";
            StartPosition = FormStartPosition.CenterScreen;
            Load += TelaIniciarNovoJogo_Load;
            ResumeLayout(false);
            PerformLayout();
            this.Text = "TelaIniciarNovoJogo";
        }

        #endregion
        private ComboBox listEscolheNacionalidade;
        private Label label1;
        private TextBox inputNomePiloto;
        private Button buttonContinuar;
        private Label label5;
        private TextBox inputSobrenomePiloto;
        private Label label2;
    }
}