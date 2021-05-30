using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Agendador
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private readonly PriorityQueue<Evento, int> _listaEventosFuturos = new();

        private readonly Dictionary<string, Recurso> _recursos = new();

        public int Tempo { get; private set; }
        public IMotorExecucao MotorExecucao { get; set; }

        public void SimularUmaExecucao()
        {
            _listaEventosFuturos.TryDequeue(out var evento, out var prioridade);

            Tempo = prioridade;
            MotorExecucao.Executar(evento);
        }

        public void Simular()
        {
            while (_listaEventosFuturos.TryDequeue(out var evento, out var prioridade))
            {
                Tempo = prioridade;
                MotorExecucao.Executar(evento);
            }
        }

        public void AgendarAgora(Evento evento)
        {
            _listaEventosFuturos.Enqueue(evento, Tempo);
        }

        public void AgendarEm(Evento evento, int tempoAdicionar)
        {
            _listaEventosFuturos.Enqueue(evento, Tempo + tempoAdicionar);
        }

        public void AgendarComTempoAbsoluto(Evento evento, int tempoAbsoluto)
        {
            _listaEventosFuturos.Enqueue(evento, tempoAbsoluto);
        }

        public void CriarRecurso(string chave, Recurso recurso)
        {
            // todo: Atribuir Id ao recurso
            _recursos.Add(chave, recurso);
        }

        public Recurso ObterRecurso(string chave) => _recursos[chave];
    }
}
