using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : Evento
    {
        private Carro _carro;
        public FinalizarServico(Carro carro)
        {
            this._carro = carro;
            Gerenciador<FinalizarServico>.nascimento(this);
        }
        protected override void Estrategia() {
            _carro.Destruir();
            
            var funcionarios = Agendador.ObterRecurso("funcionarios");
            funcionarios.Liberar(MotorPosto.FUNCIONARIOS_NECESSARIOS);

            if (MotorPosto.filaAtendimento.TamanhoAtual > 0)
            {
                var evtIniciar = new IniciarServico();
                Agendador.AgendarAgora(evtIniciar);
            }
        }
        protected override void Destruir()
        {
            Gerenciador<FinalizarServico>.morte(this);
        }
    }
}
