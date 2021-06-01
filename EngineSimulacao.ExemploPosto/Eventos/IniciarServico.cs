using System.Collections.Generic;
using EngineSimulacao.Api;

namespace EngineSimulacao.ExemploPosto.Eventos
{
    public sealed class ParametrosIniciarServico : ParametrosEvento {
        public int idCarro { get; set; }

        public ParametrosIniciarServico(int idCarro){
            this.idCarro = idCarro;
        }
    }
    public sealed class IniciarServico : Evento<MemoriaPostoGasolina>
    {
        public IniciarServico(MotorPostoGasolina Motor, ParametrosIniciarServico parametros) : base(Motor, parametros){}

        public override void Executar() {
            var recurso = this.motor.Agendador.ObterRecurso("funcionarios");
            recurso.TentarAlocar(1);

            ParametrosIniciarServico p = (ParametrosIniciarServico) this.Parametros;
            var evt = this.motor.CriarEvento<FinalizarServico>(new ParametrosFinalizarServico(p.idCarro));
            this.motor.Agendador.AgendarEm(evt, 12);
            
        }
    }
}
