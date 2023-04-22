using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json.Linq;
using ServidorEuroMilhoes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoSorteio
{
    public partial class Menu : Form
    {
        public string IP { get; private set; }
        private int[] NumerosVencedores = new int[5];
        private int[] EstrelasVencedoras = new int[2];

        public Menu()
        {
            InitializeComponent();
            NumerosVencedores = null;
            EstrelasVencedoras = null;
        }
        public Menu(string VarIp)
        {
            InitializeComponent();
            NumerosVencedores = null;
            EstrelasVencedoras = null;
            IP = VarIp;
        }

        public Menu(int[] VNumeros, int[] VEstrelas, string VarIp)
        {
            InitializeComponent();
            NumerosVencedores = null;
            EstrelasVencedoras = null;
            NumerosVencedores = VNumeros;
            EstrelasVencedoras = VEstrelas;
            IP = VarIp;
        }

        private async void Menu_Load(object sender, EventArgs e)
        {
            if(NumerosVencedores != null && EstrelasVencedoras != null)
            {
                await BuscarApostas();
            }
        }
        public async Task BuscarApostas()
        {
            try
            {
                List<Aposta> ApostasList = new List<Aposta>();
                List<Aposta> ApostasVencedoras = new List<Aposta>();

                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                var httpClient = new HttpClient(httpClientHandler);
                var channel = GrpcChannel.ForAddress("https://" + Convert.ToString(IP),
                    new GrpcChannelOptions() { HttpClient = httpClient });

                var client = new EuroMilhoes.EuroMilhoesClient(channel);

                using (var reply = client.ListaApostasVencedoras(new teste { Test = true }))
                {
                    while (await reply.ResponseStream.MoveNext())
                    {
                        var aposta = reply.ResponseStream.Current;
                        ApostasList.Add(new Aposta { Nif = Convert.ToInt32(aposta.Nif), Numeros = Convert.ToString(aposta.Numeros), Estrelas = Convert.ToString(aposta.Estrelas) });
                    }
                }

                foreach (var a in ApostasList)
                {
                    string AuxNumbers = a.Numeros;
                    string AuxStars = a.Estrelas;

                    int Niguais=0;
                    int Eiguais=0;
                    
                    string[] numbers = AuxNumbers.Split(";");
                    string[] stars = AuxStars.Split(";");

                    for(int i = 0; i < 5; i++)
                    {
                        if (NumerosVencedores.Contains(Convert.ToInt32(numbers[i])))
                        {
                            Niguais++;
                        }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        if (EstrelasVencedoras.Contains(Convert.ToInt32(stars[i])))
                        {
                            Eiguais++;
                        }
                    }

                    if(Niguais == 5 && Eiguais == 2)
                    {
                        ApostasVencedoras.Add(a);
                    }
                }

                if(ApostasVencedoras != null)
                {
                    dataGridView1.DataSource = ApostasVencedoras;
                }
                await SorteiosFalse();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao encontrar chaves vencedoras!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async Task SorteiosFalse()
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
                var reply = await client.SetSorteioAtualFalseAsync(
                                  new teste { Test = true });

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao setar Variavel SorteioAtual para false!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            PagInicial pag = new PagInicial();
            pag.Show();
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://" + Convert.ToString(IP),
                new GrpcChannelOptions() { HttpClient = httpClient });

            var client = new EuroMilhoes.EuroMilhoesClient(channel);
            var reply = await client.ApostasFeitasAsync(new teste { Test = true });

            string json = JsonSerializer.Serialize(reply);


            dynamic data = JObject.Parse(json);

            bool mensagem = Convert.ToBoolean(data.Resposta);

            if (mensagem == true)
            {
                ChaveVencedora pag = new ChaveVencedora(IP);
                pag.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ainda não foram feitas apostas!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
