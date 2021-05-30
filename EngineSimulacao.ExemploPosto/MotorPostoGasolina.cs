using System;
using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Eventos;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace EngineSimulacao.ExemploPosto
{
    public sealed class MotorPostoGasolina : IMotorExecucao
    {
        private readonly Queue<object> _filaAtendimento = new();

        public Agendador Agendador { get; set; }
        
        public void Executar(Evento evento)
        {
            var args = evento.Argumentos.Split("-");

            var r = evento switch
            {
                ChegadaCarros _ => FnChegadaCarros(args[1..]),
                IniciarServico _ => FnIniciarServico(args[1..]),
                FinalizarServico _ => FnFinalizarServico(args[1..]),
                _ => throw new Exception("")
            };
        }

        public bool FnChegadaCarros(params string[] args)
        {
            var carro = new object();

            var funcionarios = Agendador.ObterRecurso("funcionarios");

            if (funcionarios.VerificarDisponibilidade(1))
            {
                // todo:
                Agendador.AgendarAgora(new IniciarServico(""));
            }
            else
            {
                _filaAtendimento.Enqueue(carro);
            }

            // todo: finalizar 

            return true;
        }

        public bool FnIniciarServico(params string[] args)
        {
            var recurso = Agendador.ObterRecurso("funcionarios");
            recurso.TentarAlocar(1);

            Agendador.AgendarNoFinal(new FinalizarServico(""));
            //Todo: 12 segundos

            return true;
        }

        public bool FnFinalizarServico(params string[] args)
        {
            var recurso = Agendador.ObterRecurso("funcionarios");
            recurso.Liberar(1);

            if (_filaAtendimento.Any())
            {
                _filaAtendimento.Dequeue();
                Agendador.AgendarAgora(new IniciarServico(""));
            }

            return true;
        }
    }
}
