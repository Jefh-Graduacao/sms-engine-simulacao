namespace EngineSimulacao.Api
{
    public abstract class Evento : ITemId
    {
        public int Id { get; private set; }

        protected Evento()
        {
            Id = Gerenciador<Evento>.GerarId();
            Gerenciador<Evento>.nascimento(this);
        }

        public void Executar()
        {
            Estrategia();
            Gerenciador<Evento>.morte(this);
            Destruir();
        }

        protected abstract void Estrategia();
        protected abstract void Destruir();
    }
}