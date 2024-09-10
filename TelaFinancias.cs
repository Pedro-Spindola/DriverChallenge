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
            LoadTela();
        }

        private void LoadTela()
        {
            if (financia.Patrocinadores[0].ContratoValido == false && financia.Patrocinadores[0].TempoPropostaContrato == 0)
            {
                Tf_rescindir1_btn.Visible = false;
                Tf_aceitar1_btn.Visible = false;
                Tf_rejeitar1_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else if (financia.Patrocinadores[0].ContratoValido == false)
            {
                Tf_rescindir1_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else
            {
                Tf_aceitar1_btn.Visible = false;
                Tf_rejeitar1_btn.Visible = false;
                labelContratoRestantes01.Text = "RESTANTES";
            }
            if (financia.Patrocinadores[1].ContratoValido == false && financia.Patrocinadores[1].TempoPropostaContrato == 0)
            {
                Tf_rescindir2_btn.Visible = false;
                Tf_aceitar2_btn.Visible = false;
                Tf_rejeitar2_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else if (financia.Patrocinadores[1].ContratoValido == false)
            {
                Tf_rescindir2_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else
            {
                Tf_aceitar2_btn.Visible = false;
                Tf_rejeitar2_btn.Visible = false;
                labelContratoRestantes01.Text = "RESTANTES";
            }
            if (financia.Patrocinadores[2].ContratoValido == false && financia.Patrocinadores[2].TempoPropostaContrato == 0)
            {
                Tf_rescindir3_btn.Visible = false;
                Tf_aceitar3_btn.Visible = false;
                Tf_rejeitar3_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else if (financia.Patrocinadores[2].ContratoValido == false)
            {
                Tf_rescindir3_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else
            {
                Tf_aceitar3_btn.Visible = false;
                Tf_rejeitar3_btn.Visible = false;
                labelContratoRestantes01.Text = "RESTANTES";
            }
            if (financia.Patrocinadores[3].ContratoValido == false && financia.Patrocinadores[3].TempoPropostaContrato == 0)
            {
                Tf_rescindir4_btn.Visible = false;
                Tf_aceitar4_btn.Visible = false;
                Tf_rejeitar4_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else if (financia.Patrocinadores[3].ContratoValido == false)
            {
                Tf_rescindir4_btn.Visible = false;
                labelContratoRestantes01.Text = "CONTRATO";
            }
            else
            {
                Tf_aceitar4_btn.Visible = false;
                Tf_rejeitar4_btn.Visible = false;
                labelContratoRestantes01.Text = "RESTANTES";
            }

            tf_valorDeSalario.Text = financia.SalarioDaEquipe.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_valorTotalPatrocinadores.Text = financia.SalarioPatrocinadores.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_valorTotalEmConta.Text = financia.DinheiroJogadorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            tf_despesasEscritorio.Text = financia.CustoEscritorio.ToString("C", new System.Globalization.CultureInfo("pt-BR"));

            tf_nomePatrocinador1.Text = financia.Patrocinadores[0].NomeDaEmpresa;
            string caminhoImagem01 = Path.Combine("Paises", financia.Patrocinadores[0].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador1.Image = Image.FromFile(caminhoImagem01);
            
            if (financia.Patrocinadores[0].ContratoValido == false) { tf_contratoPatrocinador1.Text = ""; tf_valorPatrocinador1.Text = ""; }
            if (financia.Patrocinadores[0].ContratoValido) { tf_contratoPatrocinador1.Text = financia.Patrocinadores[0].TempoDeContratoSemanal.ToString() + " semanas"; tf_valorPatrocinador1.Text = financia.Patrocinadores[0].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }
            if(financia.Patrocinadores[0].ContratoValido == false && financia.Patrocinadores[0].TempoPropostaContrato > 0) { tf_contratoPatrocinador1.Text = financia.Patrocinadores[0].TempoDeContrato.ToString() + " meses"; tf_valorPatrocinador1.Text = financia.Patrocinadores[0].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }

            tf_nomePatrocinador2.Text = financia.Patrocinadores[1].NomeDaEmpresa;
            string caminhoImagem02 = Path.Combine("Paises", financia.Patrocinadores[1].NacionalidadeDaEmpresa + ".png");

            if (financia.Patrocinadores[1].ContratoValido == false) { tf_contratoPatrocinador2.Text = ""; tf_valorPatrocinador2.Text = ""; }
            if (financia.Patrocinadores[1].ContratoValido){ tf_contratoPatrocinador2.Text = financia.Patrocinadores[1].TempoDeContratoSemanal.ToString() + " semanas"; tf_nacPatrocinador2.Image = Image.FromFile(caminhoImagem02); tf_valorPatrocinador2.Text = financia.Patrocinadores[1].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR"));}
            if (financia.Patrocinadores[1].ContratoValido == false && financia.Patrocinadores[1].TempoPropostaContrato > 0) {tf_contratoPatrocinador2.Text = financia.Patrocinadores[1].TempoDeContrato.ToString() + " meses"; tf_nacPatrocinador2.Image = Image.FromFile(caminhoImagem02); tf_valorPatrocinador2.Text = financia.Patrocinadores[1].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }

            tf_nomePatrocinador3.Text = financia.Patrocinadores[2].NomeDaEmpresa;
            string caminhoImagem03 = Path.Combine("Paises", financia.Patrocinadores[2].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador3.Image = Image.FromFile(caminhoImagem03);

            if (financia.Patrocinadores[2].ContratoValido == false) { tf_contratoPatrocinador3.Text = ""; tf_valorPatrocinador3.Text = ""; }
            if (financia.Patrocinadores[2].ContratoValido) { tf_contratoPatrocinador3.Text = financia.Patrocinadores[2].TempoDeContratoSemanal.ToString() + " semanas"; tf_valorPatrocinador3.Text = financia.Patrocinadores[2].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }
            if (financia.Patrocinadores[2].ContratoValido == false && financia.Patrocinadores[2].TempoPropostaContrato > 0) { tf_contratoPatrocinador3.Text = financia.Patrocinadores[2].TempoDeContrato.ToString() + " meses"; tf_valorPatrocinador3.Text = financia.Patrocinadores[2].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }

            tf_nomePatrocinador4.Text = financia.Patrocinadores[3].NomeDaEmpresa;
            string caminhoImagem04 = Path.Combine("Paises", financia.Patrocinadores[3].NacionalidadeDaEmpresa + ".png");
            tf_nacPatrocinador4.Image = Image.FromFile(caminhoImagem04);
           
            if (financia.Patrocinadores[3].ContratoValido == false){ tf_contratoPatrocinador4.Text = ""; tf_valorPatrocinador4.Text = ""; }
            if (financia.Patrocinadores[3].ContratoValido){ tf_contratoPatrocinador4.Text = financia.Patrocinadores[3].TempoDeContratoSemanal.ToString() + " semanas"; tf_valorPatrocinador4.Text = financia.Patrocinadores[3].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }
            if (financia.Patrocinadores[3].ContratoValido == false && financia.Patrocinadores[3].TempoPropostaContrato > 0) { tf_contratoPatrocinador4.Text = financia.Patrocinadores[3].TempoDeContrato.ToString() + " meses"; tf_valorPatrocinador4.Text = financia.Patrocinadores[3].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Tf_aceitar1_btn_Click(object sender, EventArgs e)
        {
            financia.Patrocinadores[0].ContratoValido = true;
            Tf_aceitar1_btn.Visible = false;
            Tf_rejeitar1_btn.Visible = false;
            Tf_rescindir1_btn.Visible = true;
            financia.EspacoContratoDisponivel--;
            LoadTela();
        }

        private void Tf_rejeitar1_btn_Click(object sender, EventArgs e)
        {
            financia.limparPatrocinador(financia.Patrocinadores[0]);
            LoadTela();
        }

        private void Tf_aceitar2_btn_Click(object sender, EventArgs e)
        {
            financia.Patrocinadores[1].ContratoValido = true;
            Tf_aceitar2_btn.Visible = false;
            Tf_rejeitar2_btn.Visible = false;
            Tf_rescindir2_btn.Visible = true;
            financia.EspacoContratoDisponivel--;
            LoadTela();
        }

        private void Tf_rejeitar2_btn_Click(object sender, EventArgs e)
        {
            financia.limparPatrocinador(financia.Patrocinadores[1]);
            LoadTela();
        }

        private void Tf_aceitar3_btn_Click(object sender, EventArgs e)
        {
            financia.Patrocinadores[2].ContratoValido = true;
            Tf_aceitar3_btn.Visible = false;
            Tf_rejeitar3_btn.Visible = false;
            Tf_rescindir3_btn.Visible = true;
            financia.EspacoContratoDisponivel--;
            LoadTela();
        }

        private void Tf_rejeitar3_btn_Click(object sender, EventArgs e)
        {
            financia.limparPatrocinador(financia.Patrocinadores[2]);
            LoadTela();
        }

        private void Tf_aceitar4_btn_Click(object sender, EventArgs e)
        {
            financia.Patrocinadores[3].ContratoValido = true;
            Tf_aceitar4_btn.Visible = false;
            Tf_rejeitar4_btn.Visible = false;
            Tf_rescindir4_btn.Visible = true;
            financia.EspacoContratoDisponivel--;
            LoadTela();
        }

        private void Tf_rejeitar4_btn_Click(object sender, EventArgs e)
        {
            financia.limparPatrocinador(financia.Patrocinadores[3]);
            LoadTela();
        }

        private void Tf_rescindir1_btn_Click(object sender, EventArgs e)
        {
        }

        private void Tf_rescindir2_btn_Click(object sender, EventArgs e)
        {
        }

        private void Tf_rescindir3_btn_Click(object sender, EventArgs e)
        {
        }

        private void Tf_rescindir4_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
