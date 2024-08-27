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
    public partial class TelaEquipes : Form
    {
        Principal principal;
        Equipe[] equipe;
        Piloto[] piloto;
        Pista[] pista;
        public TelaEquipes(Principal principal, Equipe[] equipe, Piloto[] piloto)
        {
            InitializeComponent();
            this.principal = principal;
            this.equipe = equipe;
            this.piloto = piloto;
        }
        private void TelaEquipes_Load(object sender, EventArgs e)
        {
            CriarDataGridViewClassEquipes(dvgTelaEquipesExibirTodasEquipes);
            PreencherDataGridViewClassEquipes(dvgTelaEquipesExibirTodasEquipes);
            AtualizarTabelas(dvgTelaEquipesExibirTodasEquipes);

            CriarDataGridViewHistoricoDoPiloto(dgvTelaEquipeExibirHistoricoEquipe);

            // Manipular o evento CellDoubleClick
            dvgTelaEquipesExibirTodasEquipes.CellDoubleClick += dvgTelaEquipesExibirTodasEquipes_CellContentClick;
        }
        private void dvgTelaEquipesExibirTodasEquipes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique duplo foi feito em uma célula válida
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int i = Convert.ToInt32(dvgTelaEquipesExibirTodasEquipes.Rows[e.RowIndex].Cells["Index"].Value);
                TpNomeEquipe.Text = equipe[i].NomeEquipe;
                TpPaisEquipe.Text = equipe[i].Sede;

                TpAerodinamica.Text = equipe[i].Aerodinamica.ToString();
                TpFreio.Text = equipe[i].Freio.ToString();
                TpAsaDianteira.Text = equipe[i].AsaDianteira.ToString();
                TpAsaTraseira.Text = equipe[i].AsaTraseira.ToString();
                TpCambio.Text = equipe[i].Cambio.ToString();
                TpEletrico.Text = equipe[i].Eletrico.ToString();
                TpDirecao.Text = equipe[i].Direcao.ToString();
                TpConfiabilidade.Text = equipe[i].Confiabilidade.ToString();
                TpMedia.Text = equipe[i].MediaEquipe.ToString();

                TpSalarioPiloto1.Text = string.Format("R$ {0:N2}", equipe[i].PrimeiroPilotoSalario);
                TpContratoPiloto1.Text = string.Format(equipe[i].PrimeiroPilotoContrato.ToString());
                TpPiloto1.Text = string.Format(equipe[i].PrimeiroPiloto);

                TpSalarioPiloto2.Text = string.Format("R$ {0:N2}", equipe[i].SegundoPilotoSalario);
                TpContratoPiloto2.Text = string.Format(equipe[i].SegundoPilotoContrato.ToString());
                TpPiloto2.Text = string.Format(equipe[i].SegundoPiloto);

                TpMotor.Text = equipe[i].NameMotor;
                label25.Text = equipe[i].ValorDoMotor.ToString();

                PreencherDataGridViewHistoricoPilotos(equipe[i].equipeTemporadas, dgvTelaEquipeExibirHistoricoEquipe);
                AtualizarTabelasHistorico(dgvTelaEquipeExibirHistoricoEquipe);

                Color corPrincipal;
                Color corSecundaria;
                corPrincipal = ColorTranslator.FromHtml(equipe[i].Cor1);
                corSecundaria = ColorTranslator.FromHtml(equipe[i].Cor2);

                TpLabelCor1A.BackColor = corPrincipal;
                TpLabelCor1B.BackColor = corSecundaria;
                TpLabelCor3A.BackColor = corPrincipal;
                TpLabelCor3B.BackColor = corSecundaria;
            }
        }
        private void CriarDataGridViewClassEquipes(DataGridView dataGridViewEquipes)
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("#", typeof(int));
            classEquipes.Columns.Add(sedeColumn);
            classEquipes.Columns.Add("Nome", typeof(string));
            classEquipes.Columns.Add("P", typeof(int));
            classEquipes.Columns.Add("1º", typeof(int));
            classEquipes.Columns.Add("2º", typeof(int));
            classEquipes.Columns.Add("3º", typeof(int));
            classEquipes.Columns.Add("Path", typeof(string));
            classEquipes.Columns.Add("Index", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Sede";
            imageColumn.Name = "Sede";
            imageColumn.DataPropertyName = "Sede";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dataGridViewEquipes.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dataGridViewEquipes.RowHeadersVisible = false;
            dataGridViewEquipes.AllowUserToAddRows = false;
            dataGridViewEquipes.AllowUserToDeleteRows = false;
            dataGridViewEquipes.AllowUserToOrderColumns = false;
            dataGridViewEquipes.AllowUserToResizeColumns = false;
            dataGridViewEquipes.AllowUserToResizeColumns = false;
            dataGridViewEquipes.AllowUserToResizeRows = false;
            dataGridViewEquipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewEquipes.ScrollBars = ScrollBars.Vertical;
            dataGridViewEquipes.AllowUserToAddRows = false;
            dataGridViewEquipes.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dataGridViewEquipes.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dataGridViewEquipes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dataGridViewEquipes.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewEquipes.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewEquipes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewEquipes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewEquipes.DataSource = classEquipes;

            // Altura das linhas
            dataGridViewEquipes.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dataGridViewEquipes.ColumnHeadersHeight = 30;

            // Defina a ordem de exibi��o das colunas com base nos �ndices
            dataGridViewEquipes.Columns["#"].DisplayIndex = 0;
            dataGridViewEquipes.Columns["Sede"].DisplayIndex = 1;
            dataGridViewEquipes.Columns["Nome"].DisplayIndex = 2;
            dataGridViewEquipes.Columns["P"].DisplayIndex = 3;
            dataGridViewEquipes.Columns["1º"].DisplayIndex = 4;
            dataGridViewEquipes.Columns["2º"].DisplayIndex = 5;
            dataGridViewEquipes.Columns["3º"].DisplayIndex = 6;
            dataGridViewEquipes.Columns["Path"].DisplayIndex = 7;
            dataGridViewEquipes.Columns["Index"].DisplayIndex = 8;

            dataGridViewEquipes.Columns["Index"].Visible = false;
            dataGridViewEquipes.Columns["Path"].Visible = false;

            dataGridViewEquipes.Columns[0].Width = 30;
            dataGridViewEquipes.Columns[1].Width = 40;
            dataGridViewEquipes.Columns[2].Width = 250;
            dataGridViewEquipes.Columns[3].Width = 50;
            dataGridViewEquipes.Columns[4].Width = 40;
            dataGridViewEquipes.Columns[5].Width = 40;
            dataGridViewEquipes.Columns[6].Width = 40;
        }
        private void PreencherDataGridViewClassEquipes(DataGridView dataGridViewEquipes)
        {
            DataTable classEquipes = (DataTable)dataGridViewEquipes.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classEquipes.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < equipe.Length; i++)
            {
                DataRow row = classEquipes.NewRow();
                row["#"] = equipe[i].PosicaoAtualCampeonato;
                row["Nome"] = equipe[i].NomeEquipe;
                row["P"] = equipe[i].PontosCampeonato;
                row["1º"] = equipe[i].PrimeiroColocado;
                row["2º"] = equipe[i].SegundoColocado;
                row["3º"] = equipe[i].TerceiroColocado;
                row["Path"] = Path.Combine("Paises", equipe[i].Sede + ".png");
                row["Index"] = i;
                classEquipes.Rows.Add(row);
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classEquipes.Rows)
            {
                string imagePath = row["Path"].ToString();
                if (!string.IsNullOrEmpty(imagePath)) // Verifica se o caminho do arquivo n�o est� vazio
                {
                    row["Sede"] = Image.FromFile(imagePath);
                }
            }
            // Atualize o DataGridView para refletir as mudan�as
            dataGridViewEquipes.DataSource = classEquipes;

            // Limpe a seleção inicial
            dataGridViewEquipes.ClearSelection();

        }
        private void CriarDataGridViewHistoricoDoPiloto(DataGridView dgv)
        {
            DataTable histoticoEquipe = new DataTable();

            histoticoEquipe.Columns.Add("#", typeof(int));
            histoticoEquipe.Columns.Add("Ano", typeof(int));
            histoticoEquipe.Columns.Add("Motor", typeof(string));
            histoticoEquipe.Columns.Add("C1", typeof(string));
            histoticoEquipe.Columns.Add("P", typeof(string));
            histoticoEquipe.Columns.Add("1º", typeof(int));
            histoticoEquipe.Columns.Add("2º", typeof(int));
            histoticoEquipe.Columns.Add("3º", typeof(int));
            histoticoEquipe.Columns.Add("Serie", typeof(string));

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

            dgv.DataSource = histoticoEquipe;

            // Altura das linhas
            dgv.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dgv.ColumnHeadersHeight = 30;

            // Defina a ordem de exibiçao das colunas com base nos índices
            dgv.Columns["#"].DisplayIndex = 0;
            dgv.Columns["Ano"].DisplayIndex = 1;
            dgv.Columns["Motor"].DisplayIndex = 2;
            dgv.Columns["C1"].DisplayIndex = 3;
            dgv.Columns["P"].DisplayIndex = 4;
            dgv.Columns["1º"].DisplayIndex = 5;
            dgv.Columns["2º"].DisplayIndex = 6;
            dgv.Columns["3º"].DisplayIndex = 7;
            dgv.Columns["Serie"].DisplayIndex = 8;

            dgv.Columns["C1"].HeaderText = string.Empty;

            dgv.Columns[0].Width = 30;
            dgv.Columns[1].Width = 40;
            dgv.Columns[2].Width = 100;
            dgv.Columns[3].Width = 10;
            dgv.Columns[4].Width = 40;
            dgv.Columns[5].Width = 40;
            dgv.Columns[6].Width = 40;
            dgv.Columns[7].Width = 40;
            dgv.Columns[8].Width = 50;
        }
        private void PreencherDataGridViewHistoricoPilotos(List<Equipe.EquipeTemporada> equipeTemporadas, DataGridView dgv)
        {
            DataTable histoticoEquipe = (DataTable)dgv.DataSource;

            // Limpa as linhas do DataGridView
            histoticoEquipe.Rows.Clear();

            // Adiciona cada piloto campeão como uma nova linha no DataGridView
            foreach (var piloto in equipeTemporadas)
            {
                // Cria uma nova linha no DataTable
                DataRow row = histoticoEquipe.NewRow();


                // Adiciona os dados do piloto à linha do DataGridView
                row["#"] = piloto.Position;
                row["Ano"] = piloto.Ano;
                row["Motor"] = piloto.Motor;
                row["C1"] = piloto.Cor1;
                row["P"] = piloto.Pontos;
                row["1º"] = piloto.Primeiro;
                row["2º"] = piloto.Segundo;
                row["3º"] = piloto.Terceiro;
                row["Serie"] = piloto.CategoriaAtual;

                // Adiciona a linha ao DataTable
                histoticoEquipe.Rows.Add(row);
            }

            // Define o DataTable como a fonte de dados do DataGridView
            dgv.DataSource = histoticoEquipe;
        }
        public void AtualizarTabelasHistorico(DataGridView dgv)
        {
            DataTable histoticoEquipe = (DataTable)dgv.DataSource;

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
                string cor1Texto = dgv.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgv.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgv.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            dgv.ClearSelection();
        }
        public void AtualizarTabelas(DataGridView dgv)
        {
            DataTable classEquipes = (DataTable)dgv.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells["#"].Value = i + 1;
            }
            // Ordene automaticamente a coluna 4 do maior para o menor
            dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);

            dgv.ClearSelection();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
