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
        private Dictionary<int, (int, string, string, string)> combinacaoPitStop;



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
            var todasCombinacoes = new Dictionary<int, (int, string, string, string)>
            {
                // Ordenado por quantidade de voltas (e IDs ajustados)
                { 1, (2, "Macio", "Duro", "") }, // 49 voltas
                { 2, (2, "Duro", "Macio", "") }, // 49 voltas
                { 3, (2, "Médio", "Médio", "") }, // 52 voltas
                { 4, (3, "Macio", "Macio", "Médio") }, // 56 voltas
                { 5, (3, "Macio", "Macio", "Duro") }, // 64 voltas
                { 6, (2, "Médio", "Duro", "") }, // 60 voltas
                { 7, (2, "Duro", "Médio", "") }, // 60 voltas
                { 8, (3, "Macio", "Médio", "Médio") }, // 67 voltas
                { 9, (2, "Duro", "Duro", "") }, // 68 voltas
                { 10, (3, "Macio", "Médio", "Duro") }, // 75 voltas
                { 11, (3, "Médio", "Duro", "Macio") }, // 75 voltas
                { 12, (3, "Duro", "Macio", "Médio") }, // 75 voltas
                { 13, (3, "Duro", "Médio", "Macio") }, // 75 voltas
                { 14, (3, "Macio", "Duro", "Duro") }, // 83 voltas
                { 15, (3, "Médio", "Médio", "Duro") }, // 86 voltas
                { 16, (3, "Médio", "Duro", "Duro") }, // 94 voltas
                { 17, (3, "Duro", "Duro", "Duro") } // 102 voltas
            };

            // Inicializa o dicionário com as combinações desejadas
            combinacaoPitStop = new Dictionary<int, (int, string, string, string)>();

            foreach (var indice in indices)
            {
                if (todasCombinacoes.ContainsKey(indice))
                {
                    var combinacao = todasCombinacoes[indice];
                    // Adiciona a combinação ao dicionário com o novo formato
                    combinacaoPitStop[indice] = (combinacao.Item1, combinacao.Item2, combinacao.Item3, combinacao.Item4);
                }
            }

            MessageBox.Show(ExibirCombinacoes());
        }

        public void exibirInformacao()
        {
            Console.WriteLine($"NomeGP: {NomeGp}");
        }

        // Método para exibir as combinações
        public string ExibirCombinacoes()
        {
            var sb = new StringBuilder();
            foreach (var combinacao in combinacaoPitStop)
            {
                sb.AppendLine($"Combinação: {combinacao.Key}, Quantidade de Paradas: {combinacao.Value}");
            }
            return sb.ToString();
        }
    }
}
