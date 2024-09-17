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
    public partial class TelaFinalDeSemanaDeCorrida : Form
    {
        Principal principal;
        Equipe[] equipes;
        Piloto[] pilotos;
        Pista[] pistas;
        string categoria = "";
        string listcategoria = "";
        Random random = new Random();
        private int btnclick = 0;
        private int numberVoltasF = 0, numberVoltasT = 0;
        private int indexDoJogador;
        Color corPrincipal;
        Color corSecundaria;
        Color corTexto;
        public TelaFinalDeSemanaDeCorrida(Principal principal, Equipe[] equipes, Piloto[] pilotos, Pista[] pistas)
        {
            InitializeComponent();
            this.principal = principal;
            this.equipes = equipes;
            this.pilotos = pilotos;
            this.pistas = pistas;
        }
        private void TelaQualificacao_Load(object sender, EventArgs e)
        {
            corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
            corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
            indexDoJogador = principal.IndexDoJogador;

            AtualizarNomes();

            if (pilotos[indexDoJogador].Categoria == "F1")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(0, 10, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else if (pilotos[indexDoJogador].Categoria == "F2")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(10, 20, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(10, 20, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else if (pilotos[indexDoJogador].Categoria == "F3")
            {
                CriarDataGridViewQualificacaoEquipesF1(dvgTableF1);
                PreencherDataGridViewQualificacaoEquipesF1(20, 30, dvgTableF1);
                AtualizarDataGridViewQualificacaoEquipesF1(20, 30, dvgTableF1);
                AtualizarTabelasQualificacaoVoltas(dvgTableF1);
            }
            else
            {
                // Obtém a lista de categorias
                List<Principal> categorias = Principal.ObterListaCategoria();
                string fCategoria = pilotos[indexDoJogador].Categoria;

                // Itera sobre cada categoria na lista
                foreach (var categoria in categorias)
                {
                    if (categoria.Categoria != fCategoria && categoria.Categoria == "F1")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(0, 10, "F1");
                    }
                    else if (categoria.Categoria != fCategoria && categoria.Categoria == "F2")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(10, 20, "F2");
                    }
                    else if (categoria.Categoria != fCategoria && categoria.Categoria == "F3")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(20, 30, "F3");
                    }
                }
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].TempoDeVoltaQualificacao = 0;
                    pilotos[j].TempoCorrida = 0;
                    pilotos[j].TempoDeVoltaCorrida = 0;
                    pilotos[j].QualificacaoParaCorrida = 0;
                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = 0;
                    pilotos[j].PosicaoNaVoltaMaisRapida = 0;
                    pilotos[j].PosicaoNaCorrida = 0;
                    pilotos[j].DiferancaAnt = 0;
                    pilotos[j].DiferancaPri = 0;
                    pilotos[j].BonusRandom = 0;
                }
                MetodoParaQualificarEquipes(0, 10);
                MetodoParaQualificarEquipes(10, 20);
                MetodoParaQualificarEquipes(20, 30);
                MetodoParaQualificarPilotos("F1");
                MetodoParaQualificarPilotos("F2");
                MetodoParaQualificarPilotos("F3");
                this.Close();
            }
            labelTreinoCorrida.Text = "Treino";
        }
        private void AtualizarNomes()
        {
            labelNomeGp.Text = string.Format("GP do {0:D2}", principal.ProximoGp);
            labelNomePais.Text = principal.ProximoGpPais;
            labelSemanaGP.Text = string.Format("Semana {0:D2} / {1}", principal.ProximoGpSemana, principal.ContadorDeAno);
            pictureBoxPaisGP.ImageLocation = Path.Combine("Paises", principal.ProximoGp + ".png");
            lbQualificacaoClima.Text = string.Format("Clima: ");
            numberVoltasT = principal.ProximoGpVoltas;
            lbQualificacaoVoltas.Text = string.Format("Voltas: " + numberVoltasF + " / " + numberVoltasT);

            if (principal.CorTexto == "Branco")
            {
                panel1.ForeColor = Color.White;
            }
            else if (principal.CorTexto == "Preto")
            {
                panel1.ForeColor = Color.Black;
            }
            else if (principal.CorTexto == "Automatico")
            {
                // Calcula o brilho da cor (luminosidade)
                double brilho = (corPrincipal.R * 0.299 + corPrincipal.G * 0.587 + corPrincipal.B * 0.114) / 255;

                if (brilho < 0.4)
                {
                    pictureBoxBtnContinuarQualificacao.Image = Properties.Resources.menu_continuar_w;
                    panel1.ForeColor = Color.White;
                }
                else
                {
                    pictureBoxBtnContinuarQualificacao.Image = Properties.Resources.menu_continuar_b;
                    panel1.ForeColor = Color.Black;
                }
            }
            panel1.BackColor = corPrincipal;
            panel2.BackColor = corSecundaria;
            panel3.BackColor = corSecundaria;
        }
        private void contadorDeVoltas()
        {
            lbQualificacaoVoltas.Text = string.Format("Voltas: " + numberVoltasF + " / " + numberVoltasT);
        }
        private void AtualizarTabelasQualificacaoVoltas(DataGridView dvgTableQualificacaoF1)
        {
            DataTable qualificacaoEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 5 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[6], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void CriarDataGridViewQualificacaoEquipesF1(DataGridView dvgTableQualificacaoF1)
        {
            DataTable qualificacaoEquipesF1 = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            qualificacaoEquipesF1.Columns.Add("C1", typeof(string));
            qualificacaoEquipesF1.Columns.Add("#", typeof(int));
            qualificacaoEquipesF1.Columns.Add(sedeColumn);
            qualificacaoEquipesF1.Columns.Add("Nome", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Equipe", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Melhor Tempo", typeof(string));
            qualificacaoEquipesF1.Columns.Add("Segundos", typeof(int));
            qualificacaoEquipesF1.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dvgTableQualificacaoF1.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dvgTableQualificacaoF1.RowHeadersVisible = false;
            dvgTableQualificacaoF1.Enabled = false;
            dvgTableQualificacaoF1.ScrollBars = ScrollBars.None;
            dvgTableQualificacaoF1.AllowUserToAddRows = false;
            dvgTableQualificacaoF1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabeçalho
            dvgTableQualificacaoF1.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.GridColor = Color.FromArgb(220, 220, 220);
            dvgTableQualificacaoF1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
            dvgTableQualificacaoF1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgTableQualificacaoF1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dvgTableQualificacaoF1.DataSource = qualificacaoEquipesF1;

            // Altura das linhas
            dvgTableQualificacaoF1.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dvgTableQualificacaoF1.ColumnHeadersHeight = 30;

            // Defina a ordem de exibição das colunas com base nos índices
            dvgTableQualificacaoF1.Columns["C1"].DisplayIndex = 0;
            dvgTableQualificacaoF1.Columns["#"].DisplayIndex = 1;
            dvgTableQualificacaoF1.Columns["Nac"].DisplayIndex = 2;
            dvgTableQualificacaoF1.Columns["Nome"].DisplayIndex = 3;
            dvgTableQualificacaoF1.Columns["Equipe"].DisplayIndex = 4;
            dvgTableQualificacaoF1.Columns["Melhor Tempo"].DisplayIndex = 5;
            dvgTableQualificacaoF1.Columns["Segundos"].DisplayIndex = 6;
            dvgTableQualificacaoF1.Columns["Path"].DisplayIndex = 7;

            dvgTableQualificacaoF1.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dvgTableQualificacaoF1.Columns["Path"].Visible = false;
            dvgTableQualificacaoF1.Columns["Segundos"].Visible = false;
            dvgTableQualificacaoF1.Columns["C1"].HeaderText = string.Empty;

            dvgTableQualificacaoF1.Columns[0].Width = 10;
            dvgTableQualificacaoF1.Columns[1].Width = 40;
            dvgTableQualificacaoF1.Columns[2].Width = 50;
            dvgTableQualificacaoF1.Columns[3].Width = 330;
            dvgTableQualificacaoF1.Columns[4].Width = 240;
            dvgTableQualificacaoF1.Columns[5].Width = 170;
        }
        private void PreencherDataGridViewQualificacaoEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.FormatarNumero(pilotos[i].TempoDeVoltaQualificacao);
                        row["Melhor Tempo"] = tempo;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                        classPilotos.Rows.Add(row);
                    }
                }
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarDataGridViewQualificacaoEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.FormatarNumero(pilotos[i].TempoDeVoltaQualificacao);
                        row["Melhor Tempo"] = tempo;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                        classPilotos.Rows.Add(row);
                    }
                }
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        //Divisa das funções Treino e Corrida
        private void AtualizarTabelasCorridaInicio(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 10 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[9], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarTabelasCorridaVoltas(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dvgTableQualificacaoF1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Ordene automaticamente a coluna 11 do maior para o menor
            dvgTableQualificacaoF1.Sort(dvgTableQualificacaoF1.Columns[10], ListSortDirection.Ascending);

            for (int i = 0; i < dvgTableQualificacaoF1.Rows.Count; i++)
            {
                dvgTableQualificacaoF1.Rows[i].Cells["#"].Value = i + 1;
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dvgTableQualificacaoF1.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dvgTableQualificacaoF1.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }

            dvgTableQualificacaoF1.ClearSelection();
        }
        private void CriarDataGridViewCorridaEquipesF1(DataGridView dvgTableQualificacaoF1)
        {
            DataTable CorridaEquipesF1 = new DataTable();

            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            CorridaEquipesF1.Columns.Add("C1", typeof(string));
            CorridaEquipesF1.Columns.Add("#", typeof(int));
            CorridaEquipesF1.Columns.Add(sedeColumn);
            CorridaEquipesF1.Columns.Add("Nome", typeof(string));
            CorridaEquipesF1.Columns.Add("Equipe", typeof(string));
            CorridaEquipesF1.Columns.Add("Ult. Volta", typeof(string));
            CorridaEquipesF1.Columns.Add("Dif. Ant.", typeof(string));
            CorridaEquipesF1.Columns.Add("Dif. Pri.", typeof(string));
            CorridaEquipesF1.Columns.Add("Segundos", typeof(int));
            CorridaEquipesF1.Columns.Add("Qualific", typeof(int));
            CorridaEquipesF1.Columns.Add("TempoTotal", typeof(int));
            CorridaEquipesF1.Columns.Add("Path", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dvgTableQualificacaoF1.Columns.Add(imageColumn);

            // Defina um estilo padrão com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dvgTableQualificacaoF1.RowHeadersVisible = false;
            dvgTableQualificacaoF1.Enabled = false;
            dvgTableQualificacaoF1.ScrollBars = ScrollBars.None;
            dvgTableQualificacaoF1.AllowUserToAddRows = false;
            dvgTableQualificacaoF1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabeçalho
            dvgTableQualificacaoF1.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.GridColor = Color.FromArgb(220, 220, 220);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 255);
            dvgTableQualificacaoF1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(0, 0, 0);
            dvgTableQualificacaoF1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgTableQualificacaoF1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dvgTableQualificacaoF1.DataSource = CorridaEquipesF1;

            // Altura das linhas
            dvgTableQualificacaoF1.RowTemplate.Height = 26;
            // Define a altura do cabeçalho das colunas
            dvgTableQualificacaoF1.ColumnHeadersHeight = 30;

            // Defina a ordem de exibição das colunas com base nos índices
            dvgTableQualificacaoF1.Columns["C1"].DisplayIndex = 0;
            dvgTableQualificacaoF1.Columns["#"].DisplayIndex = 1;
            dvgTableQualificacaoF1.Columns["Nac"].DisplayIndex = 2;
            dvgTableQualificacaoF1.Columns["Nome"].DisplayIndex = 3;
            dvgTableQualificacaoF1.Columns["Equipe"].DisplayIndex = 4;
            dvgTableQualificacaoF1.Columns["Ult. Volta"].DisplayIndex = 5;
            dvgTableQualificacaoF1.Columns["Dif. Ant."].DisplayIndex = 6;
            dvgTableQualificacaoF1.Columns["Dif. Pri."].DisplayIndex = 7;
            dvgTableQualificacaoF1.Columns["Segundos"].DisplayIndex = 8;
            dvgTableQualificacaoF1.Columns["Qualific"].DisplayIndex = 9;
            dvgTableQualificacaoF1.Columns["TempoTotal"].DisplayIndex = 10;
            dvgTableQualificacaoF1.Columns["Path"].DisplayIndex = 11;


            dvgTableQualificacaoF1.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dvgTableQualificacaoF1.Columns["Path"].Visible = false;
            dvgTableQualificacaoF1.Columns["Segundos"].Visible = false;
            dvgTableQualificacaoF1.Columns["Qualific"].Visible = false;
            dvgTableQualificacaoF1.Columns["TempoTotal"].Visible = false;

            dvgTableQualificacaoF1.Columns[0].Width = 10;
            dvgTableQualificacaoF1.Columns[1].Width = 40;
            dvgTableQualificacaoF1.Columns[2].Width = 50;
            dvgTableQualificacaoF1.Columns[3].Width = 270;
            dvgTableQualificacaoF1.Columns[4].Width = 170;
            dvgTableQualificacaoF1.Columns[5].Width = 100;
            dvgTableQualificacaoF1.Columns[6].Width = 100;
            dvgTableQualificacaoF1.Columns[7].Width = 100;
        }
        private void PreencherDataGridViewCorridaEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.FormatarNumero(pilotos[i].TempoDeVoltaCorrida);
                        row["Ult. Volta"] = tempo;
                        string difAnt = principal.FormatarNumero(pilotos[i].DiferancaAnt);
                        row["Dif. Ant."] = difAnt;
                        string difPri = principal.FormatarNumero(pilotos[i].DiferancaPri);
                        row["Dif. Pri."] = difPri;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Qualific"] = pilotos[i].QualificacaoParaCorrida;
                        row["TempoTotal"] = pilotos[i].TempoCorrida;
                        row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                        classPilotos.Rows.Add(row);
                    }
                }
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private void AtualizarDataGridViewCorridaEquipesF1(int equipeF1Min, int equipeF1Max, DataGridView dvgTableQualificacaoF1)
        {
            DataTable classPilotos = (DataTable)dvgTableQualificacaoF1.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < pilotos.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        row["C1"] = pilotos[i].Cor1;
                        row["Nome"] = (pilotos[i].NomePiloto + " " + pilotos[i].SobrenomePiloto);
                        row["Equipe"] = pilotos[i].EquipePiloto;
                        string tempo = principal.FormatarNumero(pilotos[i].TempoDeVoltaCorrida);
                        row["Ult. Volta"] = tempo;
                        string difAnt = principal.FormatarNumero(pilotos[i].DiferancaAnt);
                        row["Dif. Ant."] = difAnt;
                        string difPri;
                        if (pilotos[i].DiferancaPri > pistas[principal.EtapaAtual].TempoBase)
                        {
                            difPri = "+1 Volta...";
                        }
                        else if (pilotos[i].DiferancaPri > (pistas[principal.EtapaAtual].TempoBase * 2))
                        {
                            difPri = "+2 Volta...";
                        }
                        else
                        {
                            difPri = principal.FormatarNumero(pilotos[i].DiferancaPri);
                        }
                        row["Dif. Pri."] = difPri;
                        row["Segundos"] = pilotos[i].TempoDeVoltaQualificacao;
                        row["Qualific"] = pilotos[i].QualificacaoParaCorrida;
                        row["TempoTotal"] = pilotos[i].TempoCorrida;
                        row["Path"] = Path.Combine("Paises", pilotos[i].NacionalidadePiloto + ".png");

                        classPilotos.Rows.Add(row);
                    }
                }
            }
            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudanças
            dvgTableQualificacaoF1.DataSource = classPilotos;

            // Limpe a seleção inicial
            dvgTableQualificacaoF1.ClearSelection();
        }
        private async void pictureBoxBtnContinuarQualificacao_Click(object sender, EventArgs e)
        {
            // Desabilitar o btn
            pictureBoxBtnContinuarQualificacao.Enabled = false;
            string fCategoria = pilotos[indexDoJogador].Categoria;

            if (pilotos[indexDoJogador].Categoria == "F1")
            {
                await FuncaoParaRealizarSemanaDeCorrida(0, 10, fCategoria);
            }
            else if (pilotos[indexDoJogador].Categoria == "F2")
            {
                await FuncaoParaRealizarSemanaDeCorrida(10, 20, fCategoria);
            }
            else if (pilotos[indexDoJogador].Categoria == "F3")
            {
                await FuncaoParaRealizarSemanaDeCorrida(20, 30, fCategoria);
            }
            // Reabilitar o btn
            pictureBoxBtnContinuarQualificacao.Enabled = true;
        }
        private async Task FuncaoParaRealizarSemanaDeCorrida(int equipeF1Min, int equipeF1Max, string fCategoria)
        {
            btnclick++;
            if (btnclick == 1)
            {
                progressBarQualificacao.Maximum = 10;
                // Vai executar as voltas do meu treino de qualificação.
                // Nesse caso meu treino está tendo 10 voltas
                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                                equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                                pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase, 0);
                                // Está ordenando a volta mais rapida do piloto.
                                if (pilotos[j].TempoDeVoltaQualificacao > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaQualificacao == 0)
                                {
                                    pilotos[j].TempoDeVoltaQualificacao = tempoDaVoltaAtual;
                                }
                            }
                        }
                    }
                    AtualizarDataGridViewQualificacaoEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                    AtualizarTabelasQualificacaoVoltas(dvgTableF1);
                    await Task.Delay(principal.TempoRodada);
                    progressBarQualificacao.Value = i;
                }
                // Vai executar a ordem de qualificação dos pilotos.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                            {
                                if (pilotos[i].TempoDeVoltaQualificacao > pilotos[j].TempoDeVoltaQualificacao)
                                {
                                    pilotos[i].QualificacaoParaCorrida++;
                                }
                            }
                        }
                    }
                }
            }
            else if (btnclick == 2)
            {
                AtualizarNomes();
                CriarDataGridViewCorridaEquipesF1(dvgTableF1);
                PreencherDataGridViewCorridaEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                AtualizarTabelasCorridaInicio(dvgTableF1);

                labelTreinoCorrida.Text = "Corrida";
                progressBarQualificacao.Value = 0;
            }
            else if (btnclick == 3)
            {
                progressBarQualificacao.Maximum = numberVoltasT;
                AtribuirEstrategiaDePitStop(equipeF1Min, equipeF1Max, fCategoria);
                // Vai executar as voltas da cominha corrida.
                for (int i = 1; i <= numberVoltasT; i++)
                {
                    // Distribui as vantagem da classificação
                    if (i == 1)
                    {
                        for (int j = 0; j < pilotos.Length; j++)
                        {
                            for (int k = equipeF1Min; k < equipeF1Max; k++)
                            {
                                if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    pilotos[j].TempoCorrida = (pilotos[j].QualificacaoParaCorrida * 100);
                                }
                            }
                        }
                    }

                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                pilotos[j].BonusRandom += GerarBonusRandom(numberVoltasF, pilotos[j].BonusRandom);
                                int bonusCadaDezVoltas = pilotos[j].BonusRandom;
                                int bonusAdversario = 0;
                                int bonusTotalDaVolta = 0;
                                int bonusPitStop = 0;
                                if (numberVoltasT != 1 && pilotos[j].DiferancaAnt < 1000)
                                {
                                    bonusAdversario = random.Next(0, 100);
                                }
                                if (numberVoltasT != 1 && pilotos[j].DiferancaAnt > 2000)
                                {
                                    bonusAdversario = random.Next(0, 40);
                                }

                                if (pilotos[j].StatusPiloto == "1º Piloto")
                                {
                                    if (equipes[k].VoltaParaPitStopPrimeiroPiloto == numberVoltasF)
                                    {
                                        if (equipes[k].QuantidadeDeParadaPrimeiroPiloto == 0)
                                        {
                                            bonusPitStop = PitStop(pilotos[j]);
                                            equipes[k].QuantidadeDeParadaPrimeiroPiloto++;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Macio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 15;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Médio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 26;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Duro") equipes[k].VoltaParaPitStopPrimeiroPiloto += 34;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Macio") equipes[k].PneuAtualPrimeiroPiloto = 300;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Médio") equipes[k].PneuAtualPrimeiroPiloto = 200;
                                            if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Duro") equipes[k].PneuAtualPrimeiroPiloto = 100;
                                        }
                                        else if (equipes[k].QuantidadeDeParadaPrimeiroPiloto == 1)
                                        {
                                            bonusPitStop = PitStop(pilotos[j]);
                                            equipes[k].QuantidadeDeParadaPrimeiroPiloto++;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Macio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 15;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Médio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 26;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Duro") equipes[k].VoltaParaPitStopPrimeiroPiloto += 34;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Macio") equipes[k].PneuAtualPrimeiroPiloto = 300;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Médio") equipes[k].PneuAtualPrimeiroPiloto = 200;
                                            if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Duro") equipes[k].PneuAtualPrimeiroPiloto = 100;
                                        }
                                    }
                                }
                                if (pilotos[j].StatusPiloto == "2º Piloto")
                                {
                                    if (equipes[k].VoltaParaPitStopSegundoPiloto == numberVoltasF)
                                    {
                                        if (equipes[k].QuantidadeDeParadaSegundoPiloto == 0)
                                        {
                                            bonusPitStop = PitStop(pilotos[j]);
                                            equipes[k].QuantidadeDeParadaSegundoPiloto++;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Macio") equipes[k].VoltaParaPitStopSegundoPiloto += 15;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Médio") equipes[k].VoltaParaPitStopSegundoPiloto += 26;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Duro") equipes[k].VoltaParaPitStopSegundoPiloto += 34;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Macio") equipes[k].PneuAtualSegundoPiloto = 300;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Médio") equipes[k].PneuAtualSegundoPiloto = 200;
                                            if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Duro") equipes[k].PneuAtualSegundoPiloto = 100;
                                        }
                                        else if (equipes[k].QuantidadeDeParadaSegundoPiloto == 1)
                                        {
                                            bonusPitStop = PitStop(pilotos[j]);
                                            equipes[k].QuantidadeDeParadaSegundoPiloto++;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Macio") equipes[k].VoltaParaPitStopSegundoPiloto += 15;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Médio") equipes[k].VoltaParaPitStopSegundoPiloto += 26;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Duro") equipes[k].VoltaParaPitStopSegundoPiloto += 34;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Macio") equipes[k].PneuAtualSegundoPiloto = 300;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Médio") equipes[k].PneuAtualSegundoPiloto = 200;
                                            if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Duro") equipes[k].PneuAtualSegundoPiloto = 100;
                                        }
                                    }
                                }

                                if (pilotos[j].StatusPiloto == "1º Piloto")
                                {
                                    bonusTotalDaVolta = pilotos[j].BonusRandom + bonusAdversario + equipes[k].PneuAtualPrimeiroPiloto + bonusPitStop;
                                }
                                else
                                {
                                    bonusTotalDaVolta = pilotos[j].BonusRandom + bonusAdversario + equipes[k].PneuAtualSegundoPiloto + bonusPitStop;
                                }

                                int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                                equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                                pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase, bonusTotalDaVolta);
                                pilotos[j].TempoCorrida = pilotos[j].TempoCorrida + tempoDaVoltaAtual;
                                pilotos[j].TempoDeVoltaCorrida = tempoDaVoltaAtual;
                                // Está ordenando a volta mais rapida do piloto.
                                if (pilotos[j].TempoDeVoltaMaisRapidaCorrida > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaMaisRapidaCorrida == 0)
                                {
                                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = tempoDaVoltaAtual;
                                }
                            }
                        }
                    }
                    // Calcular a posição na corrida.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        pilotos[j].PosicaoNaCorrida = 0;
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[j].TempoCorrida > pilotos[k].TempoCorrida)
                                    {
                                        pilotos[j].PosicaoNaCorrida++;
                                    }
                                }
                            }
                        }
                    }
                    // Calcula a diferença de tempo do piloto para o primeiro.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[k].PosicaoNaCorrida == 0)
                                    {
                                        pilotos[j].DiferancaPri = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                    }
                                }
                            }
                        }
                    }
                    // Calcula a diferença de tempo do piloto para a anterior.
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = 0; k < pilotos.Length; k++)
                        {
                            for (int l = equipeF1Min; l < equipeF1Max; l++)
                            {
                                if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                                {
                                    if (pilotos[j].PosicaoNaCorrida == (pilotos[k].PosicaoNaCorrida + 1))
                                    {
                                        pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                    }
                                    else if (pilotos[j].PosicaoNaCorrida == 0)
                                    {
                                        pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida;
                                    }
                                }
                            }
                        }
                    }
                    AtualizarDataGridViewCorridaEquipesF1(equipeF1Min, equipeF1Max, dvgTableF1);
                    AtualizarTabelasCorridaVoltas(dvgTableF1);
                    await Task.Delay(principal.TempoRodada);
                    progressBarQualificacao.Value = i;
                    numberVoltasF = i;
                    contadorDeVoltas();
                }
                // Vai executar a ordem das voltas mais rapida dos pilotos.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                            {
                                if (pilotos[i].TempoDeVoltaMaisRapidaCorrida > pilotos[j].TempoDeVoltaMaisRapidaCorrida)
                                {
                                    pilotos[i].PosicaoNaVoltaMaisRapida++;
                                }
                            }
                        }
                    }
                }
                // Pontuando o piloto com a volta mais rapida.
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                        {
                            if (pilotos[i].PosicaoNaVoltaMaisRapida == 0)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PontoVoltaMaisRapida;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PontoVoltaMaisRapida;
                            }
                        }
                    }
                }
                // Pontuação da Corrida para os Pilotos e Equipes
                for (int i = 0; i < pilotos.Length; i++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                        {
                            pilotos[i].GpDisputado++;
                            if(pilotos[i].QualificacaoParaCorrida == 1)
                            {
                                pilotos[i].PolePosition++;
                            }
                            if (pilotos[i].PosicaoNaCorrida == 0)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PrimeiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PrimeiroLugar;
                                pilotos[i].PrimeiroColocado++;
                                equipes[k].PrimeiroColocado++;
                                pilotos[i].VitoriaCorrida++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 1)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SegundoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SegundoLugar;
                                pilotos[i].SegundoColocado++;
                                equipes[k].SegundoColocado++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 2)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.TerceiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.TerceiroLugar;
                                pilotos[i].TerceiroColocado++;
                                equipes[k].TerceiroColocado++;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 3)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuartoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuartoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 4)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuintoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuintoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 5)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SextoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SextoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 6)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SetimoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SetimoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 7)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.OitavoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.OitavoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 8)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.NonoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.NonoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 9)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 10)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoPrimeiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoPrimeiroLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 11)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSegundoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSegundoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 12)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoTerceiroLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoTerceiroLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 13)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuartoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuartoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 14)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuintoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuintoLugar;
                            }
                            else if (pilotos[i].PosicaoNaCorrida == 15)
                            {
                                pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSextoLugar;
                                equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSextoLugar;
                            }
                        }
                    }
                }
                // Atualizar o atributos classificação dos pilotos *Fazer
                // Atualizar o atributos classificação das equipes *Fazer
            }
            else if (btnclick == 4)
            {
                // Obtém a lista de categorias
                List<Principal> categorias = Principal.ObterListaCategoria();

                // Itera sobre cada categoria na lista
                foreach (var categoria in categorias)
                {
                    if (categoria.Categoria != fCategoria && categoria.Categoria == "F1")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(0, 10, "F1");
                    }
                    else if (categoria.Categoria != fCategoria && categoria.Categoria == "F2")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(10, 20, "F2");
                    }
                    else if (categoria.Categoria != fCategoria && categoria.Categoria == "F3")
                    {
                        FuncaoParaRealizarSemanaDeCorridaCPU(20, 30, "F3");
                    }
                }
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].TempoDeVoltaQualificacao = 0;
                    pilotos[j].TempoCorrida = 0;
                    pilotos[j].TempoDeVoltaCorrida = 0;
                    pilotos[j].QualificacaoParaCorrida = 0;
                    pilotos[j].TempoDeVoltaMaisRapidaCorrida = 0;
                    pilotos[j].PosicaoNaVoltaMaisRapida = 0;
                    pilotos[j].PosicaoNaCorrida = 0;
                    pilotos[j].DiferancaAnt = 0;
                    pilotos[j].DiferancaPri = 0;
                    pilotos[j].BonusRandom = 0;
                }
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    equipes[k].QuantidadeDeParadaPrimeiroPiloto = 0;
                    equipes[k].PneuAtualPrimeiroPiloto = 0;
                    equipes[k].VoltaParaPitStopPrimeiroPiloto = 0;
                    equipes[k].TrocaDePneuParada01PrimeiroPiloto = "";
                    equipes[k].TrocaDePneuParada02PrimeiroPiloto = "";
                    equipes[k].TrocaDePneuParada03PrimeiroPiloto = "";
                    equipes[k].QuantidadeDeParadaSegundoPiloto = 0;
                    equipes[k].PneuAtualSegundoPiloto = 0;
                    equipes[k].VoltaParaPitStopSegundoPiloto = 0;
                    equipes[k].TrocaDePneuParada01SegundoPiloto = "";
                    equipes[k].TrocaDePneuParada02SegundoPiloto = "";
                    equipes[k].TrocaDePneuParada03SegundoPiloto = "";
                }
                
                MetodoParaQualificarEquipes(0, 10);
                MetodoParaQualificarEquipes(10, 20);
                MetodoParaQualificarEquipes(20, 30);
                MetodoParaQualificarPilotos("F1");
                MetodoParaQualificarPilotos("F2");
                MetodoParaQualificarPilotos("F3");
                this.Close();
            }
        }
        private int GerarBonusRandom(int voltas, int bonusAtual)
        {
            Random r = new Random();

            if (voltas == 0 || voltas == 10 || voltas == 20 || voltas == 30 || voltas == 40 || voltas == 50 || voltas == 60 || voltas == 70 || voltas == 80 || voltas == 90)
            {
                int x = r.Next(1,4);
                if(x == 1)
                {
                    return 40;
                }
                if (x == 2)
                {
                    return 0;
                }
                    return -40;
            }
            return 0;
        }
        private int PitStop(Piloto piloto)
        {
            Random r = new Random();
            int x = (r.Next(piloto.Largada, 201) + r.Next(piloto.Experiencia, 201));
            x = ((x * 10) + 2500);
            return -x;
        }
        private void AtribuirEstrategiaDePitStop(int equipeF1Min, int equipeF1Max, string fCategoria)
        {
            Random random = new Random();
            for (int j = 0; j < pilotos.Length; j++)
            {
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria && pilotos[j].StatusPiloto == "1º Piloto")
                    {
                        // Recupera o dicionário combinacaoPitStop da classe Pista
                        var combinacaoPitStop = pistas[principal.EtapaAtual].CombinacaoPitStop;

                        // Seleciona uma chave aleatória do dicionário
                        int randomIndex = random.Next(combinacaoPitStop.Count);
                        int randomKey = combinacaoPitStop.Keys.ElementAt(randomIndex);

                        // Recupera o valor correspondente à chave selecionada aleatoriamente
                        var pitStopInfo = combinacaoPitStop[randomKey];

                        // Atribui cada item da tupla a uma variável separada
                        equipes[k].TrocaDePneuParada01PrimeiroPiloto = pitStopInfo.Item1;
                        equipes[k].TrocaDePneuParada02PrimeiroPiloto = pitStopInfo.Item2;
                        equipes[k].TrocaDePneuParada03PrimeiroPiloto = pitStopInfo.Item3;

                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Macio") equipes[k].VoltaParaPitStopPrimeiroPiloto = 15;
                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Médio") equipes[k].VoltaParaPitStopPrimeiroPiloto = 26;
                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Duro") equipes[k].VoltaParaPitStopPrimeiroPiloto = 34;

                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Macio") equipes[k].PneuAtualPrimeiroPiloto = 300;
                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Médio") equipes[k].PneuAtualPrimeiroPiloto = 200; 
                        if (equipes[k].TrocaDePneuParada01PrimeiroPiloto == "Duro") equipes[k].PneuAtualPrimeiroPiloto = 100;
                    }
                    if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria && pilotos[j].StatusPiloto == "2º Piloto")
                    {
                        // Recupera o dicionário combinacaoPitStop da classe Pista
                        var combinacaoPitStop = pistas[principal.EtapaAtual].CombinacaoPitStop;

                        // Seleciona uma chave aleatória do dicionário
                        int randomIndex = random.Next(combinacaoPitStop.Count);
                        int randomKey = combinacaoPitStop.Keys.ElementAt(randomIndex);

                        // Recupera o valor correspondente à chave selecionada aleatoriamente
                        var pitStopInfo = combinacaoPitStop[randomKey];

                        // Atribui cada item da tupla a uma variável separada
                        equipes[k].TrocaDePneuParada01SegundoPiloto = pitStopInfo.Item1;
                        equipes[k].TrocaDePneuParada02SegundoPiloto = pitStopInfo.Item2;
                        equipes[k].TrocaDePneuParada03SegundoPiloto = pitStopInfo.Item3;

                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Macio") equipes[k].VoltaParaPitStopSegundoPiloto = 15;
                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Médio") equipes[k].VoltaParaPitStopSegundoPiloto = 26;
                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Duro") equipes[k].VoltaParaPitStopSegundoPiloto = 34;

                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Macio") equipes[k].PneuAtualSegundoPiloto = 300;
                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Médio") equipes[k].PneuAtualSegundoPiloto = 200;
                        if (equipes[k].TrocaDePneuParada01SegundoPiloto == "Duro") equipes[k].PneuAtualSegundoPiloto = 100;
                    }
                }
            }
        }
        private void MetodoParaQualificarEquipes(int equipeMin, int equipeMax)
        {
            for (int i = equipeMin; i < equipeMax; i++)
            {
                equipes[i].PosicaoAtualCampeonato = 1;
                for (int j = equipeMin; j < equipeMax; j++)
                {
                    if (i != j)
                    {
                        if (equipes[i].PontosCampeonato <= equipes[j].PontosCampeonato)
                        {
                            if (equipes[i].PontosCampeonato == equipes[j].PontosCampeonato)
                            {
                                if (equipes[i].PrimeiroColocado == equipes[j].PrimeiroColocado)
                                {
                                    if (equipes[i].SegundoColocado == equipes[j].SegundoColocado)
                                    {
                                        if (equipes[i].TerceiroColocado == equipes[j].TerceiroColocado)
                                        {
                                            if (i > j)
                                            {
                                                equipes[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (equipes[i].TerceiroColocado < equipes[j].TerceiroColocado)
                                        {
                                            equipes[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (equipes[i].SegundoColocado < equipes[j].SegundoColocado)
                                    {
                                        equipes[i].PosicaoAtualCampeonato++;
                                    }
                                }
                                else if (equipes[i].PrimeiroColocado < equipes[j].PrimeiroColocado)
                                {
                                    equipes[i].PosicaoAtualCampeonato++;
                                }
                            }
                            else if (equipes[i].PontosCampeonato < equipes[j].PontosCampeonato)
                            {
                                equipes[i].PosicaoAtualCampeonato++;
                            }
                        }
                    }
                }
            }
        }
        private void MetodoParaQualificarPilotos(string fCategoria)
        {
            for (int i = 0; i < pilotos.Length; i++)
            {
                if (pilotos[i].Categoria == fCategoria)
                {
                    pilotos[i].PosicaoAtualCampeonato = 1;
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        if (pilotos[j].Categoria == fCategoria)
                        {
                            if (i != j)
                            {
                                if (pilotos[i].PontosCampeonato <= pilotos[j].PontosCampeonato)
                                {
                                    if (pilotos[i].PontosCampeonato == pilotos[j].PontosCampeonato)
                                    {
                                        if (pilotos[i].PrimeiroColocado == pilotos[j].PrimeiroColocado)
                                        {
                                            if (pilotos[i].SegundoColocado == pilotos[j].SegundoColocado)
                                            {
                                                if (pilotos[i].TerceiroColocado == pilotos[j].TerceiroColocado)
                                                {
                                                    if (i > j)
                                                    {
                                                        pilotos[i].PosicaoAtualCampeonato++;
                                                    }
                                                }
                                                else if (pilotos[i].TerceiroColocado < pilotos[j].TerceiroColocado)
                                                {
                                                    pilotos[i].PosicaoAtualCampeonato++;
                                                }
                                            }
                                            else if (pilotos[i].SegundoColocado < pilotos[j].SegundoColocado)
                                            {
                                                pilotos[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (pilotos[i].PrimeiroColocado < pilotos[j].PrimeiroColocado)
                                        {
                                            pilotos[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (pilotos[i].PontosCampeonato < pilotos[j].PontosCampeonato)
                                    {
                                        pilotos[i].PosicaoAtualCampeonato++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void FuncaoParaRealizarSemanaDeCorridaCPU(int equipeF1Min, int equipeF1Max, string fCategoria)
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                        {
                            int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                            equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                            pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase, 0);
                            // Está ordenando a volta mais rapida do piloto.
                            if (pilotos[j].TempoDeVoltaQualificacao > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaQualificacao == 0)
                            {
                                pilotos[j].TempoDeVoltaQualificacao = tempoDaVoltaAtual;
                            }
                        }
                    }
                }
            }
            // Vai executar a ordem de qualificação dos pilotos.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                        {
                            if (pilotos[i].TempoDeVoltaQualificacao > pilotos[j].TempoDeVoltaQualificacao)
                            {
                                pilotos[i].QualificacaoParaCorrida++;
                            }
                        }
                    }
                }
            }
            // Vai executar as voltas da cominha corrida.
            AtribuirEstrategiaDePitStop(equipeF1Min, equipeF1Max, fCategoria);
            for (int i = 1; i <= pistas[principal.EtapaAtual].NumerosDeVoltas; i++)
            {
                // Distribui as vantagem da classificação
                if (i == 1)
                {
                    for (int j = 0; j < pilotos.Length; j++)
                    {
                        for (int k = equipeF1Min; k < equipeF1Max; k++)
                        {
                            if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                pilotos[j].TempoCorrida = i * 100;
                            }
                        }
                    }
                }

                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[j].Categoria == fCategoria)
                        {
                            pilotos[j].BonusRandom += GerarBonusRandom(numberVoltasF, pilotos[j].BonusRandom);
                            int bonusCadaDezVoltas = pilotos[j].BonusRandom;
                            int bonusAdversario = 0;
                            int bonusTotalDaVolta = 0;
                            int bonusPitStop = 0;
                            if (numberVoltasT != 1 && pilotos[j].DiferancaAnt < 1000)
                            {
                                bonusAdversario = random.Next(0, 100);
                            }
                            if (numberVoltasT != 1 && pilotos[j].DiferancaAnt > 2000)
                            {
                                bonusAdversario = random.Next(0, 40);
                            }

                            if (pilotos[j].StatusPiloto == "1º Piloto")
                            {
                                if (equipes[k].VoltaParaPitStopPrimeiroPiloto == numberVoltasF)
                                {
                                    if (equipes[k].QuantidadeDeParadaPrimeiroPiloto == 0)
                                    {
                                        bonusPitStop = PitStop(pilotos[j]);
                                        equipes[k].QuantidadeDeParadaPrimeiroPiloto++;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Macio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 15;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Médio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 26;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Duro") equipes[k].VoltaParaPitStopPrimeiroPiloto += 34;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Macio") equipes[k].PneuAtualPrimeiroPiloto = 300;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Médio") equipes[k].PneuAtualPrimeiroPiloto = 200;
                                        if (equipes[k].TrocaDePneuParada02PrimeiroPiloto == "Duro") equipes[k].PneuAtualPrimeiroPiloto = 100;
                                    }
                                    else if (equipes[k].QuantidadeDeParadaPrimeiroPiloto == 1)
                                    {
                                        bonusPitStop = PitStop(pilotos[j]);
                                        equipes[k].QuantidadeDeParadaPrimeiroPiloto++;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Macio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 15;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Médio") equipes[k].VoltaParaPitStopPrimeiroPiloto += 26;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Duro") equipes[k].VoltaParaPitStopPrimeiroPiloto += 34;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Macio") equipes[k].PneuAtualPrimeiroPiloto = 300;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Médio") equipes[k].PneuAtualPrimeiroPiloto = 200;
                                        if (equipes[k].TrocaDePneuParada03PrimeiroPiloto == "Duro") equipes[k].PneuAtualPrimeiroPiloto = 100;
                                    }
                                }
                            }
                            if (pilotos[j].StatusPiloto == "2º Piloto")
                            {
                                if (equipes[k].VoltaParaPitStopSegundoPiloto == numberVoltasF)
                                {
                                    if (equipes[k].QuantidadeDeParadaSegundoPiloto == 0)
                                    {
                                        bonusPitStop = PitStop(pilotos[j]);
                                        equipes[k].QuantidadeDeParadaSegundoPiloto++;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Macio") equipes[k].VoltaParaPitStopSegundoPiloto += 15;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Médio") equipes[k].VoltaParaPitStopSegundoPiloto += 26;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Duro") equipes[k].VoltaParaPitStopSegundoPiloto += 34;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Macio") equipes[k].PneuAtualSegundoPiloto = 300;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Médio") equipes[k].PneuAtualSegundoPiloto = 200;
                                        if (equipes[k].TrocaDePneuParada02SegundoPiloto == "Duro") equipes[k].PneuAtualSegundoPiloto = 100;
                                    }
                                    else if (equipes[k].QuantidadeDeParadaSegundoPiloto == 1)
                                    {
                                        bonusPitStop = PitStop(pilotos[j]);
                                        equipes[k].QuantidadeDeParadaSegundoPiloto++;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Macio") equipes[k].VoltaParaPitStopSegundoPiloto += 15;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Médio") equipes[k].VoltaParaPitStopSegundoPiloto += 26;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Duro") equipes[k].VoltaParaPitStopSegundoPiloto += 34;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Macio") equipes[k].PneuAtualSegundoPiloto = 300;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Médio") equipes[k].PneuAtualSegundoPiloto = 200;
                                        if (equipes[k].TrocaDePneuParada03SegundoPiloto == "Duro") equipes[k].PneuAtualSegundoPiloto = 100;
                                    }
                                }
                            }

                            if (pilotos[j].StatusPiloto == "1º Piloto")
                            {
                                bonusTotalDaVolta = pilotos[j].BonusRandom + bonusAdversario + equipes[k].PneuAtualPrimeiroPiloto + bonusPitStop;
                            }
                            else
                            {
                                bonusTotalDaVolta = pilotos[j].BonusRandom + bonusAdversario + equipes[k].PneuAtualSegundoPiloto + bonusPitStop;
                            }

                            int tempoDaVoltaAtual = AlgoritmoParaVoltas(equipes[k].ValorDoMotor, equipes[k].Aerodinamica, equipes[k].Freio, equipes[k].AsaDianteira, equipes[k].AsaTraseira, equipes[k].Cambio,
                            equipes[k].Eletrico, equipes[k].Direcao, equipes[k].Confiabilidade, pilotos[j].Largada, pilotos[j].Concentracao, pilotos[j].Ultrapassagem, pilotos[j].Experiencia, pilotos[j].Rapidez,
                            pilotos[j].Chuva, pilotos[j].AcertoDoCarro, pilotos[j].Fisico, principal.ImportanciaPilotoTemporada, principal.ImportanciaCarroTemporada, pistas[principal.EtapaAtual].Curvas, pistas[principal.EtapaAtual].Retas, pistas[principal.EtapaAtual].TempoBase, pilotos[j].BonusRandom);
                            pilotos[j].TempoCorrida = pilotos[j].TempoCorrida + tempoDaVoltaAtual;
                            pilotos[j].TempoDeVoltaCorrida = tempoDaVoltaAtual;
                            // Está ordenando a volta mais rapida do piloto.
                            if (pilotos[j].TempoDeVoltaMaisRapidaCorrida > tempoDaVoltaAtual || pilotos[j].TempoDeVoltaMaisRapidaCorrida == 0)
                            {
                                pilotos[j].TempoDeVoltaMaisRapidaCorrida = tempoDaVoltaAtual;
                            }
                        }
                    }
                }
                // Calcular a posição na corrida.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    pilotos[j].PosicaoNaCorrida = 0;
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[j].TempoCorrida > pilotos[k].TempoCorrida)
                                {
                                    pilotos[j].PosicaoNaCorrida++;
                                }
                            }
                        }
                    }
                }
                // Calcula a diferença de tempo do piloto para o primeiro.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[k].PosicaoNaCorrida == 0)
                                {
                                    pilotos[j].DiferancaPri = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                }
                            }
                        }
                    }
                }
                // Calcula a diferença de tempo do piloto para a anterior.
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = 0; k < pilotos.Length; k++)
                    {
                        for (int l = equipeF1Min; l < equipeF1Max; l++)
                        {
                            if (equipes[l].NomeEquipe == pilotos[k].EquipePiloto && pilotos[j].Categoria == fCategoria)
                            {
                                if (pilotos[j].PosicaoNaCorrida == (pilotos[k].PosicaoNaCorrida + 1))
                                {
                                    pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida - pilotos[k].TempoCorrida;
                                }
                                else if (pilotos[j].PosicaoNaCorrida == 0)
                                {
                                    pilotos[j].DiferancaAnt = pilotos[j].TempoCorrida;
                                }
                            }
                        }
                    }
                }
            }
            // Vai executar a ordem das voltas mais rapida dos pilotos.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int j = 0; j < pilotos.Length; j++)
                {
                    for (int k = equipeF1Min; k < equipeF1Max; k++)
                    {
                        if (equipes[k].NomeEquipe == pilotos[j].EquipePiloto && pilotos[i].Categoria == fCategoria)
                        {
                            if (pilotos[i].TempoDeVoltaMaisRapidaCorrida > pilotos[j].TempoDeVoltaMaisRapidaCorrida)
                            {
                                pilotos[i].PosicaoNaVoltaMaisRapida++;
                            }
                        }
                    }
                }
            }
            // Pontuando o piloto com a volta mais rapida.
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        if (pilotos[i].PosicaoNaVoltaMaisRapida == 0)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PontoVoltaMaisRapida;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PontoVoltaMaisRapida;
                        }
                    }
                }
            }
            // Pontuação da Corrida para os Pilotos e Equipes
            for (int i = 0; i < pilotos.Length; i++)
            {
                for (int k = equipeF1Min; k < equipeF1Max; k++)
                {
                    if (equipes[k].NomeEquipe == pilotos[i].EquipePiloto)
                    {
                        pilotos[i].GpDisputado++;
                        if (pilotos[i].QualificacaoParaCorrida == 1)
                        {
                            pilotos[i].PolePosition++;
                        }
                        if (pilotos[i].PosicaoNaCorrida == 0)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.PrimeiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.PrimeiroLugar;
                            pilotos[i].PrimeiroColocado++;
                            equipes[k].PrimeiroColocado++;
                            pilotos[i].VitoriaCorrida++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 1)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SegundoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SegundoLugar;
                            pilotos[i].SegundoColocado++;
                            equipes[k].SegundoColocado++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 2)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.TerceiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.TerceiroLugar;
                            pilotos[i].TerceiroColocado++;
                            equipes[k].TerceiroColocado++;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 3)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuartoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuartoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 4)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.QuintoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.QuintoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 5)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SextoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SextoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 6)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.SetimoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.SetimoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 7)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.OitavoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.OitavoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 8)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.NonoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.NonoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 9)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 10)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoPrimeiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoPrimeiroLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 11)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSegundoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSegundoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 12)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoTerceiroLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoTerceiroLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 13)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuartoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuartoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 14)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoQuintoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoQuintoLugar;
                        }
                        else if (pilotos[i].PosicaoNaCorrida == 15)
                        {
                            pilotos[i].PontosCampeonato = pilotos[i].PontosCampeonato + principal.DecimoSextoLugar;
                            equipes[k].PontosCampeonato = equipes[k].PontosCampeonato + principal.DecimoSextoLugar;
                        }
                    }
                }
            }
            // Atualizar o atributos classificação dos pilotos *Fazer
            // Atualizar o atributos classificação das equipes *Fazer
        }
        private int AlgoritmoParaVoltas(int motor, int aerodinamica, int freio, int asaDianteira, int asaTraseira, int cambio,
        int eletrico, int direcao, int confiabilidade, int largada, int concentracao, int ultrapassagem, int experiencia, int rapidez,
        int chuva, int acertoCarro, int fisico, int importanciPiloto, int importanciaCarro, int curvas, int retas, int tempoBase, int bonusRandom)
        {
            Random r = new Random();

            int t = 5000; //valorPadraoTempo

            int medCarro = ((aerodinamica + freio + asaDianteira + asaTraseira + cambio + eletrico + direcao + confiabilidade) / 8);
            int medPiloto = (largada + concentracao + ultrapassagem + experiencia + rapidez + chuva + acertoCarro + fisico) / 8;
            int medCarroVel = (r.Next(aerodinamica / 2, aerodinamica + 1) + r.Next(asaDianteira / 2, asaDianteira + 1) + r.Next(asaTraseira / 2, asaTraseira + 1) + r.Next(freio / 2, freio + 1)) / 4;
            int medCarroQual = (r.Next(cambio / 2, cambio + 1) + r.Next(confiabilidade / 2, confiabilidade + 1) + r.Next(direcao / 2, direcao + 1) + r.Next(eletrico / 2, eletrico + 1)) / 4;
            int medPilotVel = (r.Next(ultrapassagem / 2, ultrapassagem + 1) + r.Next(experiencia / 2, experiencia + 1) + r.Next(rapidez / 2, rapidez + 1)) / 3;
            int medPilotFis = (r.Next(concentracao / 2, concentracao + 1) + r.Next(acertoCarro / 2, acertoCarro + 1) + r.Next(fisico / 2, fisico + 1)) / 3;

            int atributo01 = ((motor + medCarro) + (r.Next(1, medCarro)));
            int atributo02 = (medCarroVel + medPiloto + medPilotVel + rapidez) * ((retas / 100) + 1);
            int atributo03 = (medCarroQual + medPiloto + medPilotFis) * ((curvas / 100) + 1);
            int atributo04 = (medCarroVel + medPilotVel);
            int atributo05 = (medCarroQual + medPilotFis);
            int atributo06 = ((medPiloto + motor) + (r.Next(1, medPiloto)));
            int atributo07 = (medPilotVel + medPilotFis) * ((importanciPiloto / 100) + 1);
            int atributo08 = (medCarroVel + medCarroQual) * ((importanciaCarro / 100) + 1);
            int atributo09 = (medPilotVel + medPilotFis) * ((retas / 100) + 1);
            int atributo10 = (medCarroVel + medCarroQual) * ((curvas / 100) + 1);

            int somaDosAtributos = ((r.Next((atributo01 / 2), atributo01) + r.Next((atributo02 / 2), atributo02) + r.Next((atributo03 / 2), atributo03) + r.Next((atributo04 / 2), atributo04) + r.Next((atributo05 / 2), atributo05) + (r.Next((atributo06 / 2), atributo06) * 2) + r.Next((atributo07 / 2), atributo07) + r.Next((atributo08 / 2), atributo08) + r.Next((atributo09 / 2), atributo09) + r.Next((atributo10 / 2), atributo10) + bonusRandom) * 3);
            int volta = ((tempoBase + t) - somaDosAtributos);

            return volta;
        }
    }
}
