using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : Evento
    {
        public FinalizarServico() : base() { }

        public FinalizarServico(Dictionary<string, object> parametros) : base(parametros)
        {
        }
    }
}
