using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : Evento
    {
        private Carro _carro;
        private List<Alocacao<Recurso>> _funcionariosAlocados;
        public FinalizarServico(Carro carro, List<Alocacao<Recurso>> funcionariosAlocados)
        {
            this._carro = carro;
            this._funcionariosAlocados = funcionariosAlocados;
            Gerenciador<FinalizarServico>.nascimento(this);
        }
        protected override void Estrategia() {
            _carro.Destruir();
            
            ConjuntoRecurso<Recurso>.Liberar(this._funcionariosAlocados);

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
