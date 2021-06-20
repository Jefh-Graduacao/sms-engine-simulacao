using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class ColetaDeDados
    {
        public static List<HistoricoBase> ListaDeHistoricos { get; } = new();

        public static void NovoHistorico<T>(Historico<T> historico) where T : Gerenciado
        {
            ListaDeHistoricos.Add(historico);
        }
    }
}