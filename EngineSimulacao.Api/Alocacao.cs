namespace EngineSimulacao.Api
{
    public class Alocacao<T> : ITemId where T : Recurso, new()
    {
        public int Id { get; }
        public T Recurso { get; }

        public Alocacao(T recurso)
        {
            Id = Gerenciador<Alocacao<T>>.GerarId();
            Gerenciador<Alocacao<T>>.nascimento(this);
            Recurso = recurso;
            Recurso.Alocado = true;
        }

        public void Desalocar()
        {
            Gerenciador<Alocacao<T>>.morte(this);
            Recurso.Alocado = false;
        }
    }
}