using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Equipe
    {
        // Atributos dados da equipe.
        public string NomeEquipe { get; set; } = "";
        public string Cor1 { get; set; } = "";
        public string Cor2 { get; set; } = "";
        public string CorT { get; set; } = "";
        public string Sede { get; set; } = "";
        public string Categoria { get; set; } = "";
        // Atributos de classificação de campeonato.
        public int PosicaoAtualCampeonato { get; set; } = 1;
        public int PontosCampeonato { get; set; } = 0;
        public int PrimeiroColocado { get; set; } = 0;
        public int SegundoColocado { get; set; } = 0;
        public int TerceiroColocado { get; set; } = 0;
        public int ClassificacaoCampeonato { get; set; } = 0;
        // Atributos de capacidade da equipe.
        public int Aerodinamica { get; set; } = 0;
        public int Freio { get; set; } = 0;
        public int AsaDianteira { get; set; } = 0;
        public int AsaTraseira { get; set; } = 0;
        public int Cambio { get; set; } = 0;
        public int Eletrico { get; set; } = 0;
        public int Direcao { get; set; } = 0;
        public int Confiabilidade { get; set; } = 0;
        public int MediaEquipe { get; set; } = 0;
        public int ValorDoMotor { get; set; } = 0; // Propriedade para o valor do motor da equipe.
        public string NameMotor { get; set; } = "";
        // Atributos do primeiro piloto da equipe.
        public string PrimeiroPiloto { get; set; } = "";
        public int PrimeiroPilotoContrato { get; set; } = 0;
        public double PrimeiroPilotoSalario { get; set; } = 0;
        // Atributos do segundo piloto da equipe.
        public string SegundoPiloto { get; set; } = "";
        public int SegundoPilotoContrato { get; set; } = 0;
        public double SegundoPilotoSalario { get; set; } = 0;
        // Atributos do primeiro piloto da equipe para o próximo ano.
        public string ProximoAnoPrimeiroPiloto { get; set; } = "";
        public int ProximoAnoPrimeiroPilotoContrato { get; set; } = 0;
        public double ProximoAnoPrimeiroPilotoSalario { get; set; } = 0;
        // Atributos do segundo piloto da equipe para o próximo ano.
        public string ProximoAnoSegundoPiloto { get; set; } = "";
        public int ProximoAnoSegundoPilotoContrato { get; set; } = 0;
        public double ProximoAnoSegundoPilotoSalario { get; set; } = 0;
        // Atributos do primeiro piloto para pitStop.
        public int PneuAtualPrimeiroPiloto { get; set; } = 0;
        public int QuantidadeDeParadaPrimeiroPiloto { get; set; } = 0;
        public int VoltaParaPitStopPrimeiroPiloto { get; set; } = 0;
        public string TrocaDePneuParada01PrimeiroPiloto { get; set; } = "";
        public string TrocaDePneuParada02PrimeiroPiloto { get; set; } = "";
        public string TrocaDePneuParada03PrimeiroPiloto { get; set; } = "";
        // Atributos do segundo piloto para pitStop.
        public int PneuAtualSegundoPiloto { get; set; } = 0;
        public int QuantidadeDeParadaSegundoPiloto { get; set; } = 0;
        public int VoltaParaPitStopSegundoPiloto { get; set; } = 0;
        public string TrocaDePneuParada01SegundoPiloto { get; set; } = "";
        public string TrocaDePneuParada02SegundoPiloto { get; set; } = "";
        public string TrocaDePneuParada03SegundoPiloto { get; set; } = "";
        // Atributos do Rank.
        public int PosicaoDoRank { get; set; } = 0;
        public int PontuacaoRank { get; set; } = 0;
        public int TitulosF1 { get; set; } = 0;
        public int TitulosF2 { get; set; } = 0;
        public int TitulosF3 { get; set; } = 0;
        // Lista das temporadas.
        public List<EquipeTemporada> EquipeTemporadas { get; set; } = [];

        // Método para criar uma nova equiepe.
        public Equipe() {}
        public Equipe(string nomeEquipe, string cor1, string cor2, string corT, string sede, int aerodinamica, int freio, int asaDianteira, int asaTraseira, int cambio,
        int eletrico, int direcao, int confiabilidade, string nomeDoMotor, string categoria, Motor motores)
        {
            // Informações
            this.NomeEquipe = nomeEquipe;
            this.Cor1 = cor1;
            this.Cor2 = cor2;
            this.CorT = corT;
            this.Sede = sede;

            // Atributos do carro
            this.Aerodinamica = aerodinamica;
            this.Freio = freio;
            this.AsaDianteira = asaDianteira;
            this.AsaTraseira = asaTraseira;
            this.Cambio = cambio;
            this.Eletrico = eletrico;
            this.Direcao = direcao;
            this.Confiabilidade = confiabilidade;

            this.MediaEquipe = ((aerodinamica + freio + asaDianteira + asaTraseira + cambio + eletrico + direcao + confiabilidade) / 8);

            this.NameMotor = nomeDoMotor;
            this.ValorDoMotor = motores.ObterValorDoMotor(nomeDoMotor);
            this.Categoria = categoria;

        }
        public void AdicionarHistoricosEquipe(int position, int ano, string motor, string cor1, int pontos, int primeiro, int segundo, int terceiro, string categoriaAtual)
        {
            EquipeTemporadas.Add(new EquipeTemporada { Position = position, Ano = ano, Motor = motor, Cor1 = cor1, Pontos = pontos, Primeiro = primeiro, Segundo = segundo, Terceiro = terceiro, CategoriaAtual = categoriaAtual });
        }
        public class EquipeTemporada
        {
            public required int Position { get; set; }
            public required int Ano { get; set; }
            public required string Motor { get; set; }
            public required string Cor1 { get; set; }
            public required int Pontos { get; set; }
            public required int Primeiro { get; set; }
            public required int Segundo { get; set; }
            public required int Terceiro { get; set; }
            public required string CategoriaAtual { get; set; }
        }
        public void AtualizarMedia()
        {
            MediaEquipe = ((Aerodinamica + Freio + AsaDianteira + AsaTraseira + Cambio + Eletrico + Direcao + Confiabilidade) / 8);
        }
    }
}
