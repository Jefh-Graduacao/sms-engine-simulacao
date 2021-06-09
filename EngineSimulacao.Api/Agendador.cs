using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class Agendador
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private static readonly PriorityQueue<EventoGerenciado, int> _listaEventosFuturos = new();
        public static int Tempo { get; private set; }

        public static void SimularUmaExecucao()
        {
            _listaEventosFuturos.TryDequeue(out var evento, out var prioridade);

            Tempo = prioridade;
            evento.Executar();
        }

        public static void Simular()
        {
            while (_listaEventosFuturos.TryDequeue(out var evento, out var prioridade))
            {
                Tempo = prioridade;
                evento.Executar();
            }
        }

        private static void AgendarEvento(EventoGerenciado evento, int tempoSelecionado)
        {
            _listaEventosFuturos.Enqueue(evento, tempoSelecionado);
        }

        public static void AgendarAgora(EventoGerenciado evento)
        {
            AgendarEvento(evento, Tempo);
        }

        public static void AgendarEm(EventoGerenciado evento, int tempoAdicionar)
        {
            AgendarEvento(evento, Tempo + tempoAdicionar);
        }

        public static void AgendarComTempoAbsoluto(EventoGerenciado evento, int tempoAbsoluto)
        {
            AgendarEvento(evento, tempoAbsoluto);
        }
    }
}
