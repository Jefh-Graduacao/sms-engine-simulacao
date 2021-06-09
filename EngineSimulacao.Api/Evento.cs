namespace EngineSimulacao.Api
{
    public abstract class Evento : Gerenciado
    {
        protected Evento()
        {
            this._nascerEmTodosOsNiveis();
        }

        public void Executar()
        {
            Estrategia();
            this._morrerEmTodosOsNiveis();
        }

        protected abstract void Estrategia();
    }
}