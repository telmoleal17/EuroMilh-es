using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Servidor.Data;
using Servidor.Models;

namespace ServidorEuroMilhoes
{
    public class GreeterService : EuroMilhoes.EuroMilhoesBase
    {
        public readonly EuroMilhoesContext _context;
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger, EuroMilhoesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<RegistarApostaReply> Registar(RegistarAposta request, ServerCallContext context)
        {
            Guardar(Convert.ToInt32(request.Nif),request.Numeros,request.Estrelas,Convert.ToDateTime(request.Data));
            return Task.FromResult(new RegistarApostaReply
            {
                Resposta = "O NIF: " + request.Nif + " apostou a chave " + request.Numeros + " " + request.Estrelas + " foi apostada com sucesso em " + request.Data
            });
        }

        public void Guardar(int nif, string numeros, string Estrelas, DateTime Data)
        {
            ApostasAtuai aposta = new ApostasAtuai();
            aposta.Nif = Convert.ToInt32(nif);
            aposta.Numeros = Convert.ToString(numeros);
            aposta.Estrelas = Convert.ToString(Estrelas);
            aposta.DataAposta = Convert.ToDateTime(Data);
            _context.ApostasAtuais.Add(aposta);
            _context.SaveChanges();
        }

        public override async Task ListaApostas(Nif nif, IServerStreamWriter<ApostaUtilizador> responseStream, ServerCallContext context)
        {
            List<ApostasAtuai> historico = _context.ApostasAtuais.Where(n => n.Nif == nif.Niff).ToList();
            List<ApostaUtilizador> apostaAtuais = new List<ApostaUtilizador>();
            ApostaUtilizador aposta1 = new ApostaUtilizador();

            foreach (var h in historico)
            {
                aposta1 = new ApostaUtilizador();
                aposta1.Numeros = h.Numeros;
                aposta1.Estrelas = h.Estrelas;
                aposta1.Data = Convert.ToString(h.DataAposta);
                aposta1.SorteioAtual = Convert.ToBoolean(h.SorteioAtual);

                apostaAtuais.Add(aposta1);
            }

            foreach (var a in apostaAtuais)
            {
                await responseStream.WriteAsync(a);
            }
        }

        public override Task<Connection> TesteConeccao(teste test, ServerCallContext context)
        {
            return Task.FromResult(new Connection
            {
                StatusConnection = true
            });
        }

        public override async Task ListaApostasAtuais(teste teste, IServerStreamWriter<TodasApostas> responseStream, ServerCallContext context)
        {
            List<ApostasAtuai> historico = _context.ApostasAtuais.Where(n => n.SorteioAtual == false).ToList();
            List<TodasApostas> apostaAtuais = new List<TodasApostas>();
            TodasApostas aposta1 = new TodasApostas();

            foreach (var h in historico)
            {
                aposta1 = new TodasApostas();
                aposta1.Nif = h.Nif;
                aposta1.Numeros = h.Numeros;
                aposta1.Estrelas = h.Estrelas;
                aposta1.Data = Convert.ToString(h.DataAposta);

                apostaAtuais.Add(aposta1);
            }

            foreach (var a in apostaAtuais)
            {
                await responseStream.WriteAsync(a);
            }
        }

        public async override Task<RespostaArquivar> ArquivarApostas(teste Teste, ServerCallContext context)
        {
            string resposta;

            List<ApostasAtuai> historico = _context.ApostasAtuais.Where(n => n.SorteioAtual == false).ToList();
            
            ApostasArquivada apostaArq = new ApostasArquivada();
            
            if (historico.Count == 0)
            {
                resposta = "Não existem apostas para arquivar!";
            }
            else
            {
                try
                {
                    foreach (var h in historico)
                    {
                        apostaArq.Nif = Convert.ToInt32(h.Nif);
                        apostaArq.DataAposta = Convert.ToDateTime(h.DataAposta);
                        apostaArq.Numeros = Convert.ToString(h.Numeros);
                        apostaArq.Estrelas = Convert.ToString(h.Estrelas);
                        _context.ApostasArquivadas.Add(apostaArq);
                        await _context.SaveChangesAsync();
                    }

                    foreach (var c in historico)
                    {
                        var ApostaAtual = _context.ApostasAtuais.SingleOrDefault(u => u.Nif == c.Nif && u.Numeros == c.Numeros && u.Estrelas == c.Estrelas && u.DataAposta == c.DataAposta);
                        if (ApostaAtual != null)
                        {
                            _context.ApostasAtuais.Remove(ApostaAtual);
                        }
                        await _context.SaveChangesAsync();
                    }


                    resposta = "As apostas foram arquivadas com sucesso!";
                }
                catch(Exception e)
                {
                    resposta = "Erro ao Arquivar!";
                }
            }

            return await Task.FromResult(new RespostaArquivar
            {
                Resposta = resposta.ToString()
            });
        }

        public override async Task ListaApostasVencedoras(teste Teste, IServerStreamWriter<ApostasVencedoras> responseStream, ServerCallContext context)
        {
            List<ApostasAtuai> historico = _context.ApostasAtuais.Where(n => n.SorteioAtual == true).ToList();
            List<ApostasVencedoras> apostaAtuais = new List<ApostasVencedoras>();
            ApostasVencedoras aposta1 = new ApostasVencedoras();

            foreach (var h in historico)
            {
                aposta1 = new ApostasVencedoras();
                aposta1.Nif = h.Nif;
                aposta1.Numeros = h.Numeros;
                aposta1.Estrelas = h.Estrelas;

                apostaAtuais.Add(aposta1);
            }

            foreach (var a in apostaAtuais)
            {
                await responseStream.WriteAsync(a);
            }
        }

        public override Task<RespostaSorteio> SetSorteioAtualFalse(teste Teste, ServerCallContext context)
        {

            List<ApostasAtuai> SorteiosFalse = _context.ApostasAtuais.Where(s => s.SorteioAtual == true).ToList();

            foreach (var s in SorteiosFalse)
            {
                var aposta = _context.ApostasAtuais.SingleOrDefault(u => u.Nif == s.Nif && u.Numeros == s.Numeros && u.Estrelas == s.Estrelas && u.DataAposta == s.DataAposta);
                if (aposta != null)
                {
                    aposta.SorteioAtual = false;
                    _context.Update(aposta);
                    _context.SaveChanges();
                }
            }

            return Task.FromResult(new RespostaSorteio
            {
                Resposta = "Dados foram atualizados, Sorteio Atual igual a false"
            });
        }

        public override Task<RespostaApostasFeitas> ApostasFeitas(teste Teste, ServerCallContext context)
        {
            bool resposta;

            int apostas = _context.ApostasAtuais.Where(n => n.SorteioAtual == true).Count();

            if(apostas > 0)
            {
                resposta = true;
            }
            else
            {
                resposta = false;
            }

            return Task.FromResult(new RespostaApostasFeitas
            {
                Resposta = Convert.ToBoolean(resposta)
            });
        }
    }
}
