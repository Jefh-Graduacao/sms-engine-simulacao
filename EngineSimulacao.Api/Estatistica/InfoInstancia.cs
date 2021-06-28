namespace EngineSimulacao.Api
{
    public class InfoInstancia<T>
    {
        public T Instancia { set; get; }
        public bool Vivo { set; get; }
        public int Prioridade { set; get; }
        public double TempoCriacao { set; get; }
        public double TempoDestruicao { set; get; } = int.MinValue;
        public double TempoDeVida => Vivo ? Agendador.Tempo - TempoCriacao : TempoDestruicao - TempoCriacao;

        public InfoInstancia(T instancia)
        {
            Instancia = instancia;
        }

        public void Viver()
        {
            Vivo = true;
            TempoCriacao = Agendador.Tempo;
            Prioridade = int.MinValue;
        }

        public void Morrer()
        {
            TempoDestruicao = Agendador.Tempo;
            Vivo = false;
        }
    }
}