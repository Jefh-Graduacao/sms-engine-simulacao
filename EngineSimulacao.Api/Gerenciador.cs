using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class Gerenciador<T> where T : ITemId
    {
        private static int _contadorId;
        private static readonly Historico<T> Historico = new();

        public static int GerarId() => _contadorId++;

        public static void nascimento(T instancia)
        {
            Historico.nascimento(instancia);
        }

        public static void morte(T instancia)
        {
            Historico.morte(instancia);
        }

        public static List<InfoInstancia<T>> listarVivos()
        {
            return Historico.listarVivos();
        }

        public static List<InfoInstancia<T>> listarMortos()
        {
            return Historico.listarMortos();
        }
    }
}