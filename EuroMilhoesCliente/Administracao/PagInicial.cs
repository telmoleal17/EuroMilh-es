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

namespace Administracao
{
    public partial class PagInicial : Form
    {
        public string IP { get; private set; }
        public PagInicial()
        {
            InitializeComponent();
        }

        private async void buttonSubmeter_Click(object sender, EventArgs e)
        {
            int aux = 0;

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

            if (Convert.ToInt32(aux) == 1)
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
                        Menu menu = new Menu(IP);
                        menu.Show();
                        this.Hide();
                    }
                }
                catch (Exception d)
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
    }
}
