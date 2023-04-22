using Grpc.Net.Client;
using ServidorEuroMilhoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace GestaoSorteio
{
    public partial class ChaveVencedora : Form
    {
        private int[] Numeros = new int[5];
        private int[] Estrelas = new int[2];
        public string IP { get; private set; }
        public ChaveVencedora()
        {
            InitializeComponent();
        }

        public ChaveVencedora(string VarIP)
        {
            InitializeComponent();
            IP = VarIP;
        }

        private void linkLabelVoltar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Menu pag = new Menu();
            pag.Show();
            this.Hide();
        }

        private void buttonSubmeter_Click(object sender, EventArgs e)
        {
            int NumerosBem = 0;
            int EstrelasBem = 0;

            try
            {
                Numeros[0] = Convert.ToInt32(textBoxN1.Text);
                Numeros[1] = Convert.ToInt32(textBoxN2.Text);
                Numeros[2] = Convert.ToInt32(textBoxN3.Text);
                Numeros[3] = Convert.ToInt32(textBoxN4.Text);
                Numeros[4] = Convert.ToInt32(textBoxN5.Text);

                foreach (int n in Numeros)
                {
                    if (n > 0 && n <= 50)
                    {
                        NumerosBem++;
                    }
                }

                if (Convert.ToInt32(NumerosBem) != 5)
                {
                    MessageBox.Show("Os números devem estar entre 1 e 50!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                Estrelas[0] = Convert.ToInt32(textBoxE1.Text);
                Estrelas[1] = Convert.ToInt32(textBoxE2.Text);

                foreach (int es in Estrelas)
                {
                    if (es > 0 && es <= 12)
                    {
                        EstrelasBem++;
                    }
                }

                if (Convert.ToInt32(EstrelasBem) != 2)
                {
                    MessageBox.Show("As estrelas devem estar entre 1 e 12!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (ValoresRepetidos(Numeros, Estrelas) == true)
                {
                    MessageBox.Show("Existem Valores repetidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (NumerosBem == 5 && EstrelasBem == 2)
                {

                    MessageBox.Show("A sua chave vencedora foi indicada com sucesso!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Menu menu = new Menu(Numeros,Estrelas,IP);
                    menu.Show();
                    this.Hide();
                }

                textBoxN1.Text = "";
                textBoxN2.Text = "";
                textBoxN3.Text = "";
                textBoxN4.Text = "";
                textBoxN5.Text = "";
                textBoxE1.Text = "";
                textBoxE2.Text = "";
            }
            catch
            {
                MessageBox.Show("Não inseriu a chave corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool ValoresRepetidos(int[] VetorN, int[] VetorE)
        {

            if (VetorN.GroupBy(v => v).Any(v => v.Count() > 1) || VetorE.GroupBy(u => u).Any(u => u.Count() > 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
