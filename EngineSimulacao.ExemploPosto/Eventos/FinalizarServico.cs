using EngineSimulacao.Api;
using EngineSimulacao.ExemploPosto.Entidades;
using EngineSimulacao.ExemploPosto.Recursos;
using System.Collections.Generic;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : EventoGerenciado
    {
        private readonly Carro _carro;
        private readonly IEnumerable<IAlocacaoGerenciada<Funcionario>> _funcionariosAlocados;

        public FinalizarServico(Carro carro, IEnumerable<IAlocacaoGerenciada<Funcionario>> funcionariosAlocados)
        {
            _carro = carro;
            _funcionariosAlocados = funcionariosAlocados;
        }

        protected override void Estrategia() 
        {
            _carro.Destruir();
            
            GerenciadorDeRecursos<Funcionario>.Liberar(_funcionariosAlocados);

            if (MotorPosto.FilaAtendimento.TamanhoAtual > 0)
            {
                var evtIniciar = new IniciarServico();
                Agendador.AgendarAgora(evtIniciar);
            }
        }
    }
}
