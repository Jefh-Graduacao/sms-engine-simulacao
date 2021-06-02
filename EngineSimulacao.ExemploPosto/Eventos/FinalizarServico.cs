using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class FinalizarServico : IEvento
    {
        private MotorPostoGasolina motorPostoGasolina;
        private int idCarro;
        public FinalizarServico(MotorPostoGasolina Motor, int idCarro){
            this.motorPostoGasolina = Motor;
            this.idCarro = idCarro;
        }

        public void Executar() {
            this.motorPostoGasolina.Agendador.DestruirEntidade(this.idCarro);

            var recurso = this.motorPostoGasolina.Agendador.ObterRecurso("funcionarios");
            recurso.Liberar(1);

            if (this.motorPostoGasolina.memoria.filaAtendimento.Count > 0)
            {
                int idCarroFila = this.motorPostoGasolina.memoria.filaAtendimento.Dequeue();
                var evt = new IniciarServico(this.motorPostoGasolina, idCarroFila);
                this.motorPostoGasolina.Agendador.AgendarAgora(evt);
            }
        }
    }
}
