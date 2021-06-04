using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Agendador<EnumConjuntos> where EnumConjuntos:struct, Enum
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private readonly PriorityQueue<Evento<EnumConjuntos>, int> _listaEventosFuturos = new();
        private readonly Dictionary<string, Recurso> _recursos = new();
        private readonly List<Entidade> _entidades = new();

        public int Tempo { get; private set; }
        public Agendador(){ }
        
        public void SimularUmaExecucao()
        {
            _listaEventosFuturos.TryDequeue(out var evento, out var prioridade);

            Tempo = prioridade;
            evento.Executar();
        }

        public void Simular()
        {
            while (_listaEventosFuturos.TryDequeue(out var evento, out var prioridade))
            {
                Tempo = prioridade;
                evento.Executar();
            }
        }
        
        private void AgendarEvento(Evento<EnumConjuntos> evento, int tempoSelecionado){
            _listaEventosFuturos.Enqueue(evento, tempoSelecionado);
        }

        public void AgendarAgora(Evento<EnumConjuntos> evento) { this.AgendarEvento(evento, Tempo); }

        public void AgendarEm(Evento<EnumConjuntos> evento, int tempoAdicionar) { this.AgendarEvento(evento, Tempo + tempoAdicionar); }

        public void AgendarComTempoAbsoluto(Evento<EnumConjuntos> evento, int tempoAbsoluto) { this.AgendarEvento(evento, tempoAbsoluto); }

        public void CriarRecurso(string chave, Recurso recurso)
        {
            // todo: Atribuir Id ao recurso
            _recursos.Add(chave, recurso);
        }

        public Recurso ObterRecurso(string chave) => _recursos[chave];

        public Entidade CriarEntidade()
        {
            var entidade = new Entidade(_entidades.Count);
            _entidades.Add(entidade);
            return entidade;
        }

        public void DestruirEntidade(Entidade carro){ }
    }
}
