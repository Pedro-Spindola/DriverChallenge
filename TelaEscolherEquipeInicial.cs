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
    public partial class TelaEscolherEquipeInicial : Form
    {
        Principal principal;
        Equipe[] equipes;
        Piloto[] pilotos;
        Random random = new Random();
        private double a, b, c;
        private int d, ee, f;
        public TelaEscolherEquipeInicial(Principal principal, Equipe[] equipes, Piloto[] pilotos)
        {
            this.principal = principal;
            this.equipes = equipes;
            this.pilotos = pilotos;
            InitializeComponent();
            PreencherPropostas();
        }

        private void labelAssinar1_Click(object sender, EventArgs e)
        {
            pilotos[99].SalarioPiloto = a;
            pilotos[99].ContratoPiloto = d;
            pilotos[99].Categoria = equipes[27].Categoria;
            principal.Transferencia(pilotos, 55, 99);
            this.Close();
        }

        private void labelAssinar2_Click(object sender, EventArgs e)
        {
            pilotos[99].SalarioPiloto = b;
            pilotos[99].ContratoPiloto = ee;
            pilotos[99].Categoria = equipes[28].Categoria;
            principal.Transferencia(pilotos, 57, 99);
            this.Close();
        }

        private void labelAssinar3_Click(object sender, EventArgs e)
        {
            pilotos[99].SalarioPiloto = c;
            pilotos[99].Categoria = equipes[0].Categoria; // pilotos[99].Categoria = equipes[29].Categoria;
            principal.Transferencia(pilotos, 1, 99); // principal.Transferencia(pilotos, 59, 99);
            this.Close();
        }

        public void PreencherPropostas()
        {
            panelCorP1.BackColor = ColorTranslator.FromHtml(equipes[27].Cor1);
            panelCorS1.BackColor = ColorTranslator.FromHtml(equipes[27].Cor2);
            labelNomeEquipe1.Text = equipes[27].NomeEquipe;
            a = DefinirSalario(pilotos[99].MediaPiloto, equipes[27].Categoria);
            labelSalario1.Text = "Salário: " + string.Format("R$ {0:N2}", a);
            d = ((random.Next(1, 4) + principal.ContadorDeAno) - 1);
            labelContrato1.Text = "Contrato: " + d + " ano(s)";
            labelStatus1.Text = "Status: 2º Piloto";
            labelAssinar1.ForeColor = ColorTranslator.FromHtml(equipes[27].Cor2);
            labelAssinar1.BackColor = ColorTranslator.FromHtml(equipes[27].Cor1);
            //53

            panelCorP2.BackColor = ColorTranslator.FromHtml(equipes[28].Cor1);
            panelCorS2.BackColor = ColorTranslator.FromHtml(equipes[28].Cor2);
            labelNomeEquipe2.Text = equipes[28].NomeEquipe;
            b = DefinirSalario(pilotos[99].MediaPiloto, equipes[28].Categoria);
            labelSalario2.Text = "Salário: " + string.Format("R$ {0:N2}", b);
            ee = ((random.Next(1, 4) + principal.ContadorDeAno) - 1);
            labelContrato2.Text = "Contrato: " + ee + " ano(s)";
            labelStatus2.Text = "Status: 2º Piloto";
            labelAssinar2.ForeColor = ColorTranslator.FromHtml(equipes[28].Cor1);
            labelAssinar2.BackColor = ColorTranslator.FromHtml(equipes[28].Cor2);
            //55

            panelCorP3.BackColor = ColorTranslator.FromHtml(equipes[29].Cor2);
            panelCorS3.BackColor = ColorTranslator.FromHtml(equipes[29].Cor1);
            labelNomeEquipe3.Text = equipes[29].NomeEquipe;
            c = DefinirSalario(pilotos[99].MediaPiloto, equipes[29].Categoria);
            labelSalario3.Text = "Salário: " + string.Format("R$ {0:N2}", c);
            f = ((random.Next(1, 4) + principal.ContadorDeAno) - 1);
            labelContrato3.Text = "Contrato: " + f + " ano(s)";
            labelStatus3.Text = "Status: 2º Piloto";
            labelAssinar3.ForeColor = ColorTranslator.FromHtml(equipes[29].Cor2);
            labelAssinar3.BackColor = ColorTranslator.FromHtml(equipes[29].Cor1);
            //57
        }
        public double DefinirSalario(int medHab, string cat)
        {
            if (cat == "F1")
            {
                int hab = medHab * 20;
                int bases = random.Next(10000, 12001);
                int bonus = random.Next(5000, 10001);
                int sal = (((hab * bases) / 200) + bonus);
                return sal;
            }
            else if (cat == "F2")
            {
                int hab = medHab * 20;
                int bases = random.Next(8000, 10001);
                int bonus = random.Next(5000, 10001);
                int sal = (((hab * bases) / 200) + bonus);
                return sal;
            }
            else if (cat == "F3")
            {
                int hab = medHab * 20;
                int bases = random.Next(6000, 8001);
                int bonus = random.Next(5000, 10001);
                int sal = (((hab * bases) / 200) + bonus);
                return sal;
            }
            else { return 0; }
        }
    }
}
