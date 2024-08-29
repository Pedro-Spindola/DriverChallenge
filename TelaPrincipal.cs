using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DriverChallenge.Equipe;
using static DriverChallenge.Piloto;

namespace DriverChallenge
{
    public partial class TelaPrincipal : Form
    {
        Principal principal;
        private Equipe[] equipe = new Equipe[30];      // Criando array com a quantidade de equipes.
        private Piloto[] piloto = new Piloto[100];     // Criando array com a quantidade de pilotos.
        private Pista[] pista = new Pista[25];          // Crinado array com a quantidade de pistas. -> Mudar para o padrão depois de teste = 25 para teste e = 3
        private Random random = new Random();
        private Financia financia = new Financia();
        private Motor motor = new Motor();
        Color corPrincipal;
        Color corSecundaria;
        Color corTexto;
        private int indexDoJogador = 1;
        public TelaPrincipal(Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
        }
        public void TelaPrincipal_Load(object sender, EventArgs e)
        {
            if (principal.ConfiguracaoInicioDoGame == 1)
            {
                IniciarNovoGame();
                string caminhoPtn = "Pontuacao.txt";
                principal.ConfigurarFaixaDePontuacao(caminhoPtn);
                MetodoParaQualificarEquipes(0, 10);
                MetodoParaQualificarEquipes(10, 20);
                MetodoParaQualificarEquipes(20, 30);
                MetodoParaQualificarPilotos("F1");
                MetodoParaQualificarPilotos("F2");
                MetodoParaQualificarPilotos("F3");
            }
            else if (principal.ConfiguracaoInicioDoGame == 2)
            {
                CarregarDadosDosArquivos();
            }
            // Converte a string hexadecimal em um objeto Color
            indexDoJogador = principal.IndexDoJogador;
            principal.CorPrincipal = piloto[indexDoJogador].Cor1;
            principal.CorSecundaria = piloto[indexDoJogador].Cor2;
            corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
            corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
            AtualizarCores();
            AtualizarFinanciasSemanal();
            AtualizarNomesNaTelaInicial();
            CriarDataGridViewClassEquipes();
            CriarDataGridViewClassPilotos();
            AtualizarNomesNaTelaInicial();
            AtualizarTabelaInicioDeTemporada();
            AtualizarTabelas();
        }
        public void IniciarNovoGame()
        {
            // Gerando um array de Pilotos
            for (int i = 0; i < piloto.Length; i++)
            {
                Piloto newPiloto = new Piloto();
                if (i + 1 == piloto.Length)
                {
                    newPiloto.GeraPiloto(principal.NomeJogador, principal.SobrenomeJogador, principal.NacionalidadeJogador);
                    piloto[i] = newPiloto;
                }
                else
                {
                    newPiloto.GeraPiloto();
                    piloto[i] = newPiloto;
                }
            }
            /*
            piloto[0] = new Piloto("Max", "Verstappen", "Holanda", 26, 32, 40, 100, 100, 100, 95, 100, 100, 95, 95);
            piloto[1] = new Piloto("Sergio", "Perez", "México", 34, 32, 38, 90, 90, 90, 95, 80, 90, 90, 90);
            piloto[2] = new Piloto("Lewis", "Hamilton", "Inglaterra", 39, 32, 40, 90, 90, 90, 100, 80, 90, 90, 80);
            piloto[3] = new Piloto("George", "Russell", "Inglaterra", 26, 33, 37, 90, 90, 90, 80, 80, 80, 80, 90);
            piloto[4] = new Piloto("Charles", "Leclerc", "Mônaco", 27, 32, 37, 100, 90, 100, 90, 100, 90, 100, 90);
            piloto[5] = new Piloto("Carlos", "Sainz", "Espanha", 30, 31, 36, 90, 90, 100, 100, 90, 90, 90, 90);
            piloto[6] = new Piloto("Alexander", "Albon", "Tailândia", 28, 33, 35, 70, 80, 80, 80, 70, 80, 80, 80);
            piloto[7] = new Piloto("Logan", "Sargeant", "Estados Unidos", 24, 31, 36, 70, 70, 70, 70, 70, 70, 70, 80);
            piloto[8] = new Piloto("Fernando", "Alonso", "Espanha", 43, 33, 43, 100, 90, 90, 100, 80, 80, 90, 90);
            piloto[9] = new Piloto("Lance", "Stroll", "Canadá", 26, 30, 35, 80, 70, 75, 75, 80, 80, 85, 80);
            piloto[10] = new Piloto("Lando", "Norris", "Inglaterra", 25, 33, 37, 90, 90, 85, 85, 90, 90, 90, 80);
            piloto[11] = new Piloto("Oscar", "Piastri", "Austrália", 27, 33, 40, 80, 80, 80, 85, 85, 80, 85, 90);
            piloto[12] = new Piloto("Esteban", "Ocon", "França", 28, 33, 36, 80, 80, 80, 75, 75, 80, 80, 80);
            piloto[13] = new Piloto("Pierre", "Gasly", "França", 28, 32, 40, 80, 80, 85, 85, 90, 85, 80, 80);
            piloto[14] = new Piloto("Daniel", "Ricciardo", "Austrália", 35, 33, 36, 80, 80, 70, 100, 80, 75, 85, 80);
            piloto[15] = new Piloto("Yuki", "Tsunoda", "Japão", 24, 30, 36, 75, 75, 80, 80, 70, 75, 80, 90);
            piloto[16] = new Piloto("Valtteri", "Bottas", "Finlândia", 35, 32, 38, 70, 80, 80, 70, 80, 85, 75, 85);
            piloto[17] = new Piloto("Guanyu", "Zhou", "China", 25, 33, 35, 80, 70, 80, 70, 75, 85, 75, 70);
            piloto[18] = new Piloto("Kevin", "Magnussen", "Dinamarca", 32, 33, 40, 80, 80, 85, 85, 85, 80, 80, 85);
            piloto[19] = new Piloto("Nico", "Hülkenberg", "Alemanha", 37, 33, 39, 80, 80, 80, 100, 85, 75, 75, 80);
            */
            // Gerando as Equipes F1 (média entre 80 a 100)
            equipe[0] = new Equipe("Red Bull", "#03183B", "#C70101", "#FFFFFF", "Austria", 98, 95, 97, 99, 96, 94, 95, 100, "Honda", "F1", motor);
            equipe[1] = new Equipe("Mercedes", "#C4C4C4", "#09BF81", "#000000", "Alemanha", 90, 91, 89, 92, 90, 88, 87, 91, "Mercedes", "F1", motor);
            equipe[2] = new Equipe("Ferrari", "#FF0000", "#FFFFFF", "#000000", "Itália", 85, 87, 88, 86, 84, 90, 85, 89, "Ferrari", "F1", motor);
            equipe[3] = new Equipe("Williams", "#112685", "#FFFFFF", "#FFFFFF", "Inglaterra", 82, 84, 80, 81, 83, 85, 82, 86, "TAG", "F1", motor);
            equipe[4] = new Equipe("Aston Martin", "#004039", "#FFFFFF", "#FFFFFF", "Inglaterra", 85, 87, 89, 83, 86, 84, 82, 88, "Mercedes", "F1", motor);
            equipe[5] = new Equipe("McLaren", "#FF8D36", "#000000", "#FFFFFF", "Inglaterra", 90, 92, 91, 93, 88, 87, 89, 91, "Honda", "F1", motor);
            equipe[6] = new Equipe("Alpine", "#CE4A8D", "#2075DC", "#000000", "França", 80, 83, 85, 82, 84, 81, 80, 82, "Renault", "F1", motor);
            equipe[7] = new Equipe("Visa Cash", "#0456D9", "#B10407", "#000000", "Itália", 82, 81, 84, 83, 80, 82, 81, 84, "TAG", "F1", motor);
            equipe[8] = new Equipe("Stake Sauber", "#000000", "#0BEE23", "#FFFFFF", "Suíça", 80, 82, 81, 83, 80, 84, 81, 82, "Ferrari", "F1", motor);
            equipe[9] = new Equipe("Haas", "#002420", "#000000", "#FFFFFF", "Estados Unidos", 81, 83, 82, 84, 80, 82, 81, 85, "Ferrari", "F1", motor);

            // Equipes F2 (média entre 60 a 80)
            equipe[10] = new Equipe("MP Motorsport", "#FF883C", "#FF883C", "#FFFFFF", "Holanda", 72, 71, 75, 74, 73, 70, 72, 71, "TAG", "F2", motor);
            equipe[11] = new Equipe("Infinity Audi", "#CCCCCC", "#991F21", "#000000", "Alemanha", 68, 70, 67, 66, 72, 69, 71, 70, "Audi", "F2", motor);
            equipe[12] = new Equipe("Carlin", "#2151B0", "#75FF07", "#000000", "Inglaterra", 60, 63, 65, 62, 64, 61, 66, 65, "Renault", "F2", motor);
            equipe[13] = new Equipe("Jordan", "#FFE120", "#000000", "#FFFFFF", "Inglaterra", 75, 74, 72, 71, 73, 76, 70, 74, "Mercedes", "F2", motor);
            equipe[14] = new Equipe("Prema", "#FF3622", "#FFFFFF", "#000000", "Itália", 65, 67, 66, 69, 64, 63, 68, 70, "TAG", "F2", motor);
            equipe[15] = new Equipe("Hitech", "#808080", "#000000", "#000000", "Inglaterra", 61, 63, 60, 62, 64, 66, 65, 64, "BMW", "F2", motor);
            equipe[16] = new Equipe("DAMS", "#113861", "#48D4FF", "#FFFFFF", "França", 70, 71, 72, 73, 68, 66, 69, 70, "Renault", "F2", motor);
            equipe[17] = new Equipe("Amersfoort", "#000000", "#FF883C", "#FFFFFF", "Holanda", 64, 65, 63, 66, 62, 60, 67, 65, "Ford", "F2", motor);
            equipe[18] = new Equipe("Lamborghini", "#000000", "#FFAC11", "#FFFFFF", "Itália", 62, 63, 64, 65, 66, 60, 61, 64, "Lamborghini", "F2", motor);
            equipe[19] = new Equipe("Trident", "#3706BF", "#FF3024", "#000000", "Itália", 60, 62, 61, 64, 63, 65, 66, 67, "Toyota", "F2", motor);

            // Equipes F3 (média entre 40 a 60)
            equipe[20] = new Equipe("BMW", "#117CFF", "#FFFFFF", "#000000", "Alemanha", 55, 53, 54, 52, 50, 56, 55, 54, "BMW", "F3", motor);
            equipe[21] = new Equipe("Penske Porsche", "#FFFFFF", "#FF3629", "#000000", "Alemanha", 50, 52, 51, 49, 53, 55, 48, 50, "Audi", "F3", motor);
            equipe[22] = new Equipe("Toyota Gazoo", "#C22A1F", "#C22A1F", "#FFFFFF", "Japão", 45, 47, 46, 44, 50, 48, 49, 46, "Ford", "F3", motor);
            equipe[23] = new Equipe("Campos", "#FFB22A", "#EB3326", "#000000", "Espanha", 55, 54, 52, 53, 50, 48, 49, 51, "BMW", "F3", motor);
            equipe[24] = new Equipe("Tower Motorsports", "#FF9A1C", "#3444FF", "#000000", "Canadá", 48, 46, 47, 45, 49, 50, 51, 48, "Ford", "F3", motor);
            equipe[25] = new Equipe("Team WRT", "#55BEFF", "#55BEFF", "#FFFFFF", "Bélgica", 42, 45, 44, 43, 46, 40, 41, 44, "Lamborghini", "F3", motor);
            equipe[26] = new Equipe("Proton", "#9551FF", "#9551FF", "#FFFFFF", "Alemanha", 44, 43, 46, 42, 45, 40, 41, 44, "Toyota", "F3", motor);
            equipe[27] = new Equipe("Kessel", "#FF0081", "#236EFF", "#FFFFFF", "Suíça", 39, 41, 43, 42, 44, 40, 45, 46, "Ford", "F3", motor);
            equipe[28] = new Equipe("Action Express", "#FF6E63", "#CCCCCC", "#000000", "Estados Unidos", 40, 43, 42, 41, 45, 39, 44, 46, "Toyota", "F3", motor);
            equipe[29] = new Equipe("Team Senna", "#2D7D4E", "#FFD91C", "#000000", "Brasil", 45, 47, 46, 44, 48, 49, 50, 46, "Lamborghini", "F3", motor);


            // Método para chamar uma Tela, onde jogador vai escolher a sua equipe inicial.
            // EscolherEquipeInicialDoJogador();

            // Vai atribur os pilotos as suas equipes.
            for (int i = 0; i < (equipe.Length * 2); i++)
            {
                int equipeIndex = i / 2; // Equipe 0 para pilotos 0 e 1, equipe 1 para pilotos 2 e 3, etc.

                Equipe equipeSelecionada = equipe[equipeIndex];
                if (i % 2 == 0)
                {
                    piloto[i].EquipePiloto = equipeSelecionada.NomeEquipe;
                    piloto[i].StatusPiloto = "1º Piloto";
                    piloto[i].Cor1 = equipeSelecionada.Cor1;
                    piloto[i].Cor2 = equipeSelecionada.Cor2;
                    piloto[i].Categoria = equipeSelecionada.Categoria;
                    if (piloto[i].Categoria == "F1") piloto[i].XpPiloto = 400;
                    if (piloto[i].Categoria == "F2") piloto[i].XpPiloto = 200;
                    if (piloto[i].ContratoPiloto == 0) piloto[i].ContratoPiloto = ((random.Next(1, 4) + principal.ContadorDeAno) - 1);
                    if (piloto[i].SalarioPiloto == 0) piloto[i].SalarioPiloto = DefinirSalario(piloto[i].MediaPiloto, equipeSelecionada.Categoria);
                    equipeSelecionada.PrimeiroPiloto = $"{piloto[i].NomePiloto} {piloto[i].SobrenomePiloto}";
                    equipeSelecionada.PrimeiroPilotoContrato = piloto[i].ContratoPiloto;
                    equipeSelecionada.PrimeiroPilotoSalario = piloto[i].SalarioPiloto;

                    if (piloto[i].ContratoPiloto == principal.ContadorDeAno)
                    {
                        piloto[i].ProximoAnoContratoPiloto = 0;
                        piloto[i].ProximoAnoEquipePiloto = "";
                        piloto[i].ProximoAnoSalarioPiloto = 0;
                        piloto[i].ProximoAnoStatusPiloto = "";

                        equipeSelecionada.ProximoAnoPrimeiroPiloto = "";
                        equipeSelecionada.ProximoAnoPrimeiroPilotoContrato = 0;
                        equipeSelecionada.ProximoAnoPrimeiroPilotoSalario = 0;
                    }
                    else if (piloto[i].ContratoPiloto > principal.ContadorDeAno)
                    {
                        piloto[i].ProximoAnoContratoPiloto = piloto[i].ContratoPiloto;
                        piloto[i].ProximoAnoEquipePiloto = piloto[i].EquipePiloto;
                        piloto[i].ProximoAnoSalarioPiloto = piloto[i].SalarioPiloto;
                        piloto[i].ProximoAnoStatusPiloto = piloto[i].StatusPiloto;

                        equipeSelecionada.ProximoAnoPrimeiroPiloto = $"{piloto[i].NomePiloto} {piloto[i].SobrenomePiloto}";
                        equipeSelecionada.ProximoAnoPrimeiroPilotoContrato = piloto[i].ContratoPiloto;
                        equipeSelecionada.ProximoAnoPrimeiroPilotoSalario = piloto[i].SalarioPiloto;
                    }
                }
                else
                {
                    piloto[i].EquipePiloto = equipeSelecionada.NomeEquipe;
                    piloto[i].StatusPiloto = "2º Piloto";
                    piloto[i].Cor1 = equipeSelecionada.Cor1;
                    piloto[i].Cor2 = equipeSelecionada.Cor2;
                    piloto[i].Categoria = equipeSelecionada.Categoria;
                    if (piloto[i].Categoria == "F1") piloto[i].XpPiloto = 400;
                    if (piloto[i].Categoria == "F2") piloto[i].XpPiloto = 200;
                    if (piloto[i].ContratoPiloto == 0) piloto[i].ContratoPiloto = ((random.Next(1, 4) + principal.ContadorDeAno) - 1);
                    if (piloto[i].SalarioPiloto == 0) piloto[i].SalarioPiloto = DefinirSalario(piloto[i].MediaPiloto, equipeSelecionada.Categoria);
                    equipeSelecionada.SegundoPiloto = $"{piloto[i].NomePiloto} {piloto[i].SobrenomePiloto}";
                    equipeSelecionada.SegundoPilotoContrato = piloto[i].ContratoPiloto;
                    equipeSelecionada.SegundoPilotoSalario = piloto[i].SalarioPiloto;

                    if (piloto[i].ContratoPiloto == principal.ContadorDeAno)
                    {
                        piloto[i].ProximoAnoContratoPiloto = 0;
                        piloto[i].ProximoAnoEquipePiloto = "";
                        piloto[i].ProximoAnoSalarioPiloto = 0;
                        piloto[i].ProximoAnoStatusPiloto = "";

                        equipeSelecionada.ProximoAnoSegundoPiloto = "";
                        equipeSelecionada.ProximoAnoSegundoPilotoContrato = 0;
                        equipeSelecionada.ProximoAnoSegundoPilotoSalario = 0;
                    }
                    else if (piloto[i].ContratoPiloto > principal.ContadorDeAno)
                    {
                        piloto[i].ProximoAnoContratoPiloto = piloto[i].ContratoPiloto;
                        piloto[i].ProximoAnoEquipePiloto = piloto[i].EquipePiloto;
                        piloto[i].ProximoAnoSalarioPiloto = piloto[i].SalarioPiloto;
                        piloto[i].ProximoAnoStatusPiloto = piloto[i].StatusPiloto;

                        equipeSelecionada.ProximoAnoSegundoPiloto = $"{piloto[i].NomePiloto} {piloto[i].SobrenomePiloto}";
                        equipeSelecionada.ProximoAnoSegundoPilotoContrato = piloto[i].ContratoPiloto;
                        equipeSelecionada.ProximoAnoSegundoPilotoSalario = piloto[i].SalarioPiloto;
                    }
                }
            }
            principal.XpTurnoSemanal(piloto);
            CriandoOsDadosPistas();
            EmbaralharPistas();
            DefinirDataDaSemanaDeProvaDaPista();

            principal.IdadeJogador = piloto[indexDoJogador].IdadePiloto;

            principal.ProximoGp = pista[0].NomeGp;
            principal.ProximoGpPais = pista[0].NomeCircuito;
            principal.ProximoGpSemana = pista[0].SemanaDaProva;
            principal.ProximoGpVoltas = pista[0].NumerosDeVoltas;

        }
        public double DefinirSalario(int mediaHabilidade, string categoria)
        {
            if (categoria == "F1")
            {
                int habilidade = mediaHabilidade * 20;
                int bases = random.Next(10000, 12001);
                int bonus = random.Next(5000, 10001);
                int salario = (((habilidade * bases) / 200) + bonus);
                return salario;
            }
            if (categoria == "F2")
            {
                int habilidade = mediaHabilidade * 20;
                int bases = random.Next(8000, 10001);
                int bonus = random.Next(5000, 10001);
                int salario = (((habilidade * bases) / 200) + bonus);
                return salario;
            }
            if (categoria == "F3")
            {
                int habilidade = mediaHabilidade * 20;
                int bases = random.Next(6000, 8001);
                int bonus = random.Next(5000, 10001);
                int salario = (((habilidade * bases) / 200) + bonus);
                return salario;
            }
            return 0;
        }
        public void EscolherEquipeInicialDoJogador()
        {
            TelaEscolherEquipeInicial telaEscolherEquipeInicial = new TelaEscolherEquipeInicial(principal, equipe, piloto);
            telaEscolherEquipeInicial.ShowDialog();
        }
        public void CriandoOsDadosPistas() // Depois de finalizar os testem desbloquear o restantes das pistas.
        {
            pista[0] = new Pista("Austrália", "Melbourne", 58, 44, 56, 76800);
            pista[1] = new Pista("Itália", "Monza", 53, 35, 65, 70200);
            pista[2] = new Pista("Brasil", "Interlagos", 71, 42, 58, 65400);

            pista[3] = new Pista("Bahrein", "Sakhir", 57, 43, 57, 77400);
            pista[4] = new Pista("Arábia Saudita", "Corniche Circuit", 50, 58, 42, 76200);
            pista[5] = new Pista("Japão", "Suzuka", 53, 59, 41, 75420);
            pista[6] = new Pista("China", "Shanghai", 56, 48, 52, 81000);
            pista[7] = new Pista("Estados Unidos", "Miami", 57, 56, 44, 73200);
            pista[8] = new Pista("Itália", "Imola", 63, 32, 62, 72600);
            pista[9] = new Pista("Mônaco", "Monte Carlo", 78, 64, 36, 67800);
            pista[10] = new Pista("Canadá", "Gilles Vileneuve", 70, 40, 60, 67200);
            pista[11] = new Pista("Espanha", "Catalunha", 66, 40, 60, 69000);
            pista[12] = new Pista("Áustri", "Red Bull Ring", 71, 25, 75, 63600);
            pista[13] = new Pista("Reino Unido", "Silverstone", 52, 43, 57, 74400);
            pista[14] = new Pista("Holanda", "Zandvoort", 72, 48, 52, 67200);
            pista[15] = new Pista("Hungria", "Hungaroring", 70, 46, 54, 72000);
            pista[16] = new Pista("Bélgica", "Spa-Francorchamps", 44, 47, 53, 88200);
            pista[17] = new Pista("África do Sul", "Kyalami", 45, 55, 72, 70800);
            pista[18] = new Pista("México", "Hermanos Rodríguez", 71, 38, 62, 67800);
            pista[19] = new Pista("Azerbaijão", "Baku", 51, 59, 41, 88200);
            pista[20] = new Pista("Cingapura", "Marina Bay", 62, 52, 48, 82200);
            pista[21] = new Pista("Qatar", "Lusail Circuit", 57, 59, 41, 72480);
            pista[22] = new Pista("Estados Unidos", "Las Vegas", 50, 70, 30, 81100);
            pista[23] = new Pista("Emirados Árabes Unidos", "Yas Marina", 58, 39, 61, 74400);
            pista[24] = new Pista("Alemanha", "Hockenheimring", 67, 42, 58, 68400);


        }
        public void EmbaralharPistas()
        {
            Random random = new Random();
            // Embaralhe as pistas usando o algoritmo de Fisher-Yates
            for (int i = pista.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                // Trocar as pista[i] e pista[j]
                (pista[j], pista[i]) = (pista[i], pista[j]);
            }
        }
        public void DefinirDataDaSemanaDeProvaDaPista() // Depois de finalizar os testem desbloquear o restantes das pistas.
        {
            pista[0].SemanaDaProva = 5;
            pista[1].SemanaDaProva = 7;
            pista[2].SemanaDaProva = 8;

            pista[3].SemanaDaProva = 10;
            pista[4].SemanaDaProva = 12;
            pista[5].SemanaDaProva = 15;
            pista[6].SemanaDaProva = 17;
            pista[7].SemanaDaProva = 19;
            pista[8].SemanaDaProva = 20;
            pista[9].SemanaDaProva = 22;
            pista[10].SemanaDaProva = 23;
            pista[11].SemanaDaProva = 25;
            pista[12].SemanaDaProva = 27;
            pista[13].SemanaDaProva = 28;
            pista[14].SemanaDaProva = 30;
            pista[15].SemanaDaProva = 32;
            pista[16].SemanaDaProva = 34;
            pista[17].SemanaDaProva = 36;
            pista[18].SemanaDaProva = 37;
            pista[19].SemanaDaProva = 39;
            pista[20].SemanaDaProva = 41;
            pista[21].SemanaDaProva = 42;
            pista[22].SemanaDaProva = 44;
            pista[23].SemanaDaProva = 46;
            pista[24].SemanaDaProva = 48;


        }
        public void AtualizaStatusProxCorrida(int contador) // Depois de finalizar os testem desbloquear o restantes das pistas.
        {
            if (contador > 0 && contador <= 5)
            {
                FuncaoParaStatusDaProximaCorrida(0);
            }
            else if (contador > 5 && contador <= 7)
            {
                FuncaoParaStatusDaProximaCorrida(1);
            }
            else if (contador > 7 && contador <= 8)
            {
                FuncaoParaStatusDaProximaCorrida(2);
            }

            else if (contador > 8 && contador <= 10)
            {
                FuncaoParaStatusDaProximaCorrida(3);
            }
            else if (contador > 10 && contador <= 12)
            {
                FuncaoParaStatusDaProximaCorrida(4);
            }
            else if (contador > 12 && contador <= 15)
            {
                FuncaoParaStatusDaProximaCorrida(5);
            }
            else if (contador > 15 && contador <= 17)
            {
                FuncaoParaStatusDaProximaCorrida(6);
            }
            else if (contador > 17 && contador <= 19)
            {
                FuncaoParaStatusDaProximaCorrida(7);
            }
            else if (contador > 19 && contador <= 20)
            {
                FuncaoParaStatusDaProximaCorrida(8);
            }
            else if (contador > 20 && contador <= 22)
            {
                FuncaoParaStatusDaProximaCorrida(9);
            }
            else if (contador > 22 && contador <= 23)
            {
                FuncaoParaStatusDaProximaCorrida(10);
            }
            else if (contador > 23 && contador <= 25)
            {
                FuncaoParaStatusDaProximaCorrida(11);
            }
            else if (contador > 25 && contador <= 27)
            {
                FuncaoParaStatusDaProximaCorrida(12);
            }
            else if (contador > 27 && contador <= 28)
            {
                FuncaoParaStatusDaProximaCorrida(13);
            }
            else if (contador > 28 && contador <= 30)
            {
                FuncaoParaStatusDaProximaCorrida(14);
            }
            else if (contador > 30 && contador <= 32)
            {
                FuncaoParaStatusDaProximaCorrida(15);
            }
            else if (contador > 32 && contador <= 34)
            {
                FuncaoParaStatusDaProximaCorrida(16);
            }
            else if (contador > 34 && contador <= 36)
            {
                FuncaoParaStatusDaProximaCorrida(17);
            }
            else if (contador > 36 && contador <= 37)
            {
                FuncaoParaStatusDaProximaCorrida(18);
            }
            else if (contador > 37 && contador <= 39)
            {
                FuncaoParaStatusDaProximaCorrida(19);
            }
            else if (contador > 39 && contador <= 41)
            {
                FuncaoParaStatusDaProximaCorrida(20);
            }
            else if (contador > 41 && contador <= 42)
            {
                FuncaoParaStatusDaProximaCorrida(21);
            }
            else if (contador > 42 && contador <= 44)
            {
                FuncaoParaStatusDaProximaCorrida(22);
            }
            else if (contador > 44 && contador <= 46)
            {
                FuncaoParaStatusDaProximaCorrida(23);
            }
            else if (contador > 46 && contador <= 48)
            {
                FuncaoParaStatusDaProximaCorrida(24);
            }
            else
            {
                principal.ProximoGp = "";
                principal.ProximoGpPais = "";
                principal.ProximoGpSemana = 0;
                principal.ProximoGpVoltas = 0;
            }
        }
        public void FuncaoParaStatusDaProximaCorrida(int i)
        {
            principal.ProximoGp = pista[i].NomeGp;
            principal.ProximoGpPais = pista[i].NomeCircuito;
            principal.ProximoGpSemana = pista[i].SemanaDaProva;
            principal.ProximoGpVoltas = pista[i].NumerosDeVoltas;
        }
        public void AtualizarCores()
        {
            if (principal.CorTexto == "Branco")
            {
                pictureBoxBtnFechar.Image = Properties.Resources.fechar_w;
                pictureBoxBtnSalvar.Image = Properties.Resources.salvar_w;
                pictureBoxBtnOpcao.Image = Properties.Resources.opcao_w;
                pictureBoxBtnContinuar.Image = Properties.Resources.menu_continuar_w;
                panel1.ForeColor = Color.White;
            }
            else if (principal.CorTexto == "Preto")
            {
                pictureBoxBtnFechar.Image = Properties.Resources.fechar_b;
                pictureBoxBtnSalvar.Image = Properties.Resources.salvar_b;
                pictureBoxBtnOpcao.Image = Properties.Resources.opcao_b;
                pictureBoxBtnContinuar.Image = Properties.Resources.menu_continuar_b;
                panel1.ForeColor = Color.Black;
            }
            else if (principal.CorTexto == "Automatico")
            {
                // Calcula o brilho da cor (luminosidade)
                double brilho = (corPrincipal.R * 0.299 + corPrincipal.G * 0.587 + corPrincipal.B * 0.114) / 255;

                if (brilho < 0.4)
                {
                    pictureBoxBtnFechar.Image = Properties.Resources.fechar_w;
                    pictureBoxBtnSalvar.Image = Properties.Resources.salvar_w;
                    pictureBoxBtnOpcao.Image = Properties.Resources.opcao_w;
                    pictureBoxBtnContinuar.Image = Properties.Resources.menu_continuar_w;
                    pictureBoxMensagemVisualizada.Image = Properties.Resources.cs_visual_w;
                    pictureBoxMensagem.Image = Properties.Resources.cx_msg_w;
                    panel1.ForeColor = Color.White;
                }
                else
                {
                    pictureBoxBtnFechar.Image = Properties.Resources.fechar_b;
                    pictureBoxBtnSalvar.Image = Properties.Resources.salvar_b;
                    pictureBoxBtnOpcao.Image = Properties.Resources.opcao_b;
                    pictureBoxBtnContinuar.Image = Properties.Resources.menu_continuar_b;
                    pictureBoxMensagemVisualizada.Image = Properties.Resources.cs_visual_b;
                    pictureBoxMensagem.Image = Properties.Resources.cx_msg_b;
                    panel1.ForeColor = Color.Black;
                }
            }
            panel1.BackColor = corPrincipal;
            panel2.BackColor = corSecundaria;
            panel3.BackColor = corSecundaria;
            panelCorP1.BackColor = corPrincipal;
            panelCorP2.BackColor = corPrincipal;
            panelCorP3.BackColor = corPrincipal;
            panelCorP4.BackColor = corPrincipal;
            panelCorP5.BackColor = corPrincipal;
            panelCorP6.BackColor = corPrincipal;
            panelCorP7.BackColor = corPrincipal;
            panelCorS1.BackColor = corSecundaria;
            panelCorS2.BackColor = corSecundaria;
            panelCorS3.BackColor = corSecundaria;
            panelCorS4.BackColor = corSecundaria;
            panelCorS5.BackColor = corSecundaria;
            panelCorS6.BackColor = corSecundaria;
        }
        public void AtualizarFinanciasSemanal()
        {
            financia.DinheiroJogadorTotal += financia.DinheiroJogadorSemanal;
        }
        public void AtualizarNomesNaTelaInicial()
        {
            labelNomeJogador.Text = string.Format("{0} {1}", principal.NomeJogador, principal.SobrenomeJogador);
            labelIdadeJogador.Text = string.Format("Idade: {0:N0}", principal.IdadeJogador.ToString());
            pictureBoxNacionalidadePiloto.ImageLocation = Path.Combine("Paises", principal.NacionalidadeJogador + ".png");
            labelSaldoNaConta.Text = string.Format("R$ {0:N0}", financia.DinheiroJogadorTotal);
            labelSaldoPorSemana.Text = string.Format("R$ {0:N0}", financia.DinheiroJogadorSemanal);
            labelDataTemporada.Text = string.Format("Semana {0:D2} / {1}", principal.ContadorDeSemana, principal.ContadorDeAno);
            labelStatusTemporada.Text = principal.StatusDaTemporada;
            if (principal.StatusDaTemporada == "Fim-Temporada")
            {
                labelGpDoPais.Text = "Fim de Temporada";
                labelNomeGP.Text = "";
                labelSemanaGP.Text = "";
            }
            else
            {
                labelGpDoPais.Text = string.Format("GP do {0:D2}", principal.ProximoGp);
                labelNomeGP.Text = principal.ProximoGpPais;
                labelSemanaGP.Text = string.Format("Semana {0:D2}", principal.ProximoGpSemana.ToString());
            }
        }
        public void AtualizarTabelas()
        {
            DataTable classEquipes = (DataTable)dgvClassificacaoEquipes.DataSource;
            DataTable classPilotos = (DataTable)dgvClassificacaoPilotos.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgvClassificacaoEquipes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgvClassificacaoPilotos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Ordene automaticamente a coluna 4 do maior para o menor
            dgvClassificacaoEquipes.Sort(dgvClassificacaoEquipes.Columns[0], ListSortDirection.Ascending);

            // Ordene automaticamente a coluna 5 do maior para o menor
            dgvClassificacaoPilotos.Sort(dgvClassificacaoPilotos.Columns[0], ListSortDirection.Ascending);

            for (int i = 0; i < dgvClassificacaoEquipes.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dgvClassificacaoPilotos.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            dgvClassificacaoEquipes.ClearSelection();
            dgvClassificacaoPilotos.ClearSelection();
        }
        public void AtualizarTabelaInicioDeTemporada()
        {
            if (piloto[indexDoJogador].Categoria == "F1")
            {
                PreencherDataGridViewClassEquipes(0, 10);
                PreencherDataGridViewClassPilotos(0, 10);
                AtualizarTabelas();
            }
            else if (piloto[indexDoJogador].Categoria == "F2")
            {
                PreencherDataGridViewClassPilotos(10, 20);
                PreencherDataGridViewClassEquipes(10, 20);
                AtualizarTabelas();
            }
            else if (piloto[indexDoJogador].Categoria == "F3")
            {
                PreencherDataGridViewClassPilotos(20, 30);
                PreencherDataGridViewClassEquipes(20, 30);
                AtualizarTabelas();
            }
            else
            {
                PreencherDataGridViewClassEquipes(0, 10);
                PreencherDataGridViewClassPilotos(0, 10);
                AtualizarTabelas();
            }
        }
        public void CriarDataGridViewClassEquipes()
        {
            DataTable classEquipes = new DataTable();
            DataColumn sedeColumn = new DataColumn("Sede", typeof(Image));

            classEquipes.Columns.Add("#", typeof(int));
            classEquipes.Columns.Add(sedeColumn);
            classEquipes.Columns.Add("C1", typeof(string));
            classEquipes.Columns.Add("Nome", typeof(string));
            classEquipes.Columns.Add("P", typeof(int));
            classEquipes.Columns.Add("1º", typeof(int));
            classEquipes.Columns.Add("2º", typeof(int));
            classEquipes.Columns.Add("3º", typeof(int));
            classEquipes.Columns.Add("Path", typeof(string));
            classEquipes.Columns.Add("Nacionalidade", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Sede";
            imageColumn.Name = "Sede";
            imageColumn.DataPropertyName = "Sede";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dgvClassificacaoEquipes.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dgvClassificacaoEquipes.RowHeadersVisible = false;
            dgvClassificacaoEquipes.Enabled = false;
            dgvClassificacaoEquipes.ScrollBars = ScrollBars.None;
            dgvClassificacaoEquipes.AllowUserToAddRows = false;
            dgvClassificacaoEquipes.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgvClassificacaoEquipes.GridColor = Color.FromArgb(220, 220, 220);
            dgvClassificacaoEquipes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassificacaoEquipes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassificacaoEquipes.Refresh();

            dgvClassificacaoEquipes.DataSource = classEquipes;

            // Altura das linhas
            dgvClassificacaoEquipes.RowTemplate.Height = 30;
            // Define a altura do cabe�alho das colunas
            dgvClassificacaoEquipes.ColumnHeadersHeight = 40;


            // Defina a ordem de exibi��o das colunas com base nos indices
            dgvClassificacaoEquipes.Columns["#"].DisplayIndex = 0;
            dgvClassificacaoEquipes.Columns["Sede"].DisplayIndex = 1;
            dgvClassificacaoEquipes.Columns["C1"].DisplayIndex = 2;
            dgvClassificacaoEquipes.Columns["Nome"].DisplayIndex = 3;
            dgvClassificacaoEquipes.Columns["P"].DisplayIndex = 4;
            dgvClassificacaoEquipes.Columns["1º"].DisplayIndex = 5;
            dgvClassificacaoEquipes.Columns["2º"].DisplayIndex = 6;
            dgvClassificacaoEquipes.Columns["3º"].DisplayIndex = 7;
            dgvClassificacaoEquipes.Columns["Path"].DisplayIndex = 8;
            dgvClassificacaoEquipes.Columns["Nacionalidade"].DisplayIndex = 9;

            dgvClassificacaoEquipes.Columns["Path"].Visible = false;
            dgvClassificacaoEquipes.Columns["Nacionalidade"].Visible = false;

            dgvClassificacaoEquipes.Columns[0].Width = 40;
            dgvClassificacaoEquipes.Columns[1].Width = 50;
            dgvClassificacaoEquipes.Columns[2].Width = 10;
            dgvClassificacaoEquipes.Columns[3].Width = 190;
            dgvClassificacaoEquipes.Columns[4].Width = 50;
            dgvClassificacaoEquipes.Columns[5].Width = 40;
            dgvClassificacaoEquipes.Columns[6].Width = 40;
            dgvClassificacaoEquipes.Columns[7].Width = 40;

        }
        public void PreencherDataGridViewClassEquipes(int equipeMinima, int equipeMaxima)
        {
            DataTable classEquipes = (DataTable)dgvClassificacaoEquipes.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classEquipes.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = equipeMinima; i < equipeMaxima; i++)
            {
                DataRow row = classEquipes.NewRow();

                row["#"] = equipe[i].PosicaoAtualCampeonato;
                row["C1"] = equipe[i].Cor1;
                row["Nome"] = equipe[i].NomeEquipe;
                row["P"] = equipe[i].PontosCampeonato;
                row["1º"] = equipe[i].PrimeiroColocado;
                row["2º"] = equipe[i].SegundoColocado;
                row["3º"] = equipe[i].TerceiroColocado;
                row["Path"] = Path.Combine("Paises", equipe[i].Sede + ".png");
                row["Nacionalidade"] = equipe[i].Sede;
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
            // Atualize o DataGridView para refletir as mudancas
            dgvClassificacaoEquipes.DataSource = classEquipes;

            // Limpe a seleção inicial
            dgvClassificacaoEquipes.ClearSelection();
        }
        public void CriarDataGridViewClassPilotos()
        {
            DataTable classPilotos = new DataTable();
            DataColumn sedeColumn = new DataColumn("Nac", typeof(Image));

            classPilotos.Columns.Add("#", typeof(int));
            classPilotos.Columns.Add(sedeColumn);
            classPilotos.Columns.Add("Nome", typeof(string));
            classPilotos.Columns.Add("C1", typeof(string));
            classPilotos.Columns.Add("Equipe", typeof(string));
            classPilotos.Columns.Add("P", typeof(int));
            classPilotos.Columns.Add("1º", typeof(int));
            classPilotos.Columns.Add("2º", typeof(int));
            classPilotos.Columns.Add("3º", typeof(int));
            classPilotos.Columns.Add("Path", typeof(string));
            classPilotos.Columns.Add("Cor1", typeof(string));
            classPilotos.Columns.Add("Cor2", typeof(string));
            classPilotos.Columns.Add("Nacionalidade", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Nac";
            imageColumn.Name = "Nac";
            imageColumn.DataPropertyName = "Nac";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            dgvClassificacaoPilotos.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            dgvClassificacaoPilotos.RowHeadersVisible = false;
            dgvClassificacaoPilotos.Enabled = false;
            dgvClassificacaoPilotos.ScrollBars = ScrollBars.None;
            dgvClassificacaoPilotos.AllowUserToAddRows = false;
            dgvClassificacaoPilotos.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 180, 180); // Define a cor das linhas do cabe�alho
            dgvClassificacaoPilotos.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            dgvClassificacaoPilotos.GridColor = Color.FromArgb(220, 220, 220);
            dgvClassificacaoPilotos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassificacaoPilotos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvClassificacaoPilotos.DataSource = classPilotos;

            // Altura das linhas
            dgvClassificacaoPilotos.RowTemplate.Height = 26;
            // Define a altura do cabecalho das colunas
            dgvClassificacaoPilotos.ColumnHeadersHeight = 30;

            // Defina a ordem de exibicao das colunas com base nos indices
            dgvClassificacaoPilotos.Columns["#"].DisplayIndex = 0;
            dgvClassificacaoPilotos.Columns["Nac"].DisplayIndex = 1;
            dgvClassificacaoPilotos.Columns["Nome"].DisplayIndex = 2;
            dgvClassificacaoPilotos.Columns["C1"].DisplayIndex = 3;
            dgvClassificacaoPilotos.Columns["Equipe"].DisplayIndex = 4;
            dgvClassificacaoPilotos.Columns["P"].DisplayIndex = 5;
            dgvClassificacaoPilotos.Columns["1º"].DisplayIndex = 6;
            dgvClassificacaoPilotos.Columns["2º"].DisplayIndex = 7;
            dgvClassificacaoPilotos.Columns["3º"].DisplayIndex = 8;
            dgvClassificacaoPilotos.Columns["Path"].DisplayIndex = 9;
            dgvClassificacaoPilotos.Columns["Nacionalidade"].DisplayIndex = 10;

            dgvClassificacaoPilotos.Columns["Nome"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvClassificacaoPilotos.Columns["Path"].Visible = false;
            dgvClassificacaoPilotos.Columns["Cor1"].Visible = false;
            dgvClassificacaoPilotos.Columns["Cor2"].Visible = false;
            dgvClassificacaoPilotos.Columns["Nacionalidade"].Visible = false;

            dgvClassificacaoPilotos.Columns[0].Width = 40;
            dgvClassificacaoPilotos.Columns[1].Width = 50;
            dgvClassificacaoPilotos.Columns[2].Width = 160;
            dgvClassificacaoPilotos.Columns[3].Width = 10;
            dgvClassificacaoPilotos.Columns[4].Width = 110;
            dgvClassificacaoPilotos.Columns[5].Width = 40;
            dgvClassificacaoPilotos.Columns[6].Width = 30;
            dgvClassificacaoPilotos.Columns[7].Width = 30;
            dgvClassificacaoPilotos.Columns[8].Width = 30;
        }
        public void PreencherDataGridViewClassPilotos(int equipeMinima, int equipeMaxima)
        {

            DataTable classPilotos = (DataTable)dgvClassificacaoPilotos.DataSource;

            // Limpe todas as linhas existentes no DataTable
            classPilotos.Rows.Clear();

            // Percorra o array de equipes usando um loop for
            for (int i = 0; i < piloto.Length; i++)
            {
                DataRow row = classPilotos.NewRow();

                for (int k = equipeMinima; k < equipeMaxima; k++)
                {
                    if (equipe[k].NomeEquipe == piloto[i].EquipePiloto)
                    {
                        row["#"] = piloto[i].PosicaoAtualCampeonato;
                        row["Nome"] = (piloto[i].NomePiloto + " " + piloto[i].SobrenomePiloto);
                        row["C1"] = piloto[i].Cor1;
                        row["Equipe"] = piloto[i].EquipePiloto;
                        row["P"] = piloto[i].PontosCampeonato;
                        row["1º"] = piloto[i].PrimeiroColocado;
                        row["2º"] = piloto[i].SegundoColocado;
                        row["3º"] = piloto[i].TerceiroColocado;
                        row["Path"] = Path.Combine("Paises", piloto[i].NacionalidadePiloto + ".png");
                        row["Nacionalidade"] = piloto[i].NacionalidadePiloto;

                        classPilotos.Rows.Add(row);
                    }
                }
            }

            // Percorra as linhas da tabela classF1
            foreach (DataRow row in classPilotos.Rows)
            {
                string imagePath = row["Path"].ToString();
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudancas
            dgvClassificacaoPilotos.DataSource = classPilotos;

            // Limpe a selecao inicial
            dgvClassificacaoPilotos.ClearSelection();
        }
        public void FinalDeTemporadaDataBaseCampeoes(string cata)
        {
            DataTable classEquipes = (DataTable)dgvClassificacaoEquipes.DataSource;
            DataTable classPilotos = (DataTable)dgvClassificacaoPilotos.DataSource;

            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgvClassificacaoEquipes.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Desative a opção de ordenação em todas as colunas
            foreach (DataGridViewColumn column in dgvClassificacaoPilotos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            // Ordene automaticamente a coluna 4 do maior para o menor
            dgvClassificacaoEquipes.Sort(dgvClassificacaoEquipes.Columns[0], ListSortDirection.Ascending);

            // Ordene automaticamente a coluna 5 do maior para o menor
            dgvClassificacaoPilotos.Sort(dgvClassificacaoPilotos.Columns[0], ListSortDirection.Ascending);

            for (int i = 0; i < dgvClassificacaoEquipes.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dgvClassificacaoPilotos.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value.ToString();

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < 1; i++)
            {
                principal.AdicionarPilotoCampeao(cata, principal.ContadorDeAno, dgvClassificacaoPilotos.Rows[i].Cells["Nacionalidade"].Value.ToString(), dgvClassificacaoPilotos.Rows[i].Cells["Nome"].Value.ToString(), Convert.ToInt32(dgvClassificacaoPilotos.Rows[i].Cells["P"].Value.ToString()), dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value.ToString(), dgvClassificacaoPilotos.Rows[i].Cells["Equipe"].Value.ToString());
                principal.AdicionarEquipeCampeao(cata, principal.ContadorDeAno, dgvClassificacaoEquipes.Rows[i].Cells["Nacionalidade"].Value.ToString(), dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value.ToString(), dgvClassificacaoEquipes.Rows[i].Cells["Nome"].Value.ToString(), Convert.ToInt32(dgvClassificacaoEquipes.Rows[i].Cells["P"].Value.ToString()));
            }
            dgvClassificacaoEquipes.ClearSelection();
            dgvClassificacaoPilotos.ClearSelection();
        }
        public void FinalDeTemporadaHistoricosDosPilotos()
        {
            for (int i = 0; i < piloto.Length; i++)
            {
                piloto[i].AdicionarHistoricosPiloto(piloto[i].PosicaoAtualCampeonato, principal.ContadorDeAno, piloto[i].Cor1, piloto[i].EquipePiloto, piloto[i].PontosCampeonato, piloto[i].Categoria);
            }
            for (int j = 0; j < equipe.Length; j++)
            {
                equipe[j].AdicionarHistoricosEquipe(equipe[j].PosicaoAtualCampeonato, principal.ContadorDeAno, equipe[j].NameMotor, equipe[j].Cor1, equipe[j].PontosCampeonato, equipe[j].PrimeiroColocado, equipe[j].SegundoColocado, equipe[j].TerceiroColocado, equipe[j].Categoria);
            }
        }
        public void FinalDeTemporadaLimpaTable()
        {
            for (int i = 0; i < piloto.Length; i++)
            {
                piloto[i].PontosCampeonato = 0;
                piloto[i].PrimeiroColocado = 0;
                piloto[i].SegundoColocado = 0;
                piloto[i].TerceiroColocado = 0;
                piloto[i].PosicaoAtualCampeonato = 0;
            }
            for (int j = 0; j < equipe.Length; j++)
            {
                equipe[j].PontosCampeonato = 0;
                equipe[j].PrimeiroColocado = 0;
                equipe[j].SegundoColocado = 0;
                equipe[j].TerceiroColocado = 0;
            }
            int equipeMin = 0;
            int equipeMax = 10;
            int position = 1;
            for (int l = equipeMin; l < equipeMax; l++)
            {
                for (int k = 0; k < piloto.Length; k++)
                {
                    if (equipe[l].NomeEquipe == piloto[k].EquipePiloto)
                    {
                        piloto[k].PosicaoAtualCampeonato = position;
                        position++;
                    }
                }
            }
            equipeMin = 10;
            equipeMax = 20;
            position = 1;
            for (int l = equipeMin; l < equipeMax; l++)
            {
                for (int k = 0; k < piloto.Length; k++)
                {
                    if (equipe[l].NomeEquipe == piloto[k].EquipePiloto)
                    {
                        piloto[k].PosicaoAtualCampeonato = position;
                        position++;
                    }
                }
            }
            equipeMin = 20;
            equipeMax = 30;
            position = 1;
            for (int l = equipeMin; l < equipeMax; l++)
            {
                for (int k = 0; k < piloto.Length; k++)
                {
                    if (equipe[l].NomeEquipe == piloto[k].EquipePiloto)
                    {
                        piloto[k].PosicaoAtualCampeonato = position;
                        position++;
                    }
                }
            }
        }
        public void FinalDeTemporadaAtualizarDB()
        {
            for (int i = 0; i < piloto.Length; i++)
            {
                piloto[i].IdadePiloto++;
                piloto[i].ContratoPiloto = piloto[i].ProximoAnoContratoPiloto;
                piloto[i].EquipePiloto = piloto[i].ProximoAnoEquipePiloto;
                piloto[i].SalarioPiloto = piloto[i].ProximoAnoSalarioPiloto;
                piloto[i].StatusPiloto = piloto[i].ProximoAnoStatusPiloto;
                if (piloto[i].Categoria == "F1" && piloto[i].PosicaoNaCorrida == 1) piloto[i].TituloF1++;
                if (piloto[i].Categoria == "F2" && piloto[i].PosicaoNaCorrida == 1) piloto[i].TituloF2++;
                if (piloto[i].Categoria == "F3" && piloto[i].PosicaoNaCorrida == 1) piloto[i].TituloF3++;
            }
            for (int i = 0; i < equipe.Length; i++)
            {
                equipe[i].PrimeiroPiloto = equipe[i].ProximoAnoPrimeiroPiloto;
                equipe[i].PrimeiroPilotoContrato = equipe[i].ProximoAnoPrimeiroPilotoContrato;
                equipe[i].PrimeiroPilotoSalario = equipe[i].ProximoAnoPrimeiroPilotoSalario;

                equipe[i].SegundoPiloto = equipe[i].ProximoAnoSegundoPiloto;
                equipe[i].SegundoPilotoContrato = equipe[i].ProximoAnoSegundoPilotoContrato;
                equipe[i].SegundoPilotoSalario = equipe[i].ProximoAnoSegundoPilotoSalario;
            }
            for (int i = 0; i < equipe.Length; i++)
            {
                for (int j = 0; j < piloto.Length; j++)
                {
                    if (piloto[j].EquipePiloto == equipe[i].NomeEquipe)
                    {
                        piloto[j].Cor1 = equipe[i].Cor1;
                        piloto[j].Cor2 = equipe[i].Cor2;
                        piloto[j].Categoria = equipe[i].Categoria;
                    }
                    else if (piloto[j].EquipePiloto == "")
                    {
                        piloto[j].Cor1 = "";
                        piloto[j].Cor2 = "";
                        piloto[j].Categoria = "";
                    }
                }
            }
        }
        public void InicioDeTemporadaAtualizarContratos()
        {
            for (int i = 0; i < equipe.Length; i++)
            {
                if (equipe[i].PrimeiroPilotoContrato == principal.ContadorDeAno)
                {
                    equipe[i].ProximoAnoPrimeiroPiloto = "";
                    equipe[i].ProximoAnoPrimeiroPilotoContrato = 0;
                    equipe[i].ProximoAnoPrimeiroPilotoSalario = 0;
                }
                else if (equipe[i].PrimeiroPilotoContrato > principal.ContadorDeAno)
                {
                    equipe[i].ProximoAnoPrimeiroPiloto = equipe[i].PrimeiroPiloto;
                    equipe[i].ProximoAnoPrimeiroPilotoContrato = equipe[i].PrimeiroPilotoContrato;
                    equipe[i].ProximoAnoPrimeiroPilotoSalario = equipe[i].PrimeiroPilotoSalario;
                }
                if (equipe[i].SegundoPilotoContrato == principal.ContadorDeAno)
                {
                    equipe[i].ProximoAnoSegundoPiloto = "";
                    equipe[i].ProximoAnoSegundoPilotoContrato = 0;
                    equipe[i].ProximoAnoSegundoPilotoSalario = 0;
                }
                else if (equipe[i].SegundoPilotoContrato > principal.ContadorDeAno)
                {
                    equipe[i].ProximoAnoSegundoPiloto = equipe[i].SegundoPiloto;
                    equipe[i].ProximoAnoSegundoPilotoContrato = equipe[i].SegundoPilotoContrato;
                    equipe[i].ProximoAnoSegundoPilotoSalario = equipe[i].SegundoPilotoSalario;
                }
            }
            for (int i = 0; i < piloto.Length; i++)
            {
                if (piloto[i].ContratoPiloto == principal.ContadorDeAno)
                {
                    piloto[i].ProximoAnoContratoPiloto = 0;
                    piloto[i].ProximoAnoEquipePiloto = "";
                    piloto[i].ProximoAnoSalarioPiloto = 0;
                    piloto[i].ProximoAnoStatusPiloto = "";
                }
                else if (piloto[i].ContratoPiloto > principal.ContadorDeAno)
                {
                    piloto[i].ProximoAnoContratoPiloto = piloto[i].ContratoPiloto;
                    piloto[i].ProximoAnoEquipePiloto = piloto[i].EquipePiloto;
                    piloto[i].ProximoAnoSalarioPiloto = piloto[i].SalarioPiloto;
                    piloto[i].ProximoAnoStatusPiloto = piloto[i].StatusPiloto;

                }
            }
        }
        public void OfertaDeContrato()
        {
            List<int> indicesAleatorios = new List<int>();
            for (int i = 0; i < piloto.Length; i++)
            {
                indicesAleatorios.Add(i);
            }
            foreach (Equipe equipe in equipe)
            {
                if (equipe.ProximoAnoPrimeiroPiloto == "")
                {
                    int opcaoDeOferta = random.Next(1, 6);  //20% de chance de fazer uma oferta na semana. (1 a 5, sendo 3 oferta concedida.)
                    if (opcaoDeOferta == 3)
                    {
                        Shuffle(indicesAleatorios);
                        int decicaoDeRenovação = random.Next(1, 3); // Vai decidir se a oferta vai ser de renovação ou de um novo piloto.
                        int mediaMin;
                        switch (equipe.Categoria)
                        {
                            case "F1":
                                mediaMin = 70;
                                break;
                            case "F2":
                                mediaMin = 40;
                                break;
                            default:
                                mediaMin = 10;
                                break;
                        }
                        if (decicaoDeRenovação == 1)
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].EquipePiloto == equipe.NomeEquipe && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                                    if (piloto[indice].SalarioPiloto < ofertaDeSalario)
                                    {
                                        piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                        piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                        piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;
                                        piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";

                                        equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                        equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                        equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;

                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                    piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";
                                    piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                    piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                                    equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                    equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                    equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (equipe.ProximoAnoSegundoPiloto == "")
                {
                    int opcaoDeOferta = random.Next(1, 6);  //20% de chance de fazer uma oferta na semana. (1 a 5, sendo 3 oferta concedida.)
                    if (opcaoDeOferta == 3)
                    {
                        Shuffle(indicesAleatorios);
                        int decicaoDeRenovação = random.Next(1, 3); // Vai decidir se a oferta vai ser de renovação ou de um novo piloto.
                        int mediaMin;
                        switch (equipe.Categoria)
                        {
                            case "F1":
                                mediaMin = 70;
                                break;
                            case "F2":
                                mediaMin = 40;
                                break;
                            default:
                                mediaMin = 10;
                                break;
                        }
                        if (decicaoDeRenovação == 1)
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].EquipePiloto == equipe.NomeEquipe && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                                    if (piloto[indice].SalarioPiloto < ofertaDeSalario)
                                    {
                                        piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                        piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                        piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;
                                        piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";

                                        equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                        equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                        equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                    piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";
                                    piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                    piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                                    equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                    equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                    equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OfertaDeContratoFimDeAno()
        {
            List<int> indicesAleatorios = new List<int>();
            for (int i = 0; i < piloto.Length; i++)
            {
                indicesAleatorios.Add(i);
            }
            indicesAleatorios.Sort((x, y) => piloto[y].MediaPiloto.CompareTo(piloto[x].MediaPiloto));

            foreach (Equipe equipe in equipe)
            {
                if (equipe.ProximoAnoPrimeiroPiloto == "")
                {

                    foreach (int indice in indicesAleatorios)
                    {
                        if (piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                        {
                            piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                            piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";
                            piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                            piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                            equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                            equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                            equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                            break;
                        }
                    }
                }
                if (equipe.ProximoAnoSegundoPiloto == "")
                {
                    foreach (int indice in indicesAleatorios)
                    {
                        if (piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                        {
                            piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                            piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";
                            piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                            piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                            equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                            equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                            equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                            break;
                        }
                    }
                }
            }

        }
        public void ContratoDeMotores()
        {
            foreach (Equipe equipe in equipe)
            {
                int contratoMotor = random.Next(1, 6);

                if (contratoMotor == 3)
                {
                    string novoMotor = motor.ObterNomeAleatorioDoMotor();
                    equipe.NameMotor = novoMotor;
                    equipe.ValorDoMotor = motor.ObterValorDoMotor(equipe.NameMotor);
                }
            }
        }
        public void AposentarPilot()
        {
            for (int i = 0; i < piloto.Length; i++)
            {
                if (piloto[i].IdadePiloto == piloto[i].AposentadoriaPiloto && piloto[i].EquipePiloto == "")
                {
                    Piloto newPiloto = new Piloto();
                    newPiloto.GeraPiloto();
                    piloto[i] = newPiloto;
                    // Vamos aposetar o piloto, prescisa salvar os dados do piloto aposentado, ainda não foi feito.
                }
                piloto[i].PontosCampeonato = 0;
            }
        }
        public static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public void MetodoParaQualificarEquipes(int equipeMin, int equipeMax)
        {
            for (int i = equipeMin; i < equipeMax; i++)
            {
                equipe[i].PosicaoAtualCampeonato = 1;
                for (int j = equipeMin; j < equipeMax; j++)
                {
                    if (i != j)
                    {
                        if (equipe[i].PontosCampeonato <= equipe[j].PontosCampeonato)
                        {
                            if (equipe[i].PontosCampeonato == equipe[j].PontosCampeonato)
                            {
                                if (equipe[i].PrimeiroColocado == equipe[j].PrimeiroColocado)
                                {
                                    if (equipe[i].SegundoColocado == equipe[j].SegundoColocado)
                                    {
                                        if (equipe[i].TerceiroColocado == equipe[j].TerceiroColocado)
                                        {
                                            if (i > j)
                                            {
                                                equipe[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (equipe[i].TerceiroColocado < equipe[j].TerceiroColocado)
                                        {
                                            equipe[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (equipe[i].SegundoColocado < equipe[j].SegundoColocado)
                                    {
                                        equipe[i].PosicaoAtualCampeonato++;
                                    }
                                }
                                else if (equipe[i].PrimeiroColocado < equipe[j].PrimeiroColocado)
                                {
                                    equipe[i].PosicaoAtualCampeonato++;
                                }
                            }
                            else if (equipe[i].PontosCampeonato < equipe[j].PontosCampeonato)
                            {
                                equipe[i].PosicaoAtualCampeonato++;
                            }
                        }
                    }
                }
            }
        }
        public void MetodoParaQualificarPilotos(string fCategoria)
        {
            for (int i = 0; i < piloto.Length; i++)
            {
                if (piloto[i].Categoria == fCategoria)
                {
                    piloto[i].PosicaoAtualCampeonato = 1;
                    for (int j = 0; j < piloto.Length; j++)
                    {
                        if (piloto[j].Categoria == fCategoria)
                        {
                            if (i != j)
                            {
                                if (piloto[i].PontosCampeonato <= piloto[j].PontosCampeonato)
                                {
                                    if (piloto[i].PontosCampeonato == piloto[j].PontosCampeonato)
                                    {
                                        if (piloto[i].PrimeiroColocado == piloto[j].PrimeiroColocado)
                                        {
                                            if (piloto[i].SegundoColocado == piloto[j].SegundoColocado)
                                            {
                                                if (piloto[i].TerceiroColocado == piloto[j].TerceiroColocado)
                                                {
                                                    if (i > j)
                                                    {
                                                        piloto[i].PosicaoAtualCampeonato++;
                                                    }
                                                }
                                                else if (piloto[i].TerceiroColocado < piloto[j].TerceiroColocado)
                                                {
                                                    piloto[i].PosicaoAtualCampeonato++;
                                                }
                                            }
                                            else if (piloto[i].SegundoColocado < piloto[j].SegundoColocado)
                                            {
                                                piloto[i].PosicaoAtualCampeonato++;
                                            }
                                        }
                                        else if (piloto[i].PrimeiroColocado < piloto[j].PrimeiroColocado)
                                        {
                                            piloto[i].PosicaoAtualCampeonato++;
                                        }
                                    }
                                    else if (piloto[i].PontosCampeonato < piloto[j].PontosCampeonato)
                                    {
                                        piloto[i].PosicaoAtualCampeonato++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void SalvarDadosDosArquivo()
        {
            // Copia referências (ou você pode implementar uma cópia profunda se necessário).
            Principal salvarPrincipal = principal;
            Equipe[] salvarEquipe = equipe;
            Piloto[] salvarPiloto = piloto;
            Pista[] salvarPista = pista;
            Financia salvaFinancia = financia;

            // Cria um objeto DadosCompletos para salvar todos os dados.
            DadosCompletos dadosCompletos = new DadosCompletos
            {
                Principal = salvarPrincipal,
                Equipes = salvarEquipe,
                Pilotos = salvarPiloto,
                Pistas = salvarPista,
                Financia = salvaFinancia
            };

            try
            {
                // Serializa os dados para JSON e salva no arquivo.
                string json = JsonSerializer.Serialize(dadosCompletos);
                File.WriteAllText("dados_completos.json", json);
                MessageBox.Show("Dados das equipes e pilotos salvos com sucesso.");
            }
            catch (Exception ex)
            {
                // Tratamento de erros de salvamento.
                MessageBox.Show($"Erro ao salvar os dados: {ex.Message}");
            }
        }
        public void CarregarDadosDosArquivos()
        {
            try
            {
                if (File.Exists("dados_completos.json"))
                {
                    // Lê o arquivo JSON e desserializa os dados.
                    string json = File.ReadAllText("dados_completos.json");
                    DadosCompletos dadosCompletos = JsonSerializer.Deserialize<DadosCompletos>(json);

                    if (dadosCompletos != null)
                    {
                        // Carrega os dados desserializados nas variáveis do jogo.
                        principal = dadosCompletos.Principal;
                        equipe = dadosCompletos.Equipes;
                        piloto = dadosCompletos.Pilotos;
                        pista = dadosCompletos.Pistas;
                        financia = dadosCompletos.Financia;
                        MessageBox.Show("Dados das equipes e pilotos carregados com sucesso.");
                    }
                    else
                    {
                        MessageBox.Show("Erro: Dados nulos após a desserialização.");
                    }
                }
                else
                {
                    MessageBox.Show("Arquivo 'dados_completos.json' não encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erros ao carregar os dados.
                MessageBox.Show($"Erro ao carregar os dados: {ex.Message}");
            }
        }
        public void PictureBoxBtnContinuar_Click(object sender, EventArgs e)
        {
            if (principal.ContadorDeSemana == principal.ProximoGpSemana)
            {
                TelaFinalDeSemanaDeCorrida telaFinalDeSemanaDeCorrida = new TelaFinalDeSemanaDeCorrida(principal, equipe, piloto, pista);
                telaFinalDeSemanaDeCorrida.ShowDialog();

                if (piloto[indexDoJogador].Categoria == "F1")
                {
                    PreencherDataGridViewClassEquipes(0, 10);
                    PreencherDataGridViewClassPilotos(0, 10);
                }
                else if (piloto[indexDoJogador].Categoria == "F2")
                {
                    PreencherDataGridViewClassEquipes(10, 20);
                    PreencherDataGridViewClassPilotos(10, 20);
                }
                else if (piloto[indexDoJogador].Categoria == "F3")
                {
                    PreencherDataGridViewClassEquipes(20, 30);
                    PreencherDataGridViewClassPilotos(20, 30);
                }
                else
                {
                    PreencherDataGridViewClassEquipes(0, 10);
                    PreencherDataGridViewClassPilotos(0, 10);
                }

                AtualizarTabelas();
                OfertaDeContrato();
                principal.PotenciaMotoresEquipe(motor, equipe);
                principal.XpTurnoSemanal(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarFinanciasSemanal();
                AtualizarNomesNaTelaInicial();

            }
            else if (principal.ContadorDeSemana == principal.TotalSemanas)
            {
                FinalDeTemporadaHistoricosDosPilotos();

                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        PreencherDataGridViewClassEquipes(0, 10);
                        PreencherDataGridViewClassPilotos(0, 10);
                        FinalDeTemporadaDataBaseCampeoes("F1");
                    }
                    else if (i == 1)
                    {
                        PreencherDataGridViewClassPilotos(10, 20);
                        PreencherDataGridViewClassEquipes(10, 20);
                        FinalDeTemporadaDataBaseCampeoes("F2");
                    }
                    else if (i == 2)
                    {
                        PreencherDataGridViewClassPilotos(20, 30);
                        PreencherDataGridViewClassEquipes(20, 30);
                        FinalDeTemporadaDataBaseCampeoes("F3");
                    }
                }
                OfertaDeContrato();
                // PREENCHER TODOS OS CONTRATOS
                OfertaDeContratoFimDeAno();
                principal.XpTurnoSemanal(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarFinanciasSemanal();
                FinalDeTemporadaAtualizarDB();
                InicioDeTemporadaAtualizarContratos();
                ContratoDeMotores();
                AposentarPilot();

                FinalDeTemporadaLimpaTable();

                principal.CorPrincipal = piloto[indexDoJogador].Cor1;
                principal.CorSecundaria = piloto[indexDoJogador].Cor2;
                corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
                corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
                principal.IdadeJogador = piloto[indexDoJogador].IdadePiloto;
                AtualizarTabelaInicioDeTemporada();
                AtualizarCores();
                AtualizarNomesNaTelaInicial();
            }
            else
            {
                OfertaDeContrato();
                principal.XpTurnoSemanal(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarFinanciasSemanal();
                AtualizarNomesNaTelaInicial();
            }
        }
        public void PictureBox1_Click(object sender, EventArgs e) // Botão de fechar o jogo.
        {
            this.Close();
        }
        public void PictureBox2_Click(object sender, EventArgs e) // Botão de salvar o jogo.
        {
            SalvarDadosDosArquivo();
        }
        public void PictureBox3_Click(object sender, EventArgs e) // Botão de configuração do jogo.
        {
            /*
            TelaSettings telaSettings = new TelaSettings(principal);
            telaSettings.ShowDialog();
            AtualizarCores();
            */
        }
        public void PictureBoxClassificacao_Click(object sender, EventArgs e)
        {
            TelaClassificacao telaClassificacao = new TelaClassificacao(principal, equipe, piloto, pista);
            telaClassificacao.ShowDialog();
        }
        public void PictureBoxPilotos_Click(object sender, EventArgs e)
        {
            TelaPilotos telaPilotos = new TelaPilotos(principal, equipe, piloto);
            telaPilotos.ShowDialog();
        }
        public void PictureBoxEquipes_Click(object sender, EventArgs e)
        {
            TelaEquipes telaEquipes = new TelaEquipes(principal, equipe, piloto);
            telaEquipes.ShowDialog();
        }
        public void PictureBoxFinancias_Click(object sender, EventArgs e)
        {
            TelaFinancias telaFinancias = new TelaFinancias(principal, financia);
            telaFinancias.ShowDialog();
        }
    }
    class DadosCompletos
    {
        public required Principal Principal { get; set; }
        public required Equipe[] Equipes { get; set; }
        public required Piloto[] Pilotos { get; set; }
        public required Pista[] Pistas { get; set; }
        public required Financia Financia { get; set; }
    }
}
