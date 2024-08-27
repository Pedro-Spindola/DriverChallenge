using Microsoft.VisualBasic;
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

namespace DriverChallenge
{
    public partial class TelaFinancias : Form
    {
        Principal principal;
        Financia financia;
        public TelaFinancias(Principal principal, Financia financia)
        {
            this.principal = principal;
            this.financia = financia;
            InitializeComponent();
        }
        private void TelaFinancias_Load(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
