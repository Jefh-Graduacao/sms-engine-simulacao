namespace EngineSimulacao.Api
{
    public abstract class EntidadeGerenciada : Gerenciado
    {
        protected EntidadeGerenciada()
        {
            this._nascerEmTodosOsNiveis();
        }

        public void Destruir()
        {
            this._morrerEmTodosOsNiveis();
        }
    }
}