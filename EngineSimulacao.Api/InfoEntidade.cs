namespace EngineSimulacao.Api
{
    /// <summary>
    /// Guarda informações do ciclo de vida de uma entidade dentro de um conjunto específico
    /// </summary>
    public class InfoEntidade
    {
        public bool NoConjunto = true;
        public int TempoEntrada;
        public int TempoSaida;
        public int Prioridade;
        public Entidade Entidade;

        public InfoEntidade(Entidade Entidade, int TempoEntrada){
            this.Entidade = Entidade;
            this.TempoEntrada = TempoEntrada;
            this.Prioridade = -1;
        }
        public InfoEntidade(Entidade Entidade, int TempoEntrada, int Prioridade){
            this.Entidade = Entidade;
            this.TempoEntrada = TempoEntrada;
            this.Prioridade = Prioridade;
        }
    }
}