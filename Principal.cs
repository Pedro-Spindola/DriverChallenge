using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DriverChallenge.Equipe;

namespace DriverChallenge
{
    public class Principal
    {
        // Atributos fundamentais para o game.
        public Random random = new Random();
        public Historico historico = new Historico();
        // Atributos para definir as cores da tela.
        public string CorPrincipal { get; set; } = "";
        public string CorSecundaria { get; set; } = "";
        public string CorTexto { get; set; } = "Automatico";
        // Atributos relacionados ao jogador.
        public string NomeJogador { get; set; } = "";
        public string SobrenomeJogador { get; set; } = "";
        public int IdadeJogador { get; set; } = 18;
        public string NacionalidadeJogador { get; set; } = "";
        public int HabilidadeJogador { get; set; } = 32;
        public int IndexDoJogador { get; set; } = 99;
        // Atributos responsáveis pela data do game.
        public int ContadorDeSemana { get; set; } = 1;
        public int ContadorDeAno { get; set; } = 2024;
        public string StatusDaTemporada { get; set; } = "Pre-Temporada";
        public int TotalSemanas { get; set; } = 10;  // Total de 52 semanas, mudar após os testes. para teste deixa em 10
        // Atributos responsáveis pelas informações semanais do game.
        public string ProximoGp { get; set; } = "";
        public string ProximoGpPais { get; set; } = "";
        public int ProximoGpSemana { get; set; } = 0;
        public int EtapaAtual { get; set; } = 0;
        public int ProximoGpVoltas { get; set; } = 0;
        // Variável sendo utilizada na Tela de Classificação.
        public string Categoria { get; set; } = "";
        public int ConfiguracaoInicioDoGame { get; set; } = 0;  // 1 = Novo Jogo, 2 = Continuar Game
        public int TempoRodada { get; set; } = 200;  // Tempo que vai passar o game em milissegundos: Padrão vai ser 200
        // Variável que vai decidir a importância do Carro e Piloto na temporada.
        public int ImportanciaCarroTemporada { get; set; } = 50;
        public int ImportanciaPilotoTemporada { get; set; } = 50;
        // Atributos para definir as pontuações de cada posição no campeonato.
        public int PrimeiroLugar { get; set; } = 0;
        public int SegundoLugar { get; set; } = 0;
        public int TerceiroLugar { get; set; } = 0;
        public int QuartoLugar { get; set; } = 0;
        public int QuintoLugar { get; set; } = 0;
        public int SextoLugar { get; set; } = 0;
        public int SetimoLugar { get; set; } = 0;
        public int OitavoLugar { get; set; } = 0;
        public int NonoLugar { get; set; } = 0;
        public int DecimoLugar { get; set; } = 0;
        public int DecimoPrimeiroLugar { get; set; } = 0;
        public int DecimoSegundoLugar { get; set; } = 0;
        public int DecimoTerceiroLugar { get; set; } = 0;
        public int DecimoQuartoLugar { get; set; } = 0;
        public int DecimoQuintoLugar { get; set; } = 0;
        public int DecimoSextoLugar { get; set; } = 0;
        public int DecimoSetimoLugar { get; set; } = 0;
        public int DecimoOitavoLugar { get; set; } = 0;
        public int DecimoNonoLugar { get; set; } = 0;
        public int VigessimoLugar { get; set; } = 0;
        public int PontoVoltaMaisRapida { get; set; } = 0;

        public Principal(){}
        public void ConfigurarFaixaDePontuacao(String caminhoArquivo)   // Metodo para atribuir a pontução que vai ser utilizada no game.
        {
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                // Vai ler o meu arquivo e atribuir o valor passado, caso estiver incompleto, o valor será 0;
                PrimeiroLugar = int.Parse(sr.ReadLine() ?? "0");
                SegundoLugar = int.Parse(sr.ReadLine() ?? "0");
                TerceiroLugar = int.Parse(sr.ReadLine() ?? "0");
                QuartoLugar = int.Parse(sr.ReadLine() ?? "0");
                QuintoLugar = int.Parse(sr.ReadLine() ?? "0");
                SextoLugar = int.Parse(sr.ReadLine() ?? "0");
                SetimoLugar = int.Parse(sr.ReadLine() ?? "0");
                OitavoLugar = int.Parse(sr.ReadLine() ?? "0");
                NonoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoPrimeiroLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoSegundoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoTerceiroLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoQuartoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoQuintoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoSextoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoSetimoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoOitavoLugar = int.Parse(sr.ReadLine() ?? "0");
                DecimoNonoLugar = int.Parse(sr.ReadLine() ?? "0");
                VigessimoLugar = int.Parse(sr.ReadLine() ?? "0");
                PontoVoltaMaisRapida = int.Parse(sr.ReadLine() ?? "0");
            }
        }
        public void ContinuarTurno()
        {
            if (TotalSemanas > ContadorDeSemana)
            {
                ContadorDeSemana++;
                if (ContadorDeSemana > 4 && ContadorDeSemana <= 9)
                {
                    StatusDaTemporada = "Andamento"; // *Atualizar o valor apos finalizar os teste. -> (ContadorDeSemana > 4 && ContadorDeSemana <= 48) Para teste  (ContadorDeSemana > 4 && ContadorDeSemana <= 9)
                }
                else if (ContadorDeSemana > 9)
                {
                    StatusDaTemporada = "Fim-Temporada"; // *Atualizar o valor apos finalizar os teste. -> (ContadorDeSemana > 48)
                }
            }
            else
            {
                ContadorDeAno++;
                ContadorDeSemana = 1;
                StatusDaTemporada = "Pre-Temporada";
            }
        }
        public string FormatarNumero(double tempoTotalMilissegundos)
        {
            if (tempoTotalMilissegundos > 3599999) // Convertendo milissegundos para horas, minutos, segundos e milissegundos
            {
                int horas = (int)(tempoTotalMilissegundos / (1000 * 60 * 60));
                int minutos = (int)((tempoTotalMilissegundos / (1000 * 60)) % 60);
                int segundos = (int)((tempoTotalMilissegundos / 1000) % 60);
                int milissegundos = (int)(tempoTotalMilissegundos % 1000);
                string tempoFormatado = $"{horas}:{minutos:00}:{segundos:00}.{milissegundos:000}";
                return tempoFormatado; ;
            }
            else // Convertendo milissegundos para minutos, segundos e milissegundos
            {
                int minutos = (int)(tempoTotalMilissegundos / (1000 * 60));
                int segundos = (int)((tempoTotalMilissegundos / 1000) % 60);
                int milissegundos = (int)(tempoTotalMilissegundos % 1000);
                string tempoFormatado = $"{minutos}:{segundos:00}.{milissegundos:000}";
                return tempoFormatado;
            }
        }
        public string NomeAbreviado(string nome, string sobrenome)
        {
            char primeiraLetra = nome[0];
            string nomeCompleto = $"{primeiraLetra}. {sobrenome}";

            return nomeCompleto;
        }
        public static List<Principal> ObterListaCategoria() // Metoda para criar lista das categorias disponivel no game.
        {
            List<Principal> listSerie = new List<Principal>
            {
            new Principal { Categoria = "F1"},
            new Principal { Categoria = "F2"},
            new Principal { Categoria = "F3"},

            };
            return listSerie;
        }
        
        // Inicio das criação das lista para guarda historicos dos pilotos e equipes campeões.
        public List<Historico.PilotoCampeao> PilotosCampeoesF1 { get; set; } = new List<Historico.PilotoCampeao>();
        public List<Historico.PilotoCampeao> PilotosCampeoesF2 { get; set; } = new List<Historico.PilotoCampeao>();
        public List<Historico.PilotoCampeao> PilotosCampeoesF3 { get; set; } = new List<Historico.PilotoCampeao>();
        public List<Historico.EquipeCampeao> EquipesCampeoesF1 { get; set; } = new List<Historico.EquipeCampeao>();
        public List<Historico.EquipeCampeao> EquipesCampeoesF2 { get; set; } = new List<Historico.EquipeCampeao>();
        public List<Historico.EquipeCampeao> EquipesCampeoesF3 { get; set; } = new List<Historico.EquipeCampeao>();

        public void AdicionarPilotoCampeao(string categoria, int ano, string sede, string nome, int pontos, string cor1, string equipe) // Método para adicionar um piloto campeão à lista
        {
            if (categoria == "F1") PilotosCampeoesF1.Add(new Historico.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
            if (categoria == "F2") PilotosCampeoesF2.Add(new Historico.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
            if (categoria == "F3") PilotosCampeoesF3.Add(new Historico.PilotoCampeao { Ano = ano, Sede = sede, Nome = nome, Pontos = pontos, C1 = cor1, Equipe = equipe });
        }
        public void AdicionarEquipeCampeao(string categoria, int ano, string sede, string cor1, string nome, int pontos) // Método para adicionar um equipe campeão à lista
        {
            if (categoria == "F1") EquipesCampeoesF1.Add(new Historico.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
            if (categoria == "F2") EquipesCampeoesF2.Add(new Historico.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
            if (categoria == "F3") EquipesCampeoesF3.Add(new Historico.EquipeCampeao { Ano = ano, Sede = sede, C1 = cor1, Nome = nome, Pontos = pontos });
        }
        public void Transferencia(Piloto[] pilot, int indice1, int indice2) // NÃO GOSTEI, FAZER MUDANÇAS PARA MANTER O JOGADOR COM O INDEX 99 (Metodo para adicionar o piloto a equipe no inicio do game.
        {
            (pilot[indice2], pilot[indice1]) = (pilot[indice1], pilot[indice2]);
            IndexDoJogador = indice1;
        }
        List<string> atributosListaPilotos = new List<string>()
        {
            "largada",
            "concentracao",
            "ultrapassagem",
            "experiencia",
            "rapidez",
            "chuva",
            "acertoDoCarro",
            "fisico"
        };
        List<string> atributosListaEquipes = new List<string>()
        {
            "aerodinamica",
            "freio",
            "asaDianteira",
            "asaTraseira",
            "cambio",
            "eletrico",
            "direcao",
            "confiabilidade"
        };
        public void XpTurnoSemanal(Piloto[] piloto)
        {
            foreach (Piloto pilotoSelecionado in piloto)
            {
                double newXp = pilotoSelecionado.XpPiloto + pilotoSelecionado.PotencialPiloto;
                pilotoSelecionado.XpPiloto = newXp;

                if (pilotoSelecionado.XpPiloto >= 1)
                {
                    if (pilotoSelecionado.IdadePiloto <= pilotoSelecionado.AugePiloto)
                    {
                        // Adicionar pontos para acrescentar.
                        do
                        {
                            if (pilotoSelecionado.XpPiloto >= 1 && pilotoSelecionado.MediaPiloto < 100)
                            {
                                string atributoAleatorio = atributosListaPilotos[random.Next(atributosListaPilotos.Count)];
                                switch (atributoAleatorio)
                                {
                                    case "largada":
                                        if (pilotoSelecionado.Largada < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Largada++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "concentracao":
                                        if (pilotoSelecionado.Concentracao < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Concentracao++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "ultrapassagem":
                                        if (pilotoSelecionado.Ultrapassagem < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Ultrapassagem++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "experiencia":
                                        if (pilotoSelecionado.Experiencia < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Experiencia++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "rapidez":
                                        if (pilotoSelecionado.Rapidez < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Rapidez++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "chuva":
                                        if (pilotoSelecionado.Chuva < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Chuva++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "acertoDoCarro":
                                        if (pilotoSelecionado.AcertoDoCarro < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.AcertoDoCarro++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    case "fisico":
                                        if (pilotoSelecionado.Fisico < 100)
                                        {
                                            pilotoSelecionado.XpPiloto--;
                                            pilotoSelecionado.Fisico++;
                                            pilotoSelecionado.AtualizarMedia();
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (pilotoSelecionado.MediaPiloto == 100)
                            {
                                pilotoSelecionado.XpPiloto--;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        } while (true);
                    }
                    else
                    do
                    {
                        if (pilotoSelecionado.XpPiloto >= 1 && pilotoSelecionado.MediaPiloto > 1)
                        {
                            string atributoAleatorio = atributosListaPilotos[random.Next(atributosListaPilotos.Count)];
                            switch (atributoAleatorio)
                            {
                                case "largada":
                                    if (pilotoSelecionado.Largada > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Largada--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "concentracao":
                                    if (pilotoSelecionado.Concentracao > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Concentracao--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "ultrapassagem":
                                    if (pilotoSelecionado.Ultrapassagem > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Ultrapassagem--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "experiencia":
                                    if (pilotoSelecionado.Experiencia > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Experiencia--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "rapidez":
                                    if (pilotoSelecionado.Rapidez > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Rapidez--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "chuva":
                                    if (pilotoSelecionado.Chuva > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Chuva--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "acertoDoCarro":
                                    if (pilotoSelecionado.AcertoDoCarro > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.AcertoDoCarro--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                case "fisico":
                                    if (pilotoSelecionado.Fisico > 0)
                                    {
                                        pilotoSelecionado.XpPiloto--;
                                        pilotoSelecionado.Fisico--;
                                        pilotoSelecionado.AtualizarMedia();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    } while (true);
                }
            }
        }
        public void XpEquipeSemanal(Equipe[] equipe)
        {
            foreach (Equipe equipeSelecionado in equipe)
            {
                int newXp = random.Next(1, 4);   // Vai sortear entre 1 e 3 (1 = -1      2 = 0      3 = 1)

                do
                {
                    if (equipeSelecionado.MediaEquipe <= 100 && equipeSelecionado.MediaEquipe >= 10 && newXp != 0)
                    {
                        string atributoAleatorio = atributosListaEquipes[random.Next(atributosListaEquipes.Count)];
                        switch (atributoAleatorio)
                        {
                            case "aerodinamica":
                                if (equipeSelecionado.Aerodinamica <= 100 && equipeSelecionado.Aerodinamica >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1: 
                                            if (equipeSelecionado.Aerodinamica <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Aerodinamica -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Aerodinamica >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Aerodinamica += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "freio":
                                if (equipeSelecionado.Freio <= 100 && equipeSelecionado.Freio >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.Freio <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Freio -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Freio >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Freio += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "asaDianteira":
                                if (equipeSelecionado.AsaDianteira <= 100 && equipeSelecionado.AsaDianteira >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.AsaDianteira <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.AsaDianteira -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.AsaDianteira >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.AsaDianteira += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "asaTraseira":
                                if (equipeSelecionado.AsaTraseira <= 100 && equipeSelecionado.AsaTraseira >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.AsaTraseira <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.AsaTraseira -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.AsaTraseira >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.AsaTraseira += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "cambio":
                                if (equipeSelecionado.Cambio <= 100 && equipeSelecionado.Cambio >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.Cambio <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Cambio -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Cambio >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Cambio += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "eletrico":
                                if (equipeSelecionado.Eletrico <= 100 && equipeSelecionado.Eletrico >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.Eletrico <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Eletrico -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Eletrico >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Eletrico += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "direcao":
                                if (equipeSelecionado.Direcao <= 100 && equipeSelecionado.Direcao >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.Direcao <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Direcao -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Direcao >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Direcao += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            case "confiabilidade":
                                if (equipeSelecionado.Confiabilidade <= 100 && equipeSelecionado.Confiabilidade >= 10)
                                {
                                    switch (newXp)
                                    {
                                        case 1:
                                            if (equipeSelecionado.Confiabilidade <= 10)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Confiabilidade -= 1;
                                                newXp = 0;
                                                break;
                                            }
                                        case 2:
                                            newXp = 0;
                                            break;
                                        case 3:
                                            if (equipeSelecionado.Confiabilidade >= 100)
                                            {
                                                newXp = 0;
                                                break;
                                            }
                                            else
                                            {
                                                equipeSelecionado.Confiabilidade += 1;
                                                newXp = 0;
                                                break;
                                            }
                                        default:
                                            break;
                                    }
                                    equipeSelecionado.AtualizarMedia();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else if (equipeSelecionado.MediaEquipe == 100 || equipeSelecionado.MediaEquipe == 9)
                    {
                        newXp = 0;
                        break;
                    }
                    else
                    {
                        newXp = 0;
                        break;
                    }
                } while (true);
            }
        }
        public void PotenciaMotoresEquipe(Motor motor, Equipe[] equipes)
        {
            List<string> nomesDosMotores = motor.ObterNomesDosMotores();

            for (int i = 0; i < nomesDosMotores.Count; i++)
            {
                string nomeDoMotor = nomesDosMotores[i];
                int novoValor = random.Next(1, 4);      // Vai sortear entre 1 e 3 (1 = -1      2 = 0      3 = 1) 
                motor.AlterarValorDoMotor(nomeDoMotor, novoValor);
            }
            foreach (Equipe equipe in equipes)
            {
                equipe.ValorDoMotor = motor.ObterValorDoMotor(equipe.NameMotor);
            }
        }
    }
}
