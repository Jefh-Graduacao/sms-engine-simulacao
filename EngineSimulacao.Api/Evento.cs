using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public abstract class Evento
    {
        public int Id { get; set; }
        public int Tempo { get; set; }
        public Dictionary<string, object> Parametros = new();

        protected Evento() { }

        protected Evento(Dictionary<string, object> parametros)
        {
            Parametros = parametros;
        }
    }
}