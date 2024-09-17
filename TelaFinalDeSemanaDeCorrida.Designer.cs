using DriverChallenge.Properties;
using System.Windows.Forms;

namespace DriverChallenge
{
    partial class TelaFinalDeSemanaDeCorrida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaFinalDeSemanaDeCorrida));
            panel = new Panel();
            dvgTableF1 = new DataGridView();
            panel1 = new Panel();
            lbQualificacaoVoltas = new Label();
            pictureBox1 = new PictureBox();
            lbQualificacaoClima = new Label();
            panel4 = new Panel();
            labelNomeGp = new Label();
            labelSemanaGP = new Label();
            labelNomePais = new Label();
            pictureBoxBtnContinuarQualificacao = new PictureBox();
            pictureBoxPaisGP = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            progressBarQualificacao = new ProgressBar();
            labelTreinoCorrida = new Label();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dvgTableF1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBtnContinuarQualificacao).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPaisGP).BeginInit();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.FromArgb(230, 240, 240);
            panel.Controls.Add(dvgTableF1);
            panel.Location = new Point(250, 190);
            panel.Margin = new Padding(0);
            panel.Name = "panel";
            panel.Size = new Size(840, 550);
            panel.TabIndex = 2;
            // 
            // dvgTableF1
            // 
            dvgTableF1.AllowUserToAddRows = false;
            dvgTableF1.AllowUserToDeleteRows = false;
            dvgTableF1.AllowUserToResizeColumns = false;
            dvgTableF1.AllowUserToResizeRows = false;
            dvgTableF1.BorderStyle = BorderStyle.None;
            dvgTableF1.Dock = DockStyle.Fill;
            dvgTableF1.Location = new Point(0, 0);
            dvgTableF1.Margin = new Padding(0);
            dvgTableF1.Name = "dvgTableF1";
            dvgTableF1.ReadOnly = true;
            dvgTableF1.Size = new Size(840, 550);
            dvgTableF1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(lbQualificacaoVoltas);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(lbQualificacaoClima);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(pictureBoxBtnContinuarQualificacao);
            panel1.Controls.Add(pictureBoxPaisGP);
            panel1.Location = new Point(0, 10);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1340, 80);
            panel1.TabIndex = 3;
            // 
            // lbQualificacaoVoltas
            // 
            lbQualificacaoVoltas.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lbQualificacaoVoltas.ImageAlign = ContentAlignment.MiddleLeft;
            lbQualificacaoVoltas.Location = new Point(50, 10);
            lbQualificacaoVoltas.Margin = new Padding(0);
            lbQualificacaoVoltas.Name = "lbQualificacaoVoltas";
            lbQualificacaoVoltas.Padding = new Padding(5, 0, 0, 0);
            lbQualificacaoVoltas.Size = new Size(140, 20);
            lbQualificacaoVoltas.TabIndex = 6;
            lbQualificacaoVoltas.Text = "Voltas:  0 / 00";
            lbQualificacaoVoltas.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(20, 45);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(30, 20);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // lbQualificacaoClima
            // 
            lbQualificacaoClima.Font = new Font("Segoe UI", 8F);
            lbQualificacaoClima.ImageAlign = ContentAlignment.MiddleLeft;
            lbQualificacaoClima.Location = new Point(50, 45);
            lbQualificacaoClima.Margin = new Padding(0);
            lbQualificacaoClima.Name = "lbQualificacaoClima";
            lbQualificacaoClima.Padding = new Padding(5, 0, 0, 0);
            lbQualificacaoClima.Size = new Size(100, 20);
            lbQualificacaoClima.TabIndex = 4;
            lbQualificacaoClima.Text = "Clima: Chuva";
            lbQualificacaoClima.TextAlign = ContentAlignment.MiddleLeft;
            lbQualificacaoClima.Visible = false;
            // 
            // panel4
            // 
            panel4.Controls.Add(labelNomeGp);
            panel4.Controls.Add(labelSemanaGP);
            panel4.Controls.Add(labelNomePais);
            panel4.Location = new Point(520, 0);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(300, 80);
            panel4.TabIndex = 1;
            // 
            // labelNomeGp
            // 
            labelNomeGp.Font = new Font("Segoe UI", 10F);
            labelNomeGp.Location = new Point(75, 30);
            labelNomeGp.Margin = new Padding(0);
            labelNomeGp.Name = "labelNomeGp";
            labelNomeGp.Size = new Size(150, 20);
            labelNomeGp.TabIndex = 2;
            labelNomeGp.Text = "Nome";
            labelNomeGp.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelSemanaGP
            // 
            labelSemanaGP.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            labelSemanaGP.Location = new Point(75, 50);
            labelSemanaGP.Margin = new Padding(0);
            labelSemanaGP.Name = "labelSemanaGP";
            labelSemanaGP.Size = new Size(150, 30);
            labelSemanaGP.TabIndex = 1;
            labelSemanaGP.Text = "Semana 00 0000";
            labelSemanaGP.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelNomePais
            // 
            labelNomePais.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            labelNomePais.Location = new Point(75, 0);
            labelNomePais.Margin = new Padding(0);
            labelNomePais.Name = "labelNomePais";
            labelNomePais.Size = new Size(150, 30);
            labelNomePais.TabIndex = 0;
            labelNomePais.Text = "GP do Pais";
            labelNomePais.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBoxBtnContinuarQualificacao
            // 
            pictureBoxBtnContinuarQualificacao.Cursor = Cursors.Hand;
            pictureBoxBtnContinuarQualificacao.Image = Resources.menu_continuar_b;
            pictureBoxBtnContinuarQualificacao.Location = new Point(1140, 15);
            pictureBoxBtnContinuarQualificacao.Margin = new Padding(0);
            pictureBoxBtnContinuarQualificacao.Name = "pictureBoxBtnContinuarQualificacao";
            pictureBoxBtnContinuarQualificacao.Size = new Size(150, 50);
            pictureBoxBtnContinuarQualificacao.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxBtnContinuarQualificacao.TabIndex = 0;
            pictureBoxBtnContinuarQualificacao.TabStop = false;
            pictureBoxBtnContinuarQualificacao.Click += pictureBoxBtnContinuarQualificacao_Click;
            // 
            // pictureBoxPaisGP
            // 
            pictureBoxPaisGP.Location = new Point(20, 10);
            pictureBoxPaisGP.Margin = new Padding(0);
            pictureBoxPaisGP.Name = "pictureBoxPaisGP";
            pictureBoxPaisGP.Size = new Size(30, 20);
            pictureBoxPaisGP.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxPaisGP.TabIndex = 3;
            pictureBoxPaisGP.TabStop = false;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1340, 10);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Location = new Point(0, 90);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1340, 10);
            panel3.TabIndex = 5;
            // 
            // progressBarQualificacao
            // 
            progressBarQualificacao.Location = new Point(250, 140);
            progressBarQualificacao.Margin = new Padding(0);
            progressBarQualificacao.Name = "progressBarQualificacao";
            progressBarQualificacao.Size = new Size(840, 10);
            progressBarQualificacao.Style = ProgressBarStyle.Continuous;
            progressBarQualificacao.TabIndex = 8;
            // 
            // labelTreinoCorrida
            // 
            labelTreinoCorrida.BackColor = Color.Transparent;
            labelTreinoCorrida.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            labelTreinoCorrida.Location = new Point(570, 105);
            labelTreinoCorrida.Margin = new Padding(0);
            labelTreinoCorrida.Name = "labelTreinoCorrida";
            labelTreinoCorrida.Size = new Size(200, 30);
            labelTreinoCorrida.TabIndex = 9;
            labelTreinoCorrida.Text = "Treino Qualificação";
            labelTreinoCorrida.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TelaFinalDeSemanaDeCorrida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(80, 80, 80);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1340, 760);
            ControlBox = false;
            Controls.Add(labelTreinoCorrida);
            Controls.Add(progressBarQualificacao);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TelaFinalDeSemanaDeCorrida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TelaFinalDeSemanaDeCorrida";
            Load += TelaQualificacao_Load;
            panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dvgTableF1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxBtnContinuarQualificacao).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPaisGP).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel;
        private Panel panel1;
        private PictureBox pictureBoxBtnContinuarQualificacao;
        private Panel panel4;
        private Panel panel2;
        private Panel panel3;
        private Label labelNomePais;
        private Label labelSemanaGP;
        private Label labelNomeGp;
        private DataGridView dvgTableF1;
        private PictureBox pictureBoxPaisGP;
        private ProgressBar progressBarQualificacao;
        private Label labelTreinoCorrida;
        private Label lbQualificacaoVoltas;
        private PictureBox pictureBox1;
        private Label lbQualificacaoClima;
    }
}