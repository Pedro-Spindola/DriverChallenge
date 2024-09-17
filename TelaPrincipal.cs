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
using static DriverChallenge.Financia;
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
                Console.WriteLine("Carregado");
            }
            // Converte a string hexadecimal em um objeto Color
            principal.CorPrincipal = piloto[principal.IndexDoJogador].Cor1;
            principal.CorSecundaria = piloto[principal.IndexDoJogador].Cor2;
            corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
            corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
            AtualizarCores();
            AtualizarNomesNaTelaInicial();
            CriarDataGridViewClassEquipes();
            CriarDataGridViewClassPilotos();
            CriarDataGridViewCaixaDeEmail();
            AtualizarNomesNaTelaInicial();
            AtualizarTabelaInicioDeTemporada();
            AtualizarTabelas();
            PreencherDataGridViewCaixaDeEmail();
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

            equipe[0] = new Equipe("Blue Falcon", "#03183B", "#C70101", "#FFFFFF", "Austria", 92, 91, 94, 90, 93, 96, 94, 91, "Honda", "F1", motor);
            equipe[1] = new Equipe("Silver Arrow", "#C4C4C4", "#09BF81", "#000000", "Alemanha", 86, 87, 84, 85, 86, 85, 84, 87, "Mercedes", "F1", motor);
            equipe[2] = new Equipe("Scarlet Horse", "#FF0000", "#FFFFFF", "#000000", "Itália", 83, 82, 88, 86, 85, 84, 85, 84, "Ferrari", "F1", motor);
            equipe[3] = new Equipe("Royal Racing", "#112685", "#FFFFFF", "#FFFFFF", "Inglaterra", 77, 74, 76, 75, 76, 74, 77, 76, "TAG", "F1", motor);
            equipe[4] = new Equipe("Green Viper", "#004039", "#FFFFFF", "#FFFFFF", "Inglaterra", 81, 79, 82, 80, 78, 81, 79, 80, "Mercedes", "F1", motor);
            equipe[5] = new Equipe("Orange Rocket", "#FF8D36", "#000000", "#FFFFFF", "Inglaterra", 91, 89, 92, 90, 91, 88, 90, 91, "Honda", "F1", motor);
            equipe[6] = new Equipe("Pink Panther", "#CE4A8D", "#2075DC", "#000000", "França", 82, 80, 79, 81, 83, 80, 81, 80, "Renault", "F1", motor);
            equipe[7] = new Equipe("Cash Chariot", "#0456D9", "#B10407", "#000000", "Itália", 76, 74, 75, 77, 74, 75, 76, 75, "TAG", "F1", motor);
            equipe[8] = new Equipe("Shadow Wolf", "#000000", "#0BEE23", "#FFFFFF", "Suíça", 71, 70, 69, 72, 70, 71, 70, 71, "Ferrari", "F1", motor);
            equipe[9] = new Equipe("Eagle Racing", "#002420", "#000000", "#FFFFFF", "Estados Unidos", 72, 69, 71, 70, 70, 71, 69, 71, "Ferrari", "F1", motor);
            equipe[10] = new Equipe("Speed Masters", "#FF883C", "#FF883C", "#FFFFFF", "Holanda", 67, 64, 65, 66, 64, 66, 67, 64, "TAG", "F2", motor);
            equipe[11] = new Equipe("Infinity Power", "#CCCCCC", "#991F21", "#000000", "Alemanha", 66, 64, 65, 66, 66, 65, 64, 66, "Audi", "F2", motor);
            equipe[12] = new Equipe("Blue Wings", "#2151B0", "#75FF07", "#000000", "Inglaterra", 61, 62, 60, 63, 61, 60, 62, 61, "Renault", "F2", motor);
            equipe[13] = new Equipe("Golden Arrow", "#FFE120", "#000000", "#FFFFFF", "Inglaterra", 62, 60, 61, 60, 59, 60, 61, 60, "Mercedes", "F2", motor);
            equipe[14] = new Equipe("Red Blaze", "#FF3622", "#FFFFFF", "#000000", "Itália", 56, 54, 55, 55, 57, 55, 54, 55, "TAG", "F2", motor);
            equipe[15] = new Equipe("Steel Phoenix", "#808080", "#000000", "#000000", "Inglaterra", 51, 49, 50, 50, 51, 52, 50, 51, "BMW", "F2", motor);
            equipe[16] = new Equipe("Frostbite Racing", "#113861", "#48D4FF", "#FFFFFF", "França", 46, 44, 45, 46, 47, 45, 44, 46, "Renault", "F2", motor);
            equipe[17] = new Equipe("Black Stallion", "#000000", "#FF883C", "#FFFFFF", "Holanda", 46, 44, 45, 46, 45, 46, 45, 46, "Ford", "F2", motor);
            equipe[18] = new Equipe("Lambro Racing", "#000000", "#FFAC11", "#FFFFFF", "Itália", 41, 39, 40, 40, 41, 40, 39, 41, "Lamborghini", "F2", motor);
            equipe[19] = new Equipe("Aqua Force", "#3706BF", "#FF3024", "#000000", "Itália", 42, 40, 41, 41, 43, 41, 40, 42, "Toyota", "F2", motor);
            equipe[20] = new Equipe("Blue Thunder", "#117CFF", "#FFFFFF", "#000000", "Alemanha", 36, 34, 35, 36, 35, 36, 34, 35, "BMW", "F3", motor);
            equipe[21] = new Equipe("White Hawk", "#FFFFFF", "#FF3629", "#000000", "Alemanha", 34, 36, 35, 34, 35, 34, 36, 34, "Audi", "F3", motor);
            equipe[22] = new Equipe("Red Tiger", "#C22A1F", "#C22A1F", "#FFFFFF", "Japão", 31, 29, 30, 31, 30, 31, 29, 30, "Ford", "F3", motor);
            equipe[23] = new Equipe("Fire Fox", "#FFB22A", "#EB3326", "#000000", "Espanha", 26, 24, 25, 26, 25, 26, 24, 25, "BMW", "F3", motor);
            equipe[24] = new Equipe("Maple Leaf Racing", "#FF9A1C", "#3444FF", "#000000", "Canadá", 21, 20, 22, 21, 20, 21, 20, 22, "Ford", "F3", motor);
            equipe[25] = new Equipe("Bright Light", "#55BEFF", "#55BEFF", "#FFFFFF", "Bélgica", 22, 21, 23, 22, 20, 22, 21, 22, "Lamborghini", "F3", motor);
            equipe[26] = new Equipe("Purple Lightning", "#9551FF", "#9551FF", "#FFFFFF", "Alemanha", 16, 14, 15, 16, 15, 16, 14, 15, "Toyota", "F3", motor);
            equipe[27] = new Equipe("Swiss Blaze", "#FF0081", "#236EFF", "#FFFFFF", "Suíça", 11, 10, 12, 11, 10, 11, 10, 12, "Ford", "F3", motor);
            equipe[28] = new Equipe("Iron Eagle", "#FF6E63", "#CCCCCC", "#000000", "Estados Unidos", 12, 10, 11, 12, 10, 12, 10, 12, "Toyota", "F3", motor);
            equipe[29] = new Equipe("Green Lightning", "#2D7D4E", "#FFD91C", "#000000", "Brasil", 13, 10, 12, 13, 10, 13, 10, 12, "Lamborghini", "F3", motor);

            */
            // Equipes F1 (média entre 70 a 100)
            equipe[0] = new Equipe("Red Bull", "#03183B", "#C70101", "#FFFFFF", "Áustria", 92, 91, 94, 90, 93, 96, 94, 91, "Honda", "F1", motor);
            equipe[1] = new Equipe("Mercedes", "#C4C4C4", "#09BF81", "#000000", "Alemanha", 86, 87, 84, 85, 86, 85, 84, 87, "Mercedes", "F1", motor);
            equipe[2] = new Equipe("Ferrari", "#FF0000", "#FFFFFF", "#000000", "Itália", 83, 82, 88, 86, 85, 84, 85, 84, "Ferrari", "F1", motor);
            equipe[3] = new Equipe("Williams", "#112685", "#FFFFFF", "#FFFFFF", "Inglaterra", 77, 74, 76, 75, 76, 74, 77, 76, "TAG", "F1", motor);
            equipe[4] = new Equipe("Aston Martin", "#004039", "#FFFFFF", "#FFFFFF", "Inglaterra", 81, 79, 82, 80, 78, 81, 79, 80, "Mercedes", "F1", motor);
            equipe[5] = new Equipe("McLaren", "#FF8D36", "#000000", "#FFFFFF", "Inglaterra", 91, 89, 92, 90, 91, 88, 90, 91, "Honda", "F1", motor);
            equipe[6] = new Equipe("Alpine", "#CE4A8D", "#2075DC", "#000000", "França", 82, 80, 79, 81, 83, 80, 81, 80, "Renault", "F1", motor);
            equipe[7] = new Equipe("Visa Cash", "#0456D9", "#B10407", "#000000", "Itália", 76, 74, 75, 77, 74, 75, 76, 75, "TAG", "F1", motor);
            equipe[8] = new Equipe("Stake Sauber", "#000000", "#0BEE23", "#FFFFFF", "Suíça", 71, 70, 69, 72, 70, 71, 70, 71, "Ferrari", "F1", motor);
            equipe[9] = new Equipe("Haas", "#002420", "#000000", "#FFFFFF", "Estados Unidos", 72, 69, 71, 70, 70, 71, 69, 71, "Ferrari", "F1", motor);

            // Equipe F2 (média entre 40 a 70)
            equipe[10] = new Equipe("MP Motorsport", "#FF883C", "#FF883C", "#FFFFFF", "Holanda", 67, 64, 65, 66, 64, 66, 67, 64, "TAG", "F2", motor);
            equipe[11] = new Equipe("Infinity Audi", "#CCCCCC", "#991F21", "#000000", "Alemanha", 66, 64, 65, 66, 66, 65, 64, 66, "Audi", "F2", motor);
            equipe[12] = new Equipe("Carlin", "#2151B0", "#75FF07", "#000000", "Inglaterra", 61, 62, 60, 63, 61, 60, 62, 61, "Renault", "F2", motor);
            equipe[13] = new Equipe("Jordan", "#FFE120", "#000000", "#FFFFFF", "Inglaterra", 62, 60, 61, 60, 59, 60, 61, 60, "Mercedes", "F2", motor);
            equipe[14] = new Equipe("Prema", "#FF3622", "#FFFFFF", "#000000", "Itália", 56, 54, 55, 55, 57, 55, 54, 55, "TAG", "F2", motor);
            equipe[15] = new Equipe("Hitech", "#808080", "#000000", "#000000", "Inglaterra", 51, 49, 50, 50, 51, 52, 50, 51, "BMW", "F2", motor);
            equipe[16] = new Equipe("DAMS", "#113861", "#48D4FF", "#FFFFFF", "França", 46, 44, 45, 46, 47, 45, 44, 46, "Renault", "F2", motor);
            equipe[17] = new Equipe("Amersfoort", "#000000", "#FF883C", "#FFFFFF", "Holanda", 46, 44, 45, 46, 45, 46, 45, 46, "Ford", "F2", motor);
            equipe[18] = new Equipe("Lamborghini", "#000000", "#FFAC11", "#FFFFFF", "Itália", 41, 39, 40, 40, 41, 40, 39, 41, "Lamborghini", "F2", motor);
            equipe[19] = new Equipe("Trident", "#3706BF", "#FF3024", "#000000", "Itália", 42, 40, 41, 41, 43, 41, 40, 42, "Toyota", "F2", motor);

            // Equipe F3 (média entre 10 a 40)
            equipe[20] = new Equipe("BMW", "#117CFF", "#FFFFFF", "#000000", "Alemanha", 36, 34, 35, 36, 35, 36, 34, 35, "BMW", "F3", motor);
            equipe[21] = new Equipe("Penske Porsche", "#FFFFFF", "#FF3629", "#000000", "Alemanha", 34, 36, 35, 34, 35, 34, 36, 34, "Audi", "F3", motor);
            equipe[22] = new Equipe("Toyota Gazoo", "#C22A1F", "#C22A1F", "#FFFFFF", "Japão", 31, 29, 30, 31, 30, 31, 29, 30, "Ford", "F3", motor);
            equipe[23] = new Equipe("Campos", "#FFB22A", "#EB3326", "#000000", "Espanha", 26, 24, 25, 26, 25, 26, 24, 25, "BMW", "F3", motor);
            equipe[24] = new Equipe("Tower Motorsports", "#FF9A1C", "#3444FF", "#000000", "Canadá", 21, 20, 22, 21, 20, 21, 20, 22, "Ford", "F3", motor);
            equipe[25] = new Equipe("Team WRT", "#55BEFF", "#55BEFF", "#FFFFFF", "Bélgica", 22, 21, 23, 22, 20, 22, 21, 22, "Lamborghini", "F3", motor);
            equipe[26] = new Equipe("Proton", "#9551FF", "#9551FF", "#FFFFFF", "Alemanha", 16, 14, 15, 16, 15, 16, 14, 15, "Toyota", "F3", motor);
            equipe[27] = new Equipe("Kessel", "#FF0081", "#236EFF", "#FFFFFF", "Suíça", 11, 10, 12, 11, 10, 11, 10, 12, "Ford", "F3", motor);
            equipe[28] = new Equipe("Action Express", "#FF6E63", "#CCCCCC", "#000000", "Estados Unidos", 12, 10, 11, 12, 10, 12, 10, 12, "Toyota", "F3", motor);
            equipe[29] = new Equipe("Team Senna", "#2D7D4E", "#FFD91C", "#000000", "Brasil", 13, 10, 12, 13, 10, 13, 10, 12, "Lamborghini", "F3", motor);

            // Método para chamar uma Tela, onde jogador vai escolher a sua equipe inicial.
            EscolherEquipeInicialDoJogador();
            Random r = new Random();
            for (int i = 0; i < piloto.Length; i++)
            {
                
                if (i >= 0 && i <= 19)
                {
                    piloto[i].MetodoProvisorioParaAumentarIdade(r.Next(8, 11));
                }
                if (i >= 20 && i <= 39)
                {
                    piloto[i].MetodoProvisorioParaAumentarIdade(r.Next(5, 9));
                }
                if (i >= 60 && i <= 69)
                {
                    piloto[i].MetodoProvisorioParaAumentarIdade(r.Next(8, 11));
                }
                if (i >= 70 && i <= 79)
                {
                    piloto[i].MetodoProvisorioParaAumentarIdade(r.Next(5, 9));
                }
            }

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
                    principal.XpTurnoSemanal(piloto);
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
                    principal.XpTurnoSemanal(piloto);
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
            // principal.XpTurnoSemanalJogador(piloto);
            CriandoOsDadosPistas();
            EmbaralharPistas();
            DefinirDataDaSemanaDeProvaDaPista();

            principal.IdadeJogador = piloto[principal.IndexDoJogador].IdadePiloto;

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
                int valorArredondado = (int)(Math.Round(salario / 5.0) * 5);
                return valorArredondado;
            }
            if (categoria == "F2")
            {
                int habilidade = mediaHabilidade * 20;
                int bases = random.Next(8000, 10001);
                int bonus = random.Next(5000, 10001);
                int salario = (((habilidade * bases) / 200) + bonus);
                int valorArredondado = (int)(Math.Round(salario / 5.0) * 5);
                return valorArredondado;
            }
            if (categoria == "F3")
            {
                int habilidade = mediaHabilidade * 20;
                int bases = random.Next(6000, 8001);
                int bonus = random.Next(5000, 10001);
                int salario = (((habilidade * bases) / 200) + bonus);
                int valorArredondado = (int)(Math.Round(salario / 5.0) * 5);
                return valorArredondado;
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
            pista[0] = new Pista("Austrália", "Melbourne", 58, 44, 56, 76800, 7, 8, 9, 10, 11);
            pista[1] = new Pista("Itália", "Monza", 53, 35, 65, 70200, 4, 5, 6, 7, 8, 9);
            pista[2] = new Pista("Brasil", "Interlagos", 71, 42, 58, 65400, 16, 17, 18, 19, 20, 21);
            pista[3] = new Pista("Bahrein", "Sakhir", 57, 43, 57, 77400, 7, 8, 9, 10, 11);
            pista[4] = new Pista("Arábia Saudita", "Corniche Circuit", 49, 58, 42, 76200, 3, 4, 5, 6);
            pista[5] = new Pista("Japão", "Suzuka", 53, 59, 41, 75420, 4, 5, 6, 10, 11);
            pista[6] = new Pista("China", "Shanghai", 56, 48, 52, 81000, 7, 8, 9, 10, 11);
            pista[7] = new Pista("Estados Unidos", "Miami", 57, 56, 44, 73200, 7, 8, 9, 10, 11);
            pista[8] = new Pista("Itália", "Imola", 63, 32, 62, 72600, 12, 13, 14, 15);
            pista[9] = new Pista("Mônaco", "Monte Carlo", 78, 64, 36, 67800, 22, 23, 24, 25, 26, 27);
            pista[10] = new Pista("Canadá", "Gilles Vileneuve", 70, 40, 60, 67200, 16, 17, 18, 19, 20, 21);
            pista[11] = new Pista("Espanha", "Catalunha", 66, 40, 60, 69000, 12, 13, 14, 15);
            pista[12] = new Pista("Áustria", "Red Bull Ring", 71, 25, 75, 63600, 16, 17, 18, 19, 20, 21);
            pista[13] = new Pista("Reino Unido", "Silverstone", 52, 43, 57, 74400, 4, 5, 6, 10, 11);
            pista[14] = new Pista("Holanda", "Zandvoort", 72, 48, 52, 67200, 16, 17, 18, 19, 20, 21);
            pista[15] = new Pista("Hungria", "Hungaroring", 70, 46, 54, 72000, 16, 17, 18, 19, 20, 21);
            pista[16] = new Pista("Bélgica", "Spa-Francorchamps", 44, 47, 53, 88200, 1, 2, 3, 4, 5, 6);
            pista[17] = new Pista("África do Sul", "Kyalami", 45, 55, 72, 70800, 1, 2, 3, 4, 5, 6);
            pista[18] = new Pista("México", "Hermanos Rodríguez", 71, 38, 62, 67800, 16, 17, 18, 19, 20, 21);
            pista[19] = new Pista("Azerbaijão", "Baku", 51, 59, 41, 88200, 3, 4, 5, 6);
            pista[20] = new Pista("Singapura", "Marina Bay", 62, 52, 48, 82200, 12, 13, 14, 15);
            pista[21] = new Pista("Catar", "Lusail Circuit", 57, 59, 41, 72480, 7, 8, 9, 10, 11);
            pista[22] = new Pista("Estados Unidos", "Las Vegas", 50, 70, 30, 81100, 3, 4, 5, 6);
            pista[23] = new Pista("Emirados Árabes Unidos", "Yas Marina", 58, 39, 61, 74400, 7, 8, 9, 10, 11);
            pista[24] = new Pista("Alemanha", "Hockenheimring", 67, 42, 58, 68400, 15, 16, 17, 18, 19, 20, 21);
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
                    pictureBoxMensagem.Image = Properties.Resources.cx_msg_w;
                    panel1.ForeColor = Color.White;
                }
                else
                {
                    pictureBoxBtnFechar.Image = Properties.Resources.fechar_b;
                    pictureBoxBtnSalvar.Image = Properties.Resources.salvar_b;
                    pictureBoxBtnOpcao.Image = Properties.Resources.opcao_b;
                    pictureBoxBtnContinuar.Image = Properties.Resources.menu_continuar_b;
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
            panelCorP7.BackColor = corPrincipal;
            panelCorS1.BackColor = corSecundaria;
            panelCorS2.BackColor = corSecundaria;
            panelCorS3.BackColor = corSecundaria;
            panelCorS4.BackColor = corSecundaria;
            panelCorS5.BackColor = corSecundaria;
            panelCorS6.BackColor = corSecundaria;
        }
        public void AtualizarEscritorioSemanal()
        {
            for (int i = 0; i < 2; i++)
            {
                if (piloto[principal.IndexDoJogador].PropostaDeContrato[i].PropostaAceita == false && piloto[principal.IndexDoJogador].PropostaDeContrato[i].TempoPropostaContrato != 0)
                {
                    piloto[principal.IndexDoJogador].PropostaDeContrato[i].TempoPropostaContrato--;
                }
                if (piloto[principal.IndexDoJogador].PropostaDeContrato[i].TempoPropostaContrato == 0)
                {
                    foreach (Equipe equipe in equipe)
                    {
                        if (equipe.NomeEquipe == piloto[principal.IndexDoJogador].PropostaDeContrato[i].NomeDaEquipe && piloto[principal.IndexDoJogador].PropostaDeContrato[i].PropostaAceita == false)
                        {
                            if (piloto[principal.IndexDoJogador].PropostaDeContrato[i].StatusDoPiloto == "1º Piloto")
                            {
                                equipe.ProximoAnoPrimeiroPiloto = "";
                            }
                            if(piloto[principal.IndexDoJogador].PropostaDeContrato[i].StatusDoPiloto == "2º Piloto")
                            {
                                equipe.ProximoAnoSegundoPiloto = "";
                            }
                        }
                    }
                    piloto[principal.IndexDoJogador].LimparPropostaDeContrato(piloto[principal.IndexDoJogador].PropostaDeContrato[i]);
                }
            }
        }
        public void AtualizarFinanciasSemanal()
        {
            if (piloto[principal.IndexDoJogador].SalarioPiloto != financia.SalarioDaEquipe)
            {
                financia.SalarioDaEquipe = piloto[principal.IndexDoJogador].SalarioPiloto;
            }
            AtualizarSemanaPatrocinadores();
        }
        public void AtualizarSemanaPatrocinadores()
        {
            for (int i = 0; i < financia.Patrocinadores.Length; i++)
            {
                if (financia.Patrocinadores[i].ContratoValido == false && financia.Patrocinadores[i].TempoPropostaContrato != 0)
                {
                    financia.Patrocinadores[i].TempoPropostaContrato--;
                    if (financia.Patrocinadores[i].TempoPropostaContrato == 0)
                    {
                        principal.NovaMessagemEmail("Patrocinador", "Oferta de patrocínio retirada.");
                    }
                }
                if (financia.Patrocinadores[i].ContratoValido)
                {
                    financia.Patrocinadores[i].TempoDeContratoSemanal--;
                }
                if (financia.Patrocinadores[i].TempoDeContratoSemanal == 0 && financia.Patrocinadores[i].ContratoValido)
                {
                    principal.NovaMessagemEmail("Patrocinador", "Oferta de patrocínio encerrado.");
                    financia.limparPatrocinador(financia.Patrocinadores[i]);
                    financia.EspacoContratoDisponivel++;
                }
            }
            financia.DinheiroJogadorTotal += financia.retonarSalarioTotalSemanal();
        }
        public void AtualizarNomesNaTelaInicial()
        {
            labelNomeDoJogador.Text = string.Format("{0} {1}", principal.NomeJogador, principal.SobrenomeJogador);
            labelEquipeDoJogador.Text = piloto[principal.IndexDoJogador].EquipePiloto;
            labelIdadeJogador.Text = string.Format("Idade: {0:N0}", principal.IdadeJogador.ToString());
            pictureBoxNacionalidadePiloto.ImageLocation = Path.Combine("Paises", principal.NacionalidadeJogador + ".png");
            labelSaldoNaConta.Text = financia.DinheiroJogadorTotal.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
            labelSaldoPorSemana.Text = financia.retonarSalarioTotalSemanal().ToString("C", new System.Globalization.CultureInfo("pt-BR"));
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
                string cor1Texto = dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dgvClassificacaoPilotos.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

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
            if (piloto[principal.IndexDoJogador].Categoria == "F1")
            {
                PreencherDataGridViewClassEquipes(0, 10);
                PreencherDataGridViewClassPilotos(0, 10);
                AtualizarTabelas();
            }
            else if (piloto[principal.IndexDoJogador].Categoria == "F2")
            {
                PreencherDataGridViewClassPilotos(10, 20);
                PreencherDataGridViewClassEquipes(10, 20);
                AtualizarTabelas();
            }
            else if (piloto[principal.IndexDoJogador].Categoria == "F3")
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
            dgvClassificacaoEquipes.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvClassificacaoEquipes.GridColor = Color.FromArgb(200, 200, 200);
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
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
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
            dgvClassificacaoPilotos.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvClassificacaoPilotos.GridColor = Color.FromArgb(200, 200, 200);
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
                string imagePath = row["Path"]?.ToString() ?? string.Empty;
                row["Nac"] = Image.FromFile(imagePath);

            }
            // Atualize o DataGridView para refletir as mudancas
            dgvClassificacaoPilotos.DataSource = classPilotos;

            // Limpe a selecao inicial
            dgvClassificacaoPilotos.ClearSelection();
        }
        public void CriarDataGridViewCaixaDeEmail()
        {
            DataTable listMessagem = new DataTable();
            DataColumn TipoColumn = new DataColumn("Tipo", typeof(Image));

            listMessagem.Columns.Add("New", typeof(string));
            listMessagem.Columns.Add(TipoColumn);
            listMessagem.Columns.Add("Titulo", typeof(string));
            listMessagem.Columns.Add("Data", typeof(string));

            // Crie uma nova coluna de imagem para exibir as imagens
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.HeaderText = "Tipo";
            imageColumn.Name = "Tipo";
            imageColumn.DataPropertyName = "Tipo";
            imageColumn.ValueType = typeof(Image);
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Define o layout da imagem

            // Adicione a coluna de imagem ao DataGridView
            DgvCaixaDeMessagem.Columns.Add(imageColumn);

            // Defina um estilo padr�o com preenchimento para a coluna da imagem
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Padding = new Padding(5, 5, 5, 5); // Define o preenchimento (margem) desejado
            imageColumn.DefaultCellStyle = cellStyle;

            // Configurando Layout
            DgvCaixaDeMessagem.RowHeadersVisible = false;
            DgvCaixaDeMessagem.AllowUserToAddRows = false;
            DgvCaixaDeMessagem.AllowUserToDeleteRows = false;
            DgvCaixaDeMessagem.AllowUserToOrderColumns = false;
            DgvCaixaDeMessagem.AllowUserToResizeColumns = false;
            DgvCaixaDeMessagem.AllowUserToResizeColumns = false;
            DgvCaixaDeMessagem.AllowUserToResizeRows = false;
            DgvCaixaDeMessagem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DgvCaixaDeMessagem.ScrollBars = ScrollBars.Vertical;
            DgvCaixaDeMessagem.AllowUserToAddRows = false;
            DgvCaixaDeMessagem.AllowUserToAddRows = false;
            DgvCaixaDeMessagem.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            DgvCaixaDeMessagem.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 240, 240);
            DgvCaixaDeMessagem.DefaultCellStyle.SelectionForeColor = Color.Black;
            DgvCaixaDeMessagem.GridColor = Color.FromArgb(240, 240, 240);
            DgvCaixaDeMessagem.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Oculta o cabeçalho das colunas
            DgvCaixaDeMessagem.ColumnHeadersVisible = false;

            DgvCaixaDeMessagem.DataSource = listMessagem;

            // Altura das linhas
            DgvCaixaDeMessagem.RowTemplate.Height = 30;

            // Defina a ordem de exibicao das colunas com base nos indices
            DgvCaixaDeMessagem.Columns["New"].DisplayIndex = 0;
            DgvCaixaDeMessagem.Columns["Tipo"].DisplayIndex = 1;
            DgvCaixaDeMessagem.Columns["Titulo"].DisplayIndex = 2;
            DgvCaixaDeMessagem.Columns["Data"].DisplayIndex = 3;

            DgvCaixaDeMessagem.Columns["Titulo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            DgvCaixaDeMessagem.Columns[0].Width = 5;
            DgvCaixaDeMessagem.Columns[1].Width = 55;
            DgvCaixaDeMessagem.Columns[2].Width = 300;
            DgvCaixaDeMessagem.Columns[3].Width = 100;
        }
        public void PreencherDataGridViewCaixaDeEmail()
        {
            // Supondo que listMessagem seja o DataTable criado anteriormente
            DataTable listMessagem = (DataTable)DgvCaixaDeMessagem.DataSource;

            // Limpe o DataTable antes de preencher para evitar duplicatas
            listMessagem.Rows.Clear();
            int i = 0;
            // Adiciona os objetos MessagemEmail da listaEmails ao DataTable
            foreach (var email in principal.ObterListaEmails())
            {
                // Defina a imagem correspondente ao tipo de email
                Image tipoImagem = GetTipoImagem(email.TipoDaMessagem);

                // Adicione uma nova linha ao DataTable
                listMessagem.Rows.Add("", tipoImagem, email.TituloDaMessagem, email.DataDaMessagem);

                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = piloto[principal.IndexDoJogador].Cor1 ?? "ffffff";

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                DgvCaixaDeMessagem.Rows[i].Cells["New"].Style.BackColor = cor1;

                i++;
            }

            // Redefina a fonte de dados do DataGridView
            DgvCaixaDeMessagem.DataSource = listMessagem;

            Image GetTipoImagem(string tipo)
            {
                switch (tipo)
                {
                    case "Patrocinador":
                        return Properties.Resources.menu_patrocinio_b;
                    case "Contrato":
                        return Properties.Resources.menu_contratos_b;
                    case "FinalDeTemporada":
                        return Properties.Resources.calendario;
                    default:
                        return Properties.Resources.dinheiro;
                }
            }

            // Limpe a selecao inicial
            DgvCaixaDeMessagem.ClearSelection();
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
                string cor1Texto = dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoEquipes.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < dgvClassificacaoPilotos.Rows.Count; i++)
            {
                // Obter os valores das células C1 e C2 como representações de texto das cores
                string cor1Texto = dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty;

                // Converter as representações de texto das cores em cores reais
                Color cor1 = ColorTranslator.FromHtml(cor1Texto);

                // Definir as cores de fundo das células C1 e C2
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.BackColor = cor1;
                dgvClassificacaoPilotos.Rows[i].Cells["C1"].Style.ForeColor = cor1;
            }
            for (int i = 0; i < 1; i++)
            {
                principal.AdicionarPilotoCampeao(cata, principal.ContadorDeAno, dgvClassificacaoPilotos.Rows[i].Cells["Nacionalidade"].Value?.ToString() ?? string.Empty, dgvClassificacaoPilotos.Rows[i].Cells["Nome"].Value?.ToString() ?? string.Empty, Convert.ToInt32(dgvClassificacaoPilotos.Rows[i].Cells["P"].Value.ToString()), dgvClassificacaoPilotos.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty, dgvClassificacaoPilotos.Rows[i].Cells["Equipe"].Value?.ToString() ?? string.Empty);
                principal.AdicionarEquipeCampeao(cata, principal.ContadorDeAno, dgvClassificacaoEquipes.Rows[i].Cells["Nacionalidade"].Value?.ToString() ?? string.Empty, dgvClassificacaoEquipes.Rows[i].Cells["C1"].Value?.ToString() ?? string.Empty, dgvClassificacaoEquipes.Rows[i].Cells["Nome"].Value?.ToString() ?? string.Empty, Convert.ToInt32(dgvClassificacaoEquipes.Rows[i].Cells["P"].Value.ToString()));
            }
            dgvClassificacaoEquipes.ClearSelection();
            dgvClassificacaoPilotos.ClearSelection();
        }
        public void FinalDeTemporadaRankEquipe()
        {
            for (int i = 0; i < equipe.Length; i++)
            {
                int posicaoFinal = equipe[i].PosicaoAtualCampeonato;

                // Verifica e atualiza a pontuação para a categoria F1
                if (equipe[i].Categoria == "F1")
                {
                    switch (posicaoFinal)
                    {
                        case 1:
                            equipe[i].TitulosF1++;
                            equipe[i].PontuacaoRank += 30;
                            break;
                        case 2:
                            equipe[i].PontuacaoRank += 29;
                            break;
                        case 3:
                            equipe[i].PontuacaoRank += 28;
                            break;
                        case 4:
                            equipe[i].PontuacaoRank += 27;
                            break;
                        case 5:
                            equipe[i].PontuacaoRank += 26;
                            break;
                        case 6:
                            equipe[i].PontuacaoRank += 25;
                            break;
                        case 7:
                            equipe[i].PontuacaoRank += 24;
                            break;
                        case 8:
                            equipe[i].PontuacaoRank += 23;
                            break;
                        case 9:
                            equipe[i].PontuacaoRank += 22;
                            break;
                        case 10:
                            equipe[i].PontuacaoRank += 21;
                            break;
                        default:
                            break;
                    }
                }

                // Verifica e atualiza a pontuação para a categoria F2
                if (equipe[i].Categoria == "F2")
                {
                    switch (posicaoFinal)
                    {
                        case 1:
                            equipe[i].TitulosF2++;
                            equipe[i].PontuacaoRank += 20;
                            break;
                        case 2:
                            equipe[i].PontuacaoRank += 19;
                            break;
                        case 3:
                            equipe[i].PontuacaoRank += 18;
                            break;
                        case 4:
                            equipe[i].PontuacaoRank += 17;
                            break;
                        case 5:
                            equipe[i].PontuacaoRank += 16;
                            break;
                        case 6:
                            equipe[i].PontuacaoRank += 15;
                            break;
                        case 7:
                            equipe[i].PontuacaoRank += 14;
                            break;
                        case 8:
                            equipe[i].PontuacaoRank += 13;
                            break;
                        case 9:
                            equipe[i].PontuacaoRank += 12;
                            break;
                        case 10:
                            equipe[i].PontuacaoRank += 11;
                            break;
                        default:
                            break;
                    }
                }

                // Verifica e atualiza a pontuação para a categoria F3
                if (equipe[i].Categoria == "F3")
                {
                    switch (posicaoFinal)
                    {
                        case 1:
                            equipe[i].TitulosF3++;
                            equipe[i].PontuacaoRank += 10;
                            break;
                        case 2:
                            equipe[i].PontuacaoRank += 9;
                            break;
                        case 3:
                            equipe[i].PontuacaoRank += 8;
                            break;
                        case 4:
                            equipe[i].PontuacaoRank += 7;
                            break;
                        case 5:
                            equipe[i].PontuacaoRank += 6;
                            break;
                        case 6:
                            equipe[i].PontuacaoRank += 5;
                            break;
                        case 7:
                            equipe[i].PontuacaoRank += 4;
                            break;
                        case 8:
                            equipe[i].PontuacaoRank += 3;
                            break;
                        case 9:
                            equipe[i].PontuacaoRank += 2;
                            break;
                        case 10:
                            equipe[i].PontuacaoRank += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
            for (int i = 0; i < equipe.Length; i++)
            {
                equipe[i].PosicaoDoRank = 1;
                for (int j = 0; j < equipe.Length; j++)
                {
                    if (equipe[i].PontuacaoRank < equipe[j].PontuacaoRank)
                    {
                        equipe[i].PosicaoDoRank++;
                    }
                }
            }
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
                if (piloto[i].Categoria == "F1" && piloto[i].PosicaoAtualCampeonato == 1) piloto[i].TituloF1++;
                if (piloto[i].Categoria == "F2" && piloto[i].PosicaoAtualCampeonato == 1) piloto[i].TituloF2++;
                if (piloto[i].Categoria == "F3" && piloto[i].PosicaoAtualCampeonato == 1) piloto[i].TituloF3++;
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
        public void FinalDeTemporadaLimpaPropostasDeContrato()
        {
            for (int i = 0; i < 2; i++)
            {
                piloto[principal.IndexDoJogador].LimparPropostaDeContrato(piloto[principal.IndexDoJogador].PropostaDeContrato[i]);
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
                    int opcaoDeOferta = random.Next(1, 8);  //20% de chance de fazer uma oferta na semana. (1 a 5, sendo 3 oferta concedida.) -> alterado para 10% (1 a 11, sendo 4 oferta concedida.) 
                    if (opcaoDeOferta == 4)
                    {
                        Shuffle(indicesAleatorios);
                        int decicaoDeRenovação = random.Next(1, 3); // Vai decidir se a oferta vai ser de renovação ou de um novo piloto.
                        int mediaMax = 0;
                        int mediaMin = 0;
                        switch (equipe.Categoria)
                        {
                            case "F1":
                                mediaMax = 100;
                                mediaMin = 70;
                                break;
                            case "F2":
                                mediaMax = 80;
                                mediaMin = 40;
                                break;
                            default:
                                mediaMax = 50;
                                mediaMin = 10;
                                break;
                        }
                        if (decicaoDeRenovação == 1)
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                                    if (piloto[indice].SalarioPiloto < ofertaDeSalario && indice != principal.IndexDoJogador)
                                    {
                                        piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                        piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                        piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;
                                        piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";

                                        equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                        equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                        equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    }
                                    if (piloto[indice] == piloto[principal.IndexDoJogador])
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                            {
                                                equipe.ProximoAnoPrimeiroPiloto = "Negociando";
                                                piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, ofertaDeSalario, (random.Next(1, 4) + principal.ContadorDeAno), "1º Piloto");
                                                principal.NovaMessagemEmail("Contrato", "Renovação de Contrato.");
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                    piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";
                                    piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                    piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                                    equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                    equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                    equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;

                                    if (piloto[indice] == piloto[principal.IndexDoJogador])
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                            {
                                                equipe.ProximoAnoSegundoPiloto = "Negociando";
                                                piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria), (random.Next(1, 4) + principal.ContadorDeAno), "1º Piloto");
                                                principal.NovaMessagemEmail("Contrato", "Oferta de Contrato.");
                                                break;
                                            }
                                        }
                                    }
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
                        int mediaMax = 0;
                        int mediaMin = 0;
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
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                                    if (piloto[indice].SalarioPiloto < ofertaDeSalario && indice != principal.IndexDoJogador)
                                    {
                                        piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                        piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                        piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;
                                        piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";

                                        equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                        equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                        equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    }
                                    if (piloto[indice] == piloto[principal.IndexDoJogador])
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                            {
                                                equipe.ProximoAnoSegundoPiloto = "Negociando";
                                                piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, ofertaDeSalario, (random.Next(1, 4) + principal.ContadorDeAno), "2º Piloto");
                                                principal.NovaMessagemEmail("Contrato", "Renovação de Contrato.");
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            foreach (int indice in indicesAleatorios)
                            {
                                if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                                {
                                    piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                    piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";
                                    piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                    piloto[indice].ProximoAnoSalarioPiloto = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);

                                    equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                    equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                    equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                                    if (piloto[indice] == piloto[principal.IndexDoJogador])
                                    {
                                        for (int i = 0; i < 2; i++)
                                        {
                                            if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                            {
                                                equipe.ProximoAnoSegundoPiloto = "Negociando";
                                                piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria), (random.Next(1, 4) + principal.ContadorDeAno), "2º Piloto");
                                                principal.NovaMessagemEmail("Contrato", "Oferta de Contrato.");
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void OfertaDeContratoFimDeAno() // CORRIGIR BUG, QUE FAZ O JOGADOR ACEITAR O CONTRATO AUTOMATICO.
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
                    int mediaMax = 0;
                    int mediaMin = 0;
                    switch (equipe.Categoria)
                    {
                        case "F1":
                            mediaMax = 100;
                            mediaMin = 70;
                            break;
                        case "F2":
                            mediaMax = 80;
                            mediaMin = 40;
                            break;
                        default:
                            mediaMax = 50;
                            mediaMin = 10;
                            break;
                    }
                    foreach (int indice in indicesAleatorios)
                    {
                        if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                        {
                            double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                            if (piloto[indice].SalarioPiloto < ofertaDeSalario && indice != principal.IndexDoJogador)
                            {
                                piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                piloto[indice].ProximoAnoStatusPiloto = "1º Piloto";
                                piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;

                                equipe.ProximoAnoPrimeiroPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                equipe.ProximoAnoPrimeiroPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                equipe.ProximoAnoPrimeiroPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                            }
                            if (piloto[indice] == piloto[principal.IndexDoJogador])
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                    {
                                        equipe.ProximoAnoPrimeiroPiloto = "Negociando";
                                        piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, ofertaDeSalario, (random.Next(1, 4) + principal.ContadorDeAno), "1º Piloto");
                                        principal.NovaMessagemEmail("Contrato", "Oferta de Contrato.");
                                    }
                                }     
                            } 
                            break;
                        }
                    }
                }
                if (equipe.ProximoAnoSegundoPiloto == "")
                {
                    foreach (int indice in indicesAleatorios)
                    {
                        int mediaMax = 0;
                        int mediaMin = 0;
                        switch (equipe.Categoria)
                        {
                            case "F1":
                                mediaMax = 100;
                                mediaMin = 70;
                                break;
                            case "F2":
                                mediaMax = 80;
                                mediaMin = 40;
                                break;
                            default:
                                mediaMax = 50;
                                mediaMin = 10;
                                break;
                        }
                        if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto)
                        {
                            double ofertaDeSalario = DefinirSalario(piloto[indice].MediaPiloto, equipe.Categoria);
                            if (piloto[indice].SalarioPiloto < ofertaDeSalario && indice != principal.IndexDoJogador)
                            {
                                piloto[indice].ProximoAnoEquipePiloto = equipe.NomeEquipe;
                                piloto[indice].ProximoAnoStatusPiloto = "2º Piloto";
                                piloto[indice].ProximoAnoContratoPiloto = (random.Next(1, 4) + principal.ContadorDeAno);
                                piloto[indice].ProximoAnoSalarioPiloto = ofertaDeSalario;

                                equipe.ProximoAnoSegundoPiloto = $"{piloto[indice].NomePiloto} {piloto[indice].SobrenomePiloto}";
                                equipe.ProximoAnoSegundoPilotoContrato = piloto[indice].ProximoAnoContratoPiloto;
                                equipe.ProximoAnoSegundoPilotoSalario = piloto[indice].ProximoAnoSalarioPiloto;
                            }
                            if (piloto[indice] == piloto[principal.IndexDoJogador])
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (piloto[indice].PropostaDeContrato[i].PropostaAceita == false && piloto[indice].PropostaDeContrato[i].TempoPropostaContrato == 0)
                                    {
                                        equipe.ProximoAnoSegundoPiloto = "Negociando";
                                        piloto[indice].PropostaDeContrato[i] = piloto[indice].NovaPropostaDeContrato(equipe.NomeEquipe, equipe.Sede, ofertaDeSalario, (random.Next(1, 4) + principal.ContadorDeAno), "2º Piloto");
                                        principal.NovaMessagemEmail("Contrato", "Oferta de Contrato.");
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        public void OfertaDeContratoFimDeAnoIaOrdemCrescente()
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
                    int mediaMax = 0;
                    int mediaMin = 0;
                    switch (equipe.Categoria)
                    {
                        case "F1":
                            mediaMax = 100;
                            mediaMin = 70;
                            break;
                        case "F2":
                            mediaMax = 80;
                            mediaMin = 40;
                            break;
                        default:
                            mediaMax = 50;
                            mediaMin = 10;
                            break;
                    }
                    foreach (int indice in indicesAleatorios)
                    {
                        if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto && piloto[indice] != piloto[principal.IndexDoJogador])
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
                    int mediaMax = 0;
                    int mediaMin = 0;
                    switch (equipe.Categoria)
                    {
                        case "F1":
                            mediaMax = 100;
                            mediaMin = 70;
                            break;
                        case "F2":
                            mediaMax = 80;
                            mediaMin = 40;
                            break;
                        default:
                            mediaMax = 50;
                            mediaMin = 10;
                            break;
                    }
                    foreach (int indice in indicesAleatorios)
                    {
                        if (piloto[indice].MediaPiloto >= mediaMin && piloto[indice].MediaPiloto <= mediaMax && piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto && piloto[indice] != piloto[principal.IndexDoJogador])
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
        public void OfertaDeContratoFimDeAnoIA()
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
                        if (piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto && piloto[indice] != piloto[principal.IndexDoJogador])
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
                        if (piloto[indice].ProximoAnoEquipePiloto == "" && piloto[indice].IdadePiloto < piloto[indice].AposentadoriaPiloto && piloto[indice] != piloto[principal.IndexDoJogador])
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
        public void OfertaDeContratoPatrocinadores()
        {
            if (financia.EspacoContratoDisponivel != 0)
            {
                Random r = new Random();
                int opcaoDeContrato = r.Next(1, 11);//11
                if (opcaoDeContrato == 1)
                {
                    for (int i = 0; i < financia.Patrocinadores.Length; i++)
                    {
                        if (financia.Patrocinadores[i].ContratoValido == false && financia.Patrocinadores[i].TempoPropostaContrato == 0)
                        {
                            financia.Patrocinadores[i] = financia.AdicionarNovoContrato(piloto[principal.IndexDoJogador]);
                            principal.NovaMessagemEmail("Patrocinador", "Oferta de patrocínio.");
                            break;
                        }
                    }
                }
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
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true, // Formata JSON com indentação
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                string json = JsonSerializer.Serialize(dadosCompletos, options);
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
                    DadosCompletos? dadosCompletos = JsonSerializer.Deserialize<DadosCompletos>(json);

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

                if (piloto[principal.IndexDoJogador].Categoria == "F1")
                {
                    PreencherDataGridViewClassEquipes(0, 10);
                    PreencherDataGridViewClassPilotos(0, 10);
                }
                else if (piloto[principal.IndexDoJogador].Categoria == "F2")
                {
                    PreencherDataGridViewClassEquipes(10, 20);
                    PreencherDataGridViewClassPilotos(10, 20);
                }
                else if (piloto[principal.IndexDoJogador].Categoria == "F3")
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
                OfertaDeContratoPatrocinadores();
                principal.PotenciaMotoresEquipe(motor, equipe);
                principal.XpTurnoSemanal(piloto);
                // principal.XpTurnoSemanalJogador(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarEscritorioSemanal();
                AtualizarFinanciasSemanal();
                AtualizarNomesNaTelaInicial();
                PreencherDataGridViewCaixaDeEmail();

            }
            else if (principal.ContadorDeSemana == principal.TotalSemanas - 1)
            {
                OfertaDeContratoFimDeAno();
                principal.ContinuarTurno();
                principal.XpTurnoSemanal(piloto);
                // principal.XpTurnoSemanalJogador(piloto);
                principal.XpEquipeSemanal(equipe);
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarEscritorioSemanal();
                AtualizarFinanciasSemanal();
                AtualizarNomesNaTelaInicial();
                PreencherDataGridViewCaixaDeEmail();
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
                principal.NovaMessagemEmail("FinalDeTemporada", "Temporada chegou ao fim.");
                OfertaDeContrato();
                OfertaDeContratoPatrocinadores();
                // PREENCHER TODOS OS CONTRATOS
                OfertaDeContratoFimDeAnoIaOrdemCrescente();
                OfertaDeContratoFimDeAnoIA();
                principal.XpTurnoSemanal(piloto);
                // principal.XpTurnoSemanalJogador(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarEscritorioSemanal();
                AtualizarFinanciasSemanal();
                FinalDeTemporadaAtualizarDB();
                FinalDeTemporadaLimpaPropostasDeContrato();
                InicioDeTemporadaAtualizarContratos();
                ContratoDeMotores();
                AposentarPilot();
                FinalDeTemporadaRankEquipe();

                FinalDeTemporadaLimpaTable();

                principal.CorPrincipal = piloto[principal.IndexDoJogador].Cor1;
                principal.CorSecundaria = piloto[principal.IndexDoJogador].Cor2;
                corPrincipal = ColorTranslator.FromHtml(principal.CorPrincipal);
                corSecundaria = ColorTranslator.FromHtml(principal.CorSecundaria);
                principal.IdadeJogador = piloto[principal.IndexDoJogador].IdadePiloto;
                EmbaralharPistas();
                DefinirDataDaSemanaDeProvaDaPista();
                AtualizarTabelaInicioDeTemporada();
                AtualizarCores();
                AtualizarNomesNaTelaInicial();
                PreencherDataGridViewCaixaDeEmail();
            }
            else
            {
                OfertaDeContrato();
                OfertaDeContratoPatrocinadores();
                principal.XpTurnoSemanal(piloto);
                // principal.XpTurnoSemanalJogador(piloto);
                principal.XpEquipeSemanal(equipe);
                principal.ContinuarTurno();
                AtualizaStatusProxCorrida(principal.ContadorDeSemana);
                AtualizarEscritorioSemanal();
                AtualizarFinanciasSemanal();
                AtualizarNomesNaTelaInicial();
                PreencherDataGridViewCaixaDeEmail();
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
        private void pictureBoxEscritorio_Click(object sender, EventArgs e)
        {
            TelaEscritorio telaEscritorio = new TelaEscritorio(principal, equipe, piloto, financia);
            telaEscritorio.ShowDialog();
        }
        private void pictureBoxMensagem_Click(object sender, EventArgs e)
        {
            principal.ApagarTodosEmails();
            PreencherDataGridViewCaixaDeEmail();
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
