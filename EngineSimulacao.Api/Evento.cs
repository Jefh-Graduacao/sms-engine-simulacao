using System;

namespace EngineSimulacao.Api
{
    public abstract class Evento:ITemID
    {
        public int Id { get; private set; }
        public Evento(){
            this.Id = Gerenciador<Evento>.gerarId();
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