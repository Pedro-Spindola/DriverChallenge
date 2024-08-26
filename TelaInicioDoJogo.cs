using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverChallenge
{
    public partial class TelaInicioDoJogo : Form
    {
        readonly Principal principal = new Principal();
        public TelaInicioDoJogo()
        {
            InitializeComponent();
        }

        private void ButtonContinuar_Click(object sender, EventArgs e)
        {
            TelaPrincipal telaprincipal = new TelaPrincipal(principal);
            this.Hide();
            principal.ConfiguracaoInicioDoGame = 2;
            telaprincipal.ShowDialog();
            this.Close();
        }

        private void ButtonNovoJogo_Click(object sender, EventArgs e)
        {
            TelaIniciarNovoJogo telaIniciarNovoJogo = new TelaIniciarNovoJogo(principal);
            this.Hide();
            principal.ConfiguracaoInicioDoGame = 1;
            telaIniciarNovoJogo.ShowDialog();
            this.Close();
        }

        private void TelaInicioDoJogo_Load(object sender, EventArgs e){}
    }
}
