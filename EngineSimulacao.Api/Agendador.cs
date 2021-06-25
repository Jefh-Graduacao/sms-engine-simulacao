using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public static class Agendador
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private static readonly PriorityQueue<EventoGerenciado, double> _listaEventosFuturos = new();
        public static double Tempo { get; private set; }

        public static void SimularUmaExecucao(Action callback = null)
        {
            _listaEventosFuturos.TryDequeue(out var evento, out var prioridade);

            Tempo = prioridade;
            evento.Executar();
            callback?.Invoke();
        }

        public static void Simular(Action callback = null)
        {
            while (_listaEventosFuturos.TryDequeue(out var evento, out var prioridade))
            {
                Tempo = prioridade;
                evento.Executar();
                callback?.Invoke();
            }
        }
        public static void SimularPorDeterminadoTempo(double tempo, Action callback = null)
        {
            while (_listaEventosFuturos.TryDequeue(out var evento, out var prioridade) && prioridade < tempo)
            {
                Tempo = prioridade;
                evento.Executar();
                callback?.Invoke();
            }
        }

        private static void AgendarEvento(EventoGerenciado evento, double tempoSelecionado)
        {
            _listaEventosFuturos.Enqueue(evento, tempoSelecionado);
        }

        public static void AgendarAgora(EventoGerenciado evento)
        {
            AgendarEvento(evento, Tempo);
        }

        public static void AgendarEm(EventoGerenciado evento, double tempoAdicionar)
        {
            AgendarEvento(evento, Tempo + tempoAdicionar);
        }

        public static void AgendarComTempoAbsoluto(EventoGerenciado evento, double tempoAbsoluto)
        {
            AgendarEvento(evento, tempoAbsoluto);
        }
    }
}
