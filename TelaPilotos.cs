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
    public partial class TelaPilotos : Form
    {
        Principal principal;
        Equipe[] equipe;
        Piloto[] piloto;
        public TelaPilotos(Principal principal, Equipe[] equipe, Piloto[] piloto)
        {
            InitializeComponent();
            this.principal = principal;
            this.equipe = equipe;
            this.piloto = piloto;
            // Manipular o evento CellDoubleClick
            dvgTelaPilotoExibirTodosPilotos.CellContentClick += DataGridViewPilotos_CellContentClick;
        }

        private void TelaPilotos_Load(object sender, EventArgs e)
        {
            CriarDataGridViewClassPilotos(dvgTelaPilotoExibirTodosPilotos);
            PreencherDataGridViewClassPilotos(dvgTelaPilotoExibirTodosPilotos);
            CriarDataGridViewHistoricoDoPiloto(dgvTelaPilotoExibirHistoricoPiloto);

        }
        private void DataGridViewPilotos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique duplo foi feito em uma célula válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int i = Convert.ToInt32(dvgTelaPilotoExibirTodosPilotos.Rows[e.RowIndex].Cells["Index"].Value);
                TpNomeCompletoPiloto.Text = piloto[i].NomePiloto + " " + piloto[i].SobrenomePiloto;
                TpIdadePiloto.Text = piloto[i].IdadePiloto.ToString() + " Anos";
                TpPaisPiloto.Text = piloto[i].NacionalidadePiloto;
                TpHabPiloto.Text = piloto[i].MediaPiloto.ToString();

                TpLargada.Text = piloto[i].Largada.ToString();
                TpConcentracao.Text = piloto[i].Concentracao.ToString();
                TpUltrapassagem.Text = piloto[i].Ultrapassagem.ToString();
                TpExperiencia.Text = piloto[i].Experiencia.ToString();
                TpRapidez.Text = piloto[i].Rapidez.ToString();
                TpChuva.Text = piloto[i].Chuva.ToString();
                TpAcertoDoCarro.Text = piloto[i].AcertoDoCarro.ToString();
                TpFisico.Text = piloto[i].Fisico.ToString();

                TpEquipePiloto.Text = piloto[i].EquipePiloto;
                TpSalarioPiloto.Text = piloto[i].SalarioPiloto == 0? "": piloto[i].SalarioPiloto.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
                TpStatusPiloto.Text = piloto[i].StatusPiloto;
                TpDuracaoPiloto.Text = piloto[i].ContratoPiloto == 0? "": piloto[i].ContratoPiloto.ToString();

                TpEquipePilotoProximoAno.Text = piloto[i].ProximoAnoEquipePiloto;
                TpSalarioPilotoProximoAno.Text = piloto[i].ProximoAnoSalarioPiloto == 0? "": piloto[i].ProximoAnoSalarioPiloto.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
                TpStatusPilotoProximoAno.Text = piloto[i].ProximoAnoStatusPiloto;
                TpDuracaoPilotoProximoAno.Text = piloto[i].ProximoAnoContratoPiloto == 0? "": piloto[i].ProximoAnoContratoPiloto.ToString();

                PreencherDataGridViewHistoricoPilotos(piloto[i].PilotosTemporadas, dgvTelaPilotoExibirHistoricoPiloto);
                AtualizarTabelas(dgvTelaPilotoExibirHistoricoPiloto);

                Color corPrincipal;
                Color corSecundaria;

                corPrincipal = ColorTranslator.FromHtml(piloto[i].Cor1);
                corSecundaria = ColorTranslator.FromHtml(piloto[i].Cor2);

                TpLabelCor1A.BackColor = corPrincipal;
                TpLabelCor1B.BackColor = corSecundaria;
                TpLabelCor2A.BackColor = corPrincipal;
                TpLabelCor2B.BackColor = corSecundaria;
                TpLabelCor3A.BackColor = corPrincipal;
                TpLabelCor3B.BackColor = corSecundaria;
                TpLabelCor4A.BackColor = corPrincipal;
                TpLabelCor4B.BackColor = corSecundaria;

            }
        }
        private void CriarDataGridViewClassPilotos(DataGridView dataGridViewPilotos)
        {
            DataTable classPilotos = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            classPilotos.Columns.Add("#", typeof(int));
            classPilotos.Columns.Add(sedeColumn);
            classPilotos.Columns.Add("Nome", typeof(string));
            classPilotos.Columns.Add("Idade", typeof(string));
            classPilotos.Columns.Add("Equipe", typeof(string));
            classPilotos.Columns.Add("Contrato", typeof(string));
            classPilotos.Columns.Add("Hab.", typeof(string));
            classPilotos.Columns.Add("Path", typeof(string));
            classPilotos.Columns.Add("Index", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dataGridViewPilotos.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dataGridViewPilotos.RowHeadersVisible = false;
            dataGridViewPilotos.AllowUserToAddRows = false;
            dataGridViewPilotos.AllowUserToDeleteRows = false;
            dataGridViewPilotos.AllowUserToOrderColumns = false;
            dataGridViewPilotos.AllowUserToResizeColumns = false;
            dataGridViewPilotos.AllowUserToResizeColumns = false;
            dataGridViewPilotos.AllowUserToResizeRows = false;
            dataGridViewPilotos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewPilotos.ScrollBars = ScrollBars.Vertical;
            dataGridViewPilotos.AllowUserToAddRows = false;
            dataGridViewPilotos.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dataGridViewPilotos.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewPilotos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dataGridViewPilotos.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewPilotos.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewPilotos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPilotos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewPilotos.DataSource = classPilotos;

            // Altura das linhas
            dataGridViewPilotos.RowTemplate.Height = 28;
            // Define a altura do cabeçalho das colunas
            dataGridViewPilotos.ColumnHeadersHeight = 24;

            // Defina a ordem de exibição das colunas com base nos índices
            dataGridViewPilotos.Columns["#"].DisplayIndex = 0;
            dataGridViewPilotos.Columns["Nac"].DisplayIndex = 1;
            dataGridViewPilotos.Columns["Nome"].DisplayIndex = 2;
            dataGridViewPilotos.Columns["Idade"].DisplayIndex = 3;
            dataGridViewPilotos.Columns["Equipe"].DisplayIndex = 4;
            dataGridViewPilotos.Columns["Contrato"].DisplayIndex = 5;
            dataGridViewPilotos.Columns["Hab."].DisplayIndex = 6;
            dataGridViewPilotos.Columns["Path"].DisplayIndex = 7;
            dataGridViewPilotos.Columns["Index"].DisplayIndex = 8;

            dataGridViewPilotos.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewPilotos.Columns["Path"].Visible = false;

            dataGridViewPilotos.Columns[0].Width = 30;
            dataGridViewPilotos.Columns[1].Width = 40;
            dataGridViewPilotos.Columns[2].Width = 110;
            dataGridViewPilotos.Columns[3].Width = 80;
            dataGridViewPilotos.Columns[4].Width = 100;
            dataGridViewPilotos.Columns[5].Width = 100;
            dataGridViewPilotos.Columns[6].Width = 30;
        }
        private void PreencherDataGridViewClassPilotos(DataGridView dataGridViewPilotos)
        {

            DataTable classPilotos = (DataTable)dataGridViewPilotos.DataSource;


            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < piloto.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                //row["#"] = i + 1;
                row["Nome"] = (piloto[i].NomePiloto + " " + piloto[i].SobrenomePiloto);
                row["Idade"] = piloto[i].IdadePiloto + " Anos";
                row["Equipe"] = piloto[i].EquipePiloto;
                if (piloto[i].ContratoPiloto == 0)
                {
                    row["Contrato"] = "Disponível";
                }
                else
                {
                    row["Contrato"] = piloto[i].ContratoPiloto;
                }
                row["Hab."] = piloto[i].MediaPiloto;
                row["Path"] = Path.Combine("Paises", piloto[i].NacionalidadePiloto + ".png");
                row["Index"] = i;

                classPilotos.Rows.Add(row);
            }

            // Ordenar pela terceira coluna (índice 2, pois os índices começam em 0)
            dataGridViewPilotos.Sort(dataGridViewPilotos.Columns[2], ListSortDirection.Ascending);

            // Atualizar a coluna "#" após a ordenação
            for (int i = 0; i < dataGridViewPilotos.Rows.Count; i++)
            {
                dataGridViewPilotos.Rows[i].Cells["#"].Value = i + 1;
            }

            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);
            }
            // Atualize o DataGridView para refletir as mudan�as
            dataGridViewPilotos.DataSource = classPilotos;

            // Limpe a seleção inicial
            dataGridViewPilotos.ClearSelection();
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
            dgv.RowTemplate.Height = 27;
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

            dgv.Columns[0].Width = 35;
            dgv.Columns[1].Width = 45;
            dgv.Columns[2].Width = 10;
            dgv.Columns[3].Width = 140;
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
        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
