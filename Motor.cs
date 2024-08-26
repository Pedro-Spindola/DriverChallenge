using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverChallenge
{
    public class Motor
    {
        private Dictionary<string, int> valoresDosMotores = new Dictionary<string, int>();

        public Motor()
        {
            // Add os valores inicial dos Motores.
            valoresDosMotores.Add("Honda", 90);
            valoresDosMotores.Add("Ferrari", 90);
            valoresDosMotores.Add("TAG", 85);
            valoresDosMotores.Add("Mercedes", 85);
            valoresDosMotores.Add("Renault", 80);
            valoresDosMotores.Add("BMW", 80);
            valoresDosMotores.Add("Ford", 75);
            valoresDosMotores.Add("Audi", 75);
            valoresDosMotores.Add("Toyota", 70);
            valoresDosMotores.Add("Lamborghini", 70);
            // ...
        }

        public int ObterValorDoMotor(string nomeDoMotor)
        {
            if (valoresDosMotores.ContainsKey(nomeDoMotor)) return valoresDosMotores[nomeDoMotor];
            return 0; // Motor desconhecido.
        }
        public void AlterarValorDoMotor(string nomeDoMotor, int novoValor)
        {
            if (valoresDosMotores.ContainsKey(nomeDoMotor))
            {
                if (valoresDosMotores[nomeDoMotor] < 100 && valoresDosMotores[nomeDoMotor] > 70)
                {
                    if (novoValor == 1) valoresDosMotores[nomeDoMotor] -= 1;
                    else if (novoValor == 3) valoresDosMotores[nomeDoMotor] += 1;
                }
                else if (valoresDosMotores[nomeDoMotor] == 100)
                {
                    if (novoValor == 1) valoresDosMotores[nomeDoMotor] -= 1;
                }
                else if (valoresDosMotores[nomeDoMotor] == 70)
                {
                    if (novoValor == 3)valoresDosMotores[nomeDoMotor] += 1;
                }
            }
        }
        public List<string> ObterNomesDosMotores()
        {
            return new List<string>(valoresDosMotores.Keys);
        }
        public string ObterNomeAleatorioDoMotor()
        {
            List<string> nomesDosMotores = ObterNomesDosMotores();
            Random random = new Random();
            int indiceAleatorio = random.Next(nomesDosMotores.Count);
            return nomesDosMotores[indiceAleatorio];
        }
    }
}
