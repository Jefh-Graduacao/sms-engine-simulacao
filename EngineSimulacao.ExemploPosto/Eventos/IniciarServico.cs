using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class IniciarServico : IEvento
    {
        private MotorPostoGasolina motorPostoGasolina;
        int idCarro;
        public IniciarServico(MotorPostoGasolina Motor, int idCarro){
            this.motorPostoGasolina = Motor;
            this.idCarro = idCarro;
        }

        public void Executar() {
            var recurso = this.motorPostoGasolina.Agendador.ObterRecurso("funcionarios");
            recurso.TentarAlocar(1);

            var evt = new FinalizarServico(this.motorPostoGasolina, this.idCarro);
            this.motorPostoGasolina.Agendador.AgendarEm(evt, 12);
        }
    }
}
