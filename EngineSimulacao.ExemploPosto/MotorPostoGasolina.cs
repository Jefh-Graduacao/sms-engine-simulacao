using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using System.Collections.Generic;
using System.Linq;

namespace EngineSimulacao.ExemploPosto
{
    public sealed class MotorPostoGasolina : IMotorExecucao
    {
        private readonly Queue<int> _filaAtendimento = new();

        public Agendador Agendador { get; set; }

        public void Executar(Evento evento)
        {
            var r = evento switch
            {
                ChegadaCarros _ => FnChegadaCarros(evento.Parametros),
                IniciarServico _ => FnIniciarServico(evento.Parametros),
                FinalizarServico _ => FnFinalizarServico(evento.Parametros),
                _ => throw new Exception("")
            };
        }

        public bool FnChegadaCarros(Dictionary<string, object> parametros)
        {
            var idCarro = Agendador.CriarEntidade("Carro");

            var funcionarios = Agendador.ObterRecurso("funcionarios");

            if (funcionarios.VerificarDisponibilidade(1))
            {
                var evento = new IniciarServico(new Dictionary<string, object>
                {
                    ["idCarro"] = idCarro
                });
            }
            else
            {
                _filaAtendimento.Enqueue(idCarro);
            }

            if (Agendador.Tempo < 100)
            {
                Agendador.AgendarEm(new ChegadaCarros(), 5);
            }

            return true;
        }

        public bool FnIniciarServico(Dictionary<string, object> parametros)
        {
            var recurso = Agendador.ObterRecurso("funcionarios");
            recurso.TentarAlocar(1);

            Agendador.AgendarEm(new FinalizarServico(parametros), 12);

            return true;
        }

        public bool FnFinalizarServico(Dictionary<string, object> parametros)
        {
            var idCarro = Convert.ToInt32(parametros["idCarro"]);
            Agendador.DestruirEntidade(idCarro);

            var recurso = Agendador.ObterRecurso("funcionarios");
            recurso.Liberar(1);

            if (_filaAtendimento.Any())
            {
                Agendador.AgendarAgora(new IniciarServico(new Dictionary<string, object>
                {
                    ["idCarro"] = _filaAtendimento.Dequeue()
                }));
            }

            return true;
        }
    }
}
