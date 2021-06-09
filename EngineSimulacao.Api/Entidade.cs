namespace EngineSimulacao.Api
{
    public abstract class Entidade : Gerenciado
    {
        protected Entidade()
        { 
            this._nascerEmTodosOsNiveis();
        }

        public void Destruir()
        {
            this._morrerEmTodosOsNiveis();
        }
    }
}