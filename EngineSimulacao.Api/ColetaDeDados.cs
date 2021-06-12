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

        public static int menorTempoDeVida(List<Passagem> lista)
        {
            if (lista.Count == 0) return 0;
            int menor = lista[0].TempoDeVida;
            foreach (var Info in lista)
            {
                if (Info.TempoDeVida < menor) menor = Info.TempoDeVida;
            }
            return menor;
        }

        public static double tempoMedioDeVida(List<Passagem> lista)
        {
            if (lista.Count == 0) return 0;
            double soma = 0;
            foreach (var Info in lista)
            {
                soma += Info.TempoDeVida;
            }
            return soma / lista.Count;
        }

        public static int maiorTempoDeVida(List<Passagem> lista)
        {
            if (lista.Count == 0) return 0;
            int maior = lista[0].TempoDeVida;
            foreach (var Info in lista)
            {
                if (Info.TempoDeVida > maior) maior = Info.TempoDeVida;
            }
            return maior;
        }
    }
}