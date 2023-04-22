using Grpc.Core;
using Grpc.Net.Client;
using ServidorEuroMilhoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Administracao
{
    public partial class Menu : Form
    {
        public string IP { get; private set; }
        public Menu()
        {
            InitializeComponent();
        }

        public Menu(string VarIp)
        {
            InitializeComponent();
            IP = VarIp;
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            label1.Text = "Conectado com: " + IP.ToString();

           await BuscarApostas();

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

                using (var reply = client.ListaApostasAtuais(new teste { Test = true }))
                {
                    while (await reply.ResponseStream.MoveNext())
                    {
                        var aposta = reply.ResponseStream.Current;
                        ApostasList.Add(new Aposta { Nif= Convert.ToInt32(aposta.Nif) ,Numeros = Convert.ToString(aposta.Numeros), Estrelas = Convert.ToString(aposta.Estrelas), DataAposta = Convert.ToDateTime(aposta.Data) });
                    }
                }

                dataGridView1.DataSource = ApostasList;
                dataGridView1.Columns[0].HeaderText = "Nif";
                dataGridView1.Columns[1].HeaderText = "Números";
                dataGridView1.Columns[2].HeaderText = "Estrelas";
                dataGridView1.Columns[3].HeaderText = "Data da Aposta";
            }
            catch (Exception e)
            {
                MessageBox.Show("A lista de apostas está vazia!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            PagInicial pag = new PagInicial();
            pag.Show();
            this.Hide();
        }

        private async void button_Arquivar_Click(object sender, EventArgs e)
        {
            await Arquivar();
        }

        public async Task Arquivar()
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
                var reply = await client.ArquivarApostasAsync(new teste { Test = true });

                string json = JsonSerializer.Serialize(reply);


                dynamic data = JObject.Parse(json);

                string mensagem = data.Resposta;

                MessageBox.Show(mensagem.ToString(), " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao arquivar as apostas!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
