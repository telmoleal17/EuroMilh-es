using Grpc.Net.Client;
using ServidorEuroMilhoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace EuroMilhoesCliente
{
    public partial class PagInicial : Form
    {
        private int i;
        private int Nif { get; set; }
        private string IP { get; set; }
        public PagInicial()
        {
            InitializeComponent();
        }

        private void PagInicial_Load(object sender, EventArgs e)
        {

        }

        private async void buttonSubmeter_Click(object sender, EventArgs e)
        {
            int aux = 0;

            if(textBoxNIF.Text == null)
            {
                MessageBox.Show("Não inseriu o NIF!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(!int.TryParse(textBoxNIF.Text, out i))
            {
                MessageBox.Show("Não inseriu o NIF corretamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBoxNIF.Text.Length != 9)
            {
                MessageBox.Show("O NIF é composto 9 números!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Nif = Convert.ToInt32(textBoxNIF.Text);
                aux++;
            }

            if (textBoxIP.Text == null)
            {
                MessageBox.Show("Não inseriu o IP!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (IsValidIp(textBoxIP.Text.ToString()) == false)
            {
                MessageBox.Show("O IP não é valido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                IP = textBoxIP.Text.ToString();
                aux++;
            }

            if (Convert.ToInt32(aux) == 2)
            {
                try
                {
                    var httpClientHandler = new HttpClientHandler();
                    httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    var httpClient = new HttpClient(httpClientHandler);

                    var channel = GrpcChannel.ForAddress("https://" + Convert.ToString(IP),
                        new GrpcChannelOptions() { HttpClient = httpClient });

                    var client = new EuroMilhoes.EuroMilhoesClient(channel);
                    var reply = await client.TesteConeccaoAsync(
                                      new teste { Test = true });

                    if (reply.StatusConnection == true)
                    {
                        Menu menu = new Menu(Nif, IP);
                        menu.Show();
                        this.Hide();
                    }
                }
                catch(Exception d)
                {
                    MessageBox.Show("Não foi possivel ligar ao servidor!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public bool IsValidIp(string addr)
        {
            IPAddress ip;
            bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
            return valid;
        }

        private void textBoxNIF_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelNIF_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
