namespace EngineSimulacao.Api
{
    public class Evento
    {
        public int Id { get; set; }

       //String que define o tipo do evento lançado, e quais seus argumentos, no formato: nomeEvento-arg1-arg2
        public string Argumentos { get; set; }
        Evento(string Argumentos){
            this.Argumentos = Argumentos;
        }
    }
}