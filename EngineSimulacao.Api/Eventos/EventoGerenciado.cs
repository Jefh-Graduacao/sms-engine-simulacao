namespace EngineSimulacao.Api
{
    public abstract class EventoGerenciado : Gerenciado
    {
        protected EventoGerenciado()
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