using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : Evento
    {
        public IniciarServico(){ 
            Gerenciador<IniciarServico>.nascimento(this);
        }

        protected override void Estrategia() {            
            if(false == MotorPosto.funcionarios.VerificarDisponibilidade(MotorPosto.FUNCIONARIOS_NECESSARIOS))
                return;

            var carro = MotorPosto.filaAtendimento.Remover();
            
            MotorPosto.funcionarios.TentarAlocar(MotorPosto.FUNCIONARIOS_NECESSARIOS);

            var evtFinalizar = new FinalizarServico(carro);
            Agendador.AgendarEm(evtFinalizar, MotorPosto.TEMPO_PARA_FINALIZAR);
        }
        protected override void Destruir()
        {
            Gerenciador<IniciarServico>.morte(this);
        }
    }
}
