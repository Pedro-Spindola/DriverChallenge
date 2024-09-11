using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DriverChallenge.Financia;

namespace DriverChallenge
{
    public class Financia
    {
        public double DinheiroJogadorTotal { get; set; } = 0;
        public double SalarioPatrocinadores { get; set; } = 0;
        public double EspacoContratoDisponivel { get; set; } = 4;
        public double SalarioDaEquipe { get; set; } = 0;
        public double CustoEscritorio { get; set; } = 15000;
        public Patrocinador[] Patrocinadores { get; set; } = new Patrocinador[4]
        {
            new Patrocinador(),
            new Patrocinador(),
            new Patrocinador(),
            new Patrocinador()
        };
        public Financia() {
        }
        public double retonarSalarioTotalSemanal()
        {
            double salarioPatrocinadoresTemporario = 0;
            for (int i = 0; i < Patrocinadores.Length; i++)
            {
                if (Patrocinadores[i].ContratoValido)
                {
                    salarioPatrocinadoresTemporario += Patrocinadores[i].ValorContrato;
                }
            }
            SalarioPatrocinadores = salarioPatrocinadoresTemporario;
            return (SalarioDaEquipe + SalarioPatrocinadores - CustoEscritorio);

        }
        public void limparPatrocinador(Patrocinador patrocinador)
        {
            patrocinador.NomeDaEmpresa = "";
            patrocinador.NacionalidadeDaEmpresa = "null";
            patrocinador.ValorContrato = 0;
            patrocinador.TempoDeContrato = 0;
            patrocinador.TempoDeContratoSemanal = 0;
            patrocinador.TempoPropostaContrato = 0;
            patrocinador.ContratoValido = false;
        }
        public Patrocinador AdicionarNovoContrato(Piloto piloto)
        {
            Random random = new Random();
            string nome = "";
            string nacionalidade = "";
            do
            {
                int indiceAleatorio = random.Next(0, ListSelecionarPatrocinador.Count);
                string patrocinadorSelecionado = ListSelecionarPatrocinador[indiceAleatorio];
                string[] dadosPatrocinador = patrocinadorSelecionado.Split(',');
                nome = dadosPatrocinador[0];
                nacionalidade = dadosPatrocinador[1];
                int patrocinadorNaoRepetido = 0;
                for (int i = 0; i < Patrocinadores.Length; i++)
                {
                    if (Patrocinadores[i].ContratoValido && Patrocinadores[i].NomeDaEmpresa == nome)
                    {
                        patrocinadorNaoRepetido++;
                    }
                }
                if(patrocinadorNaoRepetido == 0)
                {
                    break;
                }
            } while (true);

            double valor = 0;
            if (piloto.Categoria == "F1")
            {
                valor = random.Next(1000, 3501) * ((piloto.VisibilidadePiloto + piloto.MediaPiloto) / 100.0);
            }
            else if (piloto.Categoria == "F2")
            {
                valor = random.Next(2500, 7001) * ((piloto.VisibilidadePiloto + piloto.MediaPiloto) / 100.0);
            }
            else
            {
                valor = random.Next(4000, 10000) * ((piloto.VisibilidadePiloto + piloto.MediaPiloto) / 100.0);

            }
            int valorArredondado = (int)(Math.Round(valor / 10.0) * 10);
            int contrato = random.Next(6, 25); // 24 a 105 semanas, porem contrato vai ser em mês no caso 6 a 24.   Para teste vou utilizar (1, 5) 
            int tempoDeContrato = (contrato * 4);
            int tempoProposta = random.Next(2, 11);
            Patrocinador novoPatrocinador = new Patrocinador(nome, nacionalidade, valorArredondado, contrato, tempoDeContrato, tempoProposta);
            return novoPatrocinador;
        }
        public class Patrocinador
        {
            public string NomeDaEmpresa { get; set; } = "";
            public string NacionalidadeDaEmpresa { get; set; } = "null";
            public double ValorContrato { get; set; } = 0;
            public int TempoDeContrato { get; set; } = 0;
            public int TempoDeContratoSemanal {  get; set; } = 0;
            public int TempoPropostaContrato { get; set; } = 0;
            public Boolean ContratoValido { get; set; } = false; 

            public Patrocinador() { }
            public Patrocinador(string nome, string nacionalidade, double valor, int contrato, int contratoSemanal, int tempoProposta)
            {
                NomeDaEmpresa = nome;
                NacionalidadeDaEmpresa = nacionalidade;
                ValorContrato = valor;
                TempoDeContrato = contrato;
                TempoDeContratoSemanal = contratoSemanal;
                TempoPropostaContrato = tempoProposta;
            }
        }
        public List<string> ListSelecionarPatrocinador { get; } = new List<string>
        {
            "Adidas,Alemanha",
            "Audi,Alemanha",
            "Bayer AG,Alemanha",
            "BMW,Alemanha",
            "BWT,Alemanha",
            "DHL,Alemanha",
            "Hugo Boss,Alemanha",
            "Mahle,Alemanha",
            "Mercedes-Benz,Alemanha",
            "Porsche,Alemanha",
            "Puma,Alemanha",
            "Ravenol,Alemanha",
            "Red Bull,Alemanha",
            "SAP,Alemanha",
            "Sofina,Alemanha",
            "TeamViewer,Alemanha",
            "Aramco,Arábia Saudita",
            "Kaust,Arábia Saudita",
            "SABIC,Arábia Saudita",
            "Globant,Argentina",
            "MercadoLivre,Argentina",
            "Gold Fie,África do Sul",
            "BHP,Austrália",
            "Commbank,Austrália",
            "Rexona,Austrália",
            "BWT,Áustria",
            "Rauch,Áustria",
            "Red Bull,Áustria",
            "Riedel,Áustria",
            "Ambev,Brasil",
            "Banco do Brasil,Brasil",
            "Claro,Brasil",
            "Eletrobras,Brasil",
            "Hypera Pharma,Brasil",
            "Itaú,Brasil",
            "JBS,Brasil",
            "NuBank,Brasil",
            "Petrobras,Brasil",
            "Vale,Brasil",
            "Bombardier,Canadá",
            "Canada Life,Canadá",
            "RBC,Canadá",
            "LATAM,Chile",
            "Alibaba,China",
            "Huawei,China",
            "Xiaomi,China",
            "Tencent,China",
            "KIA,Coreia do Sul",
            "LG,Coreia do Sul",
            "Samsung,Coreia do Sul",
            "Bestseller,Dinamarca",
            "Carlsberg,Dinamarca",
            "Novo Nordisk,Dinamarca",
            "Emirates,Emirados Árabes Unidos",
            "Johnnie Walker,Escócia",
            "Estrella Galicia,Espanha",
            "Iberdrola,Espanha",
            "Inditex,Espanha",
            "Zara,Espanha",
            "3M,Estados Unidos",
            "AMD,Estados Unidos",
            "American Express,Estados Unidos",
            "Apple,Estados Unidos",
            "AWS,Estados Unidos",
            "Bank of America,Estados Unidos",
            "Budweiser,Estados Unidos",
            "Chase Sapphire,Estados Unidos",
            "Coca-Cola,Estados Unidos",
            "Dell,Estados Unidos",
            "ESPN,Estados Unidos",
            "Fedex,Estados Unidos",
            "General Electric,Estados Unidos",
            "General Motors,Estados Unidos",
            "Google,Estados Unidos",
            "HP,Estados Unidos",
            "IBM,Estados Unidos",
            "Intel,Estados Unidos",
            "JP Morgan Chase,Estados Unidos",
            "KFC,Estados Unidos",
            "Lockheed Martin,Estados Unidos",
            "Marlboro,Estados Unidos",
            "McDonald's,Estados Unidos",
            "Microsoft,Estados Unidos",
            "NASA,Estados Unidos",
            "Netflix,Estados Unidos",
            "Nike,Estados Unidos",
            "Nvidia,Estados Unidos",
            "Oracle,Estados Unidos",
            "PayPal,Estados Unidos",
            "Penske,Estados Unidos",
            "Qualcomm,Estados Unidos",
            "Raytheon,Estados Unidos",
            "Starbucks,Estados Unidos",
            "Tesla,Estados Unidos",
            "United Airlines,Estados Unidos",
            "UPS,Estados Unidos",
            "Visa,Estados Unidos",
            "Walmart,Estados Unidos",
            "Nokia,Finlândia",
            "Bell & Ross,França",
            "Bugatti,França",
            "Louis Vuitton,França",
            "Renault,França",
            "Heineken,Holanda",
            "Jumbo,Holanda",
            "Randstad Holding,Holanda",
            "Infosys,Índia",
            "Kingfisher,Índia",
            "Sahara,Índia",
            "Tata Consultancy,Índia",
            "Alfa Romeo,Itália",
            "Ferrari,Itália",
            "Gucci,Itália",
            "Iveco,Itália",
            "Lamborghini,Itália",
            "OZ Racing,Itália",
            "Pirelli,Itália",
            "Ray-Ban,Itália",
            "Sparco,Itália",
            "Epson,Japão",
            "Honda,Japão",
            "Mitsubishi,Japão",
            "Nissan,Japão",
            "Sony,Japão",
            "Toyota,Japão",
            "AirAsia,Malásia",
            "Petronas,Malásia",
            "Becle,México",
            "Cemex,México",
            "Telcel,México",
            "Equinor,Noruega",
            "Kongsberg,Noruega",
            "Air New Zealand,Nova Zelândia",
            "Fonterra,Nova Zelândia",
            "PKN Orlen,Polônia",
            "Banco BPI,Portugal",
            "Pingo Doce,Portugal",
            "Petrogal,Portugal",
            "Qatar Airways,Catar",
            "Qatar Petroleum,Catar",
            "Aston Martin,Reino Unido",
            "HSBC,Reino Unido",
            "Jaguar,Reino Unido",
            "Land Rover,Reino Unido",
            "Marks & Spencer,Reino Unido",
            "Rich Energy,Reino Unido",
            "Rolls-Royce,Reino Unido",
            "Shell,Reino Unido",
            "Vodafone,Reino Unido",
            "Electrolux,Suécia",
            "Ericsson,Suécia",
            "Hennes & Mauritz,Suécia",
            "Huski Chocolate,Suécia",
            "Koenigsegg,Suécia",
            "Scania,Suécia",
            "Securitas AB,Suécia",
            "Spotify,Suécia",
            "Volvo,Suécia",
            "ABB,Suíça",
            "Credit Suisse,Suíça",
            "Novartis,Suíça",
            "Nestlé,Suíça",
            "Rolex,Suíça",
            "Securitas,Suíça",
            "TAG Heuer,Suíça",
            "Singha,Tailândia",
            "PDVSA,Venezuela"
        };
    }
}
