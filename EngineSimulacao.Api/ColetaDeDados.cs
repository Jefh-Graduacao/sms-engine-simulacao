using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    static public class ColetaDeDados
    {
        static public List<HistoricoBase> listaDeHistoricos { get; private set; } = new();

        static public void NovoHistorico<T>(Historico<T> historico) where T:ITemID
        {
            listaDeHistoricos.Add(historico);
        }
    }
}