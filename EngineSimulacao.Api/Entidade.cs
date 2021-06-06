namespace EngineSimulacao.Api
{
    public abstract class Entidade:ITemID
    {
        public int Id { get; private set; }
        public Entidade()
        { 
            this.Id = Gerenciador<Entidade>.gerarId();
            Gerenciador<Entidade>.nascimento(this);
        }
        public void Destruir()
        {
            Gerenciador<Entidade>.morte(this);
            this.DestruirIntancia();
        }
        protected abstract void DestruirIntancia();
    }
}