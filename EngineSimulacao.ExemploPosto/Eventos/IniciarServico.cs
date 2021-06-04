using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : Evento<ConjuntosPosto>
    {
        public IniciarServico(MotorExecucao<ConjuntosPosto> motor): base(motor){ }

        public override void Executar() {
            var filaAtendimento = this.motor.PegarConjunto(ConjuntosPosto.filaAtendimento);
            var funcionarios = this.motor.Agendador.ObterRecurso("funcionarios");
            
            if(false == funcionarios.VerificarDisponibilidade(CONFIG.FUNCIONARIOS_NECESSARIOS))
                return;

            var carro = filaAtendimento.Remover();
            
            funcionarios.TentarAlocar(CONFIG.FUNCIONARIOS_NECESSARIOS);

            var evtFinalizar = this.motor.criarEvento<FinalizarServico>();
            evtFinalizar.setParams(carro);
            this.motor.Agendador.AgendarEm(evtFinalizar, CONFIG.TEMPO_PARA_FINALIZAR);
        }
    }
}
