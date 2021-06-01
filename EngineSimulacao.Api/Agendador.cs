using System;
using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public class Agendador<Memoria> where Memoria:new()
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private readonly PriorityQueue<Evento<Memoria>, int> _listaEventosFuturos = new();

        private readonly Dictionary<string, Recurso> _recursos = new();

        private readonly List<Entidade> _entidades = new();

        public int Tempo { get; private set; }

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
        
        private void AgendarEvento(Evento<Memoria> evento, int tempoSelecionado){
            _listaEventosFuturos.Enqueue(evento, tempoSelecionado);
        }

        public void AgendarAgora(Evento<Memoria> evento) { this.AgendarEvento(evento, Tempo); }

        public void AgendarEm(Evento<Memoria> evento, int tempoAdicionar) { this.AgendarEvento(evento, Tempo + tempoAdicionar); }

        public void AgendarComTempoAbsoluto(Evento<Memoria> evento, int tempoAbsoluto) { this.AgendarEvento(evento, tempoAbsoluto); }

        public void CriarRecurso(string chave, Recurso recurso)
        {
            // todo: Atribuir Id ao recurso
            _recursos.Add(chave, recurso);
        }

        public Recurso ObterRecurso(string chave) => _recursos[chave];

        public int CriarEntidade(string nome)
        {
            _entidades.Add(new Entidade
            {
                Nome = nome,
                TempoCriacao = Tempo
            });

            return _entidades.Count - 1;
        }

        public void DestruirEntidade(int id)
        {
            _entidades[id].TempoDestruicao = Tempo;
        }
    }
}
