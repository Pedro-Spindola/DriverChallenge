using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DriverChallenge
{
    public partial class TelaEscritorio : Form
    {
        Principal principal;
        Equipe[] equipe;
        Piloto[] piloto;
        Financia financia;
        public TelaEscritorio(Principal principal, Equipe[] equipe, Piloto[] piloto, Financia financia)
        {
            InitializeComponent();
            this.principal = principal;
            this.equipe = equipe;
            this.piloto = piloto;
            this.financia = financia;
        }
        private void TelaEscritorio_Load(object sender, EventArgs e)
        {
            CriarDataGridViewHistoricoDoPiloto(DgvHistoricoJogador);
            PreencherDataGridViewHistoricoPilotos(piloto[principal.IndexDoJogador].PilotosTemporadas, DgvHistoricoJogador);
            AtualizarTabelas(DgvHistoricoJogador);
            LoadingTela();
            LoadingPropostas();
            AtualizarAtributos();
            comboBoxXp.SelectedItem = principal.OpcaoParaXP;
        }
        public void LoadingTela()
        {
            labelNomeJogador.Text = piloto[principal.IndexDoJogador].NomePiloto + " " + piloto[principal.IndexDoJogador].SobrenomePiloto;
            labelIdadeJogador.Text = piloto[principal.IndexDoJogador].IdadePiloto.ToString() + " Anos";
            labelNacionalidadeJogador.Text = piloto[principal.IndexDoJogador].NacionalidadePiloto;
            labelHabilidadeJogador.Text = piloto[principal.IndexDoJogador].MediaPiloto.ToString();
            labelContratoAtualNomeEquipe.Text = ("Contrato " + piloto[principal.IndexDoJogador].EquipePiloto).ToUpper();
            labelSalarioJogador.Text = piloto[principal.IndexDoJogador].SalarioPiloto == 0 ? "" : piloto[principal.IndexDoJogador].SalarioPiloto.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            labelTempoDeContratoJogador.Text = piloto[principal.IndexDoJogador].ContratoPiloto == 0 ? "" : piloto[principal.IndexDoJogador].ContratoPiloto.ToString();
            labelStatusDoJogador.Text = piloto[principal.IndexDoJogador].StatusPiloto;
            labelSaldoNaConta.Text = financia.DinheiroJogadorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            int titulosTotal = (piloto[principal.IndexDoJogador].TituloF1 + piloto[principal.IndexDoJogador].TituloF2 + piloto[principal.IndexDoJogador].TituloF3);
            labelTitulosTotal.Text = titulosTotal.ToString();
            labelVitoriaTotal.Text = piloto[principal.IndexDoJogador].VitoriaCorrida.ToString();
            labelPolePositionTotal.Text = piloto[principal.IndexDoJogador].PolePosition.ToString();
            labelGpDisputadoTotal.Text = piloto[principal.IndexDoJogador].GpDisputado.ToString();
        }
        public void LoadingPropostas()
        {
            if (piloto[principal.IndexDoJogador].PropostaDeContrato[0].PropostaAceita == false && piloto[principal.IndexDoJogador].PropostaDeContrato[0].TempoPropostaContrato == 0)
            {
                labelPropostaAceita1.Visible = false;
                Tf_aceitar1_btn.Visible = false;
                Tf_rejeitar1_btn.Visible = false;
            }
            else if (piloto[principal.IndexDoJogador].PropostaDeContrato[0].PropostaAceita == false)
            {
                labelPropostaAceita1.Visible = false;
            }
            else
            {
                labelPropostaAceita1.Visible = true;
                Tf_aceitar1_btn.Visible = false;
                Tf_rejeitar1_btn.Visible = false;
            }
            if (piloto[principal.IndexDoJogador].PropostaDeContrato[1].PropostaAceita == false && piloto[principal.IndexDoJogador].PropostaDeContrato[1].TempoPropostaContrato == 0)
            {
                labelPropostaAceita2.Visible = false;
                Tf_aceitar2_btn.Visible = false;
                Tf_rejeitar2_btn.Visible = false;
            }
            else if (piloto[principal.IndexDoJogador].PropostaDeContrato[1].PropostaAceita == false)
            {
                labelPropostaAceita2.Visible = false;
            }
            else
            {
                labelPropostaAceita2.Visible = true;
                Tf_aceitar2_btn.Visible = false;
                Tf_rejeitar2_btn.Visible = false;
            }

            tf_nomeEquipe01.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[0].NomeDaEquipe;
            string caminhoImagem01 = Path.Combine("Paises", piloto[principal.IndexDoJogador].PropostaDeContrato[0].NacionalidadeDaEquipe + ".png");
            tf_nacEquipe01.Image = Image.FromFile(caminhoImagem01);
            if(piloto[principal.IndexDoJogador].PropostaDeContrato[0].TempoPropostaContrato == 0) { tf_valorProposta01.Text = ""; tf_tempoContrato01.Text = ""; tf_statusContrato01.Text = ""; }
            if(piloto[principal.IndexDoJogador].PropostaDeContrato[0].TempoPropostaContrato != 0) { tf_valorProposta01.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[0].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); tf_tempoContrato01.Text = "Até " + piloto[principal.IndexDoJogador].PropostaDeContrato[0].TempoDeContrato.ToString(); tf_statusContrato01.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[0].StatusDoPiloto; }
            tf_nomeEquipe02.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[1].NomeDaEquipe;
            string caminhoImagem02 = Path.Combine("Paises", piloto[principal.IndexDoJogador].PropostaDeContrato[1].NacionalidadeDaEquipe + ".png");
            tf_nacEquipe02.Image = Image.FromFile(caminhoImagem02);
            if (piloto[principal.IndexDoJogador].PropostaDeContrato[1].TempoPropostaContrato == 0) { tf_valorProposta02.Text = ""; tf_tempoContrato02.Text = ""; tf_statusContrato02.Text = ""; }
            if (piloto[principal.IndexDoJogador].PropostaDeContrato[1].TempoPropostaContrato != 0) { tf_valorProposta02.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[1].ValorContrato.ToString("C", new System.Globalization.CultureInfo("pt-BR")); tf_tempoContrato02.Text = "Até " + piloto[principal.IndexDoJogador].PropostaDeContrato[1].TempoDeContrato.ToString(); tf_statusContrato02.Text = piloto[principal.IndexDoJogador].PropostaDeContrato[1].StatusDoPiloto; }
        }
        public void AtualizarAtributos()
        {
            labelPontosDisponivel.Text = "PONTOS DISPONÍVEL " + ((int)Math.Floor(piloto[principal.IndexDoJogador].XpPiloto)).ToString();
            TpLargada.Text = piloto[principal.IndexDoJogador].Largada.ToString();
            TpConcentracao.Text = piloto[principal.IndexDoJogador].Concentracao.ToString();
            TpUltrapassagem.Text = piloto[principal.IndexDoJogador].Ultrapassagem.ToString();
            TpExperiencia.Text = piloto[principal.IndexDoJogador].Experiencia.ToString();
            TpRapidez.Text = piloto[principal.IndexDoJogador].Rapidez.ToString();
            TpChuva.Text = piloto[principal.IndexDoJogador].Chuva.ToString();
            TpAcertoDoCarro.Text = piloto[principal.IndexDoJogador].AcertoDoCarro.ToString();
            TpFisico.Text = piloto[principal.IndexDoJogador].Fisico.ToString();
        }
        private void CriarDataGridViewHistoricoDoPiloto(DataGridView dgv)
        {
            DataTable histoticoPiloto = new DataTable();

            histoticoPiloto.Columns.Add("#", typeof(int));
            histoticoPiloto.Columns.Add("Ano", typeof(int));
            histoticoPiloto.Columns.Add("C1", typeof(string));
            histoticoPiloto.Columns.Add("Equipe", typeof(string));
            histoticoPiloto.Columns.Add("P", typeof(string));
            histoticoPiloto.Columns.Add("Serie", typeof(string));

            // Configurando Layout
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ScrollBars = ScrollBars.Vertical;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.GridColor = Color.FromArgb(220, 220, 220);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.DataSource = histoticoPiloto;

            // Altura das linhas
            dgv.RowTemplate.Height = 25;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;

            // Defina a ordem de exibiçao das colunas com base nos índices
            dgv.Columns["#"].DisplayIndex = 0;
            dgv.Columns["Ano"].DisplayIndex = 1;
            dgv.Columns["C1"].DisplayIndex = 2;
            dgv.Columns["Equipe"].DisplayIndex = 3;
            dgv.Columns["P"].DisplayIndex = 4;
            dgv.Columns["Serie"].DisplayIndex = 5;

            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 30;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 10;
            dgv.Columns[3].Width = 190;
            dgv.Columns[4].Width = 60;
            dgv.Columns[5].Width = 100;
        }
        private void PreencherDataGridViewHistoricoPilotos(List<Piloto.PilotoTemporada> pilotosTemporadas, DataGridView dgv)
        {
            DataTable histoticoPiloto = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            histoticoPiloto.Rows.Clear();

            // Adiciona cada piloto campeão como uma nova linha no DataGridView
            foreach (var piloto in pilotosTemporadas)
            {
                // Cria uma nova linha no DataTable
                DataRow row = histoticoPiloto.NewRow();


                // Adiciona os dados do piloto à linha do DataGridView
                row["#"] = piloto.Position;
                row["Ano"] = piloto.Ano;
                row["C1"] = piloto.Cor1;
                row["Equipe"] = piloto.Equipe;
                row["P"] = piloto.Pontos;
                row["Serie"] = piloto.CategoriaAtual;

                // Adiciona a linha ao DataTable
                histoticoPiloto.Rows.Add(row);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = histoticoPiloto;
        }
        public void AtualizarTabelas(DataGridView dgv)
        {
            DataTable histoticoPiloto = (DataTable)dgv.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Ordene automaticamente a coluna 4 do maior para o menor
            dgv.Sort(dgv.Columns[1], ListSortDirection.Descending);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgv.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgv.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgv.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            dgv.ClearSelection();
        }
        private void label14_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Tf_aceitar1_btn_Click(object sender, EventArgs e)
        {
            foreach (Equipe equipe in equipe)
            {
                if (equipe.NomeEquipe == piloto[principal.IndexDoJogador].PropostaDeContrato[0].NomeDaEquipe)
                {
                    piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[0].TempoDeContrato;
                    piloto[principal.IndexDoJogador].ProximoAnoEquipePiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[0].NomeDaEquipe;
                    piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[0].ValorContrato;
                    piloto[principal.IndexDoJogador].ProximoAnoStatusPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[0].StatusDoPiloto;

                    if (piloto[principal.IndexDoJogador].PropostaDeContrato[0].StatusDoPiloto == "1º Piloto")
                    {
                        equipe.ProximoAnoPrimeiroPiloto = $"{piloto[principal.IndexDoJogador].NomePiloto} {piloto[principal.IndexDoJogador].SobrenomePiloto}";
                        equipe.ProximoAnoPrimeiroPilotoContrato = piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto;
                        equipe.ProximoAnoPrimeiroPilotoSalario = piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto;
                    }
                    else
                    {
                        equipe.ProximoAnoSegundoPiloto = $"{piloto[principal.IndexDoJogador].NomePiloto} {piloto[principal.IndexDoJogador].SobrenomePiloto}";
                        equipe.ProximoAnoSegundoPilotoContrato = piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto;
                        equipe.ProximoAnoSegundoPilotoSalario = piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto;
                    }
                }
            }
            piloto[principal.IndexDoJogador].PropostaDeContrato[0].PropostaAceita = true;
            labelPropostaAceita1.Visible = true;
            Tf_aceitar1_btn.Visible = false;
            Tf_rejeitar1_btn.Visible = false;
            LoadingPropostas();
        }
        private void Tf_rejeitar1_btn_Click(object sender, EventArgs e)
        {
            foreach (Equipe equipe in equipe)
            {
                if (equipe.NomeEquipe == piloto[principal.IndexDoJogador].PropostaDeContrato[0].NomeDaEquipe)
                {
                    if (piloto[principal.IndexDoJogador].PropostaDeContrato[0].StatusDoPiloto == "1º Piloto")
                    {
                        equipe.ProximoAnoPrimeiroPiloto = "";
                    }
                    else
                    {
                        equipe.ProximoAnoSegundoPiloto = "";
                    }

                }
            }
            piloto[principal.IndexDoJogador].LimparPropostaDeContrato(piloto[principal.IndexDoJogador].PropostaDeContrato[0]);
            Tf_aceitar1_btn.Visible = false;
            Tf_rejeitar1_btn.Visible = false;
        }
        private void Tf_aceitar2_btn_Click(object sender, EventArgs e)
        {
            foreach (Equipe equipe in equipe)
            {
                if (equipe.NomeEquipe == piloto[principal.IndexDoJogador].PropostaDeContrato[1].NomeDaEquipe)
                {
                    piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[1].TempoDeContrato;
                    piloto[principal.IndexDoJogador].ProximoAnoEquipePiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[1].NomeDaEquipe;
                    piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[1].ValorContrato;
                    piloto[principal.IndexDoJogador].ProximoAnoStatusPiloto = piloto[principal.IndexDoJogador].PropostaDeContrato[1].StatusDoPiloto;

                    if (piloto[principal.IndexDoJogador].PropostaDeContrato[1].StatusDoPiloto == "1º Piloto")
                    {
                        equipe.ProximoAnoPrimeiroPiloto = $"{piloto[principal.IndexDoJogador].NomePiloto} {piloto[principal.IndexDoJogador].SobrenomePiloto}";
                        equipe.ProximoAnoPrimeiroPilotoContrato = piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto;
                        equipe.ProximoAnoPrimeiroPilotoSalario = piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto;
                    }
                    else
                    {
                        equipe.ProximoAnoSegundoPiloto = $"{piloto[principal.IndexDoJogador].NomePiloto} {piloto[principal.IndexDoJogador].SobrenomePiloto}";
                        equipe.ProximoAnoSegundoPilotoContrato = piloto[principal.IndexDoJogador].ProximoAnoContratoPiloto;
                        equipe.ProximoAnoSegundoPilotoSalario = piloto[principal.IndexDoJogador].ProximoAnoSalarioPiloto;
                    }
                }
            }
            piloto[principal.IndexDoJogador].PropostaDeContrato[1].PropostaAceita = true;
            labelPropostaAceita2.Visible = true;
            Tf_aceitar2_btn.Visible = false;
            Tf_rejeitar2_btn.Visible = false;
            LoadingPropostas();
        }
        private void Tf_rejeitar2_btn_Click(object sender, EventArgs e)
        {
            foreach (Equipe equipe in equipe)
            {
                if (equipe.NomeEquipe == piloto[principal.IndexDoJogador].PropostaDeContrato[0].NomeDaEquipe)
                {
                    if (piloto[principal.IndexDoJogador].PropostaDeContrato[0].StatusDoPiloto == "1º Piloto")
                    {
                        equipe.ProximoAnoPrimeiroPiloto = "";
                    }
                    else
                    {
                        equipe.ProximoAnoSegundoPiloto = "";
                    }

                }
            }
            piloto[principal.IndexDoJogador].LimparPropostaDeContrato(piloto[principal.IndexDoJogador].PropostaDeContrato[1]);
            Tf_aceitar2_btn.Visible = false;
            Tf_rejeitar2_btn.Visible = false;
        }
        private void TpLargada_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Largada < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Largada++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpConcentracao_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Concentracao < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Concentracao++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpUltrapassagem_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Ultrapassagem < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Ultrapassagem++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpExperiencia_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Experiencia < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Experiencia++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpRapidez_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Rapidez < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Rapidez++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpChuva_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Chuva < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Chuva++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpAcertoDoCarro_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].AcertoDoCarro < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].AcertoDoCarro++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void TpFisico_1_Click(object sender, EventArgs e)
        {
            if (piloto[principal.IndexDoJogador].Fisico < 100 && piloto[principal.IndexDoJogador].XpPiloto >= 1)
            {
                piloto[principal.IndexDoJogador].XpPiloto--;
                piloto[principal.IndexDoJogador].Fisico++;
                piloto[principal.IndexDoJogador].AtualizarMedia();
                AtualizarAtributos();
            }
        }
        private void comboBoxXp_SelectedIndexChanged(object sender, EventArgs e)
        {
            principal.OpcaoParaXP = comboBoxXp.SelectedItem.ToString();
        }
    }
}
