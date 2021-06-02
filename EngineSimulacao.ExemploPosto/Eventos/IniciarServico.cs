using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : Evento<MemoriaPostoGasolina>
    {
        int idCarro;
        public IniciarServico(MotorExecucao<MemoriaPostoGasolina> motor): base(motor){ }
        public void setParams(int idCarro){ this.idCarro = idCarro; }

        public override void Executar() {
            var recurso = this.motor.Agendador.ObterRecurso("funcionarios");
            recurso.TentarAlocar(1);

            var evtFinalizar = this.motor.criarEvento<FinalizarServico>();
            evtFinalizar.setParams(this.idCarro);
            this.motor.Agendador.AgendarEm(evtFinalizar, 12);
        }
    }
}
