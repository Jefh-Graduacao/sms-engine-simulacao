namespace EngineSimulacao.Api
{
    public class Recurso : Gerenciado
    {
        public bool Alocado { get; set; } = false;

        public Recurso()
        {
            this._nascerEmTodosOsNiveis();
        }
    }
}
