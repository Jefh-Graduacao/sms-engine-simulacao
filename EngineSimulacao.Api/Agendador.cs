using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    static public class Agendador
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        static private readonly PriorityQueue<Evento, int> _listaEventosFuturos = new();
        static private readonly Dictionary<string, Recurso> _recursos = new();
        static public int Tempo { get; private set; }        
        static public void SimularUmaExecucao()
        {
            _listaEventosFuturos.TryDequeue(out var evento, out var prioridade);

            Tempo = prioridade;
            evento.Executar();
        }

        static public void Simular()
        {
            while(_listaEventosFuturos.TryDequeue(out var evento, out var prioridade))
            {
                Tempo = prioridade;
                evento.Executar();
            }
        }
        
        static private void AgendarEvento(Evento evento, int tempoSelecionado){
            _listaEventosFuturos.Enqueue(evento, tempoSelecionado);
        }

        static public void AgendarAgora(Evento evento) { AgendarEvento(evento, Tempo); }

        static public void AgendarEm(Evento evento, int tempoAdicionar) { AgendarEvento(evento, Tempo + tempoAdicionar); }

        static public void AgendarComTempoAbsoluto(Evento evento, int tempoAbsoluto) { AgendarEvento(evento, tempoAbsoluto); }

        static public void CriarRecurso(string chave, Recurso recurso)
        {
            // todo: Atribuir Id ao recurso
            _recursos.Add(chave, recurso);
        }

        static public Recurso ObterRecurso(string chave) => _recursos[chave];
    }
}
