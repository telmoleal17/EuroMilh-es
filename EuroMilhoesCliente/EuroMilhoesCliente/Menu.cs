using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroMilhoesCliente
{
    public partial class Menu : Form
    {
        public int Nif { get; private set; }
        public string IP { get;private set; }
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(int VarNif,string VarIP)
        {
            InitializeComponent();
            Nif = Convert.ToInt32(VarNif);
            IP = Convert.ToString(VarIP);
            labelNif.Text = Convert.ToString("Conectado com o  NIF: " +VarNif);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista(Convert.ToInt32(Nif), Convert.ToString(IP));
            lista.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistarChave registar = new RegistarChave(Convert.ToInt32(Nif), Convert.ToString(IP));
            registar.Show();
            this.Hide();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            PagInicial pag = new PagInicial();
            pag.Show();
            this.Hide();
        }
    }
}
