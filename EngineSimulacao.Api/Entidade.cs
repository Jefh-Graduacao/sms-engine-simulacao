namespace EngineSimulacao.Api
{
    public abstract class Entidade:TemID
    {
        public Entidade()
        { 
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