namespace EngineSimulacao.Api
{
    public class AlocacaoGerenciada<T> : Gerenciado where T : RecursoGerenciado, new()
    {
        public T Recurso { get; }

        public AlocacaoGerenciada(T recurso)
        {
            this._nascerEmTodosOsNiveis();
            Recurso = recurso;
            Recurso.Alocado = true;
        }

        public void Desalocar()
        {
            this._morrerEmTodosOsNiveis();
            Recurso.Alocado = false;
        }
    }
}