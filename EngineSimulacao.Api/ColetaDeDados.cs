using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class ColetaDeDados
    {
        public static List<HistoricoBase> listaDeHistoricos { get; private set; } = new();

        public static void NovoHistorico<T>(Historico<T> historico) where T : Gerenciado
        {
            listaDeHistoricos.Add(historico);
        }
    }
}