namespace EngineSimulacao.Api
{
    public class ConjuntoEntidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modo { get; set; }
        public int Tamanho { get; set; }
        public int TamanhoMáximoPossível { get; set; }
        //Public Set conjuntoDeEntidades;

        ConjuntoEntidade(string Nome){
            this.Nome = Nome;
        }
    }
}