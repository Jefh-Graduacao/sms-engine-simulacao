using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.Api
{
    public class Agendador
    {
        /// <summary>
        /// FEL - Future Event List
        /// </summary>
        private readonly List<Evento> _listaEventosFuturos = new();

        private readonly Dictionary<string, Recurso> _recursos = new();

        public int Tempo { get; }
        public IMotorExecucao MotorExecucao { get; set; }
        
        public void SimularUmaExecucao()
        {
            var evento = _listaEventosFuturos.First();
            _listaEventosFuturos.RemoveAt(0);

            MotorExecucao.Executar(evento);
        }

        public void AgendarAgora(Evento evento)
        {
            _listaEventosFuturos.Insert(0, evento);
        }

        public void AgendarNoFinal(Evento evento)
        {
            _listaEventosFuturos.Add(evento);
        }

        public void CriarRecurso(string chave, Recurso recurso)
        {
            // todo: Atribuir Id ao recurso
            _recursos.Add(chave, recurso);
        }

        public Recurso ObterRecurso(string chave) => _recursos[chave];
    }
}
