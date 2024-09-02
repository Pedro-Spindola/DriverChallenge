using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Pista
    {
        public string NomeGp { get; set; } = "";
        public string NomeCircuito { get; set; } = "";
        public int NumerosDeVoltas { get; set; } = 0;
        public int Curvas { get; set; } = 0;
        public int Retas { get; set; } = 0;
        public int TempoBase { get; set; } = 0;
        public int SemanaDaProva { get; set; } = 0;
        public Dictionary<int, (string, string, string)> CombinacaoPitStop { get; set; } = new Dictionary<int, (string, string, string)>();

        public Pista() { }

        public Pista(string nomeGp, string nomeCircuito, int numerosDeVoltas, int curvas, int retas, int tempoBase, params int[] indices)
        {
            // Atribuindo do atributos da Pista.
            this.NomeGp = nomeGp;
            this.NomeCircuito = nomeCircuito;
            this.NumerosDeVoltas = numerosDeVoltas;
            this.Curvas = curvas;
            this.Retas = retas;
            this.TempoBase = tempoBase;

            // Inicializa o dicionário com todas as combinações possíveis
            var todasCombinacoes = new Dictionary<int, (string, string, string)>
            {
                // Ordenado por quantidade de voltas (e IDs ajustados)
                { 1, ("Macio", "Duro", "") }, // 49 voltas
                { 2, ("Duro", "Macio", "") }, // 49 voltas
                { 3, ("Médio", "Médio", "") }, // 52 voltas
                { 4, ("Macio", "Macio", "Médio") }, // 56 voltas
                { 5, ("Macio", "Médio", "Macio") }, // 56 voltas
                { 6, ("Médio", "Macio", "Macio") }, // 56 voltas
                { 7, ("Macio", "Macio", "Duro") }, // 64 voltas
                { 8, ("Macio", "Duro", "Macio") }, // 64 voltas
                { 9, ("Duro", "Macio", "Macio") }, // 64 voltas
                { 10, ("Médio", "Duro", "") }, // 60 voltas
                { 11, ("Duro", "Médio", "") }, // 60 voltas
                { 12, ("Macio", "Médio", "Médio") }, // 67 voltas
                { 13, ("Médio", "Macio", "Médio") }, // 67 voltas
                { 14, ("Médio", "Médio", "Macio") }, // 67 voltas
                { 15, ("Duro", "Duro", "") }, // 68 voltas
                { 16, ("Macio", "Médio", "Duro") }, // 75 voltas
                { 17, ("Macio", "Duro", "Médio") }, // 75 voltas
                { 18, ("Médio", "Duro", "Macio") }, // 75 voltas
                { 19, ("Médio", "Macio", "Duro") }, // 75 voltas
                { 20, ("Duro", "Macio", "Médio") }, // 75 voltas
                { 21, ("Duro", "Médio", "Macio") }, // 75 voltas
                { 22, ("Macio", "Duro", "Duro") }, // 83 voltas
                { 23, ("Duro", "Macio", "Duro") }, // 83 voltas
                { 24, ("Duro", "Duro", "Macio") }, // 83 voltas
                { 25, ("Médio", "Médio", "Duro") }, // 86 voltas
                { 26, ("Médio", "Duro", "Médio") }, // 86 voltas
                { 27, ("Duro", "Médio", "Médio") }, // 86 voltas
            };

            foreach (var indice in indices)
            {
                if (todasCombinacoes.ContainsKey(indice))
                {
                    var combinacao = todasCombinacoes[indice];
                    // Adiciona a combinação ao dicionário com o novo formato
                    CombinacaoPitStop[indice] = (combinacao.Item1, combinacao.Item2, combinacao.Item3);
                }
            }
        }
    }
}
