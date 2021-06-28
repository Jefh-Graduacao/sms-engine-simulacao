using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.Api
{
    public static class ColetaDeDados
    {
        public static List<HistoricoBase> ListaDeHistoricos { get; } = new();

        public static void NovoHistorico<T>(Historico<T> historico) where T : Gerenciado
        {
            ListaDeHistoricos.Add(historico);
        }

        public static HistoricoBase GetHistoricoBase( string nome) 
            => ListaDeHistoricos.FirstOrDefault(hisBase => hisBase.Nome.Equals(nome));
    }
}