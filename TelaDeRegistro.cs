using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DriverChallenge
{
    public partial class TelaDeRegistro : Form
    {
        private string connectionString = "Server=sql10.freesqldatabase.com;Database=sql10732553;Uid=sql10732553;Pwd=SHLpbe7vWf;";

        public TelaDeRegistro()
        {
            InitializeComponent();
        }
        public static string ObterEnderecoMacWiFi()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in interfaces)
            {
                if (iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && iface.OperationalStatus == OperationalStatus.Up)
                {
                    // Retorna o endereço MAC da interface Wi-Fi
                    return BitConverter.ToString(iface.GetPhysicalAddress().GetAddressBytes());
                }
            }
            return "Nenhum endereço MAC da interface Wi-Fi encontrado.";
        }

        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            string nome = inputNomeRegistro.Text;
            string senha = inputSenhaRegistro.Text;

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            if (ValidarCredenciais(nome, senha))
            {
                // Incrementar o contador
                // IncrementarContador(nome);
                // string enderecoMac = ObterEnderecoMacWiFi();
                // 
                MessageBox.Show("Login bem-sucedido. Acesso liberado.");
            }
            else
            {
                MessageBox.Show("Nome de usuário ou senha incorretos.");
            }
        }

        private bool ValidarCredenciais(string nome, string senha)
        {
            bool loginValido = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // A senha é passada como está e será hasheada na consulta
                    string query = "SELECT COUNT(*) FROM usuarios WHERE nome = @nome AND senha = SHA2(@senha, 256)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.Parameters.AddWithValue("@senha", senha); // Senha normal

                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                        if (userCount > 0)
                        {
                            loginValido = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar credenciais: " + ex.Message);
                }
            }

            return loginValido;
        }

        private void IncrementarContador(string nome)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE usuarios SET contador = contador + 1 WHERE nome = @nome";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@nome", nome);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao incrementar contador: " + ex.Message);
                }
            }
        }
    }
}
