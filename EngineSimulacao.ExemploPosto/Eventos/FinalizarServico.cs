using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : Evento<ConjuntosPosto>
    {
        private Entidade carro;
        public FinalizarServico(MotorExecucao<ConjuntosPosto> motor): base(motor){ }
        public void setParams(Entidade carro){ this.carro = carro; }
        public override void Executar() {
            this.motor.Agendador.DestruirEntidade(carro);

            var funcionarios = this.motor.Agendador.ObterRecurso("funcionarios");
            funcionarios.Liberar(CONFIG.FUNCIONARIOS_NECESSARIOS);

            ConjuntoEntidade<ConjuntosPosto> filaAtendimento = this.motor.PegarConjunto(ConjuntosPosto.filaAtendimento);

            if (filaAtendimento.TamanhoAtual > 0)
            {
                var evtIniciar = this.motor.criarEvento<IniciarServico>();
                this.motor.Agendador.AgendarAgora(evtIniciar);
            }
        }
    }
}
