using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public abstract class HistoricoBase
    {
        public string nome { get; protected set; }
        public abstract int menorTempoDeVida();
        public abstract double tempoMedioDeVida();
        public abstract int maiorTempoDeVida();
    }

    public class Historico<T> : HistoricoBase where T : notnull, Gerenciado
    {
        public Historico()
        {
            init(gerarNomeHistorico());
        }

        public Historico(string nome)
        {
            init(nome);
        }
        
        /// <summary>
        /// Aponta qual o maior número de vezes que uma instância pode passar pelo histórico
        /// </summary>
        private int maiorNumeroDePassagensPermitidas = int.MaxValue;
        public List<InfoInstancia<T>> lista { get; private set; } = new();

        public void nascimento(T instancia)
        {
            var reviveu = reviver(instancia);

            if(false == reviveu){
                var info = new InfoInstancia<T>(instancia, this);
                info.pilhaViver();
                lista.Add(info);
            }
        }
        private bool reviver(T instancia)
        {
            InfoInstancia<T> infoExistente = lista.Find(info => info.Instancia.Id == instancia.Id);
            if(infoExistente != null){
                if(infoExistente.pilhaDePassagens.Count >= maiorNumeroDePassagensPermitidas){
                    throw new Exception("Instancia " + instancia + " já utilizou sua quantidade de passagens permitidas no histórico " + nome);
                }
                infoExistente.pilhaViver();
                return true;
            }
            return false;
        }

        public void morte(T instanciaMorta)
        {
            var infoInstanciaDestruida = lista.Find(info => info.Instancia.Id == instanciaMorta.Id);
            infoInstanciaDestruida.pilhaMorrer();
        }

        public List<InfoInstancia<T>> listarVivos()
        {
            return lista.FindAll(Info => Info.Vivo);
        }

        public List<InfoInstancia<T>> listarMortos()
        {
            return lista.FindAll(Info => false == Info.Vivo);
        }
        private List<Passagem> pegarTodasPassagens()
        {
            List<Passagem> todasPassagens = new();
            foreach (var Info in lista)
            {
                todasPassagens.AddRange(Info.pilhaDePassagens);
            }
            return todasPassagens;
        }
        public override int menorTempoDeVida()
        {
            List<Passagem> todasPassagens = this.pegarTodasPassagens();
            return ColetaDeDados.menorTempoDeVida(todasPassagens);
        }

        public override double tempoMedioDeVida()
        {
            List<Passagem> todasPassagens = this.pegarTodasPassagens();
            return ColetaDeDados.tempoMedioDeVida(todasPassagens);
        }

        public override int maiorTempoDeVida()
        {
            List<Passagem> todasPassagens = this.pegarTodasPassagens();
            return ColetaDeDados.maiorTempoDeVida(todasPassagens);
        }
        private void init(string nome)
        {
            this.nome = "Histórico " + nome;
            ColetaDeDados.NovoHistorico<T>(this);
        }
        private string pegarNomeSemGenericos()
        {
            var tipo = typeof(T);
            var tipoBase = tipo.BaseType;

            var nomeClasse = tipo.Name;
            var nomeClasseBase = tipoBase.Name;

            if (nomeClasseBase != "Object" && nomeClasseBase != Gerenciado.NomeTipo)
                return nomeClasseBase + " " + nomeClasse;
            else
                return nomeClasse;
        }
        private string pegarTiposGenericos()
        {
            var tipo = typeof(T);
            var argumentosGenericos = tipo.GetGenericArguments();
            if (argumentosGenericos.Length == 0)
                return "";
            string saida = "<";
            for (int i = 0; i < argumentosGenericos.Length; i++)
            {
                var tipoArgumento = (Type)argumentosGenericos.GetValue(i);
                saida = saida + " " + tipoArgumento.Name;
            }
            saida = saida + " >";
            return saida;
        }
        private string gerarNomeHistorico()
        {
            return pegarNomeSemGenericos() + pegarTiposGenericos();
        }
    }
}