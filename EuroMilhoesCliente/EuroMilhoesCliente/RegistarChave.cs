//using Google.Protobuf.WellKnownTypes;
//using Grpc.Net.Client;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroMilhoesCliente
{
    public partial class RegistarChave : Form
    {
        private int[] Numeros = new int[5];
        private int[] Estrelas = new int[2];

        public int Nif { get; private set; }
        public string IP { get; private set; }

        public RegistarChave()
        {
            InitializeComponent();
        }

        public RegistarChave(int VarNif,string VarIP)
        {
            Nif = Convert.ToInt32(VarNif);
            IP = Convert.ToString(VarIP);
            InitializeComponent();
        }

        private void linkLabelVoltar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Menu menu = new Menu(Convert.ToInt32(Nif), Convert.ToString(IP));
            menu.Show();
            this.Hide();
        }

        private async void buttonSubmeter_Click(object sender, EventArgs e)
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

                foreach(int n in Numeros)
                {
                    if(n > 0 && n <= 50)
                    {
                        NumerosBem++;
                    }
                }
                
                if(Convert.ToInt32(NumerosBem) != 5)
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

                if(ValoresRepetidos(Numeros,Estrelas) == true)
                {
                    MessageBox.Show("Existem Valores repetidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(NumerosBem == 5 && EstrelasBem == 2)
                {
                    string AuxNumeros = Numeros[0] + ";" + Numeros[1] + ";" + Numeros[2] + ";" + Numeros[3] + ";" + Numeros[4];
                    string AuxEstrelas = Estrelas[0] + ";" + Estrelas[1];
                    string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    var httpClientHandler = new HttpClientHandler();
                    httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    var httpClient = new HttpClient(httpClientHandler);
                    
                    var channel = GrpcChannel.ForAddress("https://" +Convert.ToString(IP),
                        new GrpcChannelOptions() { HttpClient = httpClient });

                    var client = new EuroMilhoes.EuroMilhoesClient(channel);
                    var reply = await client.RegistarAsync(
                                      new RegistarAposta { Nif = Convert.ToInt32(Nif), Numeros = AuxNumeros, Estrelas = AuxEstrelas, Data = data });
                    MessageBox.Show("A sua chave foi submetida com sucesso!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
