using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ChegadaCarros : Evento
    {
        public ChegadaCarros() : base() { }

        public ChegadaCarros(Dictionary<string, object> parametros) : base(parametros)
        {
        }
    }
}
