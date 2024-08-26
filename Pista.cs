using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Pista
    {
        private string nomeGP = "";
        private string nomeCircuito = "";
        private int numerosDeVoltas = 0;
        private int curvas = 0;
        private int retas = 0;
        private int tempoBase = 0;
        private int semanaDaProva = 0;

        public Pista(string nomeGp, string nomeCircuito, int numerosDeVoltas, int curvas, int retas, int tempoBase)
        {
            // Atribuindo do atributos da Pista.
            this.NomeGp = nomeGp;
            this.NomeCircuito = nomeCircuito;
            this.NumerosDeVoltas = numerosDeVoltas;
            this.Curvas = curvas;
            this.Retas = retas;
            this.TempoBase = tempoBase;

        }
        public void exibirInformacao()
        {
            Console.WriteLine($"NomeGP: {nomeGP}");
        }
        public string NomeGp { get; set; }
        public string NomeCircuito { get; set; }
        public int NumerosDeVoltas { get; set; }
        public int Curvas { get; set; }
        public int Retas { get; set; }
        public int TempoBase { get; set; }
        public int SemanaDaProva { get; set; }
        /*
        public class FormatacaoTempo
        {
            // Formatação de saída do tempo de pista.
            double milissegundos = 67200;
            int segundos = (int)(milissegundos / 1000);
            int minutos = segundos / 60;
            int milissegundosRestantes = (int)(milissegundos % 1000);
            string formatoTempo = string.Format("{0}:{1:D2}.{2:D3}", minutos, segundos % 60, milissegundosRestantes);
            Console.WriteLine(formatoTempo);
        }
        */
    }
}
