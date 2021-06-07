namespace EngineSimulacao.Api
{
    public abstract class Evento:TemID
    {
        public Evento(){
            Gerenciador<Evento>.nascimento(this);
        }
        public void Executar()
        {
            this.Estrategia();
            Gerenciador<Evento>.morte(this);
            this.Destruir();
        }
        protected abstract void Estrategia();
        protected abstract void Destruir();
    }
}