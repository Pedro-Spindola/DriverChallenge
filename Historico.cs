using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Historico
    {
        public class PilotoCampeao
        {
            public required int Ano { get; set; }
            public required string Sede { get; set; }
            public required string Nome { get; set; }
            public required int Pontos { get; set; }
            public required string C1 { get; set; }
            public required string Equipe { get; set; }
        }
        public class EquipeCampeao
        {
            public required int Ano { get; set; }
            public required string Sede { get; set; }
            public required string C1 { get; set; }
            public required string Nome { get; set; }
            public required int Pontos { get; set; }
        }

        public class HallFama
        {
            public required string Nome { get; set; }
        }
    }
}
