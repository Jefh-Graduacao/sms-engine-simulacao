namespace EngineSimulacao.Api
{
    public sealed class Entidade
    {
        public int Id { private set; get; }
        public Entidade(int Id){
            this.Id = Id;
        }
    }
}