using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    /// <summary>
    /// Guarda informações de tempo de uma passagem única, e se esta já terminou (atributo Vivo)
    /// </summary>
    public class Passagem {
        public bool Vivo { private set; get; }
        public int TempoCriacao { private set; get; }
        public int TempoDestruicao { private set; get; } = int.MinValue;
        public int TempoDeVida => Vivo ? Agendador.Tempo - TempoCriacao : TempoDestruicao - TempoCriacao;

        public Passagem()
        {
            Vivo = true;
            TempoCriacao = Agendador.Tempo;
        }
        public void Morrer()
        {
            Vivo = false;
            TempoDestruicao = Agendador.Tempo;
        }
    }
    
    /// <summary>
    /// Guarda informações de todas as passagens de uma determinada Instancia em um determinado histórico
    /// </summary>
    public class InfoInstancia<T> : HistoricoBase where T : notnull, Gerenciado
    {
        public T Instancia { private set; get; }
        private readonly Historico<T> _historico;
        public readonly Stack<Passagem> pilhaDePassagens = new();        
        public bool Vivo => pilhaDePassagens.Count > 0 ? pilhaDePassagens.Peek().Vivo : false;
        public InfoInstancia(T instancia, Historico<T> historico)
        {
            Instancia = instancia;
            _historico = historico;
        }

        public void pilhaViver()
        {
            validarViver();
            pilhaDePassagens.Push(new Passagem());
        }

        public void pilhaMorrer()
        {
            validarMorrer();
            var ultimaPassagem = pilhaDePassagens.Peek();
            ultimaPassagem.Morrer();
        }

        public override int menorTempoDeVida()
        {
            return ColetaDeDados.menorTempoDeVida(new List<Passagem>(pilhaDePassagens));
        }

        public override double tempoMedioDeVida()
        {
            return ColetaDeDados.tempoMedioDeVida(new List<Passagem>(pilhaDePassagens));
        }

        public override int maiorTempoDeVida()
        {
            return ColetaDeDados.maiorTempoDeVida(new List<Passagem>(pilhaDePassagens));
        }
        private void validarViver(){
            if(pilhaDePassagens.Count == 0) return;//Primeira passagem

            var ultimaPassagem = pilhaDePassagens.Peek();
            if(ultimaPassagem.Vivo) throw new Exception("Instancia " + Instancia + " já se encontra viva no histórico " + _historico.nome);
        }
        private void validarMorrer(){
            if(pilhaDePassagens.Count == 0) {
                throw new Exception("Nenhuma passagem da Instancia " + Instancia + " registrada no histórico " + _historico.nome);
            }

            var ultimaPassagem = pilhaDePassagens.Peek();
            if(false == ultimaPassagem.Vivo){
                throw new Exception("Instancia " + Instancia + " já se encontra morta no histórico " + _historico.nome);
            }
        }
    }
}