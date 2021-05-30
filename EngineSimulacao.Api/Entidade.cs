namespace EngineSimulacao.Api
{
    public sealed class Entidade
    {
        public string Nome { get; set; }
        public int TempoCriacao { get; set; }
        public int? TempoDestruicao { get; set; }
        public int Prioridade { get; set; }
        public object RedePetri { get; set; } // TODO: Substituir
    }
}