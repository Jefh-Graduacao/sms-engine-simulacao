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

        public List<InfoInstancia<T>> lista { get; private set; } = new();

        public void nascimento(T instancia)
        {
            var info = new InfoInstancia<T>(instancia);
            info.Viver();
            lista.Add(info);
        }

        public void morte(T instanciaMorta)
        {
            var infoInstanciaDestruida = lista.Find(info => info.Instancia.Id == instanciaMorta.Id);
            infoInstanciaDestruida.Morrer();
        }

        public List<InfoInstancia<T>> listarVivos()
        {
            return lista.FindAll(Info => Info.Vivo);
        }

        public List<InfoInstancia<T>> listarMortos()
        {
            return lista.FindAll(Info => false == Info.Vivo);
        }

        public override int menorTempoDeVida()
        {
            if (lista.Count == 0) return 0;
            int menor = lista[0].TempoDeVida;
            foreach (var Info in lista)
            {
                if (Info.TempoDeVida < menor) menor = Info.TempoDeVida;
            }
            return menor;
        }

        public override double tempoMedioDeVida()
        {
            if (lista.Count == 0) return 0;
            double soma = 0;
            foreach (var Info in lista)
            {
                soma += Info.TempoDeVida;
            }
            return soma / lista.Count;
        }

        public override int maiorTempoDeVida()
        {
            if (lista.Count == 0) return 0;
            int maior = lista[0].TempoDeVida;
            foreach (var Info in lista)
            {
                if (Info.TempoDeVida > maior) maior = Info.TempoDeVida;
            }
            return maior;
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