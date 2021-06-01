using System.Collections.Generic;
using EngineSimulacao.Api;
using System;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ParametrosFinalizarServico : ParametrosEvento { 
        public int idCarro { get; set; }

        public ParametrosFinalizarServico(int idCarro){
            this.idCarro = idCarro;
        }
    }
    public sealed class FinalizarServico : Evento<MemoriaPostoGasolina>
    {
        public FinalizarServico(MotorPostoGasolina Motor, ParametrosFinalizarServico parametros) : base(Motor, parametros){ }

        public override void Executar() {
            ParametrosFinalizarServico p = (ParametrosFinalizarServico) this.Parametros;
            this.motor.Agendador.DestruirEntidade(p.idCarro);

            var recurso = this.motor.Agendador.ObterRecurso("funcionarios");
            recurso.Liberar(1);

            if (this.motor.memoria.filaAtendimento.Count > 0)
            {
                int idCarroFila = this.motor.memoria.filaAtendimento.Dequeue();
                var evt = this.motor.CriarEvento<IniciarServico>(new ParametrosIniciarServico(idCarroFila));
                this.motor.Agendador.AgendarAgora(evt);
            }
        }
    }
}
