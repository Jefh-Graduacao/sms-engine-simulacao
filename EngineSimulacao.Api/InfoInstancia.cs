using System;

namespace EngineSimulacao.Api
{
    public abstract class TemID { 
        public int Id { get; private set; }

        public TemID(){
            this.Id = Gerenciador<TemID>.gerarId();
        }
    }

    public class InfoInstancia<T>
    {
        public T Instancia { set; get; }
        public bool Vivo { set; get; }
        public int Prioridade { set; get; }
        public int TempoCriacao { set; get; }
        public int TempoDestruicao { set; get; } = Int32.MinValue;
        public int TempoDeVida => Vivo ? Agendador.Tempo - TempoCriacao : TempoDestruicao - TempoCriacao;

        public InfoInstancia(T instancia)
        {
            this.Instancia = instancia;
        }
        public void viver()
        {
            this.Vivo = true;
            this.TempoCriacao = Agendador.Tempo;
            this.Prioridade = Int32.MinValue;
        }
        public void morrer(){
            this.TempoDestruicao = Agendador.Tempo;
            this.Vivo = false;
        }
    }
}