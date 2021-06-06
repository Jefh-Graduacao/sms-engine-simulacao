using System;

namespace EngineSimulacao.Api
{
    public interface ITemID { 
        int Id { get; }
    }
    public class InfoInstancia<T> where T:ITemID
    {
        public T Instancia { set; get; }
        public bool Vivo { set; get; }
        public int Prioridade { set; get; }
        public int TempoCriacao { set; get; }
        public int TempoDestruicao { set; get; } = Int32.MinValue;
        public int TempoDeVida => Vivo ? Agendador.Tempo - TempoCriacao : TempoDestruicao - TempoCriacao;
    }
}