using EuroMilhoesCliente;
using Grpc.Core;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EuroMilhoesCliente
{
    public partial class Lista : Form
    {
        public int Nif { get; private set; }
        public string IP { get; private set; }
        public Lista()
        {
            InitializeComponent();
        }

        public Lista(int VarNif, string VarIP)
        {
            Nif = Convert.ToInt32(VarNif);
            IP = Convert.ToString(VarIP);
            InitializeComponent();
        }

        public async Task BuscarApostas()
        {
            try
            {
                List<Aposta> ApostasList = new List<Aposta>();

                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var httpClient = new HttpClient(httpClientHandler);
                var channel = GrpcChannel.ForAddress("https://" + Convert.ToString(IP), 
                    new GrpcChannelOptions() { HttpClient = httpClient });

                var client = new EuroMilhoes.EuroMilhoesClient(channel);

                using (var reply = client.ListaApostas(new Nif { Niff = Nif }))
                {
                    while (await reply.ResponseStream.MoveNext())
                    {
                        var aposta = reply.ResponseStream.Current;
                        ApostasList.Add(new Aposta { Numeros = Convert.ToString(aposta.Numeros), DataAposta = Convert.ToDateTime(aposta.Data), Estrelas = Convert.ToString(aposta.Estrelas), SorteioAtual = Convert.ToBoolean(aposta.SorteioAtual) });
                    }
                }

                dataGridView1.DataSource = ApostasList;
                dataGridView1.Columns[0].HeaderText = "Números";
                dataGridView1.Columns[1].HeaderText = "Estrelas";
                dataGridView1.Columns[2].HeaderText = "Data da aposta";
                dataGridView1.Columns[3].HeaderText = "Soteio Atual?";

            }
            catch (Exception e)
            {
                MessageBox.Show("A lista de apostas está vazia!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Menu menu = new Menu(Convert.ToInt32(Nif), Convert.ToString(IP));
            menu.Show();
            this.Hide();
        }

        private async void Lista_Load(object sender, EventArgs e)
        {
            await BuscarApostas();
        }
    }
}
