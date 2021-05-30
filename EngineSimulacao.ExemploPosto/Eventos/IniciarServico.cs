using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : Evento
    {
        public IniciarServico() : base() { }

        public IniciarServico(Dictionary<string, object> parametros) : base(parametros)
        {
        }
    }
}
