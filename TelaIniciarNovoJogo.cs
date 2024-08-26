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
    public partial class TelaIniciarNovoJogo : Form
    {
        Principal principal;
        public TelaIniciarNovoJogo(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }
        private void TelaIniciarNovoJogo_Load(object sender, EventArgs e)
        {
            List<Pais> nomesPais = Pais.ObterListaDePaises();

            listEscolheNacionalidade.DataSource = nomesPais;
            listEscolheNacionalidade.DisplayMember = "Nome";
        }
        private void InputNomePiloto_TextChanged(object sender, EventArgs e)
        {
            principal.NomeJogador = inputNomePiloto.Text;
        }
        private void InputSobrenomePiloto_TextChanged(object sender, EventArgs e)
        {
            principal.SobrenomeJogador = inputSobrenomePiloto.Text;
        }

        private void ListEscolheNacionalidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            principal.NacionalidadeJogador = listEscolheNacionalidade.Text;
        }
        private void ButtonContinuar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(principal.NomeJogador))
            {
                if (!string.IsNullOrEmpty(principal.SobrenomeJogador))
                {
                    TelaPrincipal telaprincipal = new TelaPrincipal(principal);
                    this.Hide();
                    telaprincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sobrenome do jogador não pode ser em branco.");
                }
            }
            else
            {
                MessageBox.Show("Nome do jogador não pode ser em branco.");
                MessageBox.Show(principal.NomeJogador);
            }
        }
    }
}
