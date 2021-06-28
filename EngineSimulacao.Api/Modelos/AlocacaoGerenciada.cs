namespace EngineSimulacao.Api
{
    public interface IAlocacaoGerenciada<out T> where T : RecursoGerenciado, new()
    {
        T Recurso { get; }
        void Desalocar();
    }

    public class AlocacaoGerenciada<T> : Gerenciado, IAlocacaoGerenciada<T> where T : RecursoGerenciado, new()
    {
        public T Recurso { get; }

        public AlocacaoGerenciada(T recurso)
        {
            _nascerEmTodosOsNiveis();
            Recurso = recurso;
            Recurso.Alocado = true;
        }

        public void Desalocar()
        {
            _morrerEmTodosOsNiveis();
            Recurso.Alocado = false;
        }
    }
}