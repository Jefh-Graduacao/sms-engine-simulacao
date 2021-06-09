namespace EngineSimulacao.Api
{
    public class Alocacao<T> : Gerenciado where T : Recurso, new()
    {
        public T Recurso { get; }

        public Alocacao(T recurso)
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