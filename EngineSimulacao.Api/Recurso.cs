namespace EngineSimulacao.Api
{
    public class Recurso : ITemId
    {
        public int Id { get; private set; }
        public bool Alocado { get; set; } = false;

        public Recurso()
        {
            Id = Gerenciador<Recurso>.GerarId();
            Gerenciador<Recurso>.nascimento(this);
        }
    }
}
