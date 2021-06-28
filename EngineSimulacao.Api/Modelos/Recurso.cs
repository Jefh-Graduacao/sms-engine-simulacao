namespace EngineSimulacao.Api
{
    public class RecursoGerenciado : Gerenciado
    {
        public bool Alocado { get; set; } = false;

        public RecursoGerenciado()
        {
            this._nascerEmTodosOsNiveis();
        }
    }
}
