namespace EngineSimulacao.Api
{
    public class Entidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal TempoCriação { get; set; }
        public int Prioridade { get; set; }
        public object RedePetri { get; set; } // TODO: Substituir
    }
}