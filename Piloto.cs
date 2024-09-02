using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DriverChallenge.Equipe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DriverChallenge
{
    public class Piloto
    {
        // Atributos do piloto.
        private Random random = new Random();
        // Atributos pessoais.
        public string NacionalidadePiloto { get; set; } = "";
        public string NomePiloto { get; set; } = "";
        public string SobrenomePiloto { get; set; } = "";
        public int IdadePiloto { get; set; } = 0;
        public int AugePiloto { get; set; } = 0;
        public int VisibilidadePiloto { get; set; } = 0;
        public int AposentadoriaPiloto { get; set; } = 0;
        public double XpPiloto { get; set; } = 0;
        public double PotencialPiloto { get; set; } = 0;
        // Atributos de contrato.
        public string Categoria { get; set; } = "";
        public string StatusPiloto { get; set; } = "Disponivel";
        public double SalarioPiloto { get; set; } = 0;
        public int ContratoPiloto { get; set; } = 0;
        public string EquipePiloto { get; set; } = "";
        public string ProximoAnoStatusPiloto { get; set; } = "Disponivel";
        public double ProximoAnoSalarioPiloto { get; set; } = 0;
        public int ProximoAnoContratoPiloto { get; set; } = 0;
        public string ProximoAnoEquipePiloto { get; set; } = "";
        // Atributos de classificação.
        public int PosicaoAtualCampeonato { get; set; } = 0;
        public int PontosCampeonato { get; set; } = 0;
        public int PrimeiroColocado { get; set; } = 0;
        public int SegundoColocado { get; set; } = 0;
        public int TerceiroColocado { get; set; } = 0;
        // Atributos de histórico.
        public int VitoriaCorrida { get; set; } = 0;
        public int PolePosition { get; set; } = 0;
        public int TituloF1 { get; set; } = 0;
        public int TituloF2 { get; set; } = 0;
        public int TituloF3 { get; set; } = 0;
        // Atributos de capacidade do piloto.
        public int Largada { get; set; } = 0;
        public int Concentracao { get; set; } = 0;
        public int Ultrapassagem { get; set; } = 0;
        public int Experiencia { get; set; } = 0;
        public int Rapidez { get; set; } = 0;
        public int Chuva { get; set; } = 0;
        public int AcertoDoCarro { get; set; } = 0;
        public int Fisico { get; set; } = 0;
        public int MediaPiloto { get; set; } = 0;
        // Dados para treino e corrida.
        public int TempoDeVoltaQualificacao { get; set; } = 0;
        public int TempoDeVoltaCorrida { get; set; } = 0;
        public int TempoDeVoltaMaisRapidaCorrida { get; set; } = 0;
        public int PosicaoNaVoltaMaisRapida { get; set; } = 0;
        public int QualificacaoParaCorrida { get; set; } = 0;
        public int PosicaoNaCorrida { get; set; } = 0;
        public int DiferancaAnt { get; set; } = 0;
        public int TempoCorrida { get; set; } = 0;
        public int ResultadoCorrida { get; set; } = 0;
        public int DiferancaPri { get; set; } = 0;
        public int BonusRandom { get; set; } = 0;
        // Outros atributos.
        public string Cor1 { get; set; } = "";
        public string Cor2 { get; set; } = "";
        public double RandomNacionalidade { get; set; } = 0;
        public List<PilotoTemporada> PilotosTemporadas { get; set; } = [];
        public Piloto() {}

        public Piloto(string nome, string sobrenome, string nacionalidad, int idade, int auge, int aposentadoria, int largad, int concent, int ultrapassag, int experience, int rapid, int chuv, int acerto, int fisic)
        {
            PaisPiloto paisPilotos = new PaisPiloto();
            string nacionalidade = nacionalidad;

            NacionalidadePiloto = nacionalidad;
            NomePiloto = nome;
            SobrenomePiloto = sobrenome;
            XpPiloto = 0;
            PotencialPiloto = random.Next(60, 81);
            PotencialPiloto = (PotencialPiloto / 100);

            // Definir de forma aleatória a idade do piloto (18 até 21)
            IdadePiloto = idade;

            // Definir de forma aleatória o auge do piloto (30 até 36)
            AugePiloto = auge;

            // Definir de forma aleatória a aposentadoria do piloto (36 até 41)
            AposentadoriaPiloto = aposentadoria;

            // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            VisibilidadePiloto = random.Next(0, 51);

            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 30)
            Largada = largad;
            Concentracao = concent;
            Ultrapassagem = ultrapassag;
            Experiencia = experience;
            Rapidez = rapid;
            Chuva = chuv;
            AcertoDoCarro = acerto;
            Fisico = fisic;

            MediaPiloto = ((Largada + Concentracao + Ultrapassagem + Experiencia + Rapidez + Chuva + AcertoDoCarro + Fisico) / 8);
        }

        public void GeraPiloto()
        {
            PaisPiloto paisPilotos = new PaisPiloto();
            string nacionalidade = "";
            RandomNacionalidade = 7; // Após finalizar os arquivos com todos os nomes das nacionalidade altera a linha para esse -> RandomNacionalidade = random.Next(0,19);
            // Seleciona uma nacionalidade aleatória
            if (RandomNacionalidade <= 9)
            {
                nacionalidade = paisPilotos.NacionalidadesTop1[random.Next(paisPilotos.NacionalidadesTop1.Count)];
                NacionalidadePiloto = nacionalidade;
            }
            else if (RandomNacionalidade <= 14)
            {
                nacionalidade = paisPilotos.NacionalidadesTop2[random.Next(paisPilotos.NacionalidadesTop2.Count)];
                NacionalidadePiloto = nacionalidade;
            }
            else if (RandomNacionalidade <= 17)
            {
                nacionalidade = paisPilotos.NacionalidadesTop2[random.Next(paisPilotos.NacionalidadesTop2.Count)];
                NacionalidadePiloto = nacionalidade;
            }
            else
            {
                nacionalidade = paisPilotos.NacionalidadesTop3[random.Next(paisPilotos.NacionalidadesTop3.Count)];
                NacionalidadePiloto = nacionalidade;
            }
            // Construa o caminho completo para o arquivo de nomes do piloto
            string nomeArquivo = Path.Combine("NomesPilotos", "Piloto_" + nacionalidade + ".txt");
            // Construa o caminho completo para o arquivo do sobrenome do piloto
            string sobrenomeArquivo = Path.Combine("SobrenomesPilotos", "Piloto_" + nacionalidade + ".txt");
            // Verifique se o arquivo existe antes de lê-lo
            if (File.Exists(nomeArquivo))
            {
                string[] nomes = File.ReadAllLines(nomeArquivo);
                string nomeAleatorio = nomes[random.Next(nomes.Length)]; // Seleciona um nome aleatório a partir dos nomes lidos
                NomePiloto = nomeAleatorio; // Configure os campos da classe com os valores selecionados
            }
            else
            {
                Console.WriteLine("Arquivo de nomes não encontrado para a nacionalidade: " + nacionalidade); // Lida com o caso em que o arquivo não foi encontrado
            }
            if (File.Exists(sobrenomeArquivo)) // Verifique se o arquivo existe antes de lê-lo
            {
                string[] ssnomes = File.ReadAllLines(sobrenomeArquivo);
                string sobrenomeAleatorio = ssnomes[random.Next(ssnomes.Length)]; // Seleciona um nome aleatório a partir dos nomes lidos
                SobrenomePiloto = sobrenomeAleatorio;
            }
            else
            {
                Console.WriteLine("Arquivo de nomes não encontrado para a nacionalidade: " + nacionalidade);  // Lida com o caso em que o arquivo não foi encontrado
            }

            XpPiloto = 0;
            PotencialPiloto = random.Next(60, 81);
            PotencialPiloto = (PotencialPiloto / 100);
            IdadePiloto = random.Next(18, 22);  // Definir de forma aleatória a idade do piloto (18 até 21)
            AugePiloto = AugePiloto = random.Next(30, 37); // Definir de forma aleatória o auge do piloto (30 até 36)
            AposentadoriaPiloto = AposentadoriaPiloto = random.Next(36, 42); // Definir de forma aleatória a aposentadoria do piloto (36 até 41)
            VisibilidadePiloto = random.Next(0, 51);  // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 30)
            Largada = random.Next(10, 40);
            Concentracao = random.Next(10, 40);
            Ultrapassagem = random.Next(10, 40);
            Experiencia = random.Next(10, 40);
            Rapidez = random.Next(10, 40);
            Chuva = random.Next(10, 40);
            AcertoDoCarro = random.Next(10, 40);
            Fisico = random.Next(10, 40);
            MediaPiloto = ((Largada + Concentracao + Ultrapassagem + Experiencia + Rapidez + Chuva + AcertoDoCarro + Fisico) / 8);
        }
        // Criar o piloto do Jogador
        public void GeraPiloto(string nomePiloto, string sobrenomePiloto, string nacionalidadePiloto)
        {
            PaisPiloto paisPilotos = new PaisPiloto();
            NacionalidadePiloto = nacionalidadePiloto;
            NomePiloto = nomePiloto;
            SobrenomePiloto = sobrenomePiloto;
            XpPiloto = 0;
            PotencialPiloto = random.Next(60, 81);
            PotencialPiloto = (PotencialPiloto / 100);
            IdadePiloto = random.Next(18, 22);  // Definir de forma aleatória a idade do piloto (18 até 21)
            AugePiloto = AugePiloto = random.Next(30, 37); // Definir de forma aleatória o auge do piloto (30 até 36)
            AposentadoriaPiloto = AposentadoriaPiloto = random.Next(36, 42); // Definir de forma aleatória a aposentadoria do piloto (36 até 41)
            VisibilidadePiloto = random.Next(0, 51);  // Definir a visibilidade do piloto para patrocinador (entre 0 a 50)
            // Atribuindo de formas aleatória, a qualidade de cada atributos (10 a 30)
            Largada = random.Next(10, 40);
            Concentracao = random.Next(10, 40);
            Ultrapassagem = random.Next(10, 40);
            Experiencia = random.Next(10, 40);
            Rapidez = random.Next(10, 40);
            Chuva = random.Next(10, 40);
            AcertoDoCarro = random.Next(10, 40);
            Fisico = random.Next(10, 40);
            MediaPiloto = ((Largada + Concentracao + Ultrapassagem + Experiencia + Rapidez + Chuva + AcertoDoCarro + Fisico) / 8);
        }
        public void AtualizarMedia()
        {
            MediaPiloto = ((Largada + Concentracao + Ultrapassagem + Experiencia + Rapidez + Chuva + AcertoDoCarro + Fisico) / 8);
        }
        public void AdicionarHistoricosPiloto(int position, int ano, string cor1, string equipe, int pontos, string categoriaAtual)
        {
            PilotosTemporadas.Add(new PilotoTemporada { Position = position, Ano = ano, Cor1 = cor1, Equipe = equipe, Pontos = pontos, CategoriaAtual = categoriaAtual });
        }
        public class PilotoTemporada
        {
            public required int Position { get; set; }
            public required int Ano { get; set; }
            public required string Cor1 { get; set; }
            public required string Equipe { get; set; }
            public required int Pontos { get; set; }
            public required string CategoriaAtual { get; set; }
        }
    }
}
