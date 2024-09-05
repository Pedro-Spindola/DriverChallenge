using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverChallenge
{
    public partial class TelaFinancias : Form
    {
        Principal principal;
        Financia financia;
        public TelaFinancias(Principal principal, Financia financia)
        {
            this.principal = principal;
            this.financia = financia;
            InitializeComponent();
        }
        private void TelaFinancias_Load(object sender, EventArgs e)
        {
            tf_valorDeSalario.Text = financia.SalarioDaEquipe.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_valorTotalPatrocinadores.Text = financia.SalarioPatrocinadores.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_valorTotalEmConta.Text = financia.DinheiroJogadorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"));

            tf_nomePatrocinador1.Text = financia.Patrocinadores[0].NomeDaEmpresa;
            string caminhoImagem01 = Path.Combine("Paises", financia.Patrocinadores[0].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador1.Image = Image.FromFile(caminhoImagem01);
            tf_valorPatrocinador1.Text = financia.Patrocinadores[0].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_contratoPatrocinador1.Text = financia.Patrocinadores[0].TempoDeContrato.ToString() + " meses";

            tf_nomePatrocinador2.Text = financia.Patrocinadores[1].NomeDaEmpresa;
            string caminhoImagem02 = Path.Combine("Paises", financia.Patrocinadores[1].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador2.Image = Image.FromFile(caminhoImagem02);
            tf_valorPatrocinador2.Text = financia.Patrocinadores[1].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_contratoPatrocinador2.Text = financia.Patrocinadores[1].TempoDeContrato.ToString() + " meses";

            tf_nomePatrocinador3.Text = financia.Patrocinadores[2].NomeDaEmpresa;
            string caminhoImagem03 = Path.Combine("Paises", financia.Patrocinadores[2].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador3.Image = Image.FromFile(caminhoImagem03);
            tf_valorPatrocinador3.Text = financia.Patrocinadores[2].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_contratoPatrocinador3.Text = financia.Patrocinadores[2].TempoDeContrato.ToString() + " meses";

            tf_nomePatrocinador4.Text = financia.Patrocinadores[3].NomeDaEmpresa;
            string caminhoImagem04 = Path.Combine("Paises", financia.Patrocinadores[3].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador4.Image = Image.FromFile(caminhoImagem04);
            tf_valorPatrocinador4.Text = financia.Patrocinadores[3].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_contratoPatrocinador4.Text = financia.Patrocinadores[3].TempoDeContrato.ToString() + " meses";
        }


        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tf_aceitar1_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[0].ContratoValido == false)
            {
                financia.Patrocinadores[0].ContratoValido = true;
            }
        }

        private void tf_rejeitar1_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[0].ContratoValido == false)
            {
                financia.limparPatrocinador(financia.Patrocinadores[0]);
            }
        }

        private void tf_aceitar2_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[1].ContratoValido == false)
            {
                financia.Patrocinadores[1].ContratoValido = true;
            }
        }

        private void tf_rejeitar2_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[1].ContratoValido == false)
            {
                financia.limparPatrocinador(financia.Patrocinadores[1]);
            }
        }

        private void tf_aceitar3_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[2].ContratoValido == false)
            {
                financia.Patrocinadores[2].ContratoValido = true;
            }
        }

        private void tf_rejeitar3_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[2].ContratoValido == false)
            {
                financia.limparPatrocinador(financia.Patrocinadores[2]);
            }
        }

        private void tf_aceitar4_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[3].ContratoValido == false)
            {
                financia.Patrocinadores[3].ContratoValido = true;
            }
        }

        private void tf_rejeitar4_btn_Click(object sender, EventArgs e)
        {
            if (financia.Patrocinadores[3].ContratoValido == false)
            {
                financia.limparPatrocinador(financia.Patrocinadores[3]);
            }
        }
    }
}
