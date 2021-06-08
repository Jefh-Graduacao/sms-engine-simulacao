namespace EngineSimulacao.Api
{
    public abstract class Entidade : ITemId
    {
        public int Id { get; }

        protected Entidade()
        { 
            Id = Gerenciador<Entidade>.GerarId();
            Gerenciador<Entidade>.nascimento(this);
        }

        public void Destruir()
        {
            Gerenciador<Entidade>.morte(this);
            DestruirInstancia();
        }

        protected abstract void DestruirInstancia();
    }
}