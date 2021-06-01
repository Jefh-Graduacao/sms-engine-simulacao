using System.Collections.Generic;

namespace EngineSimulacao.Api
{
    public abstract class ParametrosEvento {}
    public abstract class Evento<Memoria> where Memoria:new()
    {
        public int Id { get; set; }
        public int Tempo { get; set; }
        protected MotorExecucao<Memoria> motor { get; set; }
        protected ParametrosEvento Parametros;

        protected Evento(MotorExecucao<Memoria> motor) {
            this.motor = motor;
        }

        protected Evento(MotorExecucao<Memoria> motor, ParametrosEvento parametros)
        {
            this.motor = motor;
            this.Parametros = parametros;
        }

        public abstract void Executar();
    }
}