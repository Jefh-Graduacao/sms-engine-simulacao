using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : Evento<MemoriaPostoGasolina>
    {
        private int idCarro;
        public FinalizarServico(MotorExecucao<MemoriaPostoGasolina> motor): base(motor){ }
        public void setParams(int idCarro){ this.idCarro = idCarro; }
        public override void Executar() {
            this.motor.Agendador.DestruirEntidade(this.idCarro);

            var recurso = this.motor.Agendador.ObterRecurso("funcionarios");
            recurso.Liberar(1);

            if (this.motor.memoria.filaAtendimento.Count > 0)
            {
                int idCarroFila = this.motor.memoria.filaAtendimento.Dequeue();
                var evtIniciar = this.motor.criarEvento<IniciarServico>();
                evtIniciar.setParams(idCarroFila);
                this.motor.Agendador.AgendarAgora(evtIniciar);
            }
        }
    }
}
