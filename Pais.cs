using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Pais
    {
        public string Nome { get; set; } = "";
        public string IconePais { get; set; } = "";

        public static List<Pais> ObterListaDePaises()
        {
            List<Pais> paises = new List<Pais>
        {
            new Pais { Nome = "Brasil", IconePais = "icone_brasil.png" },
            new Pais { Nome = "Estados Unidos", IconePais = "icone_eua.png" },
            // Adicione outros países aqui
        };
            return paises;
        }
    }
}
